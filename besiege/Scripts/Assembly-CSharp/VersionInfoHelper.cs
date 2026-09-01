using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class VersionInfoHelper
{
	public const string VERSION_FILE = "version_info";

	public const string VERSION_PATH = "/Resources/version_info.txt";

	public const string VERSION_CACHE_KEY = "LastAdditionalVersion";

	public static string GetPlasticChangeset()
	{
		string result = "0";
		ProcessStartInfo processStartInfo = new ProcessStartInfo();
		processStartInfo.FileName = "cm";
		processStartInfo.Arguments = "status";
		processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
		processStartInfo.RedirectStandardOutput = true;
		processStartInfo.UseShellExecute = false;
		processStartInfo.WorkingDirectory = Directory.GetParent(Application.dataPath).FullName;
		processStartInfo.RedirectStandardError = true;
		processStartInfo.CreateNoWindow = true;
		string text = string.Empty;
		try
		{
			using (Process process = Process.Start(processStartInfo))
			{
				StringBuilder stringBuilder = new StringBuilder();
				while (!process.HasExited)
				{
					stringBuilder.Append(process.StandardOutput.ReadToEnd());
					stringBuilder.Append(process.StandardError.ReadToEnd());
				}
				stringBuilder.Append(process.StandardOutput.ReadToEnd());
				stringBuilder.Append(process.StandardError.ReadToEnd());
				text = stringBuilder.ToString();
			}
			Regex regex = new Regex("cs:\\s*([0-9]+)", RegexOptions.Compiled);
			Match match = regex.Match(text);
			if (!match.Success)
			{
				throw new Exception("Not a valid cm status string");
			}
			result = match.Groups[1].Value;
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogWarning(string.Concat("[GetPlasticChangeset] Could not parse outputString=", text, " due to ", ex, ": ", ex.Message));
		}
		return result;
	}
}
