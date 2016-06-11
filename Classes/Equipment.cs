using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Classes
{
    public class Equipment
    {
        public string Type { get; set; }
        public double Quanity { get; set; }

        public Equipment() { }
        public Equipment(string type) { this.Type = type; }
        protected virtual void WhoAmI()
        {
            //I am empty for a reason
        }
    }
}
