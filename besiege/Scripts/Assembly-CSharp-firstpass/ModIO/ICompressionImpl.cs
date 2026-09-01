using System.Collections.Generic;

namespace ModIO
{
	public interface ICompressionImpl
	{
		bool ExtractAll(string archivePath, string targetDirectory);

		bool CompressFileCollection(string rootDirectory, IEnumerable<string> filePathCollection, string targetFilePath);

		bool CompressFile(string filePath, string targetFilePath);
	}
}
