using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamFormsUWPOAuth.Shared;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamFormsUWPOAuth {
  public partial class App {
    public App() {
      InitializeComponent();
      Navigator.Current.NavigateToAuthentication = async () => {
        await MainPage.Navigation.PushAsync(new AuthenticationPage());
      };
      Navigator.Current.AuthenticationComplete = async () => {
        await MainPage.Navigation.PopAsync();
      };
      MainPage = new NavigationPage(new MainPage());
    }

    public static string AppName { get; } = "XamFormsUWPOAuth";

    protected override void OnStart() {
      // Handle when your app starts
    }

    protected override void OnSleep() {
      // Handle when your app sleeps
    }

    protected override void OnResume() {
      // Handle when your app resumes
    }
  }
}
