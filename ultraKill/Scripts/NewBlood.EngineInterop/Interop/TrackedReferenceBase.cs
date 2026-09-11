using System.Runtime.InteropServices;

namespace Interop
{
	[SupportsInheritance]
	public struct TrackedReferenceBase : TrackedReferenceBase.Interface, IUpCastable<TrackedReferenceBase>
	{
		public interface Interface : IUpCastable<TrackedReferenceBase>
		{
		}

		public ScriptingGCHandle m_MonoObjectReference;

		ref TrackedReferenceBase IUpCastable<TrackedReferenceBase>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}
	}
}
