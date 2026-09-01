using System;
using System.Runtime.InteropServices;

namespace RakNet
{
	public class RemoteSystemIndex : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public uint index
		{
			get
			{
				return RakNetPINVOKE.RemoteSystemIndex_index_get(swigCPtr);
			}
			set
			{
				RakNetPINVOKE.RemoteSystemIndex_index_set(swigCPtr, value);
			}
		}

		public RemoteSystemIndex next
		{
			get
			{
				IntPtr intPtr = RakNetPINVOKE.RemoteSystemIndex_next_get(swigCPtr);
				return (intPtr == IntPtr.Zero) ? null : new RemoteSystemIndex(intPtr, false);
			}
			set
			{
				RakNetPINVOKE.RemoteSystemIndex_next_set(swigCPtr, getCPtr(value));
			}
		}

		internal RemoteSystemIndex(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(RemoteSystemIndex obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~RemoteSystemIndex()
		{
			Dispose();
		}

		public virtual void Dispose()
		{
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						RakNetPINVOKE.delete_RemoteSystemIndex(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		public RemoteSystemIndex()
			: this(RakNetPINVOKE.new_RemoteSystemIndex(), true)
		{
		}
	}
}
