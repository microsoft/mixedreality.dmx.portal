// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace DMX.Portal.Web.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker : ICloudBroker
    {
        private readonly string provisionClientId;
        private readonly string provisionClientSecret;
        private readonly string tenantId;
        private readonly string subscriptionId;
        private readonly string dmxPortalClientId;
        private readonly string dmxPortalInstance;
        private readonly string dmxPortalCallbackPath;
        private readonly string dmxPortalDomain;
        private readonly string dmxGatekeeperApiUrl;
        private readonly string dmxGatekeeperApiAccessKey;
        private readonly string dmxGatekeeperApiScopesGetAllLabs;
        private readonly string dmxGatekeeperApiScopesPostLab;
        private readonly string dmxGatekeeperApiUri;
        private readonly IAzure azure;

        public CloudBroker()
        {
            this.provisionClientId = Environment.GetEnvironmentVariable("AzureAdAppProvisionClientId");
            this.provisionClientSecret = Environment.GetEnvironmentVariable("AzureAdAppProvisionClientSecret");
            this.tenantId = Environment.GetEnvironmentVariable("AzureTenantId");
            this.subscriptionId = Environment.GetEnvironmentVariable("AzureSubscriptionId");
            this.dmxPortalClientId = Environment.GetEnvironmentVariable("AzureAdAppDmxPortalClientId");
            this.dmxPortalInstance = Environment.GetEnvironmentVariable("AzureAdDmxPortalInstance");
            this.dmxPortalCallbackPath = Environment.GetEnvironmentVariable("AzureAdDmxPortalCallbackPath");
            this.dmxPortalDomain = Environment.GetEnvironmentVariable("AzureAdDmxPortalDomain");
            this.dmxGatekeeperApiUrl = Environment.GetEnvironmentVariable("AzureAppServiceDmxGatekeeperApiUrl");
            this.dmxGatekeeperApiAccessKey = Environment.GetEnvironmentVariable("AzureAppServiceDmxGatekeeperApiAccessKey");
            this.dmxGatekeeperApiScopesGetAllLabs = Environment.GetEnvironmentVariable("AzureAdAppDmxGatekeeperScopesGetAllLabs");
            this.dmxGatekeeperApiScopesPostLab = Environment.GetEnvironmentVariable("AzureAdAppDmxGatekeeperScopesPostLab");
            this.dmxGatekeeperApiUri = Environment.GetEnvironmentVariable("AzureAdDmxGatekeeperAppIdUri");
            this.azure = AuthenticateAzure();
        }

        private IAzure AuthenticateAzure()
        {
            AzureCredentials credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(
                clientId: this.provisionClientId,
                clientSecret: this.provisionClientSecret,
                tenantId: this.tenantId,
                environment: AzureEnvironment.AzureGlobalCloud);

            return Azure.Configure().WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(azureCredentials: credentials)
                    .WithSubscription(this.subscriptionId);
        }
    }
}