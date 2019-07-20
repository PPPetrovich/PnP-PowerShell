﻿#if !ONPREMISES
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using SharePointPnP.PowerShell.CmdletHelpAttributes;
using SharePointPnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Remove, "PnPOrgAsset")]
    [CmdletHelp("Removes a given document library as a organizational asset source",
        DetailedDescription = @"Removes a given document library as a organizational asset source based on its server relative URL in your Sharepoint Online Tenant. It will not remove the document library itself. It may take some time before this change will be reflected in the webinterface.",
        SupportedPlatform = CmdletSupportedPlatform.Online,
        Category = CmdletHelpCategory.TenantAdmin)]
    [CmdletExample(
        Code = @"PS:> Remove-PnPOrgAsset -DocumentLibraryUrl ""sites/branding/logos""",
        Remarks = @"This example removes the document library with the url ""logos"" residing in the sitecollection with the url ""sites/branding/logos"" from the list with organizational assets keeping it as an Office 365 CDN source", SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> Remove-PnPOrgAsset -DocumentLibraryUrl ""sites/branding/logos"" -RemoveAsCdnSource $true",
        Remarks = @"This example removes the document library with the url ""logos"" residing in the sitecollection with the url ""sites/branding/logos"" from the list with organizational assets also removing it as a Public Office 365 CDN source", SortOrder = 2)]
    [CmdletExample(
        Code = @"PS:> Remove-PnPOrgAsset -DocumentLibraryUrl ""sites/branding/logos"" -RemoveAsCdnSource $true -CdnType Private",
        Remarks = @"This example removes the document library with the url ""logos"" residing in the sitecollection with the url ""sites/branding/logos"" from the list with organizational assets also removing it as a Private Office 365 CDN source", SortOrder = 3)]
    public class RemoveOrgAsset : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = @"The server relative url of the document library flagged as organizational asset which you want to remove, i.e. ""sites/branding/logos""")]
        public string DocumentLibraryUrl;

        [Parameter(Mandatory = false, HelpMessage = @"Boolean indicating if the document library that will no longer be flagged as an organizational asset also needs to be removed as an Office 365 CDN source")]
        public bool RemoveAsCdnSource = false;

        [Parameter(Mandatory = false, HelpMessage = @"Indicates what type of Cdn source the document library that will no longer be flagged as an organizational asset was of")]
        public SPOTenantCdnType CdnType = SPOTenantCdnType.Public;

        protected override void ExecuteCmdlet()
        {
            Tenant.RemoveFromOrgAssetsAndCdn(RemoveAsCdnSource, CdnType, DocumentLibraryUrl);
            ClientContext.ExecuteQueryRetry();
        }
    }
}
#endif