using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Naturlaboratoriet.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryDescription { get; set; }
        public ICollection <Product> Products { get; set; }
    }
}
