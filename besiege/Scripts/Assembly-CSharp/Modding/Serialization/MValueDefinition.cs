using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Modding.Serialization
{
	[Serializable]
	public class MValueDefinition : MapperTypeDefinition
	{
		[XmlAttribute("default")]
		public float Default;

		[XmlAttribute("min")]
		[DefaultValue(0f)]
		public float Min;

		[XmlIgnore]
		public bool MinSpecified;

		[DefaultValue(0f)]
		[XmlAttribute("max")]
		public float Max;

		[XmlIgnore]
		public bool MaxSpecified;

		protected override bool Validate(string elementName)
		{
			if (!base.Validate(elementName))
			{
				return false;
			}
			if (MinSpecified && !MaxSpecified)
			{
				return MissingAttribute(elementName, "max");
			}
			if (MaxSpecified && !MinSpecified)
			{
				return MissingAttribute(elementName, "min");
			}
			return true;
		}

		public override MapperType Create(SaveableDataHolder holder)
		{
			MValue mValue = ((!MinSpecified) ? holder.AddValue(DisplayName, Key, Default) : holder.AddValue(DisplayName, Key, Default, Min, Max));
			mValue.DisplayInMapper = ShowInMapper;
			return mValue;
		}
	}
}
