using System.Runtime.CompilerServices;
using Interop;
using Interop.Unity;

public static class InteropObjectExtensions
{
	public unsafe static Type* GetTypeVirtualInternal<T>(this ref T @this) where T : unmanaged, Object.Interface
	{
		Object.Vtbl<T>* _vftable = (Object.Vtbl<T>*)CastOperations.UpCasts.static_cast<T, Object>(ref @this).__vftable;
		return _vftable->GetTypeVirtualInternal((T*)Unsafe.AsPointer(ref @this));
	}
}
