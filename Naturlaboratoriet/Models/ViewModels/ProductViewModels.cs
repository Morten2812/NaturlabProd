using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Naturlaboratoriet.Models.ViewModels
{
    public class ProductViewModels
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public int CategoryID { get; set; }

        public Category Category { get; set; }

        public IFormFile PrimaryImage { get; set; }
        public IFormFile SecondaryImage1 { get; set; }
        public IFormFile SecondaryImage2 { get; set; }
        public IFormFile SecondaryImage3 { get; set; }

        public bool Instock { get; set; }


    }
}
