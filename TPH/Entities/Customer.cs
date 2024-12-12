namespace TPH.Entities;

public sealed class Customer : User
{
    public string? Address { get; set; }
    public string? Preferences { get; set; }
}
