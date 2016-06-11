using System.Windows;

namespace CharacterCreator.Classes
{
    class Weapon: Equipment
    {
        // An access point to Global.cs
        App app = Application.Current as App;

        public int Damage { get; set; }

        public Weapon() { }

        public Weapon(string name, string type, int quanity)
        {
            this.Name = name;
            this.Type = type;
            this.Quanity = quanity;
            this.Damage = app.Global.Weapons[name];
        }

        public override string ItemDetails()
        {
            return "Damage: " + this.Damage;
        }
    }
}
