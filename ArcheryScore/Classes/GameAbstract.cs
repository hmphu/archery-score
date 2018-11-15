using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace ArcheryScore.Classes
{
    public abstract class GameAbstract : IGame, INotifyPropertyChanged
    {
        int totalScore;
        int lastScore;
        SKPoint lastPoint;
        int lastScoreFontSize = 0;
        string arrowColor = "#000000";

        readonly int maxRadius;
        readonly List<ScoreRange> gameBoard;

        protected SKCanvasView canvasView;
        protected SKPoint center;
        protected SKImageInfo canvasInfo;
        protected readonly int padding = 50;

        public event PropertyChangedEventHandler PropertyChanged;

        protected abstract List<ScoreRange> GetGameBoard();

        protected GameAbstract(){
            this.gameBoard = this.GetGameBoard();
            this.gameBoard.Reverse();
            this.maxRadius = this.gameBoard[0].radius;
        }

        public int TotalScore
        {
            get { return totalScore;}
            set { totalScore = value; OnPropertyChanged("TotalScore"); }
        }

        public int LastScore
        {
            get { return lastScore; }
            set { lastScore = value; OnPropertyChanged("LastScore"); }
        }

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void Draw(SKCanvasView c)
        {
            canvasView = c;
            canvasView.HorizontalOptions = LayoutOptions.FillAndExpand;
            canvasView.VerticalOptions = LayoutOptions.FillAndExpand;
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            canvasView.EnableTouchEvents = true;
            canvasView.Touch += OnCanVasTouchAsync;
        }

        private int GetPointsByDistance(double distance)
        {
            for (int i = this.gameBoard.Count - 1; i >= 0; i--)
            {
                float radius = GetRealRadius(this.gameBoard[i].radius);
                if (radius >= distance)
                {
                    arrowColor = this.gameBoard[i].arrowColor;
                    return this.gameBoard[i].points;
                }
            }

            return 0;
        }

        private double GetDistaceFromCenter(float x, float y)
        {
            // Based on the Pythagorean theorem
            return Math.Sqrt(Math.Pow(center.X - x, 2) + Math.Pow(center.Y - y, 2));
        }

        private float GetRealRadius(int radius)
        {
            return (radius * Math.Min(canvasInfo.Width - padding, canvasInfo.Height - padding) / 2) / maxRadius;
        }

        private float GetDesignedPoint(float radius)
        {
            return radius * maxRadius * 2 / Math.Min(canvasInfo.Width - padding, canvasInfo.Height - padding);
        }

        protected async void OnCanVasTouchAsync(object sender, SKTouchEventArgs e)
        {
            double distanceFromCenter = GetDistaceFromCenter(e.Location.X, e.Location.Y);
            lastPoint = e.Location;
            lastPoint.X -= 5;
            LastScore = GetPointsByDistance(distanceFromCenter);
            TotalScore += LastScore;
            await DrawArrow();
        }

        protected void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            // Draw the targets board
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            SKPaint paint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke
            };

            canvasInfo = args.Info;
            canvas.Clear();
            center = new SKPoint(canvasInfo.Width / 2, canvasInfo.Height / 2);
            float scale = (float)canvasInfo.Width / 256f;

            gameBoard.ForEach((target) =>
            {
                float radius = GetRealRadius(target.radius);

                // Draw stroke
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = target.StrokeWidth > 0 ? target.StrokeWidth * scale : 1f * scale;
                paint.Color = SKColor.Parse(target.strokeColor);
                canvas.DrawCircle(center.X, center.Y, radius, paint);

                // Draw fill
                paint.Style = SKPaintStyle.Fill;
                paint.Color = SKColor.Parse(target.color);
                canvas.DrawCircle(center.X, center.Y, radius, paint);
            });

            if(LastScore > 0){
                canvas.DrawText("+", lastPoint, new SKPaint()
                {
                    IsAntialias = true,
                    FakeBoldText = true,
                    TextSize = lastScoreFontSize * scale,
                    Color = SKColor.Parse(arrowColor)
                });
            }else{
                canvas.DrawText("miss", lastPoint, new SKPaint()
                {
                    IsAntialias = true,
                    FakeBoldText = true,
                    TextSize = lastScoreFontSize * scale,
                    Color = SKColor.Parse("#f00")
                });
            }
        }

        async Task DrawArrow()
        {
            lastScoreFontSize = 0;

            while (lastScoreFontSize < 10)
            {
                lastScoreFontSize++;
                canvasView.InvalidateSurface();
                await Task.Delay(TimeSpan.FromSeconds(1.0 / 300));
            }
        }

        public void New()
        {
            TotalScore = 0;
            LastScore = 0;
            lastScoreFontSize = 0;
            canvasView.InvalidateSurface();
        }
    }
}
