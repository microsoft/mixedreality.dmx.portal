// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

namespace DMX.Portal.Web.Infrastructure.Build.Services.ScriptGenerations
{
    public class ScriptGenerationService
    {
        private readonly ADotNetClient adotNetClient;

        public ScriptGenerationService() =>
            this.adotNetClient = new ADotNetClient();

        public void GenerateBuildScript()
        {
            var githubPipeline = new GithubPipeline
            {
                Name = "DMX portal build",

                OnEvents = new Events
                {
                    Push = new PushEvent
                    {
                        Branches = new string[] { "main" }
                    },

                    PullRequest = new PullRequestEvent
                    {
                        Branches = new string[] { "main" }
                    }
                },

                Jobs = new Jobs
                {
                    Build = new BuildJob
                    {
                        RunsOn = BuildMachines.WindowsLatest,

                        Steps = new List<GithubTask>
                        {
                            new CheckoutTaskV2
                            {
                                Name = "Checking out code"
                            },

                            new SetupDotNetTaskV1
                            {
                                Name = "Installing .Net",

                                TargetDotNetVersion = new TargetDotNetVersion
                                {
                                    DotNetVersion = "7.0.100-preview.4.22252.9",
                                    IncludePrerelease = true
                                }
                            },

                            new RestoreTask
                            {
                                Name = "Restoring packages"
                            },

                            new DotNetBuildTask
                            {
                                Name = "Building Project(s)"
                            },

                            new TestTask
                            {
                                Name = "Running Tests"
                            }
                        }
                    }
                }
            };

            var aDotNetClient = new ADotNetClient();

            aDotNetClient.SerializeAndWriteToFile(
                githubPipeline,
                path: "../../../../.github/workflows/dotnet.yml");
        }

        public void GenerateProvisionScript()
        {
            var githubPipeline = new GithubPipeline
            {
                Name = "Provision DMX Portal",

                OnEvents = new Events
                {
                    Push = new PushEvent
                    {
                        Branches = new string[] { "main" }
                    },

                    PullRequest = new PullRequestEvent
                    {
                        Branches = new string[] { "main" }
                    }
                },

                Jobs = new Jobs
                {
                    Build = new BuildJob
                    {
                        RunsOn = BuildMachines.WindowsLatest,

                        EnvironmentVariables = new Dictionary<string, string>
                        {
                            { "AzureSubscriptionId", "${{ secrets.AZURE_SUBSCRIPTION_ID }}"},
                            { "AzureTenantId", "${{ secrets.AZURE_TENANT_ID }}" },
                            { "AzureClientId", "${{ secrets.AZURE_CLIENT_ID }}"},
                            { "AzureClientSecret", "${{ secrets.AZURE_CLIENT_SECRET }}" },
                            { "AzureAdAppProvisionClientId", "${{ secrets.AZURE_AD_APP_PROVISION_CLIENT_ID }}" },
                            { "AzureAdAppProvisionClientSecret", "${{ secrets.AZURE_AD_APP_PROVISION_CLIENT_SECRET }}" },
                            { "AzureAdDmxPortalInstance", "${{ secrets.AZURE_AD_DMX_PORTAL_INSTANCE }}" },
                            { "AzureAdDmxPortalDomain", "${{ secrets.AZURE_AD_DMX_PORTAL_DOMAIN }}" },
                            { "AzureAdDmxPortalCallbackPath", "${{ secrets.AZURE_AD_DMX_PORTAL_CALLBACKPATH }}" },
                            { "AzureAdDmxGatekeeperAppIdUri", "${{ secrets.AZURE_AD_DMX_GATEKEEPER_APP_ID_URI }}" },
                            { "AzureAdDmxGatekeeperAppScopes", "${{ secrets.AZURE_AD_DMX_GATEKEEPER_APP_SCOPES }}" },
                            { "AzureAppServiceDmxGatekeeperApiUrl", "${{ secrets.AZURE_APP_SERVICE_DMX_GATEKEEPER_API_URL }}" },
                            { "AzureAppServiceDmxGatekeeperApiAccessKey", "${{ secrets.AZURE_APP_SERVICE_DMX_GATEKEEPER_API_ACCESS_KEY }}" }
                        },

                        Steps = new List<GithubTask>
                        {
                            new CheckoutTaskV2
                            {
                                Name = "Check out"
                            },

                            new SetupDotNetTaskV1
                            {
                                Name = "Setup Dot Net Version",

                                TargetDotNetVersion = new TargetDotNetVersion
                                {
                                    DotNetVersion = "7.0.100-preview.4.22252.9",
                                    IncludePrerelease = true,
                                }
                            },

                            new RestoreTask
                            {
                                Name = "Restore"
                            },

                            new DotNetBuildTask
                            {
                                Name = "Build"
                            },

                            new RunTask
                            {
                                Name = "Provision",
                                Run = "dotnet run --project .\\DMX.Portal.Web.Infrastructure.Provision\\DMX.Portal.Web.Infrastructure.Provision.csproj"
                            }
                        }
                    }
                }
            };

            this.adotNetClient.SerializeAndWriteToFile(
                githubPipeline,
                path: "../../../../.github/workflows/provision.yml");
        }
    }
}
