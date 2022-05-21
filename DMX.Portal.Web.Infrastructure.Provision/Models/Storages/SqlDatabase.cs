// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Microsoft.Azure.Management.Sql.Fluent;

namespace DMX.Portal.Web.Infrastructure.Provision.Models.Storages
{
    public class SqlDatabase
    {
        public ISqlDatabase Database { get; set; }
        public string ConnectionString { get; set; }
    }
}