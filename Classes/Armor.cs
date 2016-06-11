using System.Windows;

namespace CharacterCreator.Classes
{
    class Armor: Equipment
    {
        // An access point to Global.cs
        App app = Application.Current as App;

        public int HealthPoints { get; set; }

        public Armor() { }

        public Armor(string name, string type, int quanity)
        {
            this.Name = name;
            this.Type = type;
            this.Quanity = quanity;
            this.HealthPoints = app.Global.Armor[name];
        }

        public override string ItemDetails()
        {
            return "Health Points: " + this.HealthPoints;
        }
    }
}
