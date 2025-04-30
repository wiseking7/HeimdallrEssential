namespace Heimdallr.App;

class Starter
{
  [STAThread]
  private static void Main(string[] args)
  {
    App app = new App();
    app.Run();
  }
}
