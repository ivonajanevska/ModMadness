using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModMadnessDomain.DTOs
{
    public class RawgGamesResponse
    {
        public List<RawgGameDto> Results { get; set; } = new();
    }
}
