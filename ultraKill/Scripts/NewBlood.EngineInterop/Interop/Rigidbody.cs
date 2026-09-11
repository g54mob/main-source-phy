using System.Runtime.InteropServices;
using Interop.Unity;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[IncompleteType]
	[SupportsInheritance]
	public struct Rigidbody : Rigidbody.Interface, IUpCastable<Rigidbody>, Component.Interface, IUpCastable<Component>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
	{
		public interface Interface : IUpCastable<Rigidbody>, Component.Interface, IUpCastable<Component>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
		{
		}

		[BaseField]
		private Component __Component;

		ref Rigidbody IUpCastable<Rigidbody>.Cast()
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
