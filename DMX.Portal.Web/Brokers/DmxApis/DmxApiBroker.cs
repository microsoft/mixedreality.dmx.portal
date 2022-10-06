// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using RESTFulSense.Clients;

namespace DMX.Portal.Web.Brokers.DmxApis
{
    public partial class DmxApiBroker : IDmxApiBroker
    {
        private HttpClient httpClient;
        private readonly IConfiguration conifguration;
        private readonly IRESTFulApiFactoryClient apiClient;

        public DmxApiBroker(HttpClient httpClient,
            IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.conifguration = configuration;
            this.apiClient = GetApiClient(configuration);
        }

        private async ValueTask<T> GetAsync<T>(string relativeUrl) =>
            await this.apiClient.GetContentAsync<T>(relativeUrl);

        private async ValueTask<T> PostAsync<T>(string relativeUrl, T content) =>
            await this.apiClient.PostContentAsync<T>(relativeUrl, content);

        private async Task GetAccessTokenForScope(string scope)
        {
            string[] scopes = GetScopesFromConfiguration(scope);

            //string accessToken =
            //    await this.tokenAcquisition.GetAccessTokenForUserAsync(scopes);

            //this.httpClient.DefaultRequestHeaders.Authorization =
            //    new AuthenticationHeaderValue("Bearer", accessToken);
        }

        private IRESTFulApiFactoryClient GetApiClient(IConfiguration configuration)
        {
            LocalConfiguration localConfigurations =
                configuration.Get<LocalConfiguration>();

            string apiBaseUrl = localConfigurations.ApiConfiguration.Url;
            this.httpClient.BaseAddress = new Uri(apiBaseUrl);

            return new RESTFulApiFactoryClient(this.httpClient);
        }

        private string[] GetScopesFromConfiguration(string scopeCategory)
        {
            LocalConfiguration localConfiguration =
                this.conifguration.Get<LocalConfiguration>();

            localConfiguration.DownstreamApi.Scopes
                .TryGetValue(scopeCategory, out string scopes);

            return scopes.Split();
        }
    }
}
