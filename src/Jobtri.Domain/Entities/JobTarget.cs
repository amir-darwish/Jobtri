namespace Jobtri.Domain.Entities;

public sealed class JobTarget
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Role { get; private set; }

    public string? Country { get; private set; }

    public string? Domain { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public bool IsEnabled { get; private set; }


    public JobTarget(
        string name,
        string role,
        string? country = null,
        string? domain = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Target name cannot be empty.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(role))
        {
            throw new ArgumentException("Role cannot be empty.", nameof(role));
        }

        Name = name.Trim();
        Role = role.Trim();

        Country = string.IsNullOrWhiteSpace(country)
            ? null
            : country.Trim();

        Domain = string.IsNullOrWhiteSpace(domain)
            ? null
            : domain.Trim();

        CreatedAt = DateTimeOffset.UtcNow;
        IsEnabled = true;
    }


    public void Enable()
    {
        IsEnabled = true;
    }


    public void Disable()
    {
        IsEnabled = false;
    }
}