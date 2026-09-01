using System;
using System.Xml.Serialization;

namespace Modding.Serialization
{
	[Serializable]
	public class VanillaBlockType : Element
	{
		[XmlText]
		public string Text;

		private int _type = -1;

		public BlockType Get()
		{
			if (_type != -1)
			{
				return (BlockType)_type;
			}
			if (int.TryParse(Text, out _type))
			{
				if (_type < Enum.GetValues(typeof(BlockType)).Length)
				{
					return (BlockType)_type;
				}
				_type = -1;
				return (BlockType)_type;
			}
			try
			{
				_type = (int)Enum.Parse(typeof(BlockType), Text, true);
				return (BlockType)_type;
			}
			catch (ArgumentException)
			{
				_type = -1;
				return (BlockType)_type;
			}
		}

		protected override bool Validate(string elementName)
		{
			if (!base.Validate(elementName))
			{
				return false;
			}
			if (string.IsNullOrEmpty(Text))
			{
				return InvalidData(elementName, "Cannot be empty!");
			}
			Get();
			if (_type == -1)
			{
				return InvalidData(elementName, Text + " is not a valid block type!");
			}
			return true;
		}
	}
}
