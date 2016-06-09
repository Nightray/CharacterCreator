using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CharacterCreator.Views;

namespace CharacterCreator.Views
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : UserControl, Interfaces.IViewableCharacter
    {
        // Access point to the ListOfCharacters object in the global scope.
        App app = Application.Current as App;
        ObservableCollection<Character> ListOfCharacters;

        private Character CurrentCharacter { get; set; }

        public Inventory(int indexNumber)
        {
            ListOfCharacters = app.Global.ListOfCharacters; // The list of characters exists in the global scope.
            InitializeComponent();

            ViewableCharacter(indexNumber);

            DisplayGreeting();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new CharacterList());
        }

        private void DisplayGreeting()
        {
            string Name = CurrentCharacter.Name;
            Name = Name.Substring(Name.Length - 1);

            string sGenetive = (Name == "s") ? "' " : "'s ";
            this.lblGreeting.Content = CurrentCharacter.Name + sGenetive + "Inventory";
        }

        public void ViewableCharacter(int characterIndex)
        {
            try
            {
                CurrentCharacter = ListOfCharacters[characterIndex];
            }
            catch (Exception)
            {

                if (!ListOfCharacters.Contains(CurrentCharacter))
                {
                    MessageBox.Show("The character you are trying to view doesn't exists anymore!", "Error!");
                }
            }
        }
    }
}
