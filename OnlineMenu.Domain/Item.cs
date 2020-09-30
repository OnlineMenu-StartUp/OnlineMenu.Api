using System;

namespace OnlineMenu.Domain
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Desctiption { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
