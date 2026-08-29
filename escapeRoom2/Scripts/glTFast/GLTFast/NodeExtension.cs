using GLTFast.Schema;
using UnityEngine;

namespace GLTFast
{
	public static class NodeExtension
	{
		public static void GetTransform(this NodeBase node, out Vector3 position, out Quaternion rotation, out Vector3 scale)
		{
			position = Vector3.zero;
			rotation = Quaternion.identity;
			scale = Vector3.one;
			if (node.matrix != null)
			{
				new Matrix4x4
				{
					m00 = node.matrix[0],
					m10 = 0f - node.matrix[1],
					m20 = 0f - node.matrix[2],
					m30 = node.matrix[3],
					m01 = 0f - node.matrix[4],
					m11 = node.matrix[5],
					m21 = node.matrix[6],
					m31 = node.matrix[7],
					m02 = 0f - node.matrix[8],
					m12 = node.matrix[9],
					m22 = node.matrix[10],
					m32 = node.matrix[11],
					m03 = 0f - node.matrix[12],
					m13 = node.matrix[13],
					m23 = node.matrix[14],
					m33 = node.matrix[15]
				}.Decompose(out var translation, out var rotation2, out var scale2);
				position = translation;
				rotation = rotation2;
				scale = scale2;
			}
			else
			{
				if (node.translation != null)
				{
					position = new Vector3(0f - node.translation[0], node.translation[1], node.translation[2]);
				}
				if (node.rotation != null)
				{
					rotation = new Quaternion(node.rotation[0], 0f - node.rotation[1], 0f - node.rotation[2], node.rotation[3]);
				}
				if (node.scale != null)
				{
					scale = new Vector3(node.scale[0], node.scale[1], node.scale[2]);
				}
			}
		}
	}
}
