using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CharacterCreator.Classes
{
    class Armor: Equipment
    {
        App app = Application.Current as App;

        public string Name { get; set; }
        public int HealthPoints { get; set; }

        public Armor() { }

        public Armor(string name, string type, double quanity)
        {
            this.Name = name;
            this.Type = type;
            this.Quanity = quanity;
            this.HealthPoints = app.Global.Armor[name];
        }

        protected override void WhoAmI()
        {
            MessageBox.Show(("Name: " + this.Name + "Type: " + this.Type + "Quanity: " + this.Quanity + "Health Points: " + this.HealthPoints));
            base.WhoAmI();
        }
    }
}
