using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Interfaces
{
    interface IViewableCharacter
    {
        /// <summary>
        /// Takes CharacterIndex and checks if the character exists in the global list.
        /// </summary>
        /// <param name="CharacterIndex"></param>
        /// <returns></returns>
        void ViewableCharacter(int characterIndex);
    }
}
