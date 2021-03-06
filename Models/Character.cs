﻿using CharacterCreator.Classes;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace CharacterCreator
{
    [XmlInclude(typeof(Equipment))]
    [XmlInclude(typeof(Armor))]
    [XmlInclude(typeof(Item))]
    [XmlInclude(typeof(Weapon))]
    public class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public Enums.CharacterSex Sex { get; set; }
        public char SexFirstLetter { get; set; }
        public Enums.Races Race { get; set; }
        public Enums.Professions Profession { get; set; }
        public ObservableCollection<Classes.Equipment> ListOfItems { get; set; }

        public Character() { }

        public Character(string name, int level, Enums.CharacterSex sex,
            Enums.Races race, Enums.Professions profession)
        {
            this.Name = name;
            this.Level = level;
            this.Sex = sex;
            this.SexFirstLetter = sex.ToString()[0];
            this.Race = race;
            this.Profession = profession;
            this.ListOfItems = new ObservableCollection<Classes.Equipment>();
        }
    }
}
