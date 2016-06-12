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

        private int selectedCharactrerIndex;
        bool EditMode = false;

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

        #region Buttons and Button related functions
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

            if (String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name))
                txtCharacterNameIsEmptyOrWhiteSpace();
            else
            {
                Character character = new Character(name, level, sex, race, profession);
                this.ListOfCharacters.Add(character);
                PrepareForm();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = lvCharacters.Items.IndexOf(item);

            this.ListOfCharacters.RemoveAt(index);

            // Checks if user is in the Character Edit mode, and resets the UI.
            if (this.EditMode)
            {
                this.spStandardButtons.Visibility = Visibility.Visible;
                this.spEditButtons.Visibility = Visibility.Collapsed;

                this.EditMode = false;
                this.selectedCharactrerIndex = -1;
                PrepareForm();
            }
        }

        private void btnEditCharacter_Click(object sender, RoutedEventArgs e)
        {

            var item = (sender as FrameworkElement).DataContext;
            int index = lvCharacters.Items.IndexOf(item);

            if (index == -1)
            {
                MessageBox.Show("You must select a character Fist!", "Error!");
            }
            else if (this.EditMode)
            {
                MessageBox.Show("You are already in the Edit Mode", "Error!");
            }
            else
            {
                this.EditMode = true;

                // Clearing the Form and removing wornings if present.
                PrepareForm();
                txtCharacterNameIsNotEmptyOrWhiteSpace();

                this.spStandardButtons.Visibility = Visibility.Collapsed;
                this.spEditButtons.Visibility = Visibility.Visible;
                
                this.selectedCharactrerIndex = index;

                this.txtCharacterName.Text = this.ListOfCharacters[index].Name;
                this.slLevel.Value = this.ListOfCharacters[index].Level;
                this.cmbSex.SelectedValue = this.ListOfCharacters[index].Sex;
                this.cmbRaces.SelectedValue = this.ListOfCharacters[index].Race;
                this.cmbProfessions.SelectedValue = this.ListOfCharacters[index].Profession;
            }



        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            this.spStandardButtons.Visibility = Visibility.Visible;
            this.spEditButtons.Visibility = Visibility.Collapsed;

            int index = this.selectedCharactrerIndex;

            this.ListOfCharacters[index].Name = this.txtCharacterName.Text;
            this.ListOfCharacters[index].Level = (int)this.slLevel.Value;
            this.ListOfCharacters[index].Sex = (Enums.CharacterSex)Enum.Parse(typeof(Enums.CharacterSex), this.cmbSex.Text);
            this.ListOfCharacters[index].Race = (Enums.Races)Enum.Parse(typeof(Enums.Races), this.cmbRaces.Text);
            this.ListOfCharacters[index].Profession = (Enums.Professions)Enum.Parse(typeof(Enums.Professions), this.cmbProfessions.Text);

            this.EditMode = false;
            Switcher.Switch(new CharacterList());

        }

        private void btnCancelChanges_Click(object sender, RoutedEventArgs e)
        {
            this.spStandardButtons.Visibility = Visibility.Visible;
            this.spEditButtons.Visibility = Visibility.Collapsed;

            this.EditMode = false;

            PrepareForm();
        }

        /// <summary>
        /// Switches the page to Inventory
        /// </summary>
        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            if (!this.EditMode)
            {
                var item = (sender as FrameworkElement).DataContext;
                int index = lvCharacters.Items.IndexOf(item);

                Switcher.Switch(new Inventory(index));
            }
            else
            {
                MessageBox.Show("You are in Character Edit mode now!", "Error!");
            }
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
                dlg.Filter = "XML documents (.xml)|*.xml";

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
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML document (.xml)|*.xml";

            Nullable<bool> result = dlg.ShowDialog();
            string filepath = "";
            if (result == true)
            {
                filepath = dlg.FileName;
            }
            if (File.Exists(filepath))
            {
                XmlFileToList(filepath);
            }
            else
            {
                MessageBox.Show("This is not the file you are looking for.");
            }
        }

        private void XmlFileToList(string filename)
        {
            using (var sr = new StreamReader(filename))
            {
                var deserializer = new XmlSerializer(typeof(ObservableCollection<Character>));
                ObservableCollection<Character> tmpList = (ObservableCollection<Character>)deserializer.Deserialize(sr);

                foreach (var item in tmpList)
                {
                    this.ListOfCharacters.Add(item);
                }
            }
        }
        #endregion

        #region txtCharacterName and related functions

        private void txtCharacterName_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCharacterNameIsNotEmptyOrWhiteSpace();
        }

        private void txtCharacterName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtCharacterName.Text) || String.IsNullOrWhiteSpace(this.txtCharacterName.Text))
                txtCharacterNameIsEmptyOrWhiteSpace();
        }

        private void txtCharacterNameIsEmptyOrWhiteSpace()
        {
            this.txtCharacterName.BorderBrush = Brushes.Red;
            this.lbEmptyCharacterName.Visibility = Visibility.Visible;
        }

        private void txtCharacterNameIsNotEmptyOrWhiteSpace()
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