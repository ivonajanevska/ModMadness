using ModMadnessDomain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModMadnessService.Interface
{
    public interface IRawgApiService
    {
        Task<List<RawgGameDto>> SearchGamesAsync(string title);
    }
}
