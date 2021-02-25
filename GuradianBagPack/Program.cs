using GuardianBagPack.Controller;
using GuardianBagPack.Model.Interface;
using System.Threading.Tasks;

namespace GuradianBagPack
{
    class Program
    {
        static void Main(string[] args)
        {
            IController controller = Controller.Instance;
            controller.Main(args);
        }
    }
}
