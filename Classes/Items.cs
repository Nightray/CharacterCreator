using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Classes
{
    public class Items
    {
        public string Type { get; set; }
        public int Quanity { get; set; }

        protected virtual void WhoAmI()
        {
            //I am empty for a reason
        }
    }
}
