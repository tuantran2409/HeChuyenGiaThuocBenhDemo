using HeChuyenGiaThuocBenh.BLL;
using HeChuyenGiaThuocBenh.UI.Forms;
using Microsoft.Extensions.Configuration;

namespace HeChuyenGiaThuocBenh.UI;

static class Program
{
    [STAThread]
    static void Main()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        string connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not configured.");

        ServiceContainer.Initialize(connectionString);

        ApplicationConfiguration.Initialize();
        Application.Run(new LoginForm());
    }
}
