namespace FIAPCloudGames.Promotions.Api.Features.Promotions.Models;

public class Promotion 
{
    private Promotion() { }

    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public string Name { get; private set; } = string.Empty;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public decimal DiscountPercentage { get; private set; }
    public string Description { get; private set; } = string.Empty; 
    public DateTime? UpdatedAt { get; private set; }

    public static Promotion Create(string name, DateTime startDate, DateTime endDate, decimal discountPercentage, string description)
    {
        return new Promotion
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Name = name,
            StartDate = startDate,
            EndDate = endDate,
            DiscountPercentage = discountPercentage,
            Description = description,
        };
    }

    public void Update(string name, DateTime startDate, DateTime endDate, decimal discountPercentage, string description)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        DiscountPercentage = discountPercentage;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }
}
