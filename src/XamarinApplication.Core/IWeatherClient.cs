using System;
using System.Threading.Tasks;
using XamarinApplication.Core.Models;

namespace XamarinApplication.Core
{
    public interface IWeatherClient : IDisposable
    {
        Task<Response> SearchAsync(string city);
    }
}