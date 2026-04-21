using Microsoft.Extensions.DependencyInjection;

namespace NotificacionesAlumnos
{
    public partial class App : Application
    {
        public App()
        {
             InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}