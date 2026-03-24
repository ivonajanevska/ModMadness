namespace ModMadnessDomain.Domain
{
    public class Game : BaseEntity
    {

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string? Image { get; set; }
        public ICollection<DLC> DLCs { get; set; } = new List<DLC>();
        public ICollection<Mod> Mods { get; set; } = new List<Mod>();
        public ICollection<GameVersion> Versions { get; set; } = new List<GameVersion>();
    }
}
