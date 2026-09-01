using System;
using Poly.UI;
using UnityEngine;

namespace Poly.Game
{
	public class SpringCoilMeshGenerator : MonoBehaviour
	{
		public SkinnedMeshRenderer renderer;

		public Transform rootJointA;

		public Transform leafJointB;

		[Header("Generate Mesh")]
		public float singleCoilLength = 0.15f;

		public float numLoopsToGenerate = 1f;

		public int numVertsInCrossSection = 5;

		public float numCrossSectionsInLoop = 8.77f;

		public float loopRadius = 0.1f;

		public float crossSectionRadius = 0.025f;

		[Header("Poly Style")]
		public bool hardEdges = true;

		public float crossSectionAngleOffset;

		[Tooltip("This results in slightly-incorrect normals & lighting.")]
		public float coilTwistsPerRevolution = 0.83f;

		[Header("Composition with nodes")]
		[Range(0f, 0.5f)]
		public float separationFromNodeCenter = 0.1f;

		[Header("Generate Mesh")]
		public InspectorButton generateCoilButton;

		public InspectorButton correctScaleButton;

		public Mesh GenerateCoilMesh()
		{
			int num = Mathf.Max(1, Mathf.RoundToInt(numLoopsToGenerate * numCrossSectionsInLoop)) + 1;
			int num2 = ((!hardEdges) ? 1 : 4);
			int num3 = num * numVertsInCrossSection * num2;
			Vector3[] array = new Vector3[num3];
			Vector3[] array2 = new Vector3[num3];
			BoneWeight[] array3 = new BoneWeight[num3];
			int num4 = (num - 1) * numVertsInCrossSection * 2;
			int num5 = 0;
			for (int i = 0; i < num; i++)
			{
				float num6 = (float)i / (float)(num - 1);
				float num7 = numLoopsToGenerate * 360f * num6;
				Quaternion quaternion = Quaternion.Euler(0f, num7, 0f);
				Quaternion quaternion2 = quaternion;
				if (hardEdges)
				{
					float num8 = ((float)i + 0.5f) / (float)(num - 1);
					float y = numLoopsToGenerate * 360f * num8;
					quaternion2 = Quaternion.Euler(0f, y, 0f);
				}
				for (int j = 0; j < numVertsInCrossSection; j++)
				{
					float z = 360f * (float)j / (float)numVertsInCrossSection + crossSectionAngleOffset + coilTwistsPerRevolution * num7;
					Quaternion quaternion3 = Quaternion.Euler(0f, 0f, z);
					Quaternion quaternion4 = quaternion3;
					if (hardEdges)
					{
						float z2 = 360f * ((float)j + 0.5f) / (float)numVertsInCrossSection + crossSectionAngleOffset + coilTwistsPerRevolution * num7;
						quaternion4 = Quaternion.Euler(0f, 0f, z2);
					}
					array[num5] = quaternion * (loopRadius * Vector3.right + quaternion3 * Vector3.right * crossSectionRadius);
					array2[num5] = quaternion2 * (quaternion4 * Vector3.right);
					array3[num5].boneIndex0 = 0;
					array3[num5].boneIndex1 = 1;
					array3[num5].weight0 = 1f - num6;
					array3[num5].weight1 = num6;
					num5++;
					if (hardEdges)
					{
						for (int k = 0; k < 3; k++)
						{
							array[num5] = array[num5 - 1];
							array2[num5] = array2[num5 - 1];
							array3[num5].boneIndex0 = array3[num5 - 1].boneIndex0;
							array3[num5].boneIndex1 = array3[num5 - 1].boneIndex1;
							array3[num5].weight0 = array3[num5 - 1].weight0;
							array3[num5].weight1 = array3[num5 - 1].weight1;
							num5++;
						}
						if (0 < i)
						{
							array2[num5 - 4] = array2[num5 - 2 - numVertsInCrossSection * num2];
							array2[num5 - 3] = array2[num5 - 1 - numVertsInCrossSection * num2];
						}
						if (0 < j)
						{
							array2[num5 - 4] = array2[num5 - 3 - num2];
							array2[num5 - 2] = array2[num5 - 1 - num2];
						}
						if (j == numVertsInCrossSection - 1)
						{
							array2[num5 - 4 - numVertsInCrossSection * num2 + num2] = array2[num5 - 3];
							array2[num5 - 2 - numVertsInCrossSection * num2 + num2] = array2[num5 - 1];
						}
					}
				}
			}
			int[] array4 = new int[num4 * 3];
			num5 = 0;
			int num9 = 0;
			while (num9 < num4 * 3)
			{
				int num10 = num5 / numVertsInCrossSection * numVertsInCrossSection + (num5 + 1) % numVertsInCrossSection;
				array4[num9] = num5;
				array4[num9 + 1] = num5 + numVertsInCrossSection;
				array4[num9 + 2] = num10;
				array4[num9 + 3] = num10;
				array4[num9 + 4] = num5 + numVertsInCrossSection;
				array4[num9 + 5] = num10 + numVertsInCrossSection;
				if (hardEdges)
				{
					array4[num9] *= num2;
					array4[num9 + 1] *= num2;
					array4[num9 + 2] *= num2;
					array4[num9 + 3] *= num2;
					array4[num9 + 4] *= num2;
					array4[num9 + 5] *= num2;
					array4[num9] += 3;
					array4[num9 + 1]++;
					array4[num9 + 2] += 2;
					array4[num9 + 3] += 2;
					array4[num9 + 4]++;
					ref int reference = ref array4[num9 + 5];
					reference = reference;
				}
				num9 += 6;
				num5++;
			}
			Mesh mesh = new Mesh();
			mesh.vertices = array;
			mesh.normals = array2;
			mesh.boneWeights = array3;
			mesh.bindposes = new Matrix4x4[2]
			{
				Matrix4x4.identity,
				Matrix4x4.identity
			};
			mesh.triangles = array4;
			mesh.RecalculateBounds();
			mesh.name = "Spring coil mesh (auto-generated)";
			return mesh;
		}

