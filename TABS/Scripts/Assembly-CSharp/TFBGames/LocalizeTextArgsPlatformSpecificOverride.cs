using System;
using UnityEngine;

namespace TFBGames
{
	[RequireComponent(typeof(LocalizeText))]
	public class LocalizeTextArgsPlatformSpecificOverride : PlatformSpecificOverride
	{
		[Tooltip("The percentage size applied to the matching Args on the LocalizeText sibling component.")]
		[SerializeField]
		private int[] localizeTextArgsSizes;

		[Tooltip("The vertical alignment change applied to the matching Args on the LocalizeText sibling component.")]
		[SerializeField]
		private int[] localizeTextArgsVerticalAlign;

		protected override void ApplyPlatformOverride()
		{
			LocalizeText component = GetComponent<LocalizeText>();
			if (localizeTextArgsSizes.Length != component.Args.Length || component.Args.Length != localizeTextArgsVerticalAlign.Length)
			{
				throw new IndexOutOfRangeException("Parameter array lengths do not match. Use 0 in argument array for no change if that's what was intended.");
			}
			for (int i = 0; i < component.Args.Length; i++)
			{
				if (localizeTextArgsSizes[i] != 0)
				{
					component.Args[i] = $"<size={localizeTextArgsSizes[i]}%>{component.Args[i]}</size>";
				}
				if (localizeTextArgsVerticalAlign[i] != 0)
				{
					component.Args[i] = $"<voffset={localizeTextArgsVerticalAlign[i]}>{component.Args[i]}</voffset>";
				}
			}
		}
	}
}
