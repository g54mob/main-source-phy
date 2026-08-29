using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class LightsPunctual
	{
		public LightPunctual[] lights;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			writer.AddArray("lights");
			LightPunctual[] array = lights;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].GltfSerialize(writer);
			}
			writer.CloseArray();
			writer.Close();
		}

		public bool JsonUtilityCleanup()
		{
			return lights != null;
		}
	}
}
