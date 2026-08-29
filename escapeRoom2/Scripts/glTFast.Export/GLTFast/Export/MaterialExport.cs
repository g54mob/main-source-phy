using System;

namespace GLTFast.Export
{
	public static class MaterialExport
	{
		private static IMaterialExport s_MaterialExport;

		public static IMaterialExport GetDefaultMaterialExport()
		{
			if (s_MaterialExport == null)
			{
				RenderPipeline renderPipeline = RenderPipelineUtils.RenderPipeline;
				switch (renderPipeline)
				{
				case RenderPipeline.BuiltIn:
				case RenderPipeline.Universal:
					s_MaterialExport = MetaMaterialExportShaderGraphs<StandardMaterialExport, GltfShaderGraphMaterialExporter>.Instance;
					break;
				case RenderPipeline.HighDefinition:
					s_MaterialExport = MetaMaterialExportShaderGraphs<HighDefinitionMaterialExport, GltfHdrpMaterialExporter>.Instance;
					break;
				default:
					throw new InvalidOperationException($"Could not determine default MaterialExport (render pipeline {renderPipeline})");
				}
			}
			return s_MaterialExport;
		}

		internal static bool AddImageExport(IGltfWritable gltf, ImageExportBase imageExport, out int textureId)
		{
			int num = gltf.AddImage(imageExport);
			if (num < 0)
			{
				textureId = -1;
				return false;
			}
			int samplerId = gltf.AddSampler(imageExport.FilterMode, imageExport.WrapModeU, imageExport.WrapModeV);
			textureId = gltf.AddTexture(num, samplerId);
			return true;
		}
	}
}
