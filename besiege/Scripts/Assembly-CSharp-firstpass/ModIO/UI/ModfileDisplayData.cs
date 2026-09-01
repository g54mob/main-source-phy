using System;

namespace ModIO.UI
{
	[Serializable]
	[Obsolete("No longer supported.")]
	public struct ModfileDisplayData
	{
		public int modfileId;

		public int modId;

		public int dateAdded;

		public string fileName;

		public long fileSize;

		public string MD5;

		public string version;

		public string changelog;

		public string metadataBlob;

		public int virusScanDate;

		public ModfileVirusScanStatus virusScanStatus;

		public ModfileVirusScanResult virusScanResult;

		public string virusScanHash;

		public static ModfileDisplayData CreateFromModfile(Modfile modfile)
		{
			return new ModfileDisplayData
			{
				modId = modfile.modId,
				modfileId = modfile.id,
				dateAdded = modfile.dateAdded,
				fileName = modfile.fileName,
				fileSize = modfile.fileSize,
				MD5 = modfile.fileHash.md5,
				version = modfile.version,
				changelog = modfile.changelog,
				metadataBlob = modfile.metadataBlob,
				virusScanDate = modfile.dateScanned,
				virusScanStatus = modfile.virusScanStatus,
				virusScanResult = modfile.virusScanResult,
				virusScanHash = modfile.virusScanHash
			};
		}
	}
}
