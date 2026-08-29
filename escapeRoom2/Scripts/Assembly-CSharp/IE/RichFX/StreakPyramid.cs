using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace IE.RichFX
{
	internal sealed class StreakPyramid
	{
		public const int MaxMipLevel = 16;

		private int _baseWidth;

		private int _baseHeight;

		private readonly (RTHandle down, RTHandle up)[] _mips = new(RTHandle, RTHandle)[16];

		public (RTHandle down, RTHandle up) this[int index] => _mips[index];

		public StreakPyramid(HDCamera camera)
		{
			Allocate(camera);
		}

		public bool CheckSize(HDCamera camera)
		{
			if (_baseWidth == camera.actualWidth)
			{
				return _baseHeight == camera.actualHeight;
			}
			return false;
		}

		public void Reallocate(HDCamera camera)
		{
			Release();
			Allocate(camera);
		}

		public void Release()
		{
			(RTHandle, RTHandle)[] mips = _mips;
			for (int i = 0; i < mips.Length; i++)
			{
				(RTHandle, RTHandle) tuple = mips[i];
				if (tuple.Item1 != null)
				{
					RTHandles.Release(tuple.Item1);
				}
				if (tuple.Item2 != null)
				{
					RTHandles.Release(tuple.Item2);
				}
			}
		}

		private void Allocate(HDCamera camera)
		{
			_baseWidth = camera.actualWidth;
			_baseHeight = camera.actualHeight;
			int num = _baseWidth;
			int height = _baseHeight / 2;
			_mips[0] = (down: RTHandles.Alloc(num, height, 1, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat), up: null);
			for (int i = 1; i < 16; i++)
			{
				num /= 2;
				_mips[i] = ((num < 4) ? (down: null, up: null) : (down: RTHandles.Alloc(num, height, 1, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat), up: RTHandles.Alloc(num, height, 1, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat)));
			}
		}
	}
}
