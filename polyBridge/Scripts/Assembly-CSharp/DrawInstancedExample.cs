using UnityEngine;

public class DrawInstancedExample : MonoBehaviour
{
	private struct InstanceData
	{
		public Color color;

		public static int size => 16;
	}

	public int numInstances = 100;

	public Mesh mesh;

	public Material material;

	public Vector3 offset;

	private const string instanceDataBufferName = "_InstanceData";

	private Matrix4x4[] matrices;

	private InstanceData[] instanceData;

	private MaterialPropertyBlock propertyBlock;

	private ComputeBuffer instanceDataBuffer;

	private void Awake()
	{
		InitializeBuffers();
	}

	private void OnDestroy()
	{
		CleanupBuffers();
	}

	private void Update()
	{
		Graphics.DrawMeshInstanced(mesh, 0, material, matrices, matrices.Length, propertyBlock);
	}

	private void InitializeBuffers()
	{
		int num = Mathf.CeilToInt(Mathf.Sqrt(numInstances));
		matrices = new Matrix4x4[numInstances];
		instanceData = new InstanceData[numInstances];
		for (int i = 0; i < numInstances; i++)
		{
			Vector3 vector = new Vector3(i % num, 0f, i / num) + offset;
			matrices[i] = Matrix4x4.Translate(vector);
			float t = (vector.x + vector.z) / (2f * (float)num);
			instanceData[i].color = Color.Lerp(Color.black, Color.white, t);
		}
		instanceDataBuffer = new ComputeBuffer(numInstances, InstanceData.size);
		instanceDataBuffer.SetData(instanceData);
		propertyBlock = new MaterialPropertyBlock();
		propertyBlock.SetBuffer("_InstanceData", instanceDataBuffer);
	}

	private void CleanupBuffers()
	{
		instanceDataBuffer?.Release();
		instanceDataBuffer = null;
		propertyBlock = null;
	}
}
