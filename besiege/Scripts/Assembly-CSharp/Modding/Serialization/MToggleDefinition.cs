using System;
using System.Xml.Serialization;

namespace Modding.Serialization
{
	[Serializable]
	public class MToggleDefinition : MapperTypeDefinition
	{
		[XmlAttribute("default")]
		public bool Default;

		public override MapperType Create(SaveableDataHolder holder)
		{
			MToggle mToggle = holder.AddToggle(DisplayName, Key, Default);
			mToggle.DisplayInMapper = ShowInMapper;
			return mToggle;
		}
	}
}
