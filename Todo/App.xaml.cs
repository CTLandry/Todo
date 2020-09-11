using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Unity;
using Unity;
using Todo.Views;
using Todo.ViewModels;

namespace Todo
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            App.Current.MainPage = new TodoListView();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Navigation Views
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<TodoListView, TodoListViewModel>();

            //Services
            //var config = ConfigLoader.LoadConfiguration();
            //containerRegistry.RegisterInstance<IConfiguration>(config);
            //containerRegistry.RegisterSingleton<I, Logger>();
            //containerRegistry.RegisterSingleton<ICache, CachingService>();
            //containerRegistry.RegisterSingleton<ISession, Session>();
            //containerRegistry.RegisterSingleton<ISocialAuth, SocialAuthenticationService>();
        }
    }
}
