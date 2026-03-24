using Microsoft.Extensions.Options;
using ModMadnessDomain.DTOs;
using ModMadnessService.API;
using ModMadnessService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModMadnessService.Implementation
{
    public class RawgApiService : IRawgApiService
    {
        private readonly HttpClient _httpClient;
        private readonly RawgSettings _settings;

        public RawgApiService(HttpClient httpClient, IOptions<RawgSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<List<RawgGameDto>> SearchGamesAsync(string title)
        {
            // Формираме URL со клучот и зборот за пребарување
            var url = $"{_settings.BaseUrl}games?key={_settings.ApiKey}&search={Uri.EscapeDataString(title)}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return new List<RawgGameDto>();
            }

            var json = await response.Content.ReadAsStringAsync();

            // Десеријализација на JSON одговорот во нашите DTO објекти
            var result = JsonSerializer.Deserialize<RawgGamesResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.Results ?? new List<RawgGameDto>();
        }

        public Task<List<RawgGameDto>> SearchGamesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
