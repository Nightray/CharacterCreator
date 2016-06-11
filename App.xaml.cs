using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
