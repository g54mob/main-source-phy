using Unity.Mathematics;

namespace MagicaCloth2
{
	public struct VirtualMeshBoneWeight
	{
		public float4 weights;

		public int4 boneIndices;

		public bool IsValid => weights[0] >= 1E-06f;

		public int Count
		{
			get
			{
				if (weights[3] > 0f)
				{
					return 4;
				}
				if (weights[2] > 0f)
				{
					return 3;
				}
				if (weights[1] > 0f)
				{
					return 2;
				}
				if (weights[0] > 0f)
				{
					return 1;
				}
				return 0;
			}
		}

		public VirtualMeshBoneWeight(int4 boneIndices, float4 weights)
		{
			this.boneIndices = boneIndices;
			this.weights = weights;
		}

		public void AddWeight(int boneIndex, float weight)
		{
			if (weight < 1E-06f)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < 4; i++)
			{
				float num2 = weights[i];
				if (num2 == 0f)
				{
					break;
				}
				if (boneIndices[i] == boneIndex)
				{
					num2 += weight;
					weights[i] = num2;
					for (int num3 = i; num3 >= 1; num3--)
					{
						if (weights[num3] > weights[num3 - 1])
						{
							num2 = weights[num3 - 1];
							weights[num3 - 1] = weights[num3];
							weights[num3] = num2;
							int value = boneIndices[num3 - 1];
							boneIndices[num3 - 1] = boneIndices[num3];
							boneIndices[num3] = value;
						}
					}
					return;
				}
				num++;
			}
			for (int j = 0; j < 4; j++)
			{
				float num4 = weights[j];
				if (num4 == 0f)
				{
					weights[j] = weight;
					boneIndices[j] = boneIndex;
					break;
				}
				if (weight > num4)
				{
					for (int num5 = 2; num5 >= j; num5--)
					{
						weights[num5 + 1] = weights[num5];
						boneIndices[num5 + 1] = boneIndices[num5];
					}
					weights[j] = weight;
					boneIndices[j] = boneIndex;
					break;
				}
			}
		}

		public void AddWeight(in VirtualMeshBoneWeight bw)
		{
			if (bw.IsValid)
			{
				for (int i = 0; i < 4; i++)
				{
					AddWeight(bw.boneIndices[i], bw.weights[i]);
				}
			}
		}

		public void AdjustWeight()
		{
			if (IsValid)
			{
				float num = math.csum(weights);
				float num2 = 1f / num;
				weights *= num2;
			}
		}

		public override string ToString()
		{
			return $"[{boneIndices}] w({weights})";
		}
	}
}
