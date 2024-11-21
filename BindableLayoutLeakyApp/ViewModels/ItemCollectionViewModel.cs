using BindableLayoutLeakyApp.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindableLayoutLeakyApp.ViewModels
{
    public class ItemCollectionViewModel : INotifyPropertyChanged
    {
        public static int InstanceCount { get; private set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        private List<ItemViewModel>? _items = null;
        public List<ItemViewModel> Items
        {
            get
            {
                if(_items == null)
                {
                    _items = [];
                    for (int i = 0; i < 2; i++)
                    {
                        _items.Add(new ItemViewModel());
                    }
                }
                
                return _items ?? [];
            }
        }
        private int _inMemoryItemCount;
        private int _inMemorySubItemCount;
        private int _inMemoryCollectionViewPageCount;
        private int _inMemoryBindableLayoutPageCount;
        private int _inMemoryNestedBindableLayoutPageCount;

        public string InMemoryItemCount => $"Current Items Still In Memory: {_inMemoryItemCount}";
        public string InMemorySubItemCount => $"Current SubItems Still In Memory: {_inMemorySubItemCount}";
        public string InMemoryCollectionViewPageCount => $"Current CollectionView Pages Count Still In Memory: {_inMemoryCollectionViewPageCount}";
        public string InMemoryBindableLayoutPageCount => $"Current BindableLayout Pages Count Still In Memory: {_inMemoryBindableLayoutPageCount}";
        public string InMemoryNestedBindableLayoutPageCount => $"Current Nested BindableLayout Pages Count Still In Memory: {_inMemoryNestedBindableLayoutPageCount}";
        public ItemCollectionViewModel()
        {
            StartCountUpdateThread();
            InstanceCount++;
        }
        ~ItemCollectionViewModel()
        {
            InstanceCount--;
        }
        private void StartCountUpdateThread()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1000);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _inMemoryItemCount = ItemViewModel.InstanceCount;
                        _inMemorySubItemCount = SubItemViewModel.InstanceCount;
                        _inMemoryCollectionViewPageCount = CollectionView_NoLeak_Page.InstanceCount;
                        _inMemoryBindableLayoutPageCount = BindableLayout_Leaky_Page.InstanceCount;
                        _inMemoryNestedBindableLayoutPageCount = NestedBindableLayout_Leaky_Page.InstanceCount;

                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InMemoryItemCount)));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InMemorySubItemCount)));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InMemoryCollectionViewPageCount)));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InMemoryBindableLayoutPageCount)));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InMemoryNestedBindableLayoutPageCount)));

                    });
                }
            });
        }
    }
}
