using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CharacterCreator.Classes
{
    class Item: Items
    {
        public string Name { get; set; }

        public Item() { }

        public Item(string name, string type, int quanity)
        {
            this.Name = name;
            this.Type = type;
            this.Quanity = quanity;
        }

        protected override void WhoAmI()
        {
            MessageBox.Show(("Name: " + this.Name + "Type: " + this.Type + "Quanity: " + this.Quanity));
            base.WhoAmI();
        }
    }
}
