using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.Model
{
    public class Category
    {
        public string Name { get; set; }

        public string ImagePath { get; set; }
        public Category(string name, string imagePath)
        {
            Name = name;
            ImagePath = imagePath;
        }

        public Category(string name)
        {
            Name = name;
            ImagePath = null;
        }
    }
}
