using Sitecore;
using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.Controls;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Data.Templates;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Resources;
using Sitecore.Security;
using Sitecore.Security.Accounts;
using Sitecore.Security.Domains;
using Sitecore.SecurityModel;
using Sitecore.Shell.Applications.ContentManager;
using Sitecore.Shell.Applications.ControlPanel.Preferences.RegionalSettings;
using Sitecore.Shell.Applications.Dialogs.ItemLister;
using Sitecore.Shell.Framework.Commands;
using Sitecore.sitecore.shell.Applications.Content_Manager.Dialogs.OneColumnPreview;
using Sitecore.StringExtensions;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.Authentication;
using Sitecore.Web.UI;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Sitecore.Web.UI.XamlSharp.Ajax;
using Sitecore.Web.UI.XamlSharp.Xaml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace Sitecore.UserClone.Models
{
    /// <summary>
    /// Represents a GridDesignerPage.
    /// </summary>
    /// <summary>
    /// Represents a GridDesignerPage.
    /// </summary>
    public class CloneUserPage : DialogPage, IHasCommandContext
    {
        /// <summary>
        /// The add.
        /// </summary>
        protected System.Web.UI.WebControls.Button Add;

        /// <summary>
        /// The client language.
        /// </summary>
        protected DropDownList ClientLanguage;

        /// <summary>
        /// The content language.
        /// </summary>
        protected DropDownList ContentLanguage;

        /// <summary>
        /// The creation date.
        /// </summary>
        protected System.Web.UI.WebControls.Label CreationDate;

        /// <summary>
        /// Change profile button.
        /// </summary>
        protected System.Web.UI.WebControls.Button ChangeProfile;

        /// <summary>
        /// The description.
        /// </summary>
        protected TextBox Description;

        ///// <summary>
        ///// The domain name.
        ///// </summary>
        //protected System.Web.UI.WebControls.Label DomainName;

        /// <summary>
        /// The email.
        /// </summary>
        protected TextBox Email;

        /// <summary>
        /// The email.
        /// </summary>
        protected TextBox Password;

        /// <summary>
        /// The email.
        /// </summary>
        protected TextBox ConfirmPassword;

        /// <summary>
        /// The full name.
        /// </summary>
        protected TextBox FullName;

        /// <summary>
        /// The is administrator.
        /// </summary>
        protected Checkbox IsAdministrator;

        /// <summary>
        /// The last activity date.
        /// </summary>
        protected System.Web.UI.WebControls.Label LastActivityDate;

        /// <summary>
        /// The last lockout date.
        /// </summary>
        protected System.Web.UI.WebControls.Label LastLockoutDate;

        /// <summary>
        /// The last login date.
        /// </summary>
        protected System.Web.UI.WebControls.Label LastLoginDate;

        /// <summary>
        /// The last password changed date.
        /// </summary>
        protected System.Web.UI.WebControls.Label LastPasswordChangedDate;

        /// <summary>
        /// The managed domains.
        /// </summary>
        protected System.Web.UI.WebControls.Button ManagedDomains;

        /// <summary>
        /// The managed domains value.
        /// </summary>
        protected HtmlInputHidden ManagedDomainsValue;

        /// <summary>
        /// The Profile ID.
        /// </summary>
        protected HtmlInputHidden ProfileItemID;

        /// <summary>
        /// The User created.
        /// </summary>
        protected HtmlInputHidden UserCreatedValue;

        /// <summary>
        /// The portrait.
        /// </summary>
        protected TextBox Portrait;

        /// <summary>
        /// The portrait image.
        /// </summary>
        protected ThemedImage PortraitImage;

        /// <summary>
        /// Current portrait.
        /// </summary>
        protected ThemedImage CurrentPortrait;

        /// <summary>
        /// The portraits.
        /// </summary>
        protected DropDownList Portraits;

        /// <summary>
        /// The properties.
        /// </summary>
        protected Scrollbox Properties;

        /// <summary>
        /// The properties frame.
        /// </summary>
        protected Frame PropertiesFrame;

        /// <summary>
        /// Edit profile button
        /// </summary>
        protected System.Web.UI.WebControls.Button EditProfile;

        /// <summary>
        /// The regional iso code.
        /// </summary>
        protected DropDownList RegionalISOCode;

        /// <summary>
        /// The DomainNames.
        /// </summary>
        protected DropDownList DomainName;

        /// <summary>
        /// The remove.
        /// </summary>
        protected System.Web.UI.WebControls.Button Remove;

        /// <summary>
        /// The roles.
        /// </summary>
        protected HtmlSelect Roles;

        /// <summary>
        /// The roles value.
        /// </summary>
        protected HtmlInputHidden RolesValue;

        /// <summary>
        /// The start content editor.
        /// </summary>
        protected HtmlInputRadioButton StartContentEditor;

        /// <summary>
        /// The start custom.
        /// </summary>
        protected HtmlInputRadioButton StartCustom;

        /// <summary>
        /// The start default.
        /// </summary>
        protected HtmlInputRadioButton StartDefault;

        /// <summary>
        /// The start desktop.
        /// </summary>
        protected HtmlInputRadioButton StartDesktop;

        /// <summary>
        /// The start preview.
        /// </summary>
        protected HtmlInputRadioButton StartPreview;

        /// <summary>
        /// The start url.
        /// </summary>
        protected TextBox StartUrl;

        /// <summary>
        /// The start web edit.
        /// </summary>
        protected HtmlInputRadioButton StartWebEdit;

        /// <summary>
        /// The tab strip.
        /// </summary>
        protected Tabstrip TabStrip;

        /// <summary>
        /// The user name.
        /// </summary>
        protected TextBox UserName;

        /// <summary>
        /// User Properties Url
        /// </summary>
        private string userPropertiesUrl
        {
            get
            {
                return (string)this.ViewState["userPropertiesUrl"];
            }
            set
            {
                this.ViewState["userPropertiesUrl"] = value;
            }
        }

        public CloneUserPage()
        {
        }

        /// <summary>
        /// Handles the Add_ click event.
        /// </summary>
        protected void Add_Click()
        {
            ClientPipelineArgs currentArgs = ContinuationManager.Current.CurrentArgs as ClientPipelineArgs;
            Assert.IsNotNull(currentArgs, "args");
            if (!currentArgs.IsPostBack)
            {
                UrlString urlString = new UrlString("/sitecore/shell/~/xaml/Sitecore.Shell.Applications.Security.SelectRoles.aspx");
                UrlHandle urlHandle = new UrlHandle();
                urlHandle["roles"] = this.RolesValue.Value;
                urlHandle.Add(urlString);
                bool flag = string.Equals(Sitecore.Context.Language.Name, "ja-JP", StringComparison.InvariantCultureIgnoreCase);
                SheerResponse.ShowModalDialog(urlString.ToString(), "1050", (flag ? "760" : "690"), string.Empty, true);
                currentArgs.WaitForPostBack();
            }
            else if (currentArgs.HasResult)
            {
                string result = currentArgs.Result;
                if (result == "-")
                {
                    result = string.Empty;
                }
                ListString listStrings = new ListString(result);
                this.Roles.Items.Clear();
                List<string> strs = new List<string>();
                foreach (string listString in listStrings)
                {
                    this.Roles.Items.Add(listString);
                    strs.Add(listString);
                }
                if (RolesInRolesManager.RolesInRolesSupported)
                {
                    foreach (string str in listStrings)
                    {
                        foreach (Role rolesForRole in RolesInRolesManager.GetRolesForRole(Role.FromName(str), true))
                        {
                            if (strs.Contains(rolesForRole.Name))
                            {
                                continue;
                            }
                            this.Roles.Items.Add(string.Concat("<", rolesForRole.Name, ">"));
                            strs.Add(rolesForRole.Name);
                        }
                    }
                }
                SheerResponse.SetAttribute(this.RolesValue.ClientID, "value", result);
                SheerResponse.SetOuterHtml(this.Roles.ClientID, this.Roles);
                return;
            }
        }

        /// <summary>
        /// Handles the Change profile_ click event.
        /// </summary>
        protected void ChangeProfile_Click()
        {
            Item item;
            ClientPipelineArgs currentArgs = ContinuationManager.Current.CurrentArgs as ClientPipelineArgs;
            Assert.IsNotNull(currentArgs, "args");
            Domain domain = DomainManager.GetDomain(this.DomainName.SelectedValue);
            object[] domainName = new object[] { this.DomainName };
            Assert.IsNotNull(domain, "Domain \"{0}\" not found", domainName);
            if (!currentArgs.IsPostBack || (currentArgs.HasResult && currentArgs.Result == "yes"))
            {
                bool usernamecheck = true;
                bool userexists = !string.IsNullOrWhiteSpace(this.UserName.Text)? User.Exists(domain.GetFullName(this.UserName.Text)) :false;
                if (!userexists && !currentArgs.IsPostBack)
                {
                    usernamecheck = false;
                    SheerResponse.Confirm("Create the user before Changing the profile");
                    currentArgs.WaitForPostBack();
                }
               else if(!userexists && currentArgs.IsPostBack)
                {
                    usernamecheck = false;
                    if (!CreatingUser())
                    {
                        return;
                    }
                    SheerResponse.SetAttribute(this.UserCreatedValue.ClientID, "value", domain.GetFullName(this.UserName.Text));
                    this.UserCreatedValue.Value = domain.GetFullName(this.UserName.Text);
                }
                if (this.UserCreatedValue != null && !string.IsNullOrWhiteSpace(this.UserCreatedValue.Value))
                {
                    usernamecheck = false;
                    User user = User.FromName(domain.GetFullName(this.UserName.Text), true);
                    Assert.IsNotNull(user, typeof(User), "User not found", new object[0]);
                    UserProfile profile = user.Profile;

                    Database database = Factory.GetDatabase(Settings.ProfileItemDatabase, false);
                    if (database == null)
                    {
                        object[] profileItemDatabase = new object[] { Settings.ProfileItemDatabase };
                        Log.SingleWarn("Cannot load user profile definitions. The profile item database {0} was not found.".FormatWith(profileItemDatabase), new object());
                        object[] objArray = new object[] { Settings.ProfileItemDatabase };
                        SheerResponse.Alert(Translate.Text("Cannot load user profile definitions. The profile item database {0} was not found.", objArray), new string[0]);
                        return;
                    }
                    Item item1 = database.GetItem("/sitecore/system/Settings/Security/Profiles");
                    if (item1 == null)
                    {
                        object[] profileItemDatabase1 = new object[] { "/sitecore/system/Settings/Security/Profiles", Settings.ProfileItemDatabase };
                        Log.SingleWarn("Cannot load user profile definitions. The \"{0}\" item was not found in the {1} database.".FormatWith(profileItemDatabase1), new object());
                        object[] objArray1 = new object[] { "/sitecore/system/Settings/Security/Profiles", Settings.ProfileItemDatabase };
                        SheerResponse.Alert(Translate.Text("Cannot load user profile definitions. The \"{0}\" item was not found in the {1} database.", objArray1), new string[0]);
                        return;
                    }
                    SelectItemOptions selectItemOption = new SelectItemOptions()
                    {
                        Root = item1
                    };
                    SelectItemOptions selectItemOption1 = selectItemOption;
                    if (user.Profile.ProfileItemId == null)
                    {
                        item = null;
                    }
                    else
                    {
                        item = database.GetItem(user.Profile.ProfileItemId);
                    }
                    selectItemOption1.SelectedItem = item;
                    selectItemOption.ShowRoot = false;
                    selectItemOption.Icon = "People/16x16/user1_preferences.png";
                    selectItemOption.Title = "Change User Profile.";
                    selectItemOption.Text = "Select the user profile you want to give the user.";
                    selectItemOption.ButtonText = "Change";
                    ModalDialogOptions modalDialogOption = new ModalDialogOptions(selectItemOption.ToUrlString().ToString())
                    {
                        Response = true,
                        Width = "970"
                    };
                    SheerResponse.ShowModalDialog(modalDialogOption);
                    currentArgs.WaitForPostBack();
                }
                if (usernamecheck)
                {
                    SheerResponse.Alert(string.Format("Please check the username before Editing the profile"));
                    return;
                }
            }
            else if (currentArgs.HasResult)
            {
                if (currentArgs.Result == "no")
                {
                    return;
                }
                else if (currentArgs.HasResult && !currentArgs.Result.Equals("no") && !currentArgs.Result.Equals("yes"))
                {
                    User user = User.FromName(domain.GetFullName(this.UserName.Text), true);
                    Assert.IsNotNull(user, typeof(User), "User not found", new object[0]);
                    UserProfile profile = user.Profile;
                    user.Profile.ProfileItemId = currentArgs.Result;
                    user.Profile.Save();
                    SheerResponse.SetInnerHtml("Properties", string.Concat("<div style='padding:4px'>", Translate.Text("The user profile has been changed. The changes will be visible the next time you open this dialog box. "), "</div>"));
                    return;
                }
            }
        }

        /// <summary>
        /// Tabstrips the click.
        /// </summary>
        /// <param name="id">The id.</param>
        protected void ClickTab(string id)
        {
            this.TabStrip.SetActive(int.Parse(id.Substring(id.LastIndexOf("_tabdiv") + "_tabdiv".Length)));
        }
        /// <summary>
        /// Creating the User.
        /// </summary>
        /// <param name="id">The id.</param>
        protected bool CreatingUser()
        {
            Domain domain = DomainManager.GetDomain(this.DomainName.SelectedValue);
            object[] domainName = new object[] { this.DomainName };
            Assert.IsNotNull(domain, "Domain \"{0}\" not found", domainName);
            if (string.IsNullOrWhiteSpace(this.UserName.Text))
            {
                SheerResponse.Alert(string.Format("User name is required."));
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.Password.Text) || string.IsNullOrWhiteSpace(this.ConfirmPassword.Text))
            {
                SheerResponse.Alert(string.Format("Password is required."));
                return false;
            }
            if (!this.Password.Text.Equals(this.ConfirmPassword.Text))
            {
                SheerResponse.Alert(string.Format("The Password and Confirmation Password must match."));
                return false;
            }
            if (!this.OnValidateUserNameInDomain())
            {
                SheerResponse.Alert(string.Format("User name is not valid in the selected domain."));
                return false;
            }
            if (!CloneUserPage.ValidateUserName(this.UserName.Text, domain))
            {
                SheerResponse.Alert(string.Format("This user name is not handled by the '{0}' domain. Change the domain or pick a different user name.", this.DomainName));
                return false;
            }
            if (!this.Validate() || !this.ValidateTicket())
            {
                return false;
            }
            this.Validate();
            if (!this.Page.IsValid)
            {
                return false;
            }
            if (this.UserCreatedValue != null && string.IsNullOrWhiteSpace(this.UserCreatedValue.Value))
            { 
                if (!CreatedUser())
                {
                    return false;
                }
            }
            else
            {
                User user = User.FromName(domain.GetFullName(this.UserName.Text), true);
                Assert.IsNotNull(user, typeof(User), "User not found", new object[0]);
                UserProfile profile = user.Profile;
                Assert.IsNotNull(profile, typeof(UserProfile));
                IEnumerable<Role> items =
                    from System.Web.UI.WebControls.ListItem item in this.Roles.Items
                    where System.Web.Security.Roles.RoleExists(item.Value)
                    select Role.FromName(item.Value);
                HttpContext current = HttpContext.Current;
                Assert.IsNotNull(current, typeof(HttpContext));
                string empty = string.Empty;
                foreach (string key in current.Request.Form.Keys)
                {
                    if (key == null || !key.EndsWith("StartUrlSelector", StringComparison.InvariantCulture))
                    {
                        continue;
                    }
                    empty = current.Request.Form[key];
                    break;
                }
                if (empty == "Default")
                {
                    empty = string.Empty;
                }
                if (empty == "Custom")
                {
                    empty = this.StartUrl.Text;
                }
                user.Roles.Replace(items);
                bool flag = false;
                profile.IsAdministrator = this.IsAdministrator.Checked;
                profile.FullName = this.FullName.Text;
                profile.Email = this.Email.Text;
                profile.Comment = this.Description != null ? this.Description.Text : string.Empty;
                profile.StartUrl = empty;
                profile.ClientLanguage = this.ClientLanguage.SelectedValue;
                profile.RegionalIsoCode = this.RegionalISOCode.SelectedValue;
                profile.ContentLanguage = this.ContentLanguage.SelectedValue;
                profile.Portrait = this.Portrait.Text;
                profile.ProfileItemId = this.ProfileItemID.Value;
                SecurityAudit.LogManagedDomainChanged(this, user.Name, profile.ManagedDomainNames, this.DomainName.SelectedValue);
                profile.ManagedDomainNames = this.DomainName.SelectedValue;
                this.SaveCustomProperties(profile, ref flag);
                profile.Save();

                string[] userName = new string[] { domain.GetFullName(this.UserName.Text) };
                Log.Audit(this, "Cloned user: {0}", userName);
            }
            return true;
        }

        /// <summary>
        /// Creating the User.
        /// </summary>
        /// <param name="id">The id.</param>
        protected bool CreatedUser()
        {
            try
            {
                Domain domain = DomainManager.GetDomain(this.DomainName.SelectedValue);
                object[] domainName = new object[] { this.DomainName };
                Assert.IsNotNull(domain, "Domain \"{0}\" not found", domainName);
                string text = this.UserName.Text;
                string str = this.Description.Text;
                string text1 = this.Email.Text;
                if (Sitecore.Security.Accounts.User.Exists(domain.GetFullName(this.UserName.Text)))
                {
                    SheerResponse.Alert(string.Format("A user with this name already exists."));
                    return false;
                }
                else
                {
                    Membership.CreateUser(domain.GetFullName(this.UserName.Text), this.Password.Text, text1);
                    User user = Sitecore.Security.Accounts.User.FromName(domain.GetFullName(this.UserName.Text), true);
                    Assert.IsNotNull(user, "User was not created");
                    
                    UserProfile profile = user.Profile;
                    Assert.IsNotNull(profile, "User has no profile");
                    IEnumerable<Role> items =
                        from System.Web.UI.WebControls.ListItem item in this.Roles.Items
                        where System.Web.Security.Roles.RoleExists(item.Value)
                        select Role.FromName(item.Value);
                    HttpContext current = HttpContext.Current;
                    Assert.IsNotNull(current, typeof(HttpContext));
                    string empty = string.Empty;
                    foreach (string key in current.Request.Form.Keys)
                    {
                        if (key == null || !key.EndsWith("StartUrlSelector", StringComparison.InvariantCulture))
                        {
                            continue;
                        }
                        empty = current.Request.Form[key];
                        break;
                    }
                    if (empty == "Default")
                    {
                        empty = string.Empty;
                    }
                    if (empty == "Custom")
                    {
                        empty = this.StartUrl.Text;
                    }
                    user.Roles.Replace(items);
                    bool flag = false;
                    profile.IsAdministrator = this.IsAdministrator.Checked;
                    profile.FullName = this.FullName.Text;
                    profile.Email = this.Email.Text;
                    profile.Comment = this.Description != null ? this.Description.Text : string.Empty;
                    profile.StartUrl = empty;
                    profile.ClientLanguage = this.ClientLanguage.SelectedValue;
                    profile.RegionalIsoCode = this.RegionalISOCode.SelectedValue;
                    profile.ContentLanguage = this.ContentLanguage.SelectedValue;
                    profile.Portrait = this.Portrait.Text;
                    profile.ProfileItemId = this.ProfileItemID.Value;
                    SecurityAudit.LogManagedDomainChanged(this, user.Name, profile.ManagedDomainNames, this.DomainName.SelectedValue);
                    profile.ManagedDomainNames = this.DomainName.SelectedValue;
                    this.SaveCustomProperties(profile, ref flag);
                    profile.Save();
                   
                    string[] userName = new string[] { domain.GetFullName(this.UserName.Text) };
                    Log.Audit(this, "Cloned user: {0}", userName);
                    return true;
                }
            }
            catch (Exception e)
            {
                SheerResponse.Alert(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Called when the validate user name in has domain.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The arguments.
        /// </param>
        protected bool OnValidateUserNameInDomain()
        {
            if (string.IsNullOrEmpty(this.DomainName.Text))
            {
                return false;
            }
            Domain domain = DomainManager.GetDomain(this.DomainName.Text);
            Assert.IsNotNull(domain, typeof(Domain));
            if (!domain.IsValidAccountName(this.UserName.Text))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates the name of the user.
        /// </summary>
        /// <param name="userName">
        /// Name of the user.
        /// </param>
        /// <param name="domain">
        /// The domain.
        /// </param>
        /// <returns>
        /// The user name.
        /// </returns>
        private static bool ValidateUserName(string userName, Domain domain)
        {
            bool flag;
            Assert.ArgumentNotNull(userName, "userName");
            Assert.ArgumentNotNull(domain, "domain");
            using (IEnumerator<Domain> enumerator = DomainManager.GetDomains().GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Domain current = enumerator.Current;
                    if (!current.HandlesAccount(userName) || !(current.Name != domain.Name))
                    {
                        continue;
                    }
                    flag = false;
                    return flag;
                }
                return true;
            }
            return flag;
        }

        /// <summary>
        /// Handles the Edit profile_ click event.
        /// </summary>
        protected void EditProfile_Click()
        {
            ClientPipelineArgs currentArgs = ContinuationManager.Current.CurrentArgs as ClientPipelineArgs;
            Assert.IsNotNull(currentArgs, "args");
            Domain domain = DomainManager.GetDomain(this.DomainName.SelectedValue);
            object[] domainName = new object[] { this.DomainName };
            Assert.IsNotNull(domain, "Domain \"{0}\" not found", domainName);
            if (!currentArgs.IsPostBack || (currentArgs.HasResult && currentArgs.Result == "yes"))
            {
                bool usernamecheck = true;
                bool userexists = !string.IsNullOrWhiteSpace(this.UserName.Text) ? User.Exists(domain.GetFullName(this.UserName.Text)) : false;
                if (!userexists && !currentArgs.IsPostBack)
                {
                    usernamecheck = false;
                    SheerResponse.Confirm("Create the user before Editing the profile");
                    currentArgs.WaitForPostBack();
                }
                else if (!userexists && currentArgs.IsPostBack)
                {
                    usernamecheck = false;
                    if (!CreatingUser())
                    {
                        return;
                    }
                    SheerResponse.SetAttribute(this.UserCreatedValue.ClientID, "value", domain.GetFullName(this.UserName.Text));
                    this.UserCreatedValue.Value = domain.GetFullName(this.UserName.Text);
                }
                if (this.UserCreatedValue != null && !string.IsNullOrWhiteSpace(this.UserCreatedValue.Value))
                {
                    usernamecheck = false;
                    User user = User.FromName(domain.GetFullName(this.UserName.Text), true);
                    Assert.IsNotNull(user, typeof(User), "User not found", new object[0]);
                    UserProfile profile = user.Profile;
                    this.ShowEditDialog(user.Profile);
                    currentArgs.WaitForPostBack();
                }
                if(usernamecheck)
                {
                    SheerResponse.Alert(string.Format("Please check the username before Editing the profile"));
                    return;
                }
            }
            else if (currentArgs.HasResult)
            {
                if (currentArgs.Result == "no")
                {
                    return;
                }
                else if (currentArgs.HasResult && !currentArgs.Result.Equals("no") && !currentArgs.Result.Equals("yes"))
                {
                    User user = User.FromName(domain.GetFullName(this.UserName.Text), true);
                    Assert.IsNotNull(user, typeof(User), "User not found", new object[0]);
                    UserProfile profile = user.Profile;

                    FieldEditorOptions fieldEditorOption = FieldEditorOptions.Parse(currentArgs.Result);
                    Database database = Factory.GetDatabase("core");
                    foreach (FieldDescriptor field in fieldEditorOption.Fields)
                    {
                        Item item = database.GetItem(field.FieldID);
                        profile[item.Name] = field.Value;
                    }
                    user.Profile.Save();
                    this.RenderSettings(profile);
                    return;
                }
            }
        }

        /// <summary>
        /// Executes the ajax command.
        /// </summary>
        /// <param name="e">
        /// The <see cref="T:Sitecore.Web.UI.XamlSharp.Ajax.AjaxCommandEventArgs" /> instance containing the event data.
        /// </param>
        protected override void ExecuteAjaxCommand(AjaxCommandEventArgs e)
        {
            Assert.ArgumentNotNull(e, "e");
            if (e.Name == "item:save")
            {
                e.Handled = true;
                return;
            }
            base.ExecuteAjaxCommand(e);
        }

        /// <summary>
        /// Formats the date.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <param name="cultureInfo">
        /// The info.
        /// </param>
        /// <returns>
        /// The formated date.
        /// </returns>
        private static string FormatDate(DateTime date, CultureInfo cultureInfo)
        {
            Assert.ArgumentNotNull(cultureInfo, "cultureInfo");
            if (date.Year < 1900)
            {
                return Translate.Text("Never");
            }
            return DateUtil.FormatLongDateTime(DateUtil.ToServerTime(date), cultureInfo);
        }

        private static List<FieldDescriptor> GetAdditionalFields(UserProfile profile)
        {
            Template templateForProfile = CloneUserPage.GetTemplateForProfile(profile);
            List<FieldDescriptor> fieldDescriptors = new List<FieldDescriptor>();
            Item profileItem = CloneUserPage.GetProfileItem(profile);
            Database database = Factory.GetDatabase("core");
            foreach(Sitecore.Data.Templates.TemplateField templateField in
                from x in (IEnumerable<Sitecore.Data.Templates.TemplateField>)templateForProfile.GetFields(false)
                orderby x.Section.Sortorder, x.Sortorder
                select x)
            {
                if (database.GetItem(templateField.ID).Appearance.Hidden)
                {
                    continue;
                }
                fieldDescriptors.Add(new FieldDescriptor(profileItem.Uri, templateField.ID, profile[templateField.Name], profileItem.Fields[templateField.Name].ContainsStandardValue));
            }
            return fieldDescriptors;
        }

        /// <summary>
        /// Gets the control ID.
        /// </summary>
        /// <param name="field">
        /// The field.
        /// </param>
        /// <returns>
        /// The control ID.
        /// </returns>
        private static string GetControlID(Sitecore.Data.Templates.TemplateField field)
        {
            Assert.ArgumentNotNull(field, "field");
            return string.Concat("Field", field.ID.ToShortID());
        }

        /// <summary>
        /// Gets the profile id.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>
        /// The profile id.
        /// </returns>
        private static ID GetProfileId(UserProfile profile)
        {
            string profileItemId = profile.ProfileItemId;
            if (string.IsNullOrEmpty(profileItemId))
            {
                return Sitecore.Data.ID.Null;
            }
            return Sitecore.Data.ID.Parse(profileItemId);
        }

        /// <summary>
        /// Gets the profile item.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>
        /// The profile item.
        /// </returns>
        private static Item GetProfileItem(UserProfile profile)
        {
            ID profileId = CloneUserPage.GetProfileId(profile);
            if (Sitecore.Data.ID.Null == profileId)
            {
                return null;
            }
            Database database = Factory.GetDatabase("core");
            Assert.IsNotNull(database, "core");
            Item item = database.GetItem(profileId);
            if (item == null)
            {
                return null;
            }
            return item;
        }

        private static Template GetTemplateForProfile(UserProfile profile)
        {
            Template template;
            Assert.ArgumentNotNull(profile, "profile");
            Item profileItem = CloneUserPage.GetProfileItem(profile);
            if (profileItem == null)
            {
                template = null;
            }
            else
            {
                template = TemplateManager.GetTemplate(profileItem);
            }
            return template;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <returns>
        /// The user.
        /// </returns>
        private static User GetUser()
        {
            return User.FromName(WebUtil.GetQueryString("us", "sitecore\audrey"), true);
        }

        /// <summary>
        /// Because profileValue could be null or string.Empty and control's value could be only string.Empty then
        /// check profileValue != controlValue does not give us 100% guarantee that nothing was changed and
        /// we also should check !string.IsNullOrEmpty(profileValue) || !string.IsNullOrEmpty(controlValue)
        /// </summary>
        /// <param name="profileValue">Profile value to check.
        /// </param>
        /// <param name="controlValue">Control value to check.
        /// </param>
        /// <returns>
        /// The has changed.
        /// </returns>
        private static bool HasChanged(string profileValue, string controlValue)
        {
            if (!string.IsNullOrEmpty(controlValue))
            {
                return profileValue != controlValue;
            }
            return !string.IsNullOrEmpty(profileValue);
        }

        /// <summary>
        /// Localizes the tabs in tabstrip.
        /// </summary>
        private void Localize()
        {
            this.Add.Text = Translate.Text(this.Add.Text);
            this.ManagedDomains.Text = Translate.Text(this.ManagedDomains.Text);
            this.ChangeProfile.Text = Translate.Text(this.ChangeProfile.Text);
        }

        /// <summary>
        /// Handles the Manage domains click event.
        /// </summary>
        protected void ManagedDomains_Click()
        {
            ClientPipelineArgs currentArgs = ContinuationManager.Current.CurrentArgs as ClientPipelineArgs;
            Assert.IsNotNull(currentArgs, "args");
            if (!currentArgs.IsPostBack)
            {
                UrlString urlString = new UrlString("/sitecore/shell/~/xaml/Sitecore.Shell.Applications.Security.EditManagedDomains.aspx");
                UrlHandle urlHandle = new UrlHandle();
                urlHandle["manageddomains"] = this.ManagedDomainsValue.Value;
                urlHandle.Add(urlString);
                SheerResponse.ShowModalDialog(urlString.ToString(), "540", "630", string.Empty, true);
                currentArgs.WaitForPostBack();
            }
            else if (currentArgs.HasResult)
            {
                string result = currentArgs.Result;
                if (result == "-")
                {
                    result = string.Empty;
                }
                SheerResponse.SetAttribute(this.ManagedDomainsValue.ClientID, "value", result);
                return;
            }
        }

        /// <summary>
        /// Handles a click on the OK button.
        /// </summary>
        /// <remarks>
        /// When the user clicks OK, the dialog is closed by calling
        /// the <see cref="M:Sitecore.Web.UI.Sheer.ClientResponse.CloseWindow">CloseWindow</see> method.
        /// </remarks>
        protected override void OK_Click()
        {
            
            Domain domain = DomainManager.GetDomain(this.DomainName.SelectedValue);
            object[] domainName = new object[] { this.DomainName };
            Assert.IsNotNull(domain, "Domain \"{0}\" not found", domainName);
            
            try
            {
                if (!CreatingUser())
                {
                    return;
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                SheerResponse.Alert(string.Format("An error occurred while updating the user:\n\n{0}", exception.Message), new string[0]);
                return;
            }
            base.OK_Click();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load"></see> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="T:System.EventArgs"></see> object that contains the event data.
        /// </param>
        protected override void OnLoad(EventArgs e)
        {
            Assert.ArgumentNotNull(e, "e");
            base.OnLoad(e);
            if (XamlControl.AjaxScriptManager.IsEvent)
            {
                return;
            }
            this.Localize();
            User user = CloneUserPage.GetUser();
            //this.UserName.Text = HttpUtility.HtmlEncode(user.GetLocalName());
            this.UserName.Text = "";
            //this.DomainName.Text = user.GetDomainName();
            this.IsAdministrator.Disabled = !Sitecore.Context.IsAdministrator;
            this.ManagedDomainsValue.Value = user.Profile.ManagedDomainNames;
            if (!Sitecore.Context.IsAdministrator)
            {
                this.ManagedDomains.Visible = false;
            }
            this.RenderRoles(user);
            this.RenderClientLanguages();
            this.RenderRegionalIsoCodes();
            this.RenderContentLanguages();
            this.RenderDomainNames(user);
            this.RenderPortraits();
            UserProfile profile = user.Profile;
            this.IsAdministrator.Checked = profile.IsAdministrator;
            this.FullName.Text = profile.FullName;
            this.Description.Text = profile.Comment;
            this.Email.Text = profile.Email;
            this.ClientLanguage.SelectedValue = profile.ClientLanguage;
            this.RegionalISOCode.SelectedValue = profile.RegionalIsoCode;
            this.ContentLanguage.SelectedValue = profile.ContentLanguage;
            this.ProfileItemID.Value = profile.ProfileItemId;
            string str = ImageBuilder.ResizeImageSrc(profile.Portrait, 48, 48).Trim();
            Assert.IsNotNull(str, "portrait");
            if (str != string.Empty)
            {
                this.CurrentPortrait.Src = str;
            }
            this.PortraitImage.Src = (string.IsNullOrEmpty(str) ? "/sitecore/images/blank.gif" : str);
            str = ImageBuilder.ResizeImageSrc(str.ToLowerInvariant(), 16, 16);
            this.Portrait.Text = str;
            this.Portraits.SelectedValue = str;
            if (CloneUserPage.GetTemplateForProfile(profile) == null)
            {
                this.EditProfile.Enabled = false;
            }
            else
            {
                this.RenderProfileTemplate(profile);
            }
            string startUrl = profile.StartUrl;
            if (string.IsNullOrEmpty(startUrl))
            {
                this.StartDefault.Checked = true;
            }
            else if (startUrl == "/sitecore/shell/default.aspx")
            {
                this.StartDesktop.Checked = true;
            }
            else if (startUrl == "/sitecore/shell/applications/clientusesoswindows.aspx")
            {
                this.StartContentEditor.Checked = true;
            }
            else if (startUrl == "/sitecore/shell/applications/webedit.aspx")
            {
                this.StartWebEdit.Checked = true;
            }
            else if (startUrl != "/sitecore/shell/applications/preview.aspx")
            {
                this.StartCustom.Checked = true;
                this.StartUrl.Text = startUrl;
            }
            else
            {
                this.StartPreview.Checked = true;
            }
            MembershipUser membershipUser = Membership.GetUser(user.Name);
            if (membershipUser != null)
            {
                CultureInfo culture = User.Current.Profile.Culture;
                this.LastLoginDate.Text = CloneUserPage.FormatDate(membershipUser.LastLoginDate, culture);
                this.CreationDate.Text = CloneUserPage.FormatDate(membershipUser.CreationDate, culture);
                this.LastActivityDate.Text = CloneUserPage.FormatDate(membershipUser.LastActivityDate, culture);
                this.LastPasswordChangedDate.Text = CloneUserPage.FormatDate(membershipUser.LastPasswordChangedDate, culture);
                this.LastLockoutDate.Text = CloneUserPage.FormatDate(membershipUser.LastLockoutDate, culture);
            }
        }

        private void PlaceInFrame(string urlString)
        {
            this.PropertiesFrame.SourceUri = urlString;
        }

        /// <summary>
        /// Portraits_s the change.
        /// </summary>
        protected void Portrait_Change()
        {
            string text = this.Portrait.Text;
            SheerResponse.SetImageSrc("PortraitImage", Images.GetThemedImageSource(text, ImageDimension.id48x48), 48, 48, string.Empty, string.Empty);
        }

        /// <summary>
        /// Portraits_s the change.
        /// </summary>
        protected void Portraits_Change()
        {
            string selectedValue = this.Portraits.SelectedValue;
            SheerResponse.SetAttribute(this.Portrait.ClientID, "value", selectedValue);
            SheerResponse.SetImageSrc("PortraitImage", Images.GetThemedImageSource(selectedValue, ImageDimension.id48x48), 48, 48, string.Empty, string.Empty);
        }

        /// <summary>
        /// Reloads the properties.
        /// </summary>
        private void ReloadProperties()
        {
            this.PropertiesFrame.SourceUri = this.PropertiesFrame.SourceUri;
        }

        /// <summary>
        /// Builds the client languages.
        /// </summary>
        private void RenderClientLanguages()
        {
            Database database = Factory.GetDatabase("core");
            Assert.IsNotNull(database, "core");
            CloneUserPage.RenderLanguages(database, this.ClientLanguage);
        }

        /// <summary>
        /// Builds the client languages.
        /// </summary>
        private void RenderContentLanguages()
        {
            Database database = Factory.GetDatabase("master");
            Assert.IsNotNull(database, "master");
            CloneUserPage.RenderLanguages(database, this.ContentLanguage);
        }

        /// <summary>
        /// Builds the Domain.
        /// </summary>
        private void RenderDomainNames(User user)
        {
            string initialDomainName = CloneUserPage.GetInitialDomainName();
            IEnumerable<Domain> managedDomains = Sitecore.Context.User.Delegation.GetManagedDomains();
            int num = managedDomains.Count<Domain>();
            foreach (Domain managedDomain in managedDomains)
            {
                if (num > 1 && managedDomain.IsDefault)
                {
                    continue;
                }
                System.Web.UI.WebControls.ListItem listItem = new System.Web.UI.WebControls.ListItem(managedDomain.Appearance.DisplayName, managedDomain.Name)
                {
                    Selected = string.Compare(initialDomainName, managedDomain.Name, StringComparison.InvariantCultureIgnoreCase) == 0
                };
                DomainName.Items.Add(listItem);
            }
            this.DomainName.SelectedValue = user.GetDomainName();
        }

        /// <summary>
        /// Gets the initial name of the domain.
        /// </summary>
        /// <returns>
        /// Initial domain name.
        /// </returns>
        private static string GetInitialDomainName()
        {
            return WebUtil.GetQueryString("do");
        }

        /// <summary>
        /// Builds the languages.
        /// </summary>
        /// <param name="database">
        /// The database.
        /// </param>
        /// <param name="dropDownList">
        /// The drop down list.
        /// </param>
        private static void RenderLanguages(Database database, DropDownList dropDownList)
        {
            Assert.ArgumentNotNull(database, "database");
            Assert.ArgumentNotNull(dropDownList, "dropDownList");
            LanguageCollection languages = LanguageManager.GetLanguages(database);
            if (languages == null)
            {
                return;
            }
            System.Web.UI.WebControls.ListItem listItem = new System.Web.UI.WebControls.ListItem(Translate.Text("Default"));
            dropDownList.Items.Add(listItem);
            listItem.Value = string.Empty;
            foreach (Language language in languages)
            {
                listItem = new System.Web.UI.WebControls.ListItem(language.GetDisplayName());
                dropDownList.Items.Add(listItem);
                listItem.Value = language.Name;
            }
        }

        /// <summary>
        /// Builds the portraits.
        /// </summary>
        private void RenderPortraits()
        {
            foreach (XmlNode configNode in Factory.GetConfigNodes("portraits/collection"))
            {
                foreach (string str in new ListString(configNode.InnerText))
                {
                    string str1 = ImageBuilder.ResizeImageSrc(str, 16, 16).Trim();
                    System.Web.UI.WebControls.ListItem listItem = new System.Web.UI.WebControls.ListItem(str1, str1.ToLowerInvariant());
                    this.Portraits.Items.Add(listItem);
                }
            }
        }

        /// <summary>
        /// Renders the profile template.
        /// </summary>
        /// <param name="profile">
        /// The profile.
        /// </param>
        private void RenderProfileTemplate(UserProfile profile)
        {
            Assert.ArgumentNotNull(CloneUserPage.GetTemplateForProfile(profile), "template");
            this.RenderSettings(profile);
        }

        /// <summary>
        /// Builds the regional ISO codes.
        /// </summary>
        private void RenderRegionalIsoCodes()
        {
            ArrayList arrayLists = new ArrayList(Language.GetCultures(CultureTypes.SpecificCultures));
            //arrayLists.Sort(new CultureComparer());
            System.Web.UI.WebControls.ListItem listItem = new System.Web.UI.WebControls.ListItem(Translate.Text("Default"));
            this.RegionalISOCode.Items.Add(listItem);
            listItem.Value = string.Empty;
            foreach (CultureInfo arrayList in arrayLists)
            {
                listItem = new System.Web.UI.WebControls.ListItem(Language.GetDisplayName(arrayList));
                this.RegionalISOCode.Items.Add(listItem);
                listItem.Value = arrayList.Name;
            }
        }

        /// <summary>
        /// Renders the roles.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        private void RenderRoles(User user)
        {
            Assert.ArgumentNotNull(user, "user");
            List<string> list = (
                from role in user.Roles
                select role.Name).ToList<string>();
            this.RolesValue.Value = string.Join("|", list.ToArray());
            foreach (string str in list)
            {
                this.Roles.Items.Add(str);
            }
            if (!RolesInRolesManager.RolesInRolesSupported)
            {
                return;
            }
            foreach (Role rolesForUser in RolesInRolesManager.GetRolesForUser(user, true))
            {
                if (list.Contains(rolesForUser.Name))
                {
                    continue;
                }
                this.Roles.Items.Add(string.Concat("<", rolesForUser.Name, ">"));
                list.Add(rolesForUser.Name);
            }
        }

        /// <summary>
        /// Renders the section.
        /// </summary>
        /// <param name="profile">
        ///   The profile.
        /// </param>
        private void RenderSettings(UserProfile profile)
        {
            OneColumnPreviewOptions oneColumnPreviewOption = new OneColumnPreviewOptions(CloneUserPage.GetAdditionalFields(profile))
            {
                PreserveSections = true,
                SaveItem = false,
                IsInFrame = true
            };
            UrlString urlString = oneColumnPreviewOption.ToUrlString();
            urlString.Add("sc_content", "core");
            this.PlaceInFrame(urlString.GetUrl());
        }

        /// <summary>
        /// Saves the custom properties.
        /// </summary>
        /// <param name="profile">
        /// The profile.
        /// </param>
        /// <param name="changed">
        /// Indicates wherever UserProfile was changed.
        /// </param>
        private void SaveCustomProperties(UserProfile profile, ref bool changed)
        {
            Assert.ArgumentNotNull(profile, "profile");
            Item profileItem = CloneUserPage.GetProfileItem(profile);
            if (profileItem == null)
            {
                return;
            }
            Database database = Factory.GetDatabase("core");
            Template template = TemplateManager.GetTemplate(profileItem.TemplateID, database);
            if (template == null)
            {
                return;
            }
            HttpContext current = HttpContext.Current;
            if (current == null)
            {
                return;
            }
            NameValueCollection form = current.Request.Form;
            ListString listStrings = new ListString(StringUtil.GetString(this.ViewState["profile_checkboxes"]));
            foreach (string key in form.Keys)
            {
                if (string.IsNullOrEmpty(key))
                {
                    continue;
                }
                int num = key.IndexOf("$Field", StringComparison.InvariantCulture);
                if (num < 0)
                {
                    continue;
                }
                ID d = ShortID.DecodeID(StringUtil.Mid(key, num + 6));
                Sitecore.Data.Templates.TemplateField field = template.GetField(d);
                if (field == null)
                {
                    continue;
                }
                string name = field.Name;
                if (string.IsNullOrEmpty(name))
                {
                    continue;
                }
                if (listStrings.Contains(name))
                {
                    listStrings.Remove(name);
                }
                string item = form[key];
                if (item == "on")
                {
                    item = "1";
                }
                if (!CloneUserPage.HasChanged(profile[name], item))
                {
                    continue;
                }
                profile[name] = item;
                changed = true;
            }
            foreach (string listString in listStrings)
            {
                if (string.IsNullOrEmpty(listString) || !(profile[listString] != string.Empty))
                {
                    continue;
                }
                profile[listString] = string.Empty;
                changed = true;
            }
        }

        private void ShowEditDialog(UserProfile profile)
        {
            FieldEditorOptions fieldEditorOption = new FieldEditorOptions(CloneUserPage.GetAdditionalFields(profile))
            {
                PreserveSections = true,
                SaveItem = false,
                IsInFrame = true
            };
            UrlString urlString = fieldEditorOption.ToUrlString();
            urlString["sc_content"] = "core";
            urlString["us"] = WebUtil.GetQueryString("us");
            ModalDialogOptions modalDialogOption = new ModalDialogOptions(urlString.ToString())
            {
                Response = true,
                Width = "800px"
            };
            SheerResponse.ShowModalDialog(modalDialogOption);
        }

        /// <summary>
        /// Gets the command context.
        /// </summary>
        /// <returns>
        /// The command context.
        /// </returns>
        CommandContext Sitecore.Shell.Framework.Commands.IHasCommandContext.GetCommandContext()
        {
            return new CommandContext();
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns>
        /// The validate.
        /// </returns>
        private bool Validate()
        {
            Regex regex = new Regex("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");
            if (string.IsNullOrEmpty(this.Email.Text) || regex.IsMatch(this.Email.Text))
            {
                return true;
            }
            SheerResponse.Alert("The e-mail address is invalid.", new string[0]);
            return false;
        }

        /// <summary>
        /// Validates the ticket.
        /// </summary>
        /// <returns>The validate ticket.</returns>
        private bool ValidateTicket()
        {
            if (!TicketManager.IsTicketValid(TicketManager.GetCurrentTicketId()))
            {
                return false;
            }
            return true;
        }

    }
}