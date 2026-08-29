using GLTFast.Vertex;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace GLTFast.Jobs
{
	[BurstCompile]
	internal struct SortAndNormalizeBoneWeightsJob : IJobParallelFor
	{
		public NativeArray<VBones> bones;

		public int skinWeights;

		public unsafe void Execute(int index)
		{
			VBones v = bones[index];
			bool flag = true;
			for (int i = 0; i < 3; i++)
			{
				float num = v.weights[i];
				float num2 = v.weights[i + 1];
				if (num < num2)
				{
					flag = false;
					break;
				}
			}
			if (!flag)
			{
				for (int j = 0; j < skinWeights; j++)
				{
					float num3 = v.weights[j];
					int num4 = j;
					for (int k = j + 1; k < 4; k++)
					{
						float num5 = v.weights[k];
						if (v.weights[k] > num3)
						{
							num3 = num5;
							num4 = k;
						}
					}
					if (num4 > j)
					{
						Swap(ref v, num4, j);
					}
				}
			}
			float num6 = 0f;
			for (int l = 0; l < skinWeights; l++)
			{
				num6 += v.weights[l];
			}
			if (math.abs(num6 - 1f) > 2E-07f && num6 > 0f)
			{
				flag = false;
				for (int m = 0; m < skinWeights; m++)
				{
					ref float reference = ref v.weights[m];
					reference /= num6;
				}
			}
			if (!flag)
			{
				bones[index] = v;
			}
		}

		private unsafe static void Swap(ref VBones v, int a, int b)
		{
			ref float reference = ref v.weights[a];
			ref float reference2 = ref v.weights[b];
			float num = v.weights[b];
			float num2 = v.weights[a];
			reference = num;
			reference2 = num2;
			ref uint reference3 = ref v.joints[a];
			ref uint reference4 = ref v.joints[b];
			uint num3 = v.joints[b];
			uint num4 = v.joints[a];
			reference3 = num3;
			reference4 = num4;
		}
	}
}
