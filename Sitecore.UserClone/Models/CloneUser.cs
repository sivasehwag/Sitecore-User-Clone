using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Shell.Framework.Commands.UserManager;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Sitecore.Web.UI.XamlSharp.Continuations;
using System;
using System.Collections.Specialized;

namespace Sitecore.UserClone.Models
{
    /// <summary>
    /// Represents the Edit User command.
    /// </summary>
    [Serializable]
    public class CloneUser : Command, ISupportsContinuation
    {
        public CloneUser()
        {
        }

        /// <summary>
        /// Executes the command in the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            string item = context.Parameters["username"];
            if (!ValidationHelper.ValidateUserWithMessage(item))
            {
                return;
            }
            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection["username"] = item;
            ClientPipelineArgs clientPipelineArg = new ClientPipelineArgs(nameValueCollection);
            ContinuationManager.Current.Start(this, "Run", clientPipelineArg);
        }

        /// <summary>
        /// Queries the state of the command.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override CommandState QueryState(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            return base.QueryState(context);
        }

        /// <summary>
        /// Runs the pipeline.
        /// </summary>
        /// <param name="args">The args.</param>
        protected static void Run(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            ListString listStrings = new ListString(args.Parameters["username"]);
            if (args.IsPostBack)
            {
                AjaxScriptManager.Current.Dispatch("usermanager:refresh");
                return;
            }
            UrlString urlString = new UrlString("/sitecore/shell/~/xaml/Sitecore.Shell.Applications.Security.CloneUser.aspx");
            urlString["us"] = listStrings[0];
            ModalDialogOptions modalDialogOption = new ModalDialogOptions(urlString.ToString())
            {
                Width = "950",
                Height = "700",
                Response = true
            };
            SheerResponse.ShowModalDialog(modalDialogOption);
            args.WaitForPostBack();
        }
    }
}
