using Microsoft.Extensions.Logging;
using MSC.CM.XaSh.Helpers;
using MSC.ConferenceMate.API.Client;
using MSC.ConferenceMate.API.Client.Interface;
using SQLite;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;

namespace MSC.CM.XaSh.Services
{
    public abstract class AzureDataLoaderBase
    {
        protected SQLiteAsyncConnection _conn = App.Database.conn;
        protected IHttpClientFactory _httpClientFactory;
        protected ILogger<AzureDataLoader> _logger;

        public AzureDataLoaderBase(IHttpClientFactory httpClientFactory = null, ILogger<AzureDataLoader> logger = null)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        protected HttpClient GetHttpClient(string httpClientName)
        {
            if (_httpClientFactory == null)
            {
                throw new InvalidOperationException($"Use of {nameof(GetHttpClient)} requires configuration and injection of the HttpClientFactory.");
            }

            //var client = _httpClientFactory == null ? new HttpClient() : _httpClientFactory.CreateClient(httpClientName);
            var client = _httpClientFactory.CreateClient(httpClientName);
            return client;
        }

        protected IWebApiDataServiceCM GetWebAPIDataService(string httpClientName)
        {
            var token = AsyncHelper.RunSync(() => SecureStorage.GetAsync(Consts.AUTH_TOKEN));

            if (httpClientName == Consts.AUTHORIZED && string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException($"A null or empty JWT value was detected by {nameof(GetWebAPIDataService)} when using the authorized httpClientName ({httpClientName}).");
            }

            return new WebApiDataServiceCM(null, GetHttpClient(httpClientName));
        }
    }
}