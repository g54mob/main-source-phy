using System;
using System.Runtime.InteropServices;

namespace RakNet
{
	public class NatTypeDetectionClient : PluginInterface2
	{
		private HandleRef swigCPtr;

		internal NatTypeDetectionClient(IntPtr cPtr, bool cMemoryOwn)
			: base(RakNetPINVOKE.NatTypeDetectionClient_SWIGUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(NatTypeDetectionClient obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~NatTypeDetectionClient()
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
						RakNetPINVOKE.delete_NatTypeDetectionClient(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		public static NatTypeDetectionClient GetInstance()
		{
			IntPtr intPtr = RakNetPINVOKE.NatTypeDetectionClient_GetInstance();
			return (intPtr == IntPtr.Zero) ? null : new NatTypeDetectionClient(intPtr, false);
		}

		public static void DestroyInstance(NatTypeDetectionClient i)
		{
			RakNetPINVOKE.NatTypeDetectionClient_DestroyInstance(getCPtr(i));
		}

		public NatTypeDetectionClient()
			: this(RakNetPINVOKE.new_NatTypeDetectionClient(), true)
		{
		}

		public void DetectNATType(SystemAddress _serverAddress)
		{
			RakNetPINVOKE.NatTypeDetectionClient_DetectNATType(swigCPtr, SystemAddress.getCPtr(_serverAddress));
			if (RakNetPINVOKE.SWIGPendingException.Pending)
			{
				throw RakNetPINVOKE.SWIGPendingException.Retrieve();
			}
		}

		public virtual void OnRNS2Recv(SWIGTYPE_p_RNS2RecvStruct recvStruct)
		{
			RakNetPINVOKE.NatTypeDetectionClient_OnRNS2Recv(swigCPtr, SWIGTYPE_p_RNS2RecvStruct.getCPtr(recvStruct));
		}

		public virtual void DeallocRNS2RecvStruct(SWIGTYPE_p_RNS2RecvStruct s, string file, uint line)
		{
			RakNetPINVOKE.NatTypeDetectionClient_DeallocRNS2RecvStruct(swigCPtr, SWIGTYPE_p_RNS2RecvStruct.getCPtr(s), file, line);
		}

		public virtual SWIGTYPE_p_RNS2RecvStruct AllocRNS2RecvStruct(string file, uint line)
		{
			IntPtr intPtr = RakNetPINVOKE.NatTypeDetectionClient_AllocRNS2RecvStruct(swigCPtr, file, line);
			return (intPtr == IntPtr.Zero) ? null : new SWIGTYPE_p_RNS2RecvStruct(intPtr, false);
		}
	}
}
