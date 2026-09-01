using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

namespace Modding.Serialization
{
	public class MColourSliderDefinition : MapperTypeDefinition
	{
		[XmlAttribute("r")]
		public float R;

		[XmlAttribute("g")]
		public float G;

		[XmlAttribute("b")]
		public float B;

		[XmlAttribute("a")]
		[DefaultValue(1f)]
		public float A = 1f;

		[XmlAttribute("snap")]
		public bool SnapColors;

		protected override bool Validate(string elementName)
		{
			if (!base.Validate(elementName))
			{
				return false;
			}
			if (R < 0f || R > 1f)
			{
				return InvalidData(elementName, "Colour values must be between 0 and 1!");
			}
			if (G < 0f || G > 1f)
			{
				return InvalidData(elementName, "Colour values must be between 0 and 1!");
			}
			if (B < 0f || B > 1f)
			{
				return InvalidData(elementName, "Colour values must be between 0 and 1!");
			}
			if (A < 0f || A > 1f)
			{
				return InvalidData(elementName, "Colour values must be between 0 and 1!");
			}
			return true;
		}

		public override MapperType Create(SaveableDataHolder holder)
		{
			MColourSlider mColourSlider = holder.AddColourSlider(defaultValue: new Color(R, G, B, A), displayName: DisplayName, key: Key, snapColors: SnapColors);
			mColourSlider.DisplayInMapper = ShowInMapper;
			return mColourSlider;
		}
	}
}
