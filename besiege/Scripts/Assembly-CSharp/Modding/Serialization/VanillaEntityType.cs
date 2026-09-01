using System;
using System.Linq;
using System.Xml.Serialization;
using InternalModding.Loading;

namespace Modding.Serialization
{
	[Serializable]
	public class VanillaEntityType : Element
	{
		[XmlText]
		public string Text;

		private int _type = -1;

		public int Get()
		{
			if (_type != -1)
			{
				return _type;
			}
			if (int.TryParse(Text, out _type))
			{
				if (PrefabMaster.LevelPrefabs[10].ContainsKey(_type) && ModIds.GetEntityByEffectiveId(_type) == null)
				{
					return _type;
				}
				_type = -1;
				return _type;
			}
			LevelPrefab levelPrefab = PrefabMaster.LevelPrefabs[10].Values.FirstOrDefault((LevelPrefab p) => p.name.Equals(Text, StringComparison.InvariantCultureIgnoreCase));
			if (levelPrefab != null)
			{
				_type = levelPrefab.ID;
			}
			else
			{
				_type = -1;
			}
			return _type;
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
				return InvalidData(elementName, Text + " is not a valid entity type!");
			}
			return true;
		}
	}
}
