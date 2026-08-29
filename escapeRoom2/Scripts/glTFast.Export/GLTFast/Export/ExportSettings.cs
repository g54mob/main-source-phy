using UnityEngine;

namespace GLTFast.Export
{
	public class ExportSettings
	{
		public GltfFormat Format { get; set; }

		public ImageDestination ImageDestination { get; set; }

		public FileConflictResolution FileConflictResolution { get; set; }

		[field: Tooltip("Light intensity values are multiplied by this factor")]
		public float LightIntensityFactor { get; set; } = 1f;

		public ComponentType ComponentMask { get; set; } = ComponentType.All;

		public Compression Compression { get; set; } = Compression.Uncompressed;

		public DracoExportSettings DracoSettings { get; set; }

		public bool Deterministic { get; set; }

		public VertexAttributeUsage PreservedVertexAttributes { get; set; }

		public int JpgQuality { get; set; } = 60;
	}
}
