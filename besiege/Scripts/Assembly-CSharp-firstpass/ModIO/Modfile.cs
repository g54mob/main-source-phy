using System;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class Modfile
	{
		public const int NULL_ID = 0;

		[JsonProperty("id")]
		public int id;

		[JsonProperty("mod_id")]
		public int modId;

		[JsonProperty("date_added")]
		public int dateAdded;

		[JsonProperty("filename")]
		public string fileName;

		[JsonProperty("filesize")]
		public long fileSize;

		[JsonProperty("filehash")]
		public FileHash fileHash;

		[JsonProperty("version")]
		public string version;

		[JsonProperty("changelog")]
		public string changelog;

		[JsonProperty("metadata_blob")]
		public string metadataBlob;

		[JsonProperty("date_scanned")]
		public int dateScanned;

		[JsonProperty("virus_status")]
		public ModfileVirusScanStatus virusScanStatus;

		[JsonProperty("virus_positive")]
		public ModfileVirusScanResult virusScanResult;

		[JsonProperty("virustotal_hash")]
		public string virusScanHash;

		[JsonProperty("download")]
		public ModfileLocator downloadLocator;
	}
}
