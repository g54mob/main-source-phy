using System;
using System.Runtime.InteropServices;

namespace RakNet
{
	public class NatPunchthroughServer : PluginInterface2
	{
		private HandleRef swigCPtr;

		public ulong lastUpdate
		{
			get
			{
				return RakNetPINVOKE.NatPunchthroughServer_lastUpdate_get(swigCPtr);
			}
			set
			{
				RakNetPINVOKE.NatPunchthroughServer_lastUpdate_set(swigCPtr, value);
			}
		}

		internal NatPunchthroughServer(IntPtr cPtr, bool cMemoryOwn)
			: base(RakNetPINVOKE.NatPunchthroughServer_SWIGUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(NatPunchthroughServer obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~NatPunchthroughServer()
		{
			Dispose();
		}

		public override void Dispose()
		{
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						RakNetPINVOKE.delete_NatPunchthroughServer(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		public static NatPunchthroughServer GetInstance()
		{
			IntPtr intPtr = RakNetPINVOKE.NatPunchthroughServer_GetInstance();
			return (intPtr == IntPtr.Zero) ? null : new NatPunchthroughServer(intPtr, false);
		}

		public static void DestroyInstance(NatPunchthroughServer i)
		{
			RakNetPINVOKE.NatPunchthroughServer_DestroyInstance(getCPtr(i));
		}

		public NatPunchthroughServer()
			: this(RakNetPINVOKE.new_NatPunchthroughServer(), true)
		{
		}

		public void SetDebugInterface(NatPunchthroughServerDebugInterface i)
		{
			RakNetPINVOKE.NatPunchthroughServer_SetDebugInterface(swigCPtr, NatPunchthroughServerDebugInterface.getCPtr(i));
		}
	}
}
