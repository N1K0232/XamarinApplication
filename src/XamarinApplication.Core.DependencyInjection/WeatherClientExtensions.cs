using Microsoft.Extensions.DependencyInjection;
using System;

namespace XamarinApplication.Core.DependencyInjection
{
    public static class WeatherClientExtensions
    {
        public static IHttpClientBuilder AddWeatherClient(this IServiceCollection services)
        {
            var httpClientBuilder = services.AddHttpClient<IWeatherClient, WeatherClient>(httpClient =>
            {
                httpClient.BaseAddress = new Uri($"{Constants.BaseUrl}{Constants.ApiKey}");
                return new WeatherClient(httpClient);
            });

            return httpClientBuilder;
        }
    }
}