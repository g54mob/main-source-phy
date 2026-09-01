using System.Net;
using System.Xml;

namespace Open.Nat
{
	internal class GetExternalIPAddressResponseMessage : ResponseMessageBase
	{
		public IPAddress ExternalIPAddress { get; private set; }

		public GetExternalIPAddressResponseMessage(XmlDocument response, string serviceType)
			: base(response, serviceType, "GetExternalIPAddressResponseMessage")
		{
			string xmlElementText = GetNode().GetXmlElementText("NewExternalIPAddress");
			ExternalIPAddress = IPAddress.Parse(xmlElementText);
		}
	}
}
