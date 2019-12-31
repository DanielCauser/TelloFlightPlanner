using System;
using System.Collections.Generic;

namespace FlightPlanner.Models
{
    public class Item
    {
        public Item()
        {
            FlightCommands = new List<FlightCommands>(); 
        }
        
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public List<FlightCommands> FlightCommands { get; set; }
    }
}