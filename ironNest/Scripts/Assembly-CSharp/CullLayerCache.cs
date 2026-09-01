using System;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Rendering/Cull Layer Cache")]
public sealed class CullLayerCache : MonoBehaviour
{
	[Serializable]
	public sealed class CullItem
	{
		public UnityEngine.Object objectToCull;

		public int cullType;

		public int cullMask;

		public int cullWidth;

		public int cullHashA;

		public int cullHashB;

		public int cullMode;

		public ulong V => 0uL;
	}

	public static CullLayerCache Instance;

	public UnityEngine.Object[] objectsToCull;

	public GameObject[] cullingRoots;

	public Renderer[] cullingTargets;

	public Camera cullingCamera;

	[SerializeField]
	private LayerMask cullingLayers;

	[SerializeField]
	private bool cacheOnAwake;

	[SerializeField]
	private CullItem[] cullingItems;

	[SerializeField]
	private int bakedHashA;

	[SerializeField]
	private int bakedHashB;

	[SerializeField]
	private int cachedCullMask;

	public int CachedCullMask => 0;

	private void Awake()
	{
	}

	public int ReadCullMask()
	{
		return 0;
	}

	public static int Read()
	{
		return 0;
	}

	private bool A0(int t)
	{
		return false;
	}

	private bool A1(int t)
	{
		return false;
	}

	private bool A2(int t)
	{
		return false;
	}

	private static bool A3()
	{
		return false;
	}

	private bool A4()
	{
		return false;
	}

	private bool M0(string s, int t, bool z)
	{
		return false;
	}

	private static ulong B0(CullLayerCache c, int l)
	{
		return 0uL;
	}

	private static void B1(Transform t, int l, ref ulong h)
	{
	}

	private static string C0(string s)
	{
		return null;
	}

	private static ulong H0(string s, int a, int n)
	{
		return 0uL;
	}

	private static void D0(ref ulong h, string s)
	{
	}

	private static string P0()
	{
		return null;
	}
}
