using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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

        //ObservableCollection<Classes.Equipment> ListOfItems;
        private ObservableCollection<Classes.Equipment> ListOfItems { get; set; }
        private Character CurrentCharacter { get; set; }

        public Inventory(int indexNumber)
        {
            this.ListOfCharacters = app.Global.ListOfCharacters;
            InitializeComponent();
            DataContext = this;

            ViewableCharacter(indexNumber);

            this.ListOfItems = new ObservableCollection<Classes.Equipment>(CurrentCharacter.ListOfItems);
            this.lvItems.ItemsSource = ListOfItems;

            DisplayGreeting();
            PrepareForm();
        }

        #region Buttons
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            string name = this.cmbItemName.Text;
            string type = this.cmbItemType.Text;
            int quanity = (int)this.slItemQuanity.Value;

            switch (type)
            {
                case "Items":
                    Classes.Item item = new Classes.Item(name, type, quanity);
                    this.ListOfItems.Add(item);
                    break;
                case "Weapons":
                    Classes.Weapon weapon = new Classes.Weapon(name, type, quanity);
                    this.ListOfItems.Add(weapon);
                    break;
                case "Armor":
                    Classes.Armor armor = new Classes.Armor(name, type, quanity);
                    this.ListOfItems.Add(armor);
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

            this.ListOfItems.RemoveAt(index);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentCharacter.ListOfItems = this.ListOfItems;
            Switcher.Switch(new CharacterList());
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.ListOfItems.Clear();
            Switcher.Switch(new CharacterList());
        }

        private void btnItemDetails_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = lvItems.Items.IndexOf(item);

            MessageBox.Show(this.ListOfItems[index].ItemDetails(), ListOfItems[index].Name + "'s details");
        }
        #endregion

        private void cmbItemType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (e.AddedItems[0] as string)
            {
                case "Items":
                    this.cmbItemName.ItemsSource = app.Global.Items;
                    this.cmbItemName.SelectedIndex = 0;
                    break;
                case "Weapons":
                    this.cmbItemName.ItemsSource = app.Global.Weapons.Keys;
                    this.cmbItemName.SelectedIndex = 0;
                    break;
                case "Armor":
                    this.cmbItemName.ItemsSource = app.Global.Armor.Keys;
                    this.cmbItemName.SelectedIndex = 0;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Prepares the form by setting up the default values.
        /// </summary>
        private void PrepareForm()
        {
            this.cmbItemType.ItemsSource = app.Global.ItemTypes;
            this.cmbItemType.SelectedIndex = 0;

            this.cmbItemName.ItemsSource = app.Global.Items;
            this.cmbItemName.SelectedIndex = 0;

            this.slItemQuanity.Value = 1;
        }

        private void DisplayGreeting()
        {
            if (!ListOfCharacters.Contains(CurrentCharacter))
            {
                this.lblGreeting.Content = "Error!";
            }
            else {
                string Name = this.CurrentCharacter.Name;
                Name = Name.Substring(Name.Length - 1);

                string sGenetive = (Name == "s") ? "' " : "'s ";
                this.lblGreeting.Content = CurrentCharacter.Name + sGenetive + "Inventory";
            }
        }

        public void ViewableCharacter(int characterIndex)
        {
            try
            {
                this.CurrentCharacter = this.ListOfCharacters[characterIndex];
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
