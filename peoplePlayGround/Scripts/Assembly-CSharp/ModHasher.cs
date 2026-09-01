using System;
using System.IO;
using System.Linq;

public static class ModHasher
{
	public static uint Compute(ModMetaData mod)
	{
		uint hash = 0u;
		byte[] buffer = new byte[1024];
		foreach (string item in from d in Directory.EnumerateFiles(mod.MetaLocation, "*.*", SearchOption.AllDirectories)
			orderby d
			select d)
		{
			if (item.EndsWith(".cs", StringComparison.InvariantCultureIgnoreCase) || item.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase))
			{
				Hash(ref hash, buffer, item);
			}
		}
		return hash;
	}

	private static void Hash(ref uint hash, byte[] buffer, string file)
	{
		if (!File.Exists(file))
		{
			return;
		}
		using FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
		while (true)
		{
			int num = fileStream.Read(buffer, 0, buffer.Length);
			if (num <= 0)
			{
				break;
			}
			hash ^= xxHash.CalculateHash(buffer, num);
		}
	}
}
