using mqttadmin.Services;
using mqttadmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


[assembly: Dependency(typeof(NavigationService))]
namespace mqttadmin.Services
{
    public class NavigationService : INavigationService
    {
        public INavigation XFNavigation { get; set; }
        readonly IDictionary<Type, Type> _viewMapping = new Dictionary<Type, Type>();

        public void RegisterViewMapping(Type viewModel, Type view)
        {
            _viewMapping.Add(viewModel, view);
        }

        public async Task NavigateTo<TVM>() where TVM : BaseViewModel
        {
            await NavigateToView(typeof(TVM));

            if (XFNavigation.NavigationStack.Last().BindingContext is BaseViewModel)
                await ((BaseViewModel)(XFNavigation.NavigationStack.Last().BindingContext)).Init();
        }

        public Task<Page> RemoveViewFromStack()
        {
            return XFNavigation.PopAsync();
        }

        async Task NavigateToView(Type viewModelType)
        {
            Type viewType;
            if (!_viewMapping.TryGetValue(viewModelType, out viewType))
                throw new ArgumentException("No view found in View Mapping BaseViewModel");

            var constructor = viewType.GetTypeInfo()
                .DeclaredConstructors
                .FirstOrDefault(DefinitionCollection => DefinitionCollection.GetParameters().Count()<=0);

            var view = constructor.Invoke(null) as Page;
            await XFNavigation.PushAsync(view, true);
        }
    }
}
