using System.Runtime.InteropServices;
using Interop.core;
using Microsoft.CodeAnalysis;
using UnityEngine;

namespace Interop
{
	[NonCopyable]
	[SupportsInheritance]
	public struct Flare : Flare.Interface, IUpCastable<Flare>, NamedObject.Interface, IUpCastable<NamedObject>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
	{
		public struct FlareElement
		{
			public uint m_ImageIndex;

			public float m_Position;

			public float m_Size;

			[NativeTypeName("ColorRGBAf")]
			public Color m_Color;

			[NativeTypeName("bool")]
			public byte m_UseLightColor;

			[NativeTypeName("bool")]
			public byte m_Rotate;

			[NativeTypeName("bool")]
			public byte m_Zoom;

			[NativeTypeName("bool")]
			public byte m_Fade;
		}

		public interface Interface : IUpCastable<Flare>, NamedObject.Interface, IUpCastable<NamedObject>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
		{
		}

		[BaseField]
		private NamedObject __NamedObject;

		public vector<FlareElement> m_Elements;

		public PPtr<Texture> m_FlareTexture;

		public int m_TextureLayout;

		[NativeTypeName("bool")]
		public byte m_UseFog;

		ref Flare IUpCastable<Flare>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref NamedObject IUpCastable<NamedObject>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __NamedObject, 1));
		}

		ref EditorExtension IUpCastable<EditorExtension>.Cast()
		{
			return ref CastOperations.UpCast<NamedObject, EditorExtension>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __NamedObject, 1)));
		}

		ref Object IUpCastable<Object>.Cast()
		{
			return ref CastOperations.UpCast<NamedObject, Object>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __NamedObject, 1)));
		}
	}
}
