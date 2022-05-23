// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Portal.Web.Infrastructure.Provision.Models.Storages;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.Sql.Fluent;

namespace DMX.Portal.Web.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker
    {
        public async ValueTask<ISqlServer> CreateSqlServerAsync(string sqlServerName, IResourceGroup resourceGroup)
        {
            return await azure.SqlServers
                .Define(sqlServerName)
                .WithRegion(Region.USCentral)
                .WithExistingResourceGroup(resourceGroup)
                .WithAdministratorLogin(this.adminName)
                .WithAdministratorPassword(this.adminAccess)
                .CreateAsync();
        }

        public async ValueTask<ISqlDatabase> CreateSqlDatabaseAsync(string sqlDatabaseName, ISqlServer sqlServer)
        {
            return await azure.SqlServers.Databases
                .Define(sqlDatabaseName).
                WithExistingSqlServer(sqlServer)
                .CreateAsync();
        }

        public SqlDatabaseAccess GetDatabaseAccess()
        {
            return new SqlDatabaseAccess
            {
                AdminAccess = this.adminAccess,
                AdminName = this.adminName
            };
        }
    }
}
