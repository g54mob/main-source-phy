using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;
using UnityEngine;

namespace ModIO
{
	public class DotNetZipCompressionImpl : ICompressionImpl
	{
		public bool ExtractAll(string archivePath, string targetDirectory)
		{
			if (string.IsNullOrEmpty(archivePath))
			{
				Debug.LogWarning("[mod.io] Unable to extract archive to target directory.\narchivePath is NULL or EMPTY.");
				return false;
			}
			if (string.IsNullOrEmpty(targetDirectory))
			{
				Debug.LogWarning("[mod.io] Unable to extract archive to target directory.\ntargetDirectory is NULL or EMPTY.");
				return false;
			}
			bool result = false;
			try
			{
				using (ZipFile zipFile = ZipFile.Read(archivePath))
				{
					zipFile.ExtractAll(targetDirectory);
					result = true;
				}
			}
			catch (Exception e)
			{
				Debug.LogWarning("[mod.io] Unable to extract archive to target directory.\nArchive: " + archivePath + "\nTarget: " + targetDirectory + "\n\n" + Utility.GenerateExceptionDebugString(e));
			}
			return result;
		}

		public bool CompressFileCollection(string rootDirectory, IEnumerable<string> filePathCollection, string targetFilePath)
		{
			if (filePathCollection == null)
			{
				Debug.LogWarning("[mod.io] Unable to compress file collection to archive.\nfilePathCollection is NULL.");
				return false;
			}
			if (string.IsNullOrEmpty(targetFilePath))
			{
				Debug.LogWarning("[mod.io] Unable to compress file collection to archive.\ntargetFilePath is NULL or EMPTY.");
				return false;
			}
			if (string.IsNullOrEmpty(rootDirectory))
			{
				rootDirectory = string.Empty;
			}
			bool result = false;
			string text = string.Empty;
			try
			{
				using (ZipFile zipFile = new ZipFile())
				{
					foreach (string item in filePathCollection)
					{
						text = item;
						string path = item.Substring(rootDirectory.Length);
						string directoryName = Path.GetDirectoryName(path);
						zipFile.AddFile(item, directoryName);
					}
					zipFile.Save(targetFilePath);
					result = true;
				}
			}
			catch (Exception e)
			{
				Debug.LogWarning("[mod.io] Unable to compress file collection to archive.\nLast Attempted File: " + text + "\nOutput: " + targetFilePath + "\n\n" + Utility.GenerateExceptionDebugString(e));
			}
			return result;
		}

		public bool CompressFile(string filePath, string targetFilePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				Debug.LogWarning("[mod.io] Unable to compress file collection to archive.\nfilePath is NULL or EMPTY.");
				return false;
			}
			if (string.IsNullOrEmpty(targetFilePath))
			{
				Debug.LogWarning("[mod.io] Unable to compress file collection to archive.\ntargetFilePath is NULL or EMPTY.");
				return false;
			}
			bool result = false;
			try
			{
				using (ZipFile zipFile = new ZipFile())
				{
					zipFile.AddFile(filePath, string.Empty);
					zipFile.Save(targetFilePath);
					result = true;
				}
			}
			catch (Exception e)
			{
				Debug.LogWarning("[mod.io] Unable to compress file to archive.\nFile: " + filePath + "\nOutput: " + targetFilePath + "\n\n" + Utility.GenerateExceptionDebugString(e));
			}
			return result;
		}
	}
}
