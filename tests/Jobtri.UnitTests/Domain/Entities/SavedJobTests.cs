using Jobtri.Domain.Entities;

namespace Jobtri.UnitTests.Domain.Entities;

public sealed class SavedJobTests
{
    [Fact]
    public void Constructor_WithValidJobIdAndNotes_ShouldCreateSavedJob()
    {
        // Arrange
        int jobId = 10;
        string notes = "Interested in remote role";

        // Act
        var savedJob = new SavedJob(jobId, notes);

        // Assert
        Assert.Equal(jobId, savedJob.JobId);
        Assert.Equal(notes, savedJob.Notes);
        Assert.True(savedJob.SavedAt <= DateTimeOffset.UtcNow);
    }

    [Fact]
    public void Constructor_WithWhitespaceNotes_ShouldSetNotesToNull()
    {
        // Arrange
        int jobId = 5;

        // Act
        var savedJob = new SavedJob(jobId, "   ");

        // Assert
        Assert.Null(savedJob.Notes);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_WithInvalidJobId_ShouldThrowArgumentException(int jobId)
    {
        // Act
        Action act = () => new SavedJob(jobId);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void UpdateNotes_ShouldModifyNotes()
    {
        // Arrange
        var savedJob = new SavedJob(1, "Initial note");

        // Act
        savedJob.UpdateNotes("Updated note");

        // Assert
        Assert.Equal("Updated note", savedJob.Notes);
    }

    [Fact]
    public void UpdateNotes_WithWhitespace_ShouldSetNotesToNull()
    {
        // Arrange
        var savedJob = new SavedJob(1, "Initial note");

        // Act
        savedJob.UpdateNotes("   ");

        // Assert
        Assert.Null(savedJob.Notes);
    }
}
