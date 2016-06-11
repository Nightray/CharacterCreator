using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator
{
    public class Global
    {
        public ObservableCollection<Character> ListOfCharacters { get; set; }

        public List<string> ItemTypes = new List<string> { "Items", "Weapons", "Armor" };
        public List<string> Items = new List<string> { "Arrow", "Bolt", "Potion",
            "Book", "Antidote", "Meat", "Water", "Bag" };
        public Dictionary<string, int> Weapons = new Dictionary<string, int> {
            { "Sword", 120 },
            { "Longsword", 145 },
            { "Greatsword", 160 },
            { "Bow", 130 },
            { "Long Bow", 140 },
            { "Crossbow", 140 },
            { "Dagger", 100 },
            { "Wand", 140 },
            { "Staff", 160 },
        }; 
        public Dictionary<string, int> Armor = new Dictionary<string, int> {
            { "Chainmail", 100 },
            { "Plate Armor", 150 },
            { "Robe", 50 },
        };

        
    }
}