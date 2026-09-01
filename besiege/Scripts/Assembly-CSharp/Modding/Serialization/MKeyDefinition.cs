using System;
using System.Xml.Serialization;
using UnityEngine;

namespace Modding.Serialization
{
	[Serializable]
	public class MKeyDefinition : MapperTypeDefinition
	{
		[XmlAttribute("default")]
		public KeyCode Default;

		public override MapperType Create(SaveableDataHolder holder)
		{
			MKey mKey = holder.AddKey(DisplayName, Key, Default);
			mKey.DisplayInMapper = ShowInMapper;
			return mKey;
		}
	}
}
