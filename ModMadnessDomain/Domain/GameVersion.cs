using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModMadnessDomain.Domain
{
    public class GameVersion : BaseEntity
    {
        public string VersionNumber { get; set; } = string.Empty;

        public Guid GameId { get; set; }
        public Game Game { get; set; } = null!;
    }
}
