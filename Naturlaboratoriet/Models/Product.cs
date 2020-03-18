using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Naturlaboratoriet.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       
        public string Price { get; set; }
        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        [Display(Name = "PrimImg")]
        public string  PrimaryImage { get; set; }
        [Display(Name="SecImg1")]
        public string SecondaryImage1 { get; set; }
        [Display(Name = "SecImg2")]
        public string SecondaryImage2 { get; set; }
        [Display(Name = "SecImg3")]
        public string SecondaryImage3 { get; set; }

        public bool Instock { get; set; }

        public Category Category { get; set; }


    }
}
