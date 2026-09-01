using UnityEngine;
using UnityEngine.Internal;

namespace HighlightingSystem
{
	[ExcludeFromDocs]
	public static class MaterialExtensions
	{
		public static void SetKeyword(this Material material, string keyword, bool state)
		{
			if (state)
			{
				material.EnableKeyword(keyword);
			}
			else
			{
				material.DisableKeyword(keyword);
			}
		}
	}
}
