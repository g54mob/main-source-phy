using TMPro;

public class DropdownUtils
{
	public static void SelectItem(TMP_Dropdown dropdown, string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		for (int i = 0; i < dropdown.options.Count; i++)
		{
			if (dropdown.options[i].text == text)
			{
				dropdown.value = i;
				break;
			}
		}
	}

	public static void SelectItem(TMP_Dropdown dropdown, int value)
	{
		if (value >= 0 && value < dropdown.options.Count)
		{
			dropdown.value = value;
		}
	}
}
