using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMX.Portal.Web.Brokers.DmxApis;
using DMX.Portal.Web.Models.Services.Foundations.LabCommands;
using DMX.Portal.Web.Models.Views.Components.LabOverviewListComponents;
using DMX.Portal.Web.Models.Views.Components.LabOverviews;
using DMX.Portal.Web.Views.Bases;
using Microsoft.AspNetCore.Components;
using SharpStyles.Models;

namespace DMX.Portal.Web.Views.Components.LabCommandsList
{
    public partial class LabCommandList
    {
        [Inject]
        public IDmxApiBroker DmxApiBroker { get; set; }

        [Parameter]
        public string LabId { get; set; }

        public LabOverviewListComponentState State { get; set; }

        public IEnumerable<LabCommand> LabCommands { get; set; }
        public StyleBase StyleElement { get; set; }
        public LabOverviewStyle LabOverviewStyle { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                SetupStyles();
                
                List<LabCommand> labCommands =
                    await this.DmxApiBroker.GetAllLabCommandsAsync();

                this.LabCommands = labCommands.Where(labCommand =>
                    labCommand.LabId == Guid.Parse(LabId));

                State = LabOverviewListComponentState.Content;
            }
            catch (Exception ex)
            {
                State = LabOverviewListComponentState.Error;
            }
        }

        public void SetupStyles()
        {
            this.LabOverviewStyle = new LabOverviewStyle
            {
                LabOverview = new SharpStyle
                {
                    Border = "1px solid lightgrey",
                    Display = "flex",
                    Width = "100%",
                    BoxShadow = "1px 1px lightgrey",
                    MarginBottom = "5px"
                },

                DeviceOverviews = new SharpStyle
                {
                    Display = "flex",
                    Width = "100%",
                    Padding = "11px"
                },

                LabOverviewDetails = new SharpStyle
                {
                    Width = "100%",
                    Padding = "25px"
                },

                LabOverviewTitle = new SharpStyle
                {
                    Display = "flex"
                }
            };
        }
    }
}
