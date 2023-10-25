using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using PapaHaha.Models;
using PapaHaha.Services;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace PapaHaha
{
    public partial class MainPage : ContentPage
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private RESTClient RESTClient = new();
        private bool? oldIsLandscape = null;
        private DateTime lastTapTime = DateTime.MinValue;
        private readonly TimeSpan refreshDelay = TimeSpan.FromMilliseconds(500);
        private string joke = string.Empty;


        public MainPage()
        {
            InitializeComponent();

            SizeChanged += MainPage_SizeChanged;
            SetLandscapePortrait();

            var test = Uri.TryCreate("old_man_smile.png", UriKind.Absolute, out Uri uri) && uri.Scheme != "file" ? ImageSource.FromUri(uri) : ImageSource.FromFile("old_man_smile.png");
            OldManIMG.Source = ImageSource.FromFile("old_man_smile.png");
        }

        private void MainPage_SizeChanged(object sender, EventArgs e) => SetLandscapePortrait();

        private void SetLandscapePortrait()
        {
            bool isLandscape = this.Width > this.Height;

            if (isLandscape == oldIsLandscape)
                return;

            oldIsLandscape = isLandscape;

            Grid.SetColumn(jokeFrame, isLandscape ? 1 : 0);
            Grid.SetColumnSpan(jokeFrame, isLandscape ? 1 : 2);
            Grid.SetRowSpan(jokeFrame, isLandscape ? 2 : 1);

            Grid.SetColumn(buttonFrame, isLandscape ? 0 : 1);
            Grid.SetRow(buttonFrame, isLandscape ? 0 : 1);
            buttons.Orientation = isLandscape ? StackOrientation.Vertical : StackOrientation.Horizontal;
        }


        private async Task<bool> AnimateTextAsync(string text, CancellationToken token)
        {
            try
            {
                Random rnd = new();

                jokeText.Text = string.Empty;
                foreach (char c in text)
                {
                    if (token.IsCancellationRequested || jokeText == null)
                        return false;

                    jokeText.Text += c;
                    await Task.Delay(rnd.Next(10, 50)); // Adjust the delay duration as needed
                }
            }
            catch (COMException)
            {
                return false;
                //This exception happens when you close the app/program while the joke is being animated.
            }

            return true;
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            if (currentTime - lastTapTime < refreshDelay)
                return; // Too soon, don't process the tap

            lastTapTime = currentTime;

            var img = sender as Image;
            await img.ScaleTo(1, 250, Easing.Linear);
            await img.ScaleTo(1.1, 250, Easing.Linear);

            // Cancel any ongoing animation
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();

            var response = await RESTClient.GetAsync("https://icanhazdadjoke.com/");
            var json = await response.Content.ReadAsStringAsync();
            var jokeProperty = JsonSerializer.Deserialize<Joke>(json);

            joke = jokeProperty.JokeText;

            CancellationToken animationToken = cancellationTokenSource.Token;

            Task<bool> animationTask;
            if (jokeProperty.Status == HttpStatusCode.OK)
                animationTask = AnimateTextAsync(joke, animationToken);
            else
                animationTask = AnimateTextAsync("Joke did not land.\nYou might not be connected to the internet.", animationToken);

            bool success = await animationTask;

            if (success)
                SemanticScreenReader.Announce(jokeText.Text);
        }

        private async void Copy_TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (string.IsNullOrEmpty(joke))
            {
                var toast = Toast.Make("Joke could not be copied.\nYou must be joking.");
                await toast.Show();
                return;
            }

            await Clipboard.SetTextAsync(joke);

            var successToast = Toast.Make("Successfully copied joke.");
            await successToast.Show();
        }

        private async void Share_TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (string.IsNullOrEmpty(joke))
            {
                var toast = Toast.Make("No joke to share, the joke is on you.");
                await toast.Show();
                return;
            }


            try
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Text = joke,
                    Title = "Share the joke!"
                });
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during sharing
                Console.WriteLine($"Error sharing text: {ex.Message}");
            }
        }

        private void Info_TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            this.ShowPopup(new InfoPopup());
        }

        private async void Settings_TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            await Shell.Current.GoToAsync("SettingsPage");
        }

    }

}
