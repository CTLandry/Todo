using Prism;
using Prism.Ioc;
using Prism.Unity;
using Todo.Infrastructure.IoC;
using Todo.Views;


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
            try
            {
                IoCServices.RegisterServices(containerRegistry);
                IoCNavigation.RegisterViewsAndViewModels(containerRegistry);
            }
            catch (System.Exception ex)
            {

            }
        }
    }
}
