using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModMadnessDomain.Domain
{
    public class DLC : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public bool IsInstalled { get; set; }

        public Guid GameId { get; set; }
        public Game Game { get; set; } = null!;
    }
}
