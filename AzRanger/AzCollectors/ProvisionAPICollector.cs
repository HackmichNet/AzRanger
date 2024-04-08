using AzRanger.Models;
using AzRanger.Models.Provision;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AzRanger.AzScanner
{
	class ProvisionAPICollector : AbstractCollector
	{
		private const String Endpoint = "/provisioningwebservice.svc";

        public ProvisionAPICollector(IAuthenticator authenticator, String tenantId, String proxy)
        {
            this.Authenticator = authenticator;
            this.TenantId = tenantId;
			this.BaseAddress = "https://provisioningapi.microsoftonline.com";
            this.Scope = new String[] { "https://graph.windows.net/.default", "offline_access" };
            this.client = Helper.GetDefaultClient(additionalHeaders, proxy);
        }

        public async Task<DirSyncFeatures> GetDirSyncFeatures()
        {
			GetCompanyDirSyncFeaturesResponse response = null;
            try
            {
				String responseString = await PostToProvisioninApi("GetCompanyDirSyncFeatures", @"<b:ReturnValue i:nil=""true""/>");
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(responseString);
				var nsmgr = new XmlNamespaceManager(doc.NameTable);
				nsmgr.AddNamespace("s", "http://www.w3.org/2003/05/soap-envelope");
				nsmgr.AddNamespace("a", "http://www.w3.org/2005/08/addressing");
				XmlNode node = doc.DocumentElement.SelectSingleNode("/s:Envelope/s:Body", nsmgr);
				string text = node.InnerXml;

				var serializer = new XmlSerializer(typeof(GetCompanyDirSyncFeaturesResponse));
				StringReader stringReader = new StringReader(text);
				response = (GetCompanyDirSyncFeaturesResponse)serializer.Deserialize(stringReader);
				DirSyncFeatures features = new DirSyncFeatures();
				foreach(DirSyncFeatureDetails feature in response.GetCompanyDirSyncFeaturesResult.ReturnValue)
                {
					if(feature.DirSyncFeature == "BlockSoftMatch")
                    {
						features.BlockSoftMatch = feature.Enabled;
                    }
                }
				return features;
			}
            catch
            {
				logger.Debug("ProvisionApiScanner.GetDirSyncFeatures: Failed");
				return null;
            }
        }

		public async Task<MsolCompanyInformation> GetMsolCompanyInformation()
        {
			GetCompanyInformationResponse response = null;
			try
			{
				response = await this.GetCompanyInformationResponse();
            }
            catch { }
			if(response != null)
            {
				MsolCompanyInformation infos = new MsolCompanyInformation();
				infos.UsersPermissionToCreateGroupsEnabled = response.GetCompanyInformationResult.ReturnValue.UsersPermissionToCreateGroupsEnabled;
				infos.UsersPermissionToReadOtherUsersEnabled = response.GetCompanyInformationResult.ReturnValue.UsersPermissionToReadOtherUsersEnabled;
				infos.AllowEmailVerifiedUsers = response.GetCompanyInformationResult.ReturnValue.AllowEmailVerifiedUsers;
				infos.AllowAdHocSubscriptions = response.GetCompanyInformationResult.ReturnValue.AllowAdHocSubscriptions;

				return infos;
            }
            else
            {
				String response2 = await PostToProvisioninApi("GetCompanyInformation", @"<b:ReturnValue i:nil=""true""/>");
				bool UsersPermissionToCreateGroupsEnabled = false;
				bool UsersPermissionToReadOtherUsersEnabled = false;
				bool AllowAdHocSubscriptions = false;
				bool AllowEmailVerifiedUsers = false;

				bool foundUsersPermissionToCreateGroupsEnabled = false;
				bool foundUsersPermissionToReadOtherUsersEnabled = false;
				bool foundAllowAdHocSubscriptions = false;
				bool foundAllowEmailVerifiedUsers = false;

				XmlDocument doc = new XmlDocument();
				doc.LoadXml(response2);

				XmlNodeList nodeUsersPermissionToCreateGroupsEnabledList = doc.GetElementsByTagName("c:UsersPermissionToCreateGroupsEnabled");
				
				if(nodeUsersPermissionToCreateGroupsEnabledList.Count == 1)
                {
					if(nodeUsersPermissionToCreateGroupsEnabledList[0].InnerText != null)
                    {
						foundUsersPermissionToCreateGroupsEnabled = true;
						if (nodeUsersPermissionToCreateGroupsEnabledList[0].InnerText == "true")
                        {
							UsersPermissionToCreateGroupsEnabled = true;
                        }
						if(nodeUsersPermissionToCreateGroupsEnabledList[0].InnerText == "false")
                        {
							UsersPermissionToReadOtherUsersEnabled = false;
                        }
                    }
                }

				XmlNodeList nodeUsersPermissionToReadOtherUsersEnabledList = doc.GetElementsByTagName("c:UsersPermissionToReadOtherUsersEnabled");
				if (nodeUsersPermissionToReadOtherUsersEnabledList.Count == 1)
				{
					if (nodeUsersPermissionToReadOtherUsersEnabledList[0].InnerText != null)
					{
						foundUsersPermissionToReadOtherUsersEnabled = true;
						if (nodeUsersPermissionToReadOtherUsersEnabledList[0].InnerText == "true")
						{
							UsersPermissionToReadOtherUsersEnabled = true;
						}
						if (nodeUsersPermissionToReadOtherUsersEnabledList[0].InnerText == "false")
						{
							UsersPermissionToReadOtherUsersEnabled = false;
						}
					}
				}

				XmlNodeList nodefoundAllowAdHocSubscriptionsList = doc.GetElementsByTagName("c:AllowAdHocSubscriptions");
				if (nodefoundAllowAdHocSubscriptionsList.Count == 1)
				{
					if (nodefoundAllowAdHocSubscriptionsList[0].InnerText != null)
					{
						foundAllowAdHocSubscriptions = true;
						if (nodefoundAllowAdHocSubscriptionsList[0].InnerText == "true")
						{
							AllowAdHocSubscriptions = true;
						}
						if (nodefoundAllowAdHocSubscriptionsList[0].InnerText == "false")
						{
							AllowAdHocSubscriptions = false;
						}
					}
				}

				XmlNodeList nodeAllowEmailVerifiedUsersList = doc.GetElementsByTagName("c:AllowEmailVerifiedUsers");
				if (nodeAllowEmailVerifiedUsersList.Count == 1)
				{
					if (nodeAllowEmailVerifiedUsersList[0].InnerText != null)
					{
						foundAllowEmailVerifiedUsers = true;
						if (nodeAllowEmailVerifiedUsersList[0].InnerText == "true")
						{
							AllowEmailVerifiedUsers = true;
						}
						if (nodeAllowEmailVerifiedUsersList[0].InnerText == "false")
						{
							AllowEmailVerifiedUsers = false;
						}
					}
				}

				if (foundUsersPermissionToCreateGroupsEnabled && foundUsersPermissionToReadOtherUsersEnabled && foundAllowAdHocSubscriptions && foundAllowEmailVerifiedUsers)
				{
					MsolCompanyInformation infos = new MsolCompanyInformation();
					infos.UsersPermissionToCreateGroupsEnabled = UsersPermissionToCreateGroupsEnabled;
					infos.UsersPermissionToReadOtherUsersEnabled = UsersPermissionToReadOtherUsersEnabled;
					infos.AllowAdHocSubscriptions = AllowAdHocSubscriptions;
					infos.AllowEmailVerifiedUsers = AllowEmailVerifiedUsers;
					return infos;
                }
                else
                {
					return null;
                }
			}
		}
			
        
		private async Task<String> PostToProvisioninApi(string command, string requestElement)
		{
            String accessToken = await this.Authenticator.GetAccessToken(this.Scope);
            if (accessToken == null)
            {
                return null;
            }
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            logger.Debug("ProvisionApiScanner.PostToProvisionApi: {0}|{1}", command, requestElement);
			string content = CreateEnvelop(this.client.DefaultRequestHeaders.Authorization.ToString().Split(' ')[1], command, requestElement);
			StringContent message = new StringContent(content.Replace("\\n", "").Replace("\\t", ""), Encoding.UTF8, "application/soap+xml");
			String url = BaseAddress + Endpoint;
			var response = await client.PostAsync(url, message);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadAsStringAsync();
				return result;
			}
			else
			{
				try
				{
					logger.Debug("ProvisionApiScanner.PostToProvisionApi: was not successful");
					logger.Debug("ProvisionApiScanner.PostToProvisionApi: Status Code {0}", response.StatusCode);
					logger.Debug(await response.Content.ReadAsStringAsync());
                }
                catch { }
			}
			return null;
		}

		private async Task<GetCompanyInformationResponse> GetCompanyInformationResponse()
        {
			String response = await PostToProvisioninApi("GetCompanyInformation", @"<b:ReturnValue i:nil=""true""/>");
			try
			{

				XmlDocument doc = new XmlDocument();
				doc.LoadXml(response);
				var nsmgr = new XmlNamespaceManager(doc.NameTable);
				nsmgr.AddNamespace("s", "http://www.w3.org/2003/05/soap-envelope");
				nsmgr.AddNamespace("a", "http://www.w3.org/2005/08/addressing");
				XmlNode node = doc.DocumentElement.SelectSingleNode("/s:Envelope/s:Body", nsmgr);
				string text = node.InnerXml;

				var serializer = new XmlSerializer(typeof(GetCompanyInformationResponse));
				StringReader stringReader = new StringReader(text);
				return (GetCompanyInformationResponse)serializer.Deserialize(stringReader);
			}
			catch (Exception e)
			{
				logger.Debug("GetCompanyInformationResponse.GetCompanyInformationResponse: Deserialization failed.");
				logger.Debug(e.Message);
				logger.Debug(response);
				return null;
			}
		}

		public async Task<SharePointInformation> GetSharepointInformation()
		{
            String accessToken = await this.Authenticator.GetAccessToken(this.Scope);
            if (accessToken == null)
            {
                return null;
            }
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            logger.Debug("ProvisionApiScanner.GetSharepointInformation: Enter function");
			GetCompanyInformationResponse getCompanyInformationResponse = await GetCompanyInformationResponse();
			string SharePointAdminUrl = null;
			string SharePointUrl = null;

			if (getCompanyInformationResponse != null)
			{
				try
				{
					foreach (var serviceInformation in getCompanyInformationResponse.GetCompanyInformationResult.ReturnValue.ServiceInformation)
					{
						for (int i = 0; i < serviceInformation.ServiceElements.XElement.ServiceExtension.ServiceParameters.ServiceParameter.Length - 1; i++)
						{
							if (serviceInformation.ServiceElements.XElement.ServiceExtension.ServiceParameters.ServiceParameter[i].Name == "RootAdminUrl")
							{
								SharePointAdminUrl = serviceInformation.ServiceElements.XElement.ServiceExtension.ServiceParameters.ServiceParameter[i].Value;
								if (SharePointAdminUrl.EndsWith("/"))
								{
									SharePointAdminUrl = SharePointAdminUrl.Remove(SharePointAdminUrl.Length - 1);
								}
							}
							if (serviceInformation.ServiceElements.XElement.ServiceExtension.ServiceParameters.ServiceParameter[i].Name == "RootIWSPOUrl")
							{
								SharePointUrl = serviceInformation.ServiceElements.XElement.ServiceExtension.ServiceParameters.ServiceParameter[i].Value;
								if (SharePointUrl.EndsWith("/"))
								{
									SharePointUrl = SharePointUrl.Remove(SharePointUrl.Length - 1);
								}
							}
							if (SharePointAdminUrl != null & SharePointUrl != null)
							{
								return new SharePointInformation(SharePointAdminUrl, SharePointUrl);
							}
						}
					}
                }
                catch { }
			}

			String response = await PostToProvisioninApi("GetCompanyInformation", @"<b:ReturnValue i:nil=""true""/>");
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(response);
			XmlNodeList nodeList = doc.GetElementsByTagName("ServiceParameter");
			foreach (XmlNode n in nodeList)
			{
				if (n.ChildNodes.Count == 2)
				{
					XmlNode curentFirstChild = n.FirstChild;
					if (curentFirstChild.Name == "Name")
					{
						if (curentFirstChild.InnerText == "RootAdminUrl")
						{
							XmlNode currentValue = n.LastChild;
							SharePointAdminUrl = currentValue.InnerText;
						}
						if (curentFirstChild.InnerText == "SPO_RootSiteUrl")
						{
							XmlNode currentValue = n.LastChild;
							SharePointUrl = currentValue.InnerText;
						}
					}
				}
				if (SharePointAdminUrl != null && SharePointUrl != null)
				{
					return new SharePointInformation(SharePointAdminUrl, SharePointUrl);
				}
			}
			
			logger.Debug("ProvisionApiScanner.GetSharepointInformation: Sharepoint Url finally not found...");
			return null;
		}

		private string CreateEnvelop(string accesstoken, string command, string requestElement)
		{
			Guid messageID = Guid.NewGuid();
			String envelop = @"
	<s:Envelope xmlns:s=""http://www.w3.org/2003/05/soap-envelope"" xmlns:a=""http://www.w3.org/2005/08/addressing"">
	<s:Header>
		<a:Action s:mustUnderstand=""1"">http://provisioning.microsoftonline.com/IProvisioningWebService/{2}</a:Action>
		<a:MessageID>urn:uuid:{0}</a:MessageID>
		<a:ReplyTo>
			  <a:Address>http://www.w3.org/2005/08/addressing/anonymous</a:Address>
		</a:ReplyTo>
			<UserIdentityHeader xmlns=""http://provisioning.microsoftonline.com/"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"">	 
				<BearerToken xmlns=""http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService"">Bearer {1}</BearerToken>
				<LiveToken i:nil=""true"" xmlns=""http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService""/>
			</UserIdentityHeader>
			<ClientVersionHeader xmlns=""http://provisioning.microsoftonline.com/"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"">
				<ClientId xmlns=""http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService"">50afce61-c917-435b-8c6d-60aa5a8b8aa7</ClientId>
				<Version xmlns=""http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService"">1.2.183.17</Version>
			</ClientVersionHeader>
			<ContractVersionHeader xmlns=""http://becwebservice.microsoftonline.com/"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"">
				<BecVersion xmlns=""http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService"">Version47</BecVersion>
			</ContractVersionHeader>
			<a:To s:mustUnderstand=""1"">https://provisioningapi.microsoftonline.com/provisioningwebservice.svc</a:To>
	</s:Header>
	<s:Body>
		<{2} xmlns=""http://provisioning.microsoftonline.com/"">
		<request xmlns:b=""http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"">
			<b:BecVersion>Version16</b:BecVersion>
			<b:TenantId i:nil=""true""/>
			{3}
		</request>
		</{2}>
	</s:Body>
	</s:Envelope>
";

			return String.Format(envelop, messageID, accesstoken, command, requestElement);
		}
	}
}
