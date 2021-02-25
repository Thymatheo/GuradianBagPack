using System;
using System.Collections.Generic;
using System.Text;

namespace GuardianBagPack.Model.Interface
{
    public interface IConfigManager
    {
        void LoadAuth();
        void WriteAuth();
    }
}
