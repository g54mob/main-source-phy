using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class NodeExtensions
	{
		public MeshGpuInstancing EXT_mesh_gpu_instancing;

		public NodeLightsPunctual KHR_lights_punctual;

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			if (EXT_mesh_gpu_instancing != null)
			{
				writer.AddProperty("EXT_mesh_gpu_instancing");
				EXT_mesh_gpu_instancing.GltfSerialize(writer);
			}
			if (KHR_lights_punctual != null)
			{
				writer.AddProperty("KHR_lights_punctual");
				KHR_lights_punctual.GltfSerialize(writer);
			}
			writer.Close();
		}
	}
}
