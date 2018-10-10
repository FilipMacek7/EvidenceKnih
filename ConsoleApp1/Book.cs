using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Book
    {
        public string Name { get; set; }
        public long Isbn = new Random().Next(1000000, 9999999);
        List<Author> AuthorList = new List<Author>();
    }
}