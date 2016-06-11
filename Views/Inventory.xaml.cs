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

        ObservableCollection<Classes.Items> ListOfItems;
        public ObservableCollection<Classes.Items> TempListOfItems { get; set; }

        private Character CurrentCharacter { get; set; }

        public Inventory(int indexNumber)
        {
            ListOfCharacters = app.Global.ListOfCharacters;
            InitializeComponent();

            ViewableCharacter(indexNumber);

            this.cmbItemType.ItemsSource = app.Global.ItemTypes;
            this.cmbItemType.SelectedIndex = 0;

            this.ListOfItems = CurrentCharacter.ListOfItems;
            this.TempListOfItems = ListOfItems;
            this.lvItems.ItemsSource = TempListOfItems;

            DisplayGreeting();
            ClearForm();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new CharacterList());
        }

        private void DisplayGreeting()
        {
            if (!ListOfCharacters.Contains(CurrentCharacter))
            {
                lblGreeting.Content = "Error!";
            }
            else {
                string Name = CurrentCharacter.Name;
                Name = Name.Substring(Name.Length - 1);

                string sGenetive = (Name == "s") ? "' " : "'s ";
                lblGreeting.Content = CurrentCharacter.Name + sGenetive + "Inventory";
            }
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
        
        /// <summary>
        /// Clears the form and brings back the default values.
        /// </summary>
        private void ClearForm()
        {
            this.cmbItemName.ItemsSource = app.Global.Items;

            this.cmbItemType.SelectedIndex = 0;
            this.cmbItemName.SelectedIndex = 0;
            this.slItemQuanity.Value = 1;
        }

        private void cmbItemType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (this.cmbItemType.Text)
            {
                case "Items":
                    this.cmbItemName.ItemsSource = app.Global.Items;
                    cmbItemName.SelectedIndex = 0;
                    break;
                case "Weapons":
                    this.cmbItemName.ItemsSource = app.Global.Weapons.Keys;
                    cmbItemName.SelectedIndex = 0;
                    break;
                case "Armor":
                    this.cmbItemName.ItemsSource = app.Global.Armor.Keys;
                    cmbItemName.SelectedIndex = 0;
                    break;
                default:
                    break;
            }
        }
    }
}
