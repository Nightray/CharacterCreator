﻿using System;
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

namespace CharacterCreator
{
    /// <summary>
    /// Interaction logic for CharacterList.xaml
    /// </summary>
    public partial class CharacterList : UserControl, ISwitchable
    {
        // Access point to the ListOfCharacters object in the global scope. 
        App app = Application.Current as App;
        ObservableCollection<Character> ListOfCharacters;

        public CharacterList()
        {
            ListOfCharacters = app.Global.ListOfCharacters; // The list of characters exists in the global scope.
            InitializeComponent();
            DataContext = this;

            this.cmbSex.ItemsSource = Enum.GetValues(typeof(Enums.CharacterSex)).Cast<Enums.CharacterSex>();
            this.cmbRaces.ItemsSource = Enum.GetValues(typeof(Enums.Races)).Cast<Enums.Races>();
            this.cmbProfessions.ItemsSource = Enum.GetValues(typeof(Enums.Professions)).Cast<Enums.Professions>();

            // For whatever reason the xaml binding doesn't work
            // with global scope character list.
            this.lvCharacters.ItemsSource = ListOfCharacters;

            ClearForm();
        }

        #region Buttons
        /// <summary>
        /// Takes values form the form, perfoms all necesary checks,
        /// if validation pases, adds the character and clears the form 
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
                ListOfCharacters.Add(character);
                ClearForm();
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = lvCharacters.Items.IndexOf(item);

            ListOfCharacters.RemoveAt(index);
        }

        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = lvCharacters.Items.IndexOf(item);

            Switcher.Switch(new Inventory(index));
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
            this.lbEmptyCharacterName.Visibility = Visibility.Collapsed;
        }
        #endregion

        /// <summary>
        /// Clears the form and brings back the default values.
        /// </summary>
        private void ClearForm()
        {
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