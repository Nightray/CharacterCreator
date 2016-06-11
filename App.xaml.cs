using System.Collections.ObjectModel;
using System.Windows;

namespace CharacterCreator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Global Global;
        public App()
        {
            this.InitializeComponent();

            Global = new Global();
            Global.ListOfCharacters = new ObservableCollection<Character>();
        }
    }
}
