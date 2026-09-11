using System.Runtime.InteropServices;

namespace Interop
{
	[SupportsInheritance]
	public struct ScriptingObjectPtr : ScriptingObjectPtr.Interface, IUpCastable<ScriptingObjectPtr>
	{
		public interface Interface : IUpCastable<ScriptingObjectPtr>
		{
		}

		[NativeTypeName("ScriptingBackendNativeObjectPtr")]
		public unsafe void* m_Target;

		ref ScriptingObjectPtr IUpCastable<ScriptingObjectPtr>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}
	}
}
