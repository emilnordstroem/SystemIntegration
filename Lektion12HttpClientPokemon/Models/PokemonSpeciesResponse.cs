namespace Lektion12HttpClientPokemon.Models
{
	public record PokemonSpecies(int count, string next, string previous, List<SpeciesDTO> results);
}
