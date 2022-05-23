using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

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
            RunsOn = BuildMachines.Windows2022,
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
    "../../../../.github/workflows/dotnet.yml");