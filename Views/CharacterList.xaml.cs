using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CharacterCreator.Views;
using System.IO;
using System.Xml.Serialization;

namespace CharacterCreator
{
    /// <summary>
    /// Interaction logic for CharacterList.xaml
    /// </summary>
    public partial class CharacterList : UserControl, ISwitchable
    {
        // Access point to the ListOfCharacters in the global scope. 
        App app = Application.Current as App;
        ObservableCollection<Character> ListOfCharacters;

        public CharacterList()
        {
            this.ListOfCharacters = app.Global.ListOfCharacters;
            InitializeComponent();
            DataContext = this;

            // For whatever reason the xaml binding doesn't work
            // with global scope character list.
            this.lvCharacters.ItemsSource = ListOfCharacters;

            PrepareForm();
        }

        #region Buttons
        /// <summary>
        /// Takes values form the form, perfoms all necesary checks,
        /// if validation pases, adds the character to the list and clears the form 
        /// </summary>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = this.txtCharacterName.Text;
            int level = (int)this.slLevel.Value;
            Enums.CharacterSex sex = (Enums.CharacterSex)Enum.Parse(typeof(Enums.CharacterSex), this.cmbSex.Text);
            Enums.Races race = (Enums.Races)Enum.Parse(typeof(Enums.Races), this.cmbRaces.Text);
            Enums.Professions profession = (Enums.Professions)Enum.Parse(typeof(Enums.Professions), this.cmbProfessions.Text);

            if (String.IsNullOrEmpty(name))
                txtCharacterNameIsEmpty();
            else
            {
                Character character = new Character(name, level, sex, race, profession);
                this.ListOfCharacters.Add(character);
                PrepareForm();
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = lvCharacters.Items.IndexOf(item);

            this.ListOfCharacters.RemoveAt(index);
        }

        /// <summary>
        /// Switches the page to Inventory
        /// </summary>
        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = lvCharacters.Items.IndexOf(item);

            Switcher.Switch(new Inventory(index));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!ListOfCharacters.Any())
            {
                MessageBox.Show("Your character list is empty.", "Error!");
            }
            else
            { 
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "CharacterCreator - " + DateTime.Now.ToString("ddMMyyhhmmss");
                dlg.DefaultExt = ".xml";
                dlg.Filter = "XML Documents (.xml)|*.xml";

                Nullable<bool> result = dlg.ShowDialog();

                if (result == true)
                {
                    string filePath = dlg.FileName;
                    ListToXmlFile(filePath);
                }
            }
        }

        private void ListToXmlFile(string filePath)
        {
            using (var sw = new StreamWriter(filePath))
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<Character>));
                serializer.Serialize(sw, ListOfCharacters);
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region txtCharacterName functions

        private void txtCharacterName_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCharacterNameIsNotEmpty();
        }

        private void txtCharacterName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtCharacterName.Text))
                txtCharacterNameIsEmpty();
        }

        private void txtCharacterNameIsEmpty()
        {
            this.txtCharacterName.BorderBrush = Brushes.Red;
            this.lbEmptyCharacterName.Visibility = Visibility.Visible;
        }

        private void txtCharacterNameIsNotEmpty()
        {
            this.txtCharacterName.ClearValue(Border.BorderBrushProperty);
            this.lbEmptyCharacterName.Visibility = Visibility.Hidden;
        }
        #endregion

        /// <summary>
        /// Prepares the form by setting up the default values.
        /// </summary>
        private void PrepareForm()
        {
            this.cmbSex.ItemsSource = Enum.GetValues(typeof(Enums.CharacterSex)).Cast<Enums.CharacterSex>();
            this.cmbRaces.ItemsSource = Enum.GetValues(typeof(Enums.Races)).Cast<Enums.Races>();
            this.cmbProfessions.ItemsSource = Enum.GetValues(typeof(Enums.Professions)).Cast<Enums.Professions>();

            this.txtCharacterName.Clear();
            this.cmbSex.SelectedIndex = 0;
            this.cmbRaces.SelectedIndex = 0;
            this.cmbProfessions.SelectedIndex = 0;
            this.slLevel.Value = 1;
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

    }
}