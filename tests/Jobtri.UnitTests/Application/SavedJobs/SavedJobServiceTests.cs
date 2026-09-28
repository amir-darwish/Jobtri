using Jobtri.Application.Abstractions.Persistence;
using Jobtri.Application.SavedJobs;
using Jobtri.Domain.Entities;

namespace Jobtri.UnitTests.Application.SavedJobs;

public sealed class SavedJobServiceTests
{
    private sealed class FakeSavedJobRepository : ISavedJobRepository
    {
        public List<SavedJob> Items { get; } = new();
        public bool SaveChangesCalled { get; private set; }

        public Task AddAsync(SavedJob savedJob, CancellationToken cancellationToken = default)
        {
            // Simulate EF Core identity generation if Id is 0
            if (savedJob.Id == 0)
            {
                var idProp = typeof(SavedJob).GetProperty(nameof(SavedJob.Id));
                idProp?.SetValue(savedJob, Items.Count + 1);
            }
            Items.Add(savedJob);
            return Task.CompletedTask;
        }

        public Task<SavedJob?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var item = Items.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(item);
        }

        public Task<List<SavedJob>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Items.ToList());
        }

        public Task<List<SavedJob>> GetByJobIdAsync(int jobId, CancellationToken cancellationToken = default)
        {
            var matches = Items.Where(x => x.JobId == jobId).ToList();
            return Task.FromResult(matches);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SaveChangesCalled = true;
            return Task.CompletedTask;
        }
    }

    [Fact]
    public async Task CreateAsync_ShouldAddAndSave()
    {
        // Arrange
        var fakeRepo = new FakeSavedJobRepository();
        var service = new SavedJobService(fakeRepo);

        // Act
        var id = await service.CreateAsync(42, "Follow up next week");

        // Assert
        Assert.True(id > 0);
        Assert.True(fakeRepo.SaveChangesCalled);
        Assert.Single(fakeRepo.Items);
        Assert.Equal(42, fakeRepo.Items[0].JobId);
        Assert.Equal("Follow up next week", fakeRepo.Items[0].Notes);
    }

    [Fact]
    public async Task GetByIdAsync_WhenExists_ShouldReturnSavedJob()
    {
        // Arrange
        var fakeRepo = new FakeSavedJobRepository();
        var service = new SavedJobService(fakeRepo);
        var id = await service.CreateAsync(10);

        // Act
        var result = await service.GetByIdAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.JobId);
    }

    [Fact]
    public async Task GetByJobIdAsync_ShouldReturnMatchingJobs()
    {
        // Arrange
        var fakeRepo = new FakeSavedJobRepository();
        var service = new SavedJobService(fakeRepo);
        await service.CreateAsync(100, "Note 1");
        await service.CreateAsync(100, "Note 2");
        await service.CreateAsync(200, "Other job");

        // Act
        var results = await service.GetByJobIdAsync(100);

        // Assert
        Assert.Equal(2, results.Count);
        Assert.All(results, item => Assert.Equal(100, item.JobId));
    }

    [Fact]
    public async Task UpdateNotesAsync_WhenExists_ShouldUpdateAndReturnTrue()
    {
        // Arrange
        var fakeRepo = new FakeSavedJobRepository();
        var service = new SavedJobService(fakeRepo);
        var id = await service.CreateAsync(5, "Old note");

        // Act
        var updated = await service.UpdateNotesAsync(id, "New note");
        var item = await service.GetByIdAsync(id);

        // Assert
        Assert.True(updated);
        Assert.NotNull(item);
        Assert.Equal("New note", item.Notes);
    }

    [Fact]
    public async Task UpdateNotesAsync_WhenNotFound_ShouldReturnFalse()
    {
        // Arrange
        var fakeRepo = new FakeSavedJobRepository();
        var service = new SavedJobService(fakeRepo);

        // Act
        var updated = await service.UpdateNotesAsync(999, "New note");

        // Assert
        Assert.False(updated);
    }
}
