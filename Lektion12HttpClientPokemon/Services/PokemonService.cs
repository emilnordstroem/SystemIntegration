using Lektion12HttpClientPokemon.Models;

namespace Lektion12HttpClientPokemon.Services
{
	public class PokemonService
	{
		private HttpClient _client;
		public PokemonService(HttpClient client)
		{
			_client = client;
		}
		public async Task<IEnumerable<SpeciesDTO>> GetPokemonSpecies()
		{
			var response = await _client.GetFromJsonAsync<PokemonSpecies>("https://pokeapi.co/api/v2/pokemon-species/");
			return response?.results;
		}
	}
}
