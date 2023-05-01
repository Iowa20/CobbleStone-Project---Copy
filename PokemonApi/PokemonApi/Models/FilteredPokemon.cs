using CsvHelper.Configuration.Attributes;

namespace PokemonApi.Models
{
    public class FilteredPokemon
    {
        //[Name("#")]
        public int Id { get; set; }
        public string Name { get; set; }

        //[Name("Type 1")]
        public string Type1 { get; set; }

        //[Name("Type 2")]
        public string Type2 { get; set; }

        public int Total { get; set; }

        public int SpAtk { get; set; }

        public int SpDef { get; set; }
        
        //[Name("Speed")]
        public int Speed { get; set; }
        
        public int Generation { get; set; }

        public bool Legendary { get; set; }
    }
}
