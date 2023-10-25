using CommunityToolkit.Maui.Views;

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
            Frame vsl = new()
            {
                BorderColor = (Color)PaletteBlue99,
                Content =
                    new Label()
                    {
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
            };

            this.Content = vsl;
        }
    }
}
