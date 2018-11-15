using SkiaSharp.Views.Forms;

namespace ArcheryScore.Classes
{
    public interface IGame
    {
        int TotalScore { get; set; }
        int LastScore { get; set; }
        void Draw(SKCanvasView c);
        void New();
    }
}
