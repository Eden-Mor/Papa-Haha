using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Shapes;

namespace PapaHaha
{
    public class InfoPopup : Popup
    {
        private const string JOKE_URL = "https://icanhazdadjoke.com/";
        private const string JOKE_API_COMPANY_NAME = "icanhazdadjoke";


        public InfoPopup()
        {
            BuildContent();
        }

        private void BuildContent()
        {
            Application.Current.Resources.TryGetValue("PaletteBlue25", out var PaletteBlue25);
            Application.Current.Resources.TryGetValue("PaletteBlue99", out var PaletteBlue99);

            this.Color = Colors.Transparent;

            Border border = new()
            {
                StrokeThickness = 1,
                Padding = 20,
                Margin = 10,
                Stroke = Colors.Black,
                BackgroundColor = Colors.White,
                StrokeShape = new RoundRectangle() { CornerRadius = 20 }
            };

            var slo = new StackLayout()
            {
                Children =
                    {
                        new Label()
                        {
                        TextColor = Colors.Black,
                        FormattedText = new FormattedString()
                        {
                            Spans =
                            {
                                new Span()
                                {
                                    Text = "This app dispenses dad jokes whenever the old man is tapped.\n\n"
                                },
                                new Span()
                                {
                                    Text = "Papa Haha gets its jokes from a generous public API.\nVisit it -> "
                                },
                                new Span()
                                {
                                    Text = JOKE_URL,
                                    TextColor = (Color)PaletteBlue25,
                                    GestureRecognizers =
                                    {
                                        new TapGestureRecognizer()
                                        {
                                            Command = new Command(async () => await Launcher.OpenAsync(JOKE_URL))
                                        }
                                    }
                                },
                                new Span()
                                {
                                    Text = "\n\nPlease show them some kindness!\nYou can also submit your own jokes into the repository!\n\nDisclaimer! We are not affiliated with "
                                },
                                new Span()
                                {
                                    Text = JOKE_API_COMPANY_NAME,
                                    TextColor = (Color)PaletteBlue25,
                                },
                                new Span()
                                {
                                    Text = "."
                                }
                            }
                        }
                    }
                    }
            };


            border.Content = slo;

            this.Content = border;
        }
    }
}
