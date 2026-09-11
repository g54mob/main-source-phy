using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace PrivateAPIBridge
{
	public static class CommandBufferExtensions
	{
		[SpecialName]
		public sealed class _003CG_003E_002428F2EE25A2DC8F2594DD5FD724DA7DB9
		{
			[SpecialName]
			public static class _003CM_003E_002468866811BD7A444FA968CCD23C53B92F
			{
			}

			[ExtensionMarker("<M>$68866811BD7A444FA968CCD23C53B92F")]
			public IntPtr GetPtr()
			{
				throw new NotSupportedException();
			}

			[ExtensionMarker("<M>$68866811BD7A444FA968CCD23C53B92F")]
			public void Internal_DrawRenderer(Renderer renderer, Material material, int submeshIndex, int shaderPass)
			{
				throw new NotSupportedException();
			}

			[ExtensionMarker("<M>$68866811BD7A444FA968CCD23C53B92F")]
			public void Internal_DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex, int shaderPass, MaterialPropertyBlock properties)
			{
				throw new NotSupportedException();
			}
		}

		public static IntPtr GetPtr(this CommandBuffer @this)
		{
			return @this.m_Ptr;
		}

		public static void Internal_DrawRenderer(this CommandBuffer @this, Renderer renderer, Material material, int submeshIndex, int shaderPass)
		{
			@this.Internal_DrawRenderer(renderer, material, submeshIndex, shaderPass);
		}

		public static void Internal_DrawMesh(this CommandBuffer @this, Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex, int shaderPass, MaterialPropertyBlock properties)
		{
			@this.Internal_DrawMesh(mesh, matrix, material, submeshIndex, shaderPass, properties);
		}
	}
}
