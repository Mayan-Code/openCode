using Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;

namespace Application
{
    public static class StartData
    {
        public static List<Product> Products = new List<Product>
        {
            new Product
            {
                Name = "Serek Wiejski - Wysokobiałkowy",
                Manufacturer = "Mlekovita, Lidl",
                Barcode = "5900512988801",
                FoodCategory = Domain.Entities.Enums.ProductCategoryType.DairyAndEggs,
                FoodUnit = Domain.Entities.Enums.ProductUnitType.Per100,
                Carbo = 2.3m,
                Fat = 3.0m,
                Protein = 15.0m
            },
            new Product
            {
                Name = "Ryż basmati",
                Manufacturer = "Kuchnia Lidla",
                Barcode = "20983758",
                FoodCategory = Domain.Entities.Enums.ProductCategoryType.Grain,
                FoodUnit = Domain.Entities.Enums.ProductUnitType.Per100,
                Carbo = 75.0m,
                Fat = 1.1m,
                Protein = 8.9m
            },
            new Product
            {
                Name = "Płatki owsiane górskie",
                Manufacturer = "",
                Barcode = "",
                FoodCategory = Domain.Entities.Enums.ProductCategoryType.Grain,
                FoodUnit = Domain.Entities.Enums.ProductUnitType.Per100,
                Carbo = 62.4m,
                Fat = 7.2m,
                Protein = 12.8m
            },
            new Product
            {
                Name = "Veggie balls",
                Manufacturer = "Garden Gourmet, Lidl",
                Barcode = "8445290493125",
                FoodCategory = Domain.Entities.Enums.ProductCategoryType.ReadyMeals,
                FoodUnit = Domain.Entities.Enums.ProductUnitType.Per100,
                Carbo = 5.20m,
                Fat = 14.4m,
                Protein = 16.2m
            },
            new Product
            {
                Name = "SBA INSTANT - WPC 80 (Wanilia)",
                Manufacturer = "Mlekovita, Lidl",
                Barcode = "5900512984797",
                FoodCategory = Domain.Entities.Enums.ProductCategoryType.Conditioners,
                FoodUnit = Domain.Entities.Enums.ProductUnitType.Per100,
                Carbo = 9.0m,
                Fat = 6.0m,
                Protein = 77.0m
            },
            new Product
            {
                Name = "Sajgonki z warzywami (bez sosu)",
                Manufacturer = "Asian Style, Lidl",
                Barcode = "4056489446736",
                FoodCategory = Domain.Entities.Enums.ProductCategoryType.ReadyMeals,
                FoodUnit = Domain.Entities.Enums.ProductUnitType.Per100,
                Carbo = 22.7m,
                Fat = 10.9m,
                Protein = 4.3m
            },
            new Product
            {
                Name = "Sos do sajgonek słodki",
                Manufacturer = "Asian Style, Lidl",
                Barcode = "4056489446736",
                FoodCategory = Domain.Entities.Enums.ProductCategoryType.ReadyMeals,
                FoodUnit = Domain.Entities.Enums.ProductUnitType.Per100,
                Carbo = 48.7m,
                Fat = 0.2m,
                Protein = 0.4m
            },
            new Product
            {
                Name = "Sajgonki z wieprzowiną i warzywami (bez sosu)",
                Manufacturer = "Asian Style, Lidl",
                Barcode = "4056489446743",
                FoodCategory = Domain.Entities.Enums.ProductCategoryType.ReadyMeals,
                FoodUnit = Domain.Entities.Enums.ProductUnitType.Per100,
                Carbo = 20.9m,
                Fat = 14.8m,
                Protein = 5.7m
            },
            new Product
            {
                Name = "Sos do sajgonek słodki z chilli",
                Manufacturer = "Asian Style, Lidl",
                Barcode = "4056489446743",
                FoodCategory = Domain.Entities.Enums.ProductCategoryType.ReadyMeals,
                FoodUnit = Domain.Entities.Enums.ProductUnitType.Per100,
                Carbo = 48.7m,
                Fat = 0.2m,
                Protein = 0.4m
            },
            new Product
            {
                Name = "Jajo kurze L",
                FoodCategory = Domain.Entities.Enums.ProductCategoryType.DairyAndEggs,
                FoodUnit = Domain.Entities.Enums.ProductUnitType.Quantity,
                Carbo = 0.6m,
                Fat = 5.4m,
                Protein = 7.0m
            },
            new Product
            {
                Name = "Pomidor",
                FoodCategory = Domain.Entities.Enums.ProductCategoryType.VegetableAndFruit,
                FoodUnit = Domain.Entities.Enums.ProductUnitType.Per100,
                Carbo = 3.0m,
                Fat = 0.2m,
                Protein = 1.0m
            },
            new Product
            {
                Name = "Ogórek",
                FoodCategory = Domain.Entities.Enums.ProductCategoryType.VegetableAndFruit,
                FoodUnit = Domain.Entities.Enums.ProductUnitType.Per100,
                Carbo = 3.13m,
                Fat = 0.11m,
                Protein = 0.65m
            },
            new Product
            {
                Name = "Sałata",
                FoodCategory = Domain.Entities.Enums.ProductCategoryType.VegetableAndFruit,
                FoodUnit = Domain.Entities.Enums.ProductUnitType.Per100,
                Carbo = 1.10m,
                Fat = 0.2m,
                Protein = 1.40m
            },
            new Product
            {
                Name = "Masło klarowane",
                FoodCategory = Domain.Entities.Enums.ProductCategoryType.Fat,
                FoodUnit = Domain.Entities.Enums.ProductUnitType.Per100,
                Carbo = 0.10m,
                Fat = 99.8m,
                Protein = 0.10m
            },
            new Product
            {
                Name = "Chleb żytni razowy",
                FoodCategory = Domain.Entities.Enums.ProductCategoryType.Grain,
                FoodUnit = Domain.Entities.Enums.ProductUnitType.Per100,
                Carbo = 42.90m,
                Fat = 1.7m,
                Protein = 6.3m
            }
        };


        public static List<ProductDto> GetFromFile()
        {
            try
            {
                var localPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                var csvFilePath = Path.Combine(localPath, "foods.tsv");
                if (File.Exists(csvFilePath))
                {
                    var d = File.ReadAllText(csvFilePath);

                    CsvParserOptions csvParserOptions = new CsvParserOptions(true, '\t');
                    CsvProductDtoMapping csvMapper = new CsvProductDtoMapping();
                    CsvParser<ProductDto> csvParser = new CsvParser<ProductDto>(csvParserOptions, csvMapper);

                    var result = csvParser
                       .ReadFromFile(csvFilePath, Encoding.UTF8)
                       .ToList();


                    return result.Select(q => q.Result).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
    }
}
