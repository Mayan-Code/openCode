using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseCreateEntity
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<Product> Products { get; set; }
        public List<Plate> Plates { get; set; }
        public List<Meal> Meals { get; set; }
        public List<TemplateMeal> TemplateMeals { get; set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
