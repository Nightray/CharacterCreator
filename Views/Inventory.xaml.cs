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

        ObservableCollection<Classes.Equipment> ListOfItems;
        public ObservableCollection<Classes.Equipment> TempListOfItems { get; set; }

        private Character CurrentCharacter { get; set; }

        public Inventory(int indexNumber)
        {
            ListOfCharacters = app.Global.ListOfCharacters;
            InitializeComponent();
            DataContext = this;

            ViewableCharacter(indexNumber);

            this.cmbItemType.ItemsSource = app.Global.ItemTypes;
            this.cmbItemType.SelectedIndex = 0;

            this.TempListOfItems = new ObservableCollection<Classes.Equipment>();
            this.ListOfItems = CurrentCharacter.ListOfItems;
            this.TempListOfItems = ListOfItems;
            this.lvItems.ItemsSource = TempListOfItems;

            DisplayGreeting();
            PrepareForm();
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
        /// Prepares the form by setting up the default values.
        /// </summary>
        private void PrepareForm()
        {
            this.cmbItemName.ItemsSource = app.Global.Items;

            this.cmbItemType.SelectedIndex = 0;
            this.cmbItemName.SelectedIndex = 0;
            this.slItemQuanity.Value = 1;
        }

        private void cmbItemType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (e.AddedItems[0] as string)
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

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            string name = this.cmbItemName.Text;
            string type = this.cmbItemType.Text;
            int quanity = (int)this.slItemQuanity.Value;

            switch (type)
            {
                case "Items":
                    Classes.Item item = new Classes.Item(name, type, quanity);
                    this.TempListOfItems.Add(item);
                    break;
                case "Weapons":
                    Classes.Weapon weapon = new Classes.Weapon(name, type, quanity);
                    this.TempListOfItems.Add(weapon);
                    break;
                case "Armor":
                    Classes.Armor armor = new Classes.Armor(name, type, quanity);
                    this.TempListOfItems.Add(armor);
                    break;
                default:
                    break;
            }
            PrepareForm();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = lvItems.Items.IndexOf(item);

            TempListOfItems.RemoveAt(index);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.ListOfItems = TempListOfItems;
            TempListOfItems.Clear();
            Switcher.Switch(new CharacterList());
        }
    }
}
