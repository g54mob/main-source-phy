using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[SupportsInheritance]
	public struct NamedObject : NamedObject.Interface, IUpCastable<NamedObject>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
	{
		public interface Interface : IUpCastable<NamedObject>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
		{
		}

		[BaseField]
		private EditorExtension __EditorExtension;

		public ConstantString m_Name;

		ref NamedObject IUpCastable<NamedObject>.Cast()
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
