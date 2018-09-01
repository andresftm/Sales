using Sales.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.InfraStructure
{
    public class InstanceLocater
    {
        public MainViewModel Main { get; set; }

        public InstanceLocater()
        {
            this.Main = new MainViewModel();
        }
    }
}
