using System.Runtime.InteropServices;
using Interop.Unity;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[SupportsInheritance]
	public struct Behaviour : Behaviour.Interface, IUpCastable<Behaviour>, Component.Interface, IUpCastable<Component>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
	{
		public interface Interface : IUpCastable<Behaviour>, Component.Interface, IUpCastable<Component>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
		{
		}

		[BaseField]
		private Component __Component;

		public byte m_Enabled;

		public byte m_IsAdded;

		ref Behaviour IUpCastable<Behaviour>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref Component IUpCastable<Component>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Component, 1));
		}

		ref EditorExtension IUpCastable<EditorExtension>.Cast()
		{
			return ref CastOperations.UpCast<Component, EditorExtension>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Component, 1)));
		}

		ref Object IUpCastable<Object>.Cast()
		{
			return ref CastOperations.UpCast<Component, Object>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Component, 1)));
		}
	}
}
