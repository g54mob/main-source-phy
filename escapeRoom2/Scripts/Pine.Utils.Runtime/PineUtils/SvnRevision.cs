using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace PineUtils
{
	public static class SvnRevision
	{
		public static int get()
		{
			int result = -1;
			string workingDirectory = Application.dataPath.Replace("/Assets", "");
			ProcessStartInfo processStartInfo = new ProcessStartInfo("svn.exe", "info");
			if (SystemInfo.operatingSystem.Contains("Mac"))
			{
				processStartInfo = new ProcessStartInfo("/usr/bin/svn", "info");
			}
			processStartInfo.WorkingDirectory = workingDirectory;
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			processStartInfo.UseShellExecute = false;
			Process process = Process.Start(processStartInfo);
			using (StreamReader streamReader = process.StandardOutput)
			{
				string text = "";
				while (text != null)
				{
					text = streamReader.ReadLine();
					if (text == null)
					{
						break;
					}
					if (text.Contains("Last Changed Rev"))
					{
						result = int.Parse(text.Split(':')[1].Trim());
						break;
					}
				}
			}
			process.WaitForExit();
			return result;
		}
	}
}
