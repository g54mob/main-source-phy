using System;
using UnityEngine;

namespace ModIO.UI
{
	[DisallowMultipleComponent]
	public abstract class EnumDropdownBase : MonoBehaviour
	{
		[Serializable]
		public struct EnumSelectionPair
		{
			public int selectionIndex;

			public int enumValue;
		}

		public EnumSelectionPair[] enumSelectionPairings = new EnumSelectionPair[0];

		public abstract string[] GetEnumNames();

		public abstract int[] GetEnumValues();

		public bool TryGetPairForSelection(int selectionIndex, out EnumSelectionPair result)
		{
			if (enumSelectionPairings != null && enumSelectionPairings.Length > 0)
			{
				EnumSelectionPair[] array = enumSelectionPairings;
				for (int i = 0; i < array.Length; i++)
				{
					EnumSelectionPair enumSelectionPair = array[i];
					if (enumSelectionPair.selectionIndex == selectionIndex)
					{
						result = enumSelectionPair;
						return true;
					}
				}
			}
			result = default(EnumSelectionPair);
			EnumSelectionPair enumSelectionPair2 = result;
			enumSelectionPair2.selectionIndex = -1;
			enumSelectionPair2.enumValue = -1;
			result = enumSelectionPair2;
			return false;
		}

		public bool TryGetPairForEnum(int enumValue, out EnumSelectionPair result)
		{
			if (enumSelectionPairings != null && enumSelectionPairings.Length > 0)
			{
				EnumSelectionPair[] array = enumSelectionPairings;
				for (int i = 0; i < array.Length; i++)
				{
					EnumSelectionPair enumSelectionPair = array[i];
					if (enumSelectionPair.enumValue == enumValue)
					{
						result = enumSelectionPair;
						return true;
					}
				}
			}
			result = default(EnumSelectionPair);
			EnumSelectionPair enumSelectionPair2 = result;
			enumSelectionPair2.selectionIndex = -1;
			enumSelectionPair2.enumValue = -1;
			result = enumSelectionPair2;
			return false;
		}
	}
}
