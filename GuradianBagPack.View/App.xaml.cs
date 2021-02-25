using GuardianBagPack.Model.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GuradianBagPack.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IView
    {
        private static readonly Lazy<App> _instance = new Lazy<App>(() => new App(), true);
        public static IView Instance { get => _instance.Value; }
        public static IController ParentController { get; set; }
        private MainWindow _mainWindow { get; set; }

        private App() : base()
        {

        }

        [STAThread]
        private void STAStart()
        {
            _mainWindow = new MainWindow(ParentController);
            _mainWindow.ShowDialog();
            //ParentController.Terminate();
        }
        public int Start(string[] args)
        {
            Thread t = new Thread(STAStart);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            return 0;
        }
    }
}
