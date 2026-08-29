using System;
using UnityEngine.Rendering;

namespace GLTFast.Export
{
	public static class VertexAttributeUsageExtension
	{
		public static VertexAttributeUsage ToVertexAttributeUsage(this VertexAttribute attr)
		{
			return attr switch
			{
				VertexAttribute.Position => VertexAttributeUsage.Position, 
				VertexAttribute.Normal => VertexAttributeUsage.Normal, 
				VertexAttribute.Tangent => VertexAttributeUsage.Tangent, 
				VertexAttribute.Color => VertexAttributeUsage.Color, 
				VertexAttribute.TexCoord0 => VertexAttributeUsage.TexCoord0, 
				VertexAttribute.TexCoord1 => VertexAttributeUsage.TexCoord1, 
				VertexAttribute.TexCoord2 => VertexAttributeUsage.TexCoord2, 
				VertexAttribute.TexCoord3 => VertexAttributeUsage.TexCoord3, 
				VertexAttribute.TexCoord4 => VertexAttributeUsage.TexCoord4, 
				VertexAttribute.TexCoord5 => VertexAttributeUsage.TexCoord5, 
				VertexAttribute.TexCoord6 => VertexAttributeUsage.TexCoord6, 
				VertexAttribute.TexCoord7 => VertexAttributeUsage.TexCoord7, 
				VertexAttribute.BlendWeight => VertexAttributeUsage.BlendWeight, 
				VertexAttribute.BlendIndices => VertexAttributeUsage.BlendIndices, 
				_ => throw new ArgumentOutOfRangeException("attr", attr, null), 
			};
		}
	}
}
