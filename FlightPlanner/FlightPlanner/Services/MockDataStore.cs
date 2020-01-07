using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanner.Models;

namespace FlightPlanner.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = "First Flight",
                    Description = "Spin - go down - go up - spin",
                    FlightCommands = new List<FlightCommands>
                    {
                        new FlightCommands
                        {
                            Action = InFlightCommandEnum.cw,
                            Value = 180
                        },
                        new FlightCommands
                        {
                            Action = InFlightCommandEnum.down,
                            Value = 50
                        },
                        new FlightCommands
                        {
                            Action = InFlightCommandEnum.up,
                            Value = 50
                        },
                        new FlightCommands
                        {
                            Action = InFlightCommandEnum.ccw,
                            Value = 180
                        }
                    }
                },
                new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = "Second flight",
                    Description = "Up 60 - forward 100 - back 100",
                    FlightCommands = new List<FlightCommands>
                    {
                        new FlightCommands
                        {
                            Action = InFlightCommandEnum.up,
                            Value = 60
                        },
                        new FlightCommands
                        {
                            Action = InFlightCommandEnum.forward,
                            Value = 100
                        },
                        new FlightCommands
                        {
                            Action = InFlightCommandEnum.back,
                            Value = 100
                        },
                    }
                }
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}