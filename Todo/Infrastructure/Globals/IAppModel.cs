using System;
using System.Threading.Tasks;

namespace Todo.Infrastructure.Globals
{
    public interface IAppModel
    {
        bool UseDarkMode { get; set; }
        bool UseDeviceThemeSettings { get; set; }
    }
}
