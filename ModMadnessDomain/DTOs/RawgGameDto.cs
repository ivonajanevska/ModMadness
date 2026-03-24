using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModMadnessDomain.DTOs
{
    public class RawgGameDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Released { get; set; } = string.Empty;
        public string Background_Image { get; set; } = string.Empty;
    }
}
