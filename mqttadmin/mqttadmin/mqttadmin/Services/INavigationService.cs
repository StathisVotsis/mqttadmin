using mqttadmin.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;

namespace mqttadmin.Services
{
    public interface INavigationService
    {
        Task<Page> RemoveViewFromStack();

        Task NavigateTo<TVM>()
            where TVM : BaseViewModel;
    }
}
