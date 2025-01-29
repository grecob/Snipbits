using Caliburn.Micro;
using System.Windows;
using SnippetManager.ViewModels;
using SnippetManager.Views;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippetManager
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container = new SimpleContainer();

            // Register services
            _container.Instance(_container);
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();

            // custom services
            _container.Singleton<ISnippetManagerService, SnippetManagerService>();
            //_container.Singleton<IHotKeyService, HotKeyService>();

            // Register view models
            _container.PerRequest<MainViewModel>();
        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            await DisplayRootViewForAsync<MainViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
