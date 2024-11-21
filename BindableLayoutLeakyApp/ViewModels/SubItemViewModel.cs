using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindableLayoutLeakyApp.ViewModels
{
    public class SubItemViewModel
    {
        public static int InstanceCount { get; private set; }
        public static int InstanceIdx { get; private set; }
        public Color MyColor { get; private set; } = Colors.Green;

        private int _myIdx;
        public string SubItemName => $"SubItem {_myIdx}";

        public SubItemViewModel()
        {
            _myIdx = InstanceIdx++;
            InstanceCount++;
        }

        ~SubItemViewModel()
        {
            InstanceCount--;
        }
    }
}
