using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Model
{
    public class Item
    {
        public Item()
        {

        }
        public Item(int id, string name, string quality, double quantity)
        {
            Id = id;
            Name = name;
            Quality = quality;
            Quantity = quantity;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quality { get; set; }
        public double Quantity { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} - Name: {Name} - Quality: {Quality} - Quantity: {Quantity}";
        }
    }
}
