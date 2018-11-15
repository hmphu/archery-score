using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ArcheryScore
{
    public partial class App : Application
    {
        public App()
        {
            //InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            base.OnStart();
            if (MainPage != null)
            {
                ((MainPage)MainPage).RestoreGame();
            }
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            if(MainPage != null){
                ((MainPage)MainPage).SaveGame();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            if(MainPage != null){
                ((MainPage)MainPage).RestoreGame();
            }
        }
    }
}
