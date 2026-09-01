using System;
using System.Xml.Serialization;

namespace Modding.Serialization
{
	[Serializable]
	public class ResourceReference : Element
	{
		[XmlAttribute("name")]
		public string Name;

		protected override bool Validate(string elementName)
		{
			if (!base.Validate(elementName))
			{
				return false;
			}
			if (string.IsNullOrEmpty(Name))
			{
				return InvalidData(elementName, "name attribute must not be empty!");
			}
			return true;
		}
	}
}
