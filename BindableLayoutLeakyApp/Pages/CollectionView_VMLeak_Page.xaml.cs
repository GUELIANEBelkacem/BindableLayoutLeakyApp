using BindableLayoutLeakyApp.ViewModels;

namespace BindableLayoutLeakyApp.Pages;

public partial class CollectionView_VMLeak_Page : ContentPage
{
    public static int InstanceCount { get; private set; }   
    public CollectionView_VMLeak_Page()
	{
		InitializeComponent();
        InstanceCount++;
    }
    ~CollectionView_VMLeak_Page()
    {
        InstanceCount--;
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