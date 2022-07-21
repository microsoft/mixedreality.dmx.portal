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
        private readonly IRESTFulApiFactoryClient apiClient;
        private readonly ITokenAcquisition tokenAcquisition;
        private readonly HttpClient httpClient;

        public DmxApiBroker(
            HttpClient httpClient,
            IConfiguration configuration,
            ITokenAcquisition tokenAcquisition)
        {
            this.httpClient = httpClient;
            this.apiClient = GetApiClient(configuration);
            this.tokenAcquisition = tokenAcquisition;
        }

        private async ValueTask<T> GetAsync<T>(string relativeUrl) =>
            await this.apiClient.GetContentAsync<T>(relativeUrl);

        private async ValueTask<T> PostAsync<T>(string relativeUrl, T content) =>
            await this.apiClient.PostContentAsync<T>(relativeUrl, content);

        private IRESTFulApiFactoryClient GetApiClient(IConfiguration configuration)
        {
            LocalConfiguration localConfigurations =
                configuration.Get<LocalConfiguration>();

            string apiBaseUrl = localConfigurations.ApiConfiguration.Url;
            this.httpClient.BaseAddress = new Uri(apiBaseUrl);

            return new RESTFulApiFactoryClient(this.httpClient);
        }

        private async Task RefreshUserTokenAsync()
        {
            string[] requiredScopes = new string[] { "api://YOUR_APP_ID/AllAccess" };

            string accessToken =
                await this.tokenAcquisition.GetAccessTokenForUserAsync(requiredScopes);

            this.httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}
