using BindableLayoutLeakyApp.Pages;
using BindableLayoutLeakyApp.ViewModels;
using System.ComponentModel;

namespace BindableLayoutLeakyApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            StartCountUpdateThread();
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

                        itemsLabel.Text = $"{ItemViewModel.InstanceCount}";
                        subItemsLabel.Text = $"{SubItemViewModel.InstanceCount}";
                        cvLabel.Text = $"{CollectionView_VMLeak_Page.InstanceCount}";
                        blLabel.Text = $"{BindableLayout_VMLeak_Page.InstanceCount}";
                        nblLabel.Text = $"{NestedBindableLayout_PageLeak_Page.InstanceCount}";
                        collectionsLabel.Text = $"{ItemCollectionViewModel.InstanceCount}"; 
                         



                    });
                }
            });
        }

        private async void NewCollectionViewPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CollectionView_VMLeak_Page());
        }

        private async void NewBindableLayoutPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BindableLayout_VMLeak_Page());
        }

        private async void NewNestedBindableLayoutPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NestedBindableLayout_PageLeak_Page());
        }
    }

}
