using InternalModding.Mapper;
using UnityEngine;

namespace Modding.Mapper
{
	public class SelectorMaterials
	{
		private readonly CustomSelectorReferences references;

		public Material RedHighlight
		{
			get
			{
				return references.RedHighlight;
			}
		}

		public Material LightBackground
		{
			get
			{
				return references.LightBackground;
			}
		}

		public Material DarkBackground
		{
			get
			{
				return references.DarkBackground;
			}
		}

		public Material DarkElement
		{
			get
			{
				return references.DarkElement;
			}
		}

		internal SelectorMaterials(CustomSelectorReferences refs)
		{
			references = refs;
		}
	}
}
