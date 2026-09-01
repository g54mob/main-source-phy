using System.Collections.Generic;

namespace ModIO
{
	public static class CompressionModule
	{
		public static readonly ICompressionImpl IMPLEMENTATION;

		static CompressionModule()
		{
			IMPLEMENTATION = new DotNetZipCompressionImpl();
		}

		public static bool ExtractAll(string archivePath, string targetDirectory)
		{
			return IMPLEMENTATION.ExtractAll(archivePath, targetDirectory);
		}

		public static bool CompressFileCollection(string rootDirectory, IEnumerable<string> filePathCollection, string targetFilePath)
		{
			return IMPLEMENTATION.CompressFileCollection(rootDirectory, filePathCollection, targetFilePath);
		}

		public static bool CompressFile(string filePath, string targetFilePath)
		{
			return IMPLEMENTATION.CompressFile(filePath, targetFilePath);
		}
	}
}
