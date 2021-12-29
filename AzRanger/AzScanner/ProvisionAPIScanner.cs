using AzRanger.Models;
using AzRanger.Models.Provision;
using System;
using System.Collections;
using System.Net.Http;
using System.Text;
using System.Xml;

namespace AzRanger.AzScanner
{
	class ProvisionAPIScanner : IScanner
	{
		public const String Endpoint = "/provisioningwebservice.svc";

		public ProvisionAPIScanner(Scanner scanner)
		{
			this.Scanner = scanner;
			this.BaseAdresse = "https://provisioningapi.microsoftonline.com";
			this.Scope = new String[] { "https://graph.windows.net/.default", "offline_access" };
		}

		public SharepointInformation GetSharepointInformation()
		{
			return (SharepointInformation)PostToProvisioninApi<GetCompanyInformationResponse>("GetCompanyInformation", @"<b:ReturnValue i:nil=""true""/>");
		}

		internal object PostToProvisioninApi<T>(string command, string requestElement)
		{
			String accessToken = this.Scanner.Authenticator.GetAccessToken(this.Scope);
			if(accessToken == null)
            {
				logger.Warn("ProvisionApiScanner.PostToProvisionApi: Failed to get access token");
				return null;
            }
			string content = CreateEnvelop(accessToken, command, requestElement);
			logger.Debug("ProvisionApiScanner.PostToProvisionApi: {0}|{1}", command, requestElement);
			using (var client = Helper.GetDefaultClient(BaseAdresse, false,null, this.Scanner.Proxy))
			using (var message = new HttpRequestMessage(HttpMethod.Post, Endpoint))
			{
				message.Content = new StringContent(content.Replace("\\n", "").Replace("\\t", ""), Encoding.UTF8, "application/soap+xml");

				var response = client.SendAsync(message).Result;
				if (response.IsSuccessStatusCode)
				{
					ArrayList r = new ArrayList();
					var result = response.Content.ReadAsStringAsync().Result;
					XmlDocument doc = new XmlDocument();
					doc.LoadXml(result);
					try
					{
						string SharePointAdminUrl = null;
						string SharePointUrl = null;
						XmlNodeList nodeList = doc.GetElementsByTagName("ServiceParameter");
						foreach (XmlNode n in nodeList)
						{
							if (n.ChildNodes.Count == 2) { 
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
						}
						return new SharepointInformation(SharePointAdminUrl, SharePointUrl);
						// return (T)serializer.Deserialize(stringReader);
					}catch(Exception e)
                    {
						logger.Debug("ProvisionApiScanner.PostToProvisionApi: Deserialization failed.");
						logger.Debug(e.Message);
						logger.Debug(result);
						return null;
					}
				}
				else
				{
					logger.Debug("ProvisionApiScanner.PostToProvisionApi: {0} was not successfull", typeof(T).ToString());
					logger.Debug("ProvisionApiScanner.PostToProvisionApi: Status Code {0}", response.StatusCode);
					logger.Debug(response.Content.ReadAsStringAsync().Result);
				}
			}
			return null;
		}

		internal SharepointInformation GetSharepointInfos(GetCompanyInformationResponse response)
		{
			foreach (var serviceInformation in response.GetCompanyInformationResult.ReturnValue.ServiceInformation)
			{
				string SharePointAdminUrl = null;
				string SharePointUrl = null;
				for (int i = 0; i < serviceInformation.ServiceElements.XElement.ServiceExtension.ServiceParameters.ServiceParameter.Length; i++)
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
						return new SharepointInformation(SharePointAdminUrl, SharePointUrl);
					}
				}
			}
			logger.Debug("Sharepoint Url not found...");
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
