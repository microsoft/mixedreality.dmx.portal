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
        private readonly string dmxGatekeeperApiUrl;
        private readonly string dmxGatekeeperApiAccessKey;
        private readonly IAzure azure;

        public CloudBroker()
        {
            this.provisionClientId = Environment.GetEnvironmentVariable("AzureAdAppProvisionClientId");
            this.provisionClientSecret = Environment.GetEnvironmentVariable("AzureAdAppProvisionClientSecret");
            this.tenantId = Environment.GetEnvironmentVariable("AzureTenantId");
            this.subscriptionId = Environment.GetEnvironmentVariable("AzureSubscriptionId");
            this.dmxGatekeeperApiUrl = Environment.GetEnvironmentVariable("AzureAppServiceDmxGatekeeperApiUrl");
            this.dmxGatekeeperApiAccessKey = Environment.GetEnvironmentVariable("AzureAppServiceDmxGatekeeperApiAccessKey");
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