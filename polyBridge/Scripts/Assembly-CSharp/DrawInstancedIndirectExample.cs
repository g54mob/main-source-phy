using UnityEngine;

public class DrawInstancedIndirectExample : MonoBehaviour
{
	private struct InstanceTransform
	{
		public Matrix4x4 matrix;

		public Matrix4x4 matrixInverse;

		public static int size => 128;
	}

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

	private const string instanceTransformsBufferName = "_InstanceTransforms";

	private Bounds bounds;

	private Matrix4x4[] matrices;

	private InstanceData[] instanceData;

	private InstanceTransform[] instanceTransforms;

	private MaterialPropertyBlock propertyBlock;

	private ComputeBuffer argsBuffer;

	private ComputeBuffer instanceTransformsBuffer;

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
		Graphics.DrawMeshInstancedIndirect(mesh, 0, material, bounds, argsBuffer, 0, propertyBlock);
	}

	private void InitializeBuffers()
	{
		bounds = new Bounds(Vector3.zero, new Vector3(100000f, 100000f, 100000f));
		argsBuffer = CreateArgsBuffer(mesh, numInstances);
		int num = Mathf.CeilToInt(Mathf.Sqrt(numInstances));
		matrices = new Matrix4x4[numInstances];
		instanceTransforms = new InstanceTransform[numInstances];
		instanceData = new InstanceData[numInstances];
		for (int i = 0; i < numInstances; i++)
		{
			Vector3 vector = new Vector3(i % num, 0f, i / num) + offset;
			instanceTransforms[i].matrix = Matrix4x4.Translate(vector);
			instanceTransforms[i].matrixInverse = instanceTransforms[i].matrix.inverse;
			float t = (vector.x + vector.z) / (2f * (float)num);
			instanceData[i].color = Color.Lerp(Color.black, Color.white, t);
		}
		instanceTransformsBuffer = new ComputeBuffer(numInstances, InstanceTransform.size);
		instanceTransformsBuffer.SetData(instanceTransforms);
		instanceDataBuffer = new ComputeBuffer(numInstances, InstanceData.size);
		instanceDataBuffer.SetData(instanceData);
		propertyBlock = new MaterialPropertyBlock();
		propertyBlock.SetBuffer("_InstanceTransforms", instanceTransformsBuffer);
		propertyBlock.SetBuffer("_InstanceData", instanceDataBuffer);
	}

	private void CleanupBuffers()
	{
		instanceDataBuffer?.Release();
		instanceDataBuffer = null;
		instanceTransformsBuffer?.Release();
		instanceTransformsBuffer = null;
		argsBuffer?.Release();
		argsBuffer = null;
		propertyBlock = null;
	}

	public static ComputeBuffer CreateArgsBuffer(Mesh mesh, int count)
	{
		int submesh = 0;
		uint[] array = new uint[5]
		{
			mesh.GetIndexCount(submesh),
			(uint)count,
			mesh.GetIndexStart(submesh),
			mesh.GetBaseVertex(submesh),
			0u
		};
		ComputeBuffer computeBuffer = new ComputeBuffer(array.Length, 4, ComputeBufferType.DrawIndirect);
		computeBuffer.SetData(array);
		return computeBuffer;
	}
}
