using System;

[Serializable]
public class SettingsButton
{
	public enum SettingsType
	{
		Slider = 0,
		Toggle = 1,
		Button = 2,
		Spacing = 3,
		Color = 4,
		GroupTitle = 5
	}

	public string title = "";

	public SettingsType settingType;
}