		public void Init()
		{
			float num = Vector3.Dot(rootJointA.position - base.transform.position, base.transform.up);
			Vector3.Dot(leafJointB.position - base.transform.position, base.transform.up);
		}

		public void SetPositionFromTo(Vector3 from, Vector3 to, Vector3 forwardHint, Vector3 offset)
		{
			if ((bool)rootJointA && (bool)leafJointB)
			{
				base.transform.rotation = Quaternion.LookRotation(forwardHint, to - from);
				base.transform.position = from;
				rootJointA.position = from + offset;
				leafJointB.position = to + offset;
				AdjustStretchedScale();
				UpdateSkinnedRendererLocalBounds();
			}
		}

		public void AdjustStretchedScale()
		{
			float num = Vector3.Dot(rootJointA.up, leafJointB.position - rootJointA.position);
			float num2 = numLoopsToGenerate * loopRadius * 2f * MathF.PI;
			float f = Mathf.Atan(num / num2);
			float num3 = 1f / Mathf.Cos(f);
			float num4 = num / num3;
			rootJointA.localScale = new Vector3(1f, num3, 1f);
			leafJointB.localPosition = num4 * Vector3.up;
		}

		public void UpdateSkinnedRendererLocalBounds()
		{
			float y = rootJointA.localScale.y;
			float num = Vector3.Distance(rootJointA.position, leafJointB.position);
			Bounds localBounds = renderer.localBounds;
			Vector3 extents = localBounds.extents;
			extents.y = 0.5f * num / y;
			localBounds.extents = extents;
			Vector3 center = localBounds.center;
			center.y = 0.5f * num / y;
			localBounds.center = center;
			renderer.localBounds = localBounds;
		}

		public SpringCoilMeshGenerator()
		{
			generateCoilButton = new InspectorButton("Generate Coil Mesh", delegate
			{
				AssignMesh(GenerateCoilMesh());
			});
			correctScaleButton = new InspectorButton("Correct Scale", AdjustStretchedScale);
		}

		public void AssignMesh(Mesh mesh)
		{
			renderer.sharedMesh = mesh;
		}
	}
}
