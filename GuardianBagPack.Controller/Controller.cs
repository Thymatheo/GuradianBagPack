using GuardianBagPack.Model.Interface;
using GuradianBagPack.View;
using System;

namespace GuardianBagPack.Controller
{
    public class Controller : IController
    {
        private IModel _model { get; set; }
        private IView _view { get; set; }
        private IConfigManager _configManager { get; set; }

        private static readonly Lazy<Controller> _instance = new Lazy<Controller>(() => new Controller(), true);
        public static IController Instance { get => _instance.Value; }

        public void Main(string[] arg)
        {
            _configManager = Model.ConfigManager.Instance;
            _configManager.LoadAuth();
            _model = Model.Model.Instance;
            _view = App.Instance;
            _view.Start(arg);
            _model.Main(arg);
            _configManager.WriteAuth();
        }

    }
}
