// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Portal.Web.Infrastructure.Provision.Brokers.Clouds;
using DMX.Portal.Web.Infrastructure.Provision.Brokers.Loggings;
using DMX.Portal.Web.Infrastructure.Provision.Models.Storages;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.Sql.Fluent;

namespace DMX.Portal.Web.Infrastructure.Provision.Services.Foundations.CloudManagements
{
    public class CloudManagementService : ICloudManagementService
    {
        private readonly ICloudBroker cloudBroker;
        private readonly ILoggingBroker loggingBroker;

        public CloudManagementService()
        {
            this.cloudBroker = new CloudBroker();
            this.loggingBroker = new LoggingBroker();
        }

        public async ValueTask<IResourceGroup> ProvisionResourceGroupAsync(string projectName, string environment)
        {
            string resourceGroupName = $"{projectName}-RESOURCES-{environment}".ToUpper();
            this.loggingBroker.LogActivity(message: $"Provisioning {resourceGroupName}...");
            IResourceGroup resourceGroup = await this.cloudBroker.CreateResourceGroupAsync(resourceGroupName);
            this.loggingBroker.LogActivity(message: $"Provisioning {resourceGroupName} completed.");

            return resourceGroup;
        }

        public async ValueTask<IAppServicePlan> ProvisionAppServicePlanAsync(string projectName, string environment, IResourceGroup resourceGroup)
        {
            string planName = $"{projectName}-PLAN-{environment}";
            this.loggingBroker.LogActivity(message: $"Provisioning {planName}...");
            IAppServicePlan appServicePlan = await this.cloudBroker.CreatePlanAsync(planName, resourceGroup);
            this.loggingBroker.LogActivity(message: $"Provisioning {planName} completed.");

            return appServicePlan;
        }

        public async ValueTask<ISqlServer> ProvisionSqlServerAsync(string projectName, string environment, IResourceGroup resourceGroup)
        {
            string sqlServerName = $"{projectName}-SqlSERVER-{environment}".ToLower();
            this.loggingBroker.LogActivity(message: $"Provisioning {sqlServerName}...");
            ISqlServer sqlServer = await this.cloudBroker.CreateSqlServerAsync(sqlServerName, resourceGroup);
            this.loggingBroker.LogActivity(message: $"Provisioning {sqlServerName} completed.");

            return sqlServer;
        }

        public async ValueTask<SqlDatabase> ProvisionSqlDatabaseAsync(
            string projectName,
            string environment,
            ISqlServer sqlServer)
        {
            string sqlDatabaseName = $"{projectName}-db-{environment}".ToLower();
            this.loggingBroker.LogActivity(message: $"Provisioning {sqlDatabaseName}...");
            ISqlDatabase database = await this.cloudBroker.CreateSqlDatabaseAsync(sqlDatabaseName, sqlServer);
            this.loggingBroker.LogActivity(message: $"Provisioning {sqlDatabaseName} completed.");

            return new SqlDatabase
            {
                Database = database,
                ConnectionString = GenerateConnectionString(database)
            };
        }

        public async ValueTask<IWebApp> ProvisionWebAppAsync(
            string projectName,
            string environment,
            string connectionString,
            IAppServicePlan servicePlan,
            IResourceGroup resourceGroup)
        {
            string appName = $"{projectName}-{environment}".ToLower();
            this.loggingBroker.LogActivity(message: $"Provisioning {appName}...");

            IWebApp webapp = await this.cloudBroker.CreateWebAppAsync(
                appName,
                connectionString,
                servicePlan,
                resourceGroup);

            this.loggingBroker.LogActivity(message: $"Provisioning {appName} completed.");

            return webapp;
        }

        private string GenerateConnectionString(ISqlDatabase sqlDatabase)
        {
            SqlDatabaseAccess access = this.cloudBroker.GetDatabaseAccess();

            return $"Server=tcp:{sqlDatabase.SqlServerName}.database.windows.net,1433;" +
                $"Initial Catalog={sqlDatabase.Name};Persist Security Info=False;" +
                $"User ID={access.AdminName};Password={access.AdminAccess};" +
                $"MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
    }
}
