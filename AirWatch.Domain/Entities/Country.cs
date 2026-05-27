using AirWatch.Domain.Common;

namespace AirWatch.Domain.Entities;

public class Country : BaseEntity
{
    public string Name { get; private set; }
    public string IsoCode { get; private set; }
    public string? Continent { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public ICollection<City> Cities { get; private set; } = new List<City>();

    public Country(string name, string isoCode, string? continent)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException("O nome do país é obrigatório.");
        if (string.IsNullOrWhiteSpace(isoCode) || isoCode.Length != 2)
            throw new InvalidOperationException("O código ISO deve ter exatamente 2 caracteres.");

        Name = name;
        IsoCode = isoCode.ToUpper();
        Continent = continent;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string isoCode, string? continent)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException("O nome do país é obrigatório.");
        if (string.IsNullOrWhiteSpace(isoCode) || isoCode.Length != 2)
            throw new InvalidOperationException("O código ISO deve ter exatamente 2 caracteres.");

        Name = name;
        IsoCode = isoCode.ToUpper();
        Continent = continent;
    }
}