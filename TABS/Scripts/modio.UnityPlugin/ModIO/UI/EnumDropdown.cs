using System;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class EnumDropdown<TEnum> : EnumDropdownBase where TEnum : struct, IConvertible
	{
		public bool TryGetSelectedValue(out TEnum enumValue, int index = -1)
		{
			int selectionIndex = index;
			if (index == -1)
			{
				selectionIndex = GetComponent<Dropdown>().value;
			}
			if (TryGetPairForSelection(selectionIndex, out var result) && Enum.IsDefined(typeof(TEnum), result.enumValue))
			{
				enumValue = (TEnum)Enum.ToObject(typeof(TEnum), result.enumValue);
				return true;
			}
			enumValue = default(TEnum);
			return false;
		}

		public override string[] GetEnumNames()
		{
			return Enum.GetNames(typeof(TEnum));
		}

		public override int[] GetEnumValues()
		{
			return (int[])Enum.GetValues(typeof(TEnum));
		}
	}
}
