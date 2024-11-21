using BindableLayoutLeakyApp.ViewModels;

namespace BindableLayoutLeakyApp.Pages;

public partial class CollectionView_NoLeak_Page : ContentPage
{
    public static int InstanceCount { get; private set; }   
    public CollectionView_NoLeak_Page()
	{
		InitializeComponent();
        InstanceCount++;
    }
    ~CollectionView_NoLeak_Page()
    {
        InstanceCount--;
    }

    private async void NewCollectionViewPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CollectionView_NoLeak_Page());
    }

    private async void NewBindableLayoutPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BindableLayout_Leaky_Page());
    }

    private async void NewNestedBindableLayoutPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NestedBindableLayout_Leaky_Page());
    }
}