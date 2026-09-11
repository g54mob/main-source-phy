using System.Runtime.InteropServices;
using Interop.Unity;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[SupportsInheritance]
	public struct AudioBehaviour : AudioBehaviour.Interface, IUpCastable<AudioBehaviour>, Behaviour.Interface, IUpCastable<Behaviour>, Component.Interface, IUpCastable<Component>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
	{
		public interface Interface : IUpCastable<AudioBehaviour>, Behaviour.Interface, IUpCastable<Behaviour>, Component.Interface, IUpCastable<Component>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
		{
		}

		[BaseField]
		private Behaviour __Behaviour;

		ref AudioBehaviour IUpCastable<AudioBehaviour>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref Behaviour IUpCastable<Behaviour>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Behaviour, 1));
		}

		ref Component IUpCastable<Component>.Cast()
		{
			return ref CastOperations.UpCast<Behaviour, Component>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Behaviour, 1)));
		}

		ref EditorExtension IUpCastable<EditorExtension>.Cast()
		{
			return ref CastOperations.UpCast<Behaviour, EditorExtension>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Behaviour, 1)));
		}

		ref Object IUpCastable<Object>.Cast()
		{
			return ref CastOperations.UpCast<Behaviour, Object>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Behaviour, 1)));
		}
	}
}
