using System;

namespace MagicaCloth2
{
	[Serializable]
	public class GizmoSerializeData
	{
		public bool always;

		public ClothDebugSettings clothDebugSettings = new ClothDebugSettings();

		public GizmoSerializeData()
		{
			clothDebugSettings.enable = true;
			clothDebugSettings.shape = true;
		}

		public bool IsAlways()
		{
			return always;
		}
	}
}
