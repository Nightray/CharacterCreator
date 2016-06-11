using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CharacterCreator.Classes
{
    class Weapon: Equipment
    {
        App app = Application.Current as App;

        public string Name { get; set; }
        public int Damage { get; set; }

        public Weapon() { }

        public Weapon(string name, string type, int quanity)
        {
            this.Name = name;
            this.Type = type;
            this.Quanity = quanity;
            this.Damage = app.Global.Weapons[name];

        }

        protected override void WhoAmI()
        {
            MessageBox.Show(("Name: " + this.Name + "Type: " + this.Type + "Quanity: " + this.Quanity + "Damage: " + this.Damage));
            base.WhoAmI();
        }
    }
}
