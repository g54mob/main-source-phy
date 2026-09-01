using System;

namespace Kamgam.SettingsGenerator;

public class SettingsPathAttribute : Attribute
{
	public string Path;

	public string[] Tags;

	public SettingsPathAttribute(string path, string[] tags)
	{
		Path = path;
		Tags = tags;
	}
}
