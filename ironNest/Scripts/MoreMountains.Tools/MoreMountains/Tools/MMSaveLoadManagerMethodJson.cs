using System;
using System.IO;

namespace MoreMountains.Tools
{
	public class MMSaveLoadManagerMethodJson : IMMSaveLoadManagerMethod
	{
		public void Save(object objectToSave, FileStream saveFile)
		{
		}

		public object Load(Type objectType, FileStream saveFile)
		{
			return null;
		}
	}
}
