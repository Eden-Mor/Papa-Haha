using CommunityToolkit.Maui.Views;

namespace PapaHaha;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    private async void Back_TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) => await Shell.Current.GoToAsync("..");
}