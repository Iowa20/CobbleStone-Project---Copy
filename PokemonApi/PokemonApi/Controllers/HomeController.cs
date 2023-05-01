using System.IO;
using System.Collections.Generic;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using PokemonApi.Models;
using System.Formats.Asn1;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Hosting.Server;

namespace PokemonApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HomeController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ParseCSV()
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "Data/pokemon.csv"); // Added a folder undedr wwroot
            List<Pokemon> pokemons = new List<Pokemon>();

            using (StreamReader streamReader = new StreamReader(path))
            using (CsvReader csvReader = new CsvReader(streamReader, System.Globalization.CultureInfo.InvariantCulture))
            {
                csvReader.Context.RegisterClassMap<PokemonMap>(); // Register the custom class map from PokemonMap.cs

                while (csvReader.Read())
                {
                    //Conditions for the parsed fitler from the .CSV file
                    
                    Pokemon pokemon = csvReader.GetRecord<Pokemon>();

                    if (pokemon.Legendary) continue;
                    if (pokemon.Type1 == "Ghost" || pokemon.Type2 == "Ghost") continue;

                    if (pokemon.Type1 == "Steel" || pokemon.Type2 == "Steel") pokemon.HP *= 2;
                    if (pokemon.Type1 == "Fire" || pokemon.Type2 == "Fire") pokemon.Attack = (int)(pokemon.Attack * 0.9);
                    if ((pokemon.Type1 == "Bug" && pokemon.Type2 == "Flying") || (pokemon.Type1 == "Flying" && pokemon.Type2 == "Bug")) pokemon.Speed = (int)(pokemon.Speed * 1.1);
                    if (pokemon.Name.StartsWith("G")) pokemon.Defense += (pokemon.Name.Length - 1) * 5;

                    pokemons.Add(pokemon);


                }

                return View("Index", pokemons); // Pass the modified list of pokemons to the Index view
            }


            
           


        }


        // Filtering Function using CSV Helper
       
        public IActionResult FilteredSearch(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return RedirectToAction("Index");
            }

            string csvFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Data/pokemon.csv");
            FilteredPokemon filteredPokemon;

            using (var reader = new StreamReader(csvFilePath))
            {
                // Set the CsvConfiguration
                
                var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    PrepareHeaderForMatch = args =>
                    {
                        //Console.WriteLine($"Header: {args.Header}"); // Print the header name to the console
                        return args.Header.ToLower();
                    }
                };

                using (var csvReader = new CsvReader(reader, config))
                {
                    csvReader.Context.RegisterClassMap<PokemonMap>(); // used mapping to properly address the headers matching

                    var pokemons = csvReader.GetRecords<Pokemon>().ToList();
                    var targetPokemon = pokemons.FirstOrDefault(p => p.Name.Equals(search, StringComparison.OrdinalIgnoreCase));

                    if (targetPokemon == null)
                    {
                        return RedirectToAction("Index");
                    }

                    filteredPokemon = new FilteredPokemon
                    {
                        Id = targetPokemon.Id,
                        Name = targetPokemon.Name,
                        Type1 = targetPokemon.Type1,
                        Type2 = targetPokemon.Type2,
                        Total = targetPokemon.Total,
                        SpAtk = targetPokemon.SpAtk,
                        SpDef = targetPokemon.SpDef,
                        Speed = targetPokemon.Speed,
                        Generation = targetPokemon.Generation,
                        Legendary = targetPokemon.Legendary,
                    };
                }
            }

            return View("FilteredSearchResult", filteredPokemon);
        }

        // About page view

        public IActionResult About()
        {
            return View();
        }






        // A code snippet to check the .CSV file matches the models


        //public IActionResult InspectCsv()
        //{
        //    var csvFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Data", "pokemon.csv");

        //    using (var reader = new StreamReader(csvFilePath))
        //    using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        //    {
        //        var records = csvReader.GetRecords<dynamic>().ToList();

        //        foreach (var record in records)
        //        {
        //            Console.WriteLine(record); 
        //        }
        //    }

        //    return View();
        //}



    }
}