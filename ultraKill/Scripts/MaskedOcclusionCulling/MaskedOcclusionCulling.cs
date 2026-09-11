using System;
using System.Runtime.InteropServices;
using System.Threading;

public ref struct MaskedOcclusionCulling : IDisposable
{
	public enum BackfaceWinding
	{
		None = 0,
		Clockwise = 1,
		CounterClockwise = 2
	}

	[Flags]
	public enum ClipPlanes
	{
		None = 0,
		Near = 1,
		Left = 2,
		Right = 4,
		Bottom = 8,
		Top = 0x10,
		Sides = 0x1E,
		All = 0x1F
	}

	public enum CullingResult
	{
		Visible = 0,
		Occluded = 1,
		ViewCulled = 3
	}

	public enum Implementation
	{
		Sse2 = 0,
		Sse41 = 1,
		Avx2 = 2,
		Avx512 = 3
	}

	[StructLayout(LayoutKind.Sequential, Size = 1)]
	private ref struct MaskedOcclusionCullingHandle
	{
	}

	public struct OcclusionCullingStatistics
	{
		public struct Occluders
		{
			public long mNumProcessedTriangles;

			public long mNumRasterizedTriangles;

			public long mNumTilesTraversed;

			public long mNumTilesUpdated;

			public long mNumTilesMerged;
		}

		public struct Occludees
		{
			public long mNumProcessedRectangles;

			public long mNumProcessedTriangles;

			public long mNumRasterizedTriangles;

			public long mNumTilesTraversed;
		}

		public Occluders mOccluders;

		public Occludees mOccludees;
	}

	public struct ScissorRect
	{
		public int mMinX;

		public int mMinY;

		public int mMaxX;

		public int mMaxY;

		public ScissorRect(int minX, int minY, int maxX, int maxY)
		{
			mMinX = minX;
			mMinY = minY;
			mMaxX = maxX;
			mMaxY = maxY;
		}
	}

	public struct TriList
	{
		public uint mNumTriangles;

		public uint mTriIdx;

		public unsafe float* mPtr;
	}

	public struct VertexLayout
	{
		public int mStride;

		public int mOffsetY;

		public int mOffsetZW;

		public VertexLayout(int stride, int offsetY, int offsetZW)
		{
			mStride = stride;
			mOffsetY = offsetY;
			mOffsetZW = offsetZW;
		}
	}

	private const string LibraryName = "MaskedOcclusionCullingNative";

	private unsafe MaskedOcclusionCullingHandle* _handle;

	public MaskedOcclusionCulling()
		: this(Implementation.Avx512)
	{
	}

	public unsafe MaskedOcclusionCulling(Implementation RequestedSIMD)
	{
		_handle = Create(RequestedSIMD);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_Create", ExactSpelling = true)]
		static extern unsafe MaskedOcclusionCullingHandle* Create(Implementation RequestedSIMD);
	}

	public unsafe MaskedOcclusionCulling(Implementation RequestedSIMD, delegate* unmanaged[Cdecl]<nuint, nuint, void*> alignedAlloc, delegate* unmanaged[Cdecl]<void*, void> alignedFree)
	{
		_handle = Create(RequestedSIMD, alignedAlloc, alignedFree);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_CreateWithAllocator", ExactSpelling = true)]
		static extern unsafe MaskedOcclusionCullingHandle* Create(Implementation RequestedSIMD, delegate* unmanaged[Cdecl]<nuint, nuint, void*> alignedAlloc, delegate* unmanaged[Cdecl]<void*, void> alignedFree);
	}

	public unsafe void SetResolution(uint width, uint height)
	{
		PInvoke(_handle, width, height);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_SetResolution", ExactSpelling = true)]
		static extern unsafe void PInvoke(MaskedOcclusionCullingHandle* moc, uint width, uint height);
	}

	public unsafe readonly void GetResolution(uint* width, uint* height)
	{
		PInvoke(_handle, width, height);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_GetResolution", ExactSpelling = true)]
		static extern unsafe void PInvoke(MaskedOcclusionCullingHandle* moc, uint* width, uint* height);
	}

	public unsafe readonly void GetResolution(out uint width, out uint height)
	{
		fixed (uint* width2 = &width)
		{
			fixed (uint* height2 = &height)
			{
				GetResolution(width2, height2);
			}
		}
	}

	public unsafe void ComputeBinWidthHeight(uint nBinsW, uint nBinsH, uint* outBinWidth, uint* outBinHeight)
	{
		PInvoke(_handle, nBinsW, nBinsH, outBinWidth, outBinHeight);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_ComputeBinWidthHeight", ExactSpelling = true)]
		static extern unsafe void PInvoke(MaskedOcclusionCullingHandle* moc, uint nBinsW, uint nBinsH, uint* outBinWidth, uint* outBinHeight);
	}

	public unsafe void ComputeBinWidthHeight(uint nBinsW, uint nBinsH, out uint outBinWidth, out uint outBinHeight)
	{
		fixed (uint* outBinWidth2 = &outBinWidth)
		{
			fixed (uint* outBinHeight2 = &outBinHeight)
			{
				ComputeBinWidthHeight(nBinsW, nBinsH, outBinWidth2, outBinHeight2);
			}
		}
	}

	public unsafe void SetNearClipPlane(float nearDist)
	{
		PInvoke(_handle, nearDist);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_SetNearClipPlane", ExactSpelling = true)]
		static extern unsafe void PInvoke(MaskedOcclusionCullingHandle* moc, float nearDist);
	}

	public unsafe readonly float GetNearClipPlane()
	{
		return PInvoke(_handle);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_GetNearClipPlane", ExactSpelling = true)]
		static extern unsafe float PInvoke(MaskedOcclusionCullingHandle* moc);
	}

	public unsafe void ClearBuffer()
	{
		PInvoke(_handle);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_ClearBuffer", ExactSpelling = true)]
		static extern unsafe float PInvoke(MaskedOcclusionCullingHandle* moc);
	}

	public unsafe void MergeBuffer(MaskedOcclusionCulling* BufferB)
	{
		PInvoke(_handle, BufferB);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_MergeBuffer", ExactSpelling = true)]
		static extern unsafe float PInvoke(MaskedOcclusionCullingHandle* moc, MaskedOcclusionCulling* BufferB);
	}

	public unsafe CullingResult RenderTriangles(float* inVtx, uint* inTris, int nTris, float* modelToClipMatrix, BackfaceWinding bfWinding, ClipPlanes clipPlaneMask, VertexLayout* vtxLayout)
	{
		return PInvoke(_handle, inVtx, inTris, nTris, modelToClipMatrix, bfWinding, clipPlaneMask, vtxLayout);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_RenderTriangles", ExactSpelling = true)]
		static extern unsafe CullingResult PInvoke(MaskedOcclusionCullingHandle* moc, float* inVtx, uint* inTris, int nTris, float* modelToClipMatrix, BackfaceWinding bfWinding, ClipPlanes clipPlaneMask, VertexLayout* vtxLayout);
	}

	public unsafe CullingResult RenderTriangles(float* inVtx, uint* inTris, int nTris, float* modelToClipMatrix = null, BackfaceWinding bfWinding = BackfaceWinding.Clockwise, ClipPlanes clipPlaneMask = ClipPlanes.All)
	{
		VertexLayout vertexLayout = new VertexLayout(16, 4, 12);
		return RenderTriangles(inVtx, inTris, nTris, modelToClipMatrix, bfWinding, clipPlaneMask, &vertexLayout);
	}

	public unsafe CullingResult TestRect(float xmin, float ymin, float xmax, float ymax, float wmin)
	{
		return PInvoke(_handle, xmin, ymin, xmax, ymax, wmin);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_TestRect", ExactSpelling = true)]
		static extern unsafe CullingResult PInvoke(MaskedOcclusionCullingHandle* moc, float xmin, float ymin, float xmax, float ymax, float wmin);
	}

	public unsafe CullingResult TestTriangles(float* inVtx, uint* inTris, int nTris, float* modelToClipMatrix, BackfaceWinding bfWinding, ClipPlanes clipPlaneMask, VertexLayout* vtxLayout)
	{
		return PInvoke(_handle, inVtx, inTris, nTris, modelToClipMatrix, bfWinding, clipPlaneMask, vtxLayout);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_TestTriangles", ExactSpelling = true)]
		static extern unsafe CullingResult PInvoke(MaskedOcclusionCullingHandle* moc, float* inVtx, uint* inTris, int nTris, float* modelToClipMatrix, BackfaceWinding bfWinding, ClipPlanes clipPlaneMask, VertexLayout* vtxLayout);
	}

	public unsafe CullingResult TestTriangles(float* inVtx, uint* inTris, int nTris, float* modelToClipMatrix = null, BackfaceWinding bfWinding = BackfaceWinding.Clockwise, ClipPlanes clipPlaneMask = ClipPlanes.All)
	{
		VertexLayout vertexLayout = new VertexLayout(16, 4, 12);
		return TestTriangles(inVtx, inTris, nTris, modelToClipMatrix, bfWinding, clipPlaneMask, &vertexLayout);
	}

	public unsafe void BinTriangles(float* inVtx, uint* inTris, int nTris, TriList* triLists, uint nBinsW, uint nBinsH, float* modelToClipMatrix, BackfaceWinding bfWinding, ClipPlanes clipPlaneMask, VertexLayout* vtxLayout)
	{
		PInvoke(_handle, inVtx, inTris, nTris, triLists, nBinsW, nBinsH, modelToClipMatrix, bfWinding, clipPlaneMask, vtxLayout);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_BinTriangles", ExactSpelling = true)]
		static extern unsafe CullingResult PInvoke(MaskedOcclusionCullingHandle* moc, float* inVtx, uint* inTris, int nTris, TriList* triLists, uint nBinsW, uint nBinsH, float* modelToClipMatrix, BackfaceWinding bfWinding, ClipPlanes clipPlaneMask, VertexLayout* vtxLayout);
	}

	public unsafe void BinTriangles(float* inVtx, uint* inTris, int nTris, TriList* triLists, uint nBinsW, uint nBinsH, float* modelToClipMatrix = null, BackfaceWinding bfWinding = BackfaceWinding.Clockwise, ClipPlanes clipPlaneMask = ClipPlanes.All)
	{
		VertexLayout vertexLayout = new VertexLayout(16, 4, 12);
		BinTriangles(inVtx, inTris, nTris, triLists, nBinsW, nBinsH, modelToClipMatrix, bfWinding, clipPlaneMask, &vertexLayout);
	}

	public unsafe void RenderTriList(TriList* triList, ScissorRect* scissor)
	{
		PInvoke(_handle, triList, scissor);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_RenderTrilist", ExactSpelling = true)]
		static extern unsafe void PInvoke(MaskedOcclusionCullingHandle* moc, TriList* triList, ScissorRect* scissor);
	}

	public unsafe void ComputePixelDepthBuffer(float* depthData, bool flipY)
	{
		PInvoke(_handle, depthData, flipY ? ((byte)1) : ((byte)0));
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_ComputePixelDepthBuffer", ExactSpelling = true)]
		static extern unsafe void PInvoke(MaskedOcclusionCullingHandle* moc, float* depthData, byte flipY);
	}

	public unsafe void GetStatistics(OcclusionCullingStatistics* stats)
	{
		PInvoke(_handle, stats);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_GetStatistics", ExactSpelling = true)]
		static extern unsafe void PInvoke(MaskedOcclusionCullingHandle* moc, OcclusionCullingStatistics* stats);
	}

	public unsafe Implementation GetImplementation()
	{
		return PInvoke(_handle);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_GetImplementation", ExactSpelling = true)]
		static extern unsafe Implementation PInvoke(MaskedOcclusionCullingHandle* moc);
	}

	[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_TransformVertices", ExactSpelling = true)]
	public unsafe static extern void TransformVertices(float* mtx, float* inVtx, float* xfVtx, uint nVtx, VertexLayout* vtxLayout);

	public unsafe static void TransformVertices(float* mtx, float* inVtx, float* xfVtx, uint nVtx)
	{
		VertexLayout vertexLayout = new VertexLayout(12, 4, 8);
		TransformVertices(mtx, inVtx, xfVtx, nVtx, &vertexLayout);
	}

	public unsafe void GetAllocFreeCallback(delegate* unmanaged[Cdecl]<nuint, nuint, void*>* alignedAlloc, delegate* unmanaged[Cdecl]<void*, void>* alignedFree)
	{
		PInvoke(_handle, alignedAlloc, alignedFree);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_GetAllocFreeCallback", ExactSpelling = true)]
		static extern unsafe void PInvoke(MaskedOcclusionCullingHandle* moc, delegate* unmanaged[Cdecl]<nuint, nuint, void*>* alignedAlloc, delegate* unmanaged[Cdecl]<void*, void>* alignedFree);
	}

	public unsafe bool RecorderStart(sbyte* outputFilePath)
	{
		return PInvoke(_handle, outputFilePath) != 0;
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_RecorderStart", ExactSpelling = true)]
		static extern unsafe byte PInvoke(MaskedOcclusionCullingHandle* moc, sbyte* outputFilePath);
	}

	public unsafe bool RecorderStart(string outputFilePath)
	{
		IntPtr intPtr = Marshal.StringToCoTaskMemUTF8(outputFilePath);
		try
		{
			return RecorderStart((sbyte*)(void*)intPtr);
		}
		finally
		{
			Marshal.FreeCoTaskMem(intPtr);
		}
	}

	public unsafe readonly void RecorderStop()
	{
		PInvoke(_handle);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_RecorderStop", ExactSpelling = true)]
		static extern unsafe void PInvoke(MaskedOcclusionCullingHandle* moc);
	}

	public unsafe void RecordRenderTriangles(float* inVtx, uint* inTris, int nTris, float* modelToClipMatrix, ClipPlanes clipPlaneMask, BackfaceWinding bfWinding, VertexLayout* vtxLayout, CullingResult cullingResult = (CullingResult)(-1))
	{
		PInvoke(_handle, inVtx, inTris, nTris, modelToClipMatrix, clipPlaneMask, bfWinding, vtxLayout, cullingResult);
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_RecordRenderTriangles", ExactSpelling = true)]
		static extern unsafe void PInvoke(MaskedOcclusionCullingHandle* moc, float* inVtx, uint* inTris, int nTris, float* modelToClipMatrix, ClipPlanes clipPlaneMask, BackfaceWinding bfWinding, VertexLayout* vtxLayout, CullingResult cullingResult);
	}

	public unsafe void RecordRenderTriangles(float* inVtx, uint* inTris, int nTris, float* modelToClipMatrix, ClipPlanes clipPlaneMask, BackfaceWinding bfWinding)
	{
		VertexLayout vertexLayout = new VertexLayout(16, 4, 12);
		RecordRenderTriangles(inVtx, inTris, nTris, modelToClipMatrix, clipPlaneMask, bfWinding, &vertexLayout);
	}

	public unsafe void Dispose()
	{
		fixed (MaskedOcclusionCullingHandle** handle = &_handle)
		{
			IntPtr intPtr = Interlocked.Exchange(ref *(IntPtr*)handle, (IntPtr)0);
			if (intPtr != (IntPtr)0)
			{
				Destroy((MaskedOcclusionCullingHandle*)(void*)intPtr);
			}
		}
		[DllImport("MaskedOcclusionCullingNative", EntryPoint = "MaskedOcclusionCulling_Destroy", ExactSpelling = true)]
		static extern unsafe void Destroy(MaskedOcclusionCullingHandle* moc);
	}
}
