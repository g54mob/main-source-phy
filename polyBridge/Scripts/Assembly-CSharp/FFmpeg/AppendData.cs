using System;
using System.Collections.Generic;

namespace FFmpeg
{
	[Serializable]
	public class AppendData
	{
		public List<string> inputPaths = new List<string>();

		public string outputPath;
	}
}
