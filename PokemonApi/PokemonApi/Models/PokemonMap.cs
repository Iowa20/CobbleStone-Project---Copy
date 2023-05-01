using CsvHelper.Configuration;
using PokemonApi.Models;

public class PokemonMap : ClassMap<Pokemon>
{
    public PokemonMap()
    {
        Map(m => m.Id).Name("#");
        Map(m => m.Name).Name("Name");
        Map(m => m.Type1).Name("Type 1"); 
        Map(m => m.Type2).Name("Type 2"); 
        Map(m => m.Total).Name("Total");
        Map(m => m.HP).Name("HP");
        Map(m => m.Attack).Name("Attack");
        Map(m => m.Defense).Name("Defense");
        Map(m => m.SpAtk).Name("Sp. Atk");
        Map(m => m.SpDef).Name("Sp. Def");
        Map(m => m.Speed).Name("Speed");
        Map(m => m.Generation).Name("Generation");
        Map(m => m.Legendary).Name("Legendary");
    }
}

