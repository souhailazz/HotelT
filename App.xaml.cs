using Microsoft.Extensions.DependencyInjection;
using Hotel.Services;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Hotel.Data;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Hotel
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();

            // Configure the DbContext with the connection string
            serviceCollection.AddDbContext<HotelReservationContext>(options =>
                options.UseSqlServer(@"Server=DESKTOP-5ITJ9LT\SQLEXPRESS;Database=HotelReservationDB5;Trusted_Connection=True;TrustServerCertificate=True;"));

            serviceCollection.AddTransient<ClientService>();
            serviceCollection.AddTransient<ReservationService>();
            serviceCollection.AddTransient<PaiementService>();
            serviceCollection.AddTransient<EmployeService>();
            serviceCollection.AddTransient<TypeDeChambreService>();
            serviceCollection.AddScoped<AdministrateurService>();

            // Register windows
            serviceCollection.AddTransient<LoginWindow>();
            serviceCollection.AddTransient<AdminWindow>();
            serviceCollection.AddTransient<signupWindow>();
            serviceCollection.AddTransient<GestionClientsWindow>();
          

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var LoginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
            LoginWindow.Show();

            
        }
    }
}
