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

namespace CharacterCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Character> CharacterList { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            CharacterList = new ObservableCollection<Character>();
            DataContext = this;

            this.cmbSex.ItemsSource = Enum.GetValues(typeof(Enums.CharacterSex)).Cast<Enums.CharacterSex>();
            this.cmbSex.SelectedIndex = 0;

            this.cmbRaces.ItemsSource = Enum.GetValues(typeof(Enums.Races)).Cast<Enums.Races>();
            this.cmbRaces.SelectedIndex = 0;

            this.cmbProfessions.ItemsSource = Enum.GetValues(typeof(Enums.Professions)).Cast<Enums.Professions>();
            this.cmbProfessions.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = this.txtbCharacterName.Text;
            int level = (int)this.slLevel.Value;
            Enums.CharacterSex sex = (Enums.CharacterSex)Enum.Parse(typeof(Enums.CharacterSex), this.cmbSex.Text);
            Enums.Races race = (Enums.Races)Enum.Parse(typeof(Enums.Races), this.cmbRaces.Text);
            Enums.Professions profession = (Enums.Professions)Enum.Parse(typeof(Enums.Professions), this.cmbProfessions.Text);

            Character character = new Character(name, level, sex, race, profession);
            CharacterList.Add(character);
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = lvCharacters.Items.IndexOf(item);

            this.CharacterList.RemoveAt(index);
        }
    }
}
