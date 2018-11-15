using System;
using Xamarin.Forms;
using SkiaSharp.Views.Forms;
using ArcheryScore.Classes;
using System.ComponentModel;

namespace ArcheryScore
{
    public partial class MainPage : ContentPage
    {
        private IGame Game;

        public MainPage()
        {
            Title = "Archery";

            Splash();

            Game = GameMaker.CreateGame(GameType.Default);
            //Game = GameMaker.CreateGame(GameType.Easy);

            // Main layout
            StackLayout layout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical
            };

            // Top panel
            StackLayout scorePanel = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(10, 10, 10, 10),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex("#2F2F2F")
            };

            // Total score label
            scorePanel.Children.Add(new Label()
            {
                Text = "SCORES:",
                FontSize = 15,
                TextColor = Color.WhiteSmoke,
                HorizontalOptions = LayoutOptions.Start
            });
            Label totalScoreLabel = new Label
            {
                BindingContext = Game,
                FontSize = 15,
                TextColor = Color.GreenYellow,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            totalScoreLabel.SetBinding(Label.TextProperty, "TotalScore");
            totalScoreLabel.PropertyChanging += OnLabelChanging;
            totalScoreLabel.PropertyChanged += OnLabelChanged;
            scorePanel.Children.Add(totalScoreLabel);

            // Last arrow label
            scorePanel.Children.Add(new Label()
            {
                Text = "LAST ARROW:",
                FontSize = 15,
                TextColor = Color.WhiteSmoke,
                HorizontalOptions = LayoutOptions.EndAndExpand
            });
            Label lastScoreLabel = new Label()
            {
                BindingContext = Game,
                FontSize = 15,
                TextColor = Color.GreenYellow,
                HorizontalOptions = LayoutOptions.End
            };
            lastScoreLabel.SetBinding(Label.TextProperty, "LastScore", stringFormat: "+{0}");
            lastScoreLabel.PropertyChanging += OnLabelChanging;
            lastScoreLabel.PropertyChanged += OnLabelChanged;
            scorePanel.Children.Add(lastScoreLabel);

            SKCanvasView canvasView = new SKCanvasView();

            // Render game
            Game.Draw(canvasView);
            layout.Children.Add(scorePanel);
            layout.Children.Add(canvasView);

            // New game button
            Button newGameButton = new Button()
            {
                Text = "New Game",
                FontSize = 15,
            };

            newGameButton.Clicked += OnNewGameButtonClicked;

            layout.Children.Add(newGameButton);

            Content = layout;
        }

        public void Splash(){
            ActivityIndicator indicator = new ActivityIndicator()
            {
                IsRunning = true,
                IsVisible = true,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            Content = indicator;
        }

        // Save game state
        public void SaveGame(){
            Application.Current.Properties["TotalScore"] = Game.TotalScore;
            Application.Current.Properties["LastScore"] = Game.LastScore;
        }

        // Restore game state
        public void RestoreGame(){
            if(Application.Current.Properties.ContainsKey("TotalScore")){
                Game.TotalScore = (int)Application.Current.Properties["TotalScore"];
            }
            if (Application.Current.Properties.ContainsKey("LastScore"))
            {
                Game.LastScore = (int)Application.Current.Properties["LastScore"];
            }
        }

        private void OnNewGameButtonClicked(object sender, EventArgs e)
        {
            Game.New();
        }

        private void OnLabelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Text") return;
            ((Label)sender).FadeTo(1, easing: Easing.CubicOut, length: 1000);
        }

        private void OnLabelChanging(object sender, Xamarin.Forms.PropertyChangingEventArgs e)
        {
            if (e.PropertyName != "Text") return;
            ((Label)sender).Opacity = 0;
        }
    }
}
