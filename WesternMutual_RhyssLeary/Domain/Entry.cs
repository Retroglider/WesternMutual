using System.ComponentModel.DataAnnotations;

namespace WesternMutual_RhyssLeary.Domain;

public class Entry
{
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Guid Guid { get; set; } = System.Guid.NewGuid(); // Business Rule: Default to new GUID.
    /// <summary>
    /// 
    /// </summary>

    public DateTime Expire { get; set; } = DateTime.Now.AddDays(AppConfig.Defaults.ExpirationDays);
    public string User { get; set; } = default!;
}
