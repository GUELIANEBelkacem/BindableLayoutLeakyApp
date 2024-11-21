using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindableLayoutLeakyApp.ViewModels
{
    public class ItemViewModel
    {
        public static int InstanceCount { get; private set; }
        public static int InstanceIdx { get; private set; }
        public Color MyColor { get; private set; } = Colors.Blue;

        private int _myIdx;
        public string ItemName => $"Item {_myIdx}";

        private List<SubItemViewModel>? _subItems = null;
        public List<SubItemViewModel> SubItems
        {
            get
            {
                if (_subItems == null)
                {
                    _subItems = [];
                    for (int i = 0; i < 2; i++)
                    {
                        _subItems.Add(new SubItemViewModel());
                    }
                }

                return _subItems ?? [];
            }
        }
        public ItemViewModel()
        {
            _myIdx = InstanceIdx++;
            InstanceCount++;
        }

        ~ItemViewModel()
        {
            InstanceCount--;
        }
    }
}
