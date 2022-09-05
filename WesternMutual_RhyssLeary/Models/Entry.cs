namespace WesternMutual_RhyssLeary.Models;

public class Entry
{
    public class CreateModel
    {
        //public Guid? Guid { get; set; } = System.Guid.NewGuid(); // Business Rule: Default to new GUID.
        public long? Expire { get; set; }
            //= 
            //DateTime.Now
            //    .AddDays(AppConfig.Defaults.ExpirationDays)
            //        .ToEpochDateTime(); // Business Rule: Default to 30 days from today.
        public string User { get; set; } = default!;

    }

    public class UpdateModel
    {
        public string? Expire { get; set; }
        public string? User { get; set; }
    }

    public class ViewModel
    {
        public string Guid { get; set; } = default!;

        public long Expire { get; set; } = default!;
        public string User { get; set; } = default!;
    }


}
