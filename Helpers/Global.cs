using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CharacterCreator
{
    public class Global
    {
        public ObservableCollection<Character> ListOfCharacters { get; set; }

        #region All the stuff used by Invenotry.cs such as: Item Types, Items, Weapons, and Armor
        public List<string> ItemTypes = new List<string> { "Items", "Weapons", "Armor" };
        public List<string> Items = new List<string> { "Health Potion", "Mana Potion",
            "Spell Book", "Antidote" };
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
        #endregion
    }
}