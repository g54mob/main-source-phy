using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Modding.Serialization
{
	[Serializable]
	public class MSliderDefinition : MapperTypeDefinition
	{
		[XmlAttribute("max")]
		public float Max;

		[XmlAttribute("min")]
		public float Min;

		[XmlAttribute("default")]
		public float Default;

		[DefaultValue(false)]
		[XmlAttribute("unclamped")]
		public bool Unclamped;

		public override MapperType Create(SaveableDataHolder holder)
		{
			MSlider mSlider = ((!Unclamped) ? holder.AddSlider(DisplayName, Key, Default, Min, Max, string.Empty) : holder.AddSliderUnclamped(DisplayName, Key, Default, Min, Max, string.Empty));
			mSlider.DisplayInMapper = ShowInMapper;
			return mSlider;
		}
	}
}
