using UnityEngine;

namespace HTraceAO.Scripts.Wrappers
{
	public class HDynamicBuffer
	{
		private ComputeBuffer _computeBuffer;

		private GraphicsBuffer _graphicsBuffer;

		private readonly BufferType _bufferType;

		private readonly int _stride;

		private int _count;

		private int _countScale;

		private Vector2Int _resolution;

		private readonly ComputeBufferType _computeBufferType;

		private readonly GraphicsBuffer.Target _graphicsBufferType;

		private readonly bool _avoidDownscale;

		public ComputeBuffer ComputeBuffer => null;

		public GraphicsBuffer GraphicsBuffer => null;

		public int Count => 0;

		public Vector2Int Resolution => default(Vector2Int);

		public bool IsCreated => false;

		public HDynamicBuffer(BufferType bufferType, int stride, int countScale = 1, ComputeBufferType computeBufferType = ComputeBufferType.Default, GraphicsBuffer.Target graphicsBufferType = GraphicsBuffer.Target.Structured, bool avoidDownscale = false)
		{
		}

		public void ReAllocIfNeeded(Vector2Int newResolution)
		{
		}

		public void SetBuffer(ComputeShader shader, string name, int kernelIndex)
		{
		}

		public void Release()
		{
		}
	}
}
