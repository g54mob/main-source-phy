using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace TND.Upscaling.Framework
{
	public readonly struct TextureRef
	{
		public delegate Texture BlitterDelegate(CommandBuffer cmd, in RenderTextureDescriptor desc, in RenderTargetIdentifier source);

		public static readonly TextureRef Null;

		private readonly Texture _texture;

		private readonly RenderTargetIdentifier _renderTargetIdentifier;

		private readonly RenderTextureSubElement _renderTextureSubElement;

		private readonly RenderTextureDescriptor _renderTextureDescriptor;

		private readonly BlitterDelegate _blitter;

		public bool IsValid => false;

		public int Width => 0;

		public int Height => 0;

		public GraphicsFormat GraphicsFormat => default(GraphicsFormat);

		public TextureRef(Texture texture, RenderTextureSubElement renderTextureSubElement = RenderTextureSubElement.Default)
		{
			_texture = null;
			_renderTargetIdentifier = default(RenderTargetIdentifier);
			_renderTextureSubElement = default(RenderTextureSubElement);
			_renderTextureDescriptor = default(RenderTextureDescriptor);
			_blitter = null;
		}

		public TextureRef(in RenderTargetIdentifier renderTargetIdentifier, in RenderTextureDescriptor renderTextureDescriptor, BlitterDelegate blitter, RenderTextureSubElement renderTextureSubElement = RenderTextureSubElement.Default)
		{
			_texture = null;
			_renderTargetIdentifier = default(RenderTargetIdentifier);
			_renderTextureSubElement = default(RenderTextureSubElement);
			_renderTextureDescriptor = default(RenderTextureDescriptor);
			_blitter = null;
		}

		public RenderTargetIdentifier GetRenderTargetIdentifier(int depthSlice = 0)
		{
			return default(RenderTargetIdentifier);
		}

		public RenderTextureSubElement GetRenderTextureSubElement()
		{
			return default(RenderTextureSubElement);
		}

		public Texture GetTexture(CommandBuffer cmd)
		{
			return null;
		}

		public IntPtr GetNativeTexturePointer(CommandBuffer cmd)
		{
			return (IntPtr)0;
		}
	}
}
