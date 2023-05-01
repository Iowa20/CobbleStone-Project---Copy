using CsvHelper.Configuration;

namespace PokemonApi.Models
{ 
public class FilteredPokemonMap : ClassMap<FilteredPokemon>
{
    public FilteredPokemonMap()
    {
        Map(m => m.Id).Name("#");
        Map(m => m.Name).Name("Name");
        Map(m => m.Type1).Name("Type 1");
        Map(m => m.Type2).Name("Type 2");
        Map(m => m.Total).Name("Total");
        Map(m => m.SpAtk).Name("Sp. Atk");
        Map(m => m.SpDef).Name("Sp. Def");
        Map(m => m.Speed).Name("Speed");
        Map(m => m.Generation).Name("Generation");
        Map(m => m.Legendary).Name("Legendary");
    }
}
}