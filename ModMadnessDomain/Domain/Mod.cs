using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModMadnessDomain.Domain
{
    public class Mod : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsEnabled { get; set; }

        public Guid GameId { get; set; }
        public Game? Game { get; set; } = null!;
    }
}
