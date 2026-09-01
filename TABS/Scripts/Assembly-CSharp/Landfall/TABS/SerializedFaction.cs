using System;

namespace Landfall.TABS
{
	[Serializable]
	public class SerializedFaction
	{
		public DatabaseID ID;

		public DatabaseID[] units;

		public string name;

		public DatabaseID icon;

		public DatabaseID color;
	}
}
