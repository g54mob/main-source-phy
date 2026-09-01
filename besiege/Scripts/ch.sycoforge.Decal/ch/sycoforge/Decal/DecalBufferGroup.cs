using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ch.sycoforge.Decal
{
	internal class DecalBufferGroup
	{
		public RenderTargetIdentifier[] RenderTargets;

		public RenderTargetIdentifier[] RenderTargetsHDR;

		public HashSet<EasyDecal> Decals;

		public DeferredFlags Flags;

		public int PassIndex;

		public RenderTargetIdentifier[] GetTargets(Camera camera)
		{
			if (!camera.hdr)
			{
				return RenderTargets;
			}
			return RenderTargetsHDR;
		}

		public static int GetPassIndex(DeferredFlags flags)
		{
			int result = -1;
			switch (flags)
			{
			case DeferredFlags.Diffuse | DeferredFlags.Normal | DeferredFlags.SpecSmooth:
				result = 0;
				break;
			case DeferredFlags.Diffuse | DeferredFlags.SpecSmooth:
				result = 1;
				break;
			case DeferredFlags.Diffuse | DeferredFlags.Normal:
				result = 2;
				break;
			case DeferredFlags.Normal | DeferredFlags.SpecSmooth:
				result = 3;
				break;
			case DeferredFlags.Diffuse:
				result = 4;
				break;
			case DeferredFlags.SpecSmooth:
				result = 5;
				break;
			case DeferredFlags.Normal:
				result = 6;
				break;
			}
			return result;
		}

		public static RenderTargetIdentifier[] GetRenderTargets(DeferredFlags flags, bool hdr)
		{
			List<RenderTargetIdentifier> list = new List<RenderTargetIdentifier>();
			List<BuiltinRenderTextureType> list2 = new List<BuiltinRenderTextureType>();
			DeferredFlags[] array = Enum.GetValues(typeof(DeferredFlags)) as DeferredFlags[];
			foreach (DeferredFlags deferredFlags in array)
			{
				if ((flags & deferredFlags) > (DeferredFlags)0)
				{
					BuiltinRenderTextureType builtinRenderTextureType = FlagToTarget(deferredFlags);
					if (builtinRenderTextureType != BuiltinRenderTextureType.None)
					{
						list2.Add(builtinRenderTextureType);
					}
				}
			}
			list2.Sort(delegate(BuiltinRenderTextureType x, BuiltinRenderTextureType y)
			{
				int num = (int)x;
				return num.CompareTo((int)y);
			});
			BuiltinRenderTextureType item = (hdr ? BuiltinRenderTextureType.CameraTarget : BuiltinRenderTextureType.GBuffer3);
			list2.Add(item);
			foreach (BuiltinRenderTextureType item2 in list2)
			{
				list.Add(item2);
			}
			return list.ToArray();
		}

		private static BuiltinRenderTextureType FlagToTarget(DeferredFlags flag)
		{
			BuiltinRenderTextureType result = BuiltinRenderTextureType.None;
			switch (flag)
			{
			case DeferredFlags.Diffuse:
				result = BuiltinRenderTextureType.GBuffer0;
				break;
			case DeferredFlags.SpecSmooth:
				result = BuiltinRenderTextureType.GBuffer1;
				break;
			case DeferredFlags.Normal:
				result = BuiltinRenderTextureType.GBuffer2;
				break;
			}
			return result;
		}
	}
}
