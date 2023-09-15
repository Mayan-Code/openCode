using Domain.Common;
using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseCreateEntity
    {
        public long Id { get; set; }
        public bool IsFavorite { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Barcode { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbo { get; set; }
        public decimal Fat { get; set; }
        public ProductCategoryType FoodCategory { get; set; }
        public ProductUnitType FoodUnit{ get; set; }
        
        public User Creator { get; set; }
        public long? CreatorId { get; set; }

        public List<ProductMeal> Meals { get; set; }
        public List<TemplateMealProduct> TemplateMeals { get; set; }

        public Product()
        {
            
        }
        public Product(string name, decimal protein, decimal carbo, decimal fat, ProductCategoryType foodCategory, ProductUnitType foodUnit, string manufacturer, string barcode)
        {
            (Name, Protein, Carbo, Fat, FoodCategory, FoodUnit, Manufacturer, Barcode) = (name, protein, carbo, fat, foodCategory, foodUnit, manufacturer, barcode);
        }
    }
}
