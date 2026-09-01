using System;
using System.Runtime.InteropServices;

namespace RakNet
{
	public class IncrementalReadInterface : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		internal IncrementalReadInterface(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(IncrementalReadInterface obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~IncrementalReadInterface()
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
						RakNetPINVOKE.delete_IncrementalReadInterface(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		public IncrementalReadInterface()
			: this(RakNetPINVOKE.new_IncrementalReadInterface(), true)
		{
		}

		public uint GetFilePart(string filename, uint startReadBytes, uint numBytesToRead, byte[] inOutByteArray, FileListNodeContext context)
		{
			uint result = RakNetPINVOKE.IncrementalReadInterface_GetFilePart(swigCPtr, filename, startReadBytes, numBytesToRead, inOutByteArray, FileListNodeContext.getCPtr(context));
			if (RakNetPINVOKE.SWIGPendingException.Pending)
			{
				throw RakNetPINVOKE.SWIGPendingException.Retrieve();
			}
			return result;
		}
	}
}
