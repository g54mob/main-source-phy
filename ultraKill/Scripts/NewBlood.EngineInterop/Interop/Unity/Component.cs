using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Interop.Unity
{
	[NonCopyable]
	[SupportsInheritance]
	public struct Component : Component.Interface, IUpCastable<Component>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
	{
		public interface Interface : IUpCastable<Component>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
		{
		}

		[BaseField]
		private EditorExtension __EditorExtension;

		public ImmediatePtr<GameObject> m_GameObject;

		ref Component IUpCastable<Component>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref EditorExtension IUpCastable<EditorExtension>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __EditorExtension, 1));
		}

		ref Object IUpCastable<Object>.Cast()
		{
			return ref CastOperations.UpCast<EditorExtension, Object>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __EditorExtension, 1)));
		}
	}
}
