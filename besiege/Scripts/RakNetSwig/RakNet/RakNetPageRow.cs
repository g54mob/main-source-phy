using System;
using System.Runtime.InteropServices;

namespace RakNet
{
	public class RakNetPageRow : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public bool isLeaf
		{
			get
			{
				return RakNetPINVOKE.RakNetPageRow_isLeaf_get(swigCPtr);
			}
			set
			{
				RakNetPINVOKE.RakNetPageRow_isLeaf_set(swigCPtr, value);
			}
		}

		public int size
		{
			get
			{
				return RakNetPINVOKE.RakNetPageRow_size_get(swigCPtr);
			}
			set
			{
				RakNetPINVOKE.RakNetPageRow_size_set(swigCPtr, value);
			}
		}

		public RakNetPageRow next
		{
			get
			{
				IntPtr intPtr = RakNetPINVOKE.RakNetPageRow_next_get(swigCPtr);
				return (intPtr == IntPtr.Zero) ? null : new RakNetPageRow(intPtr, false);
			}
			set
			{
				RakNetPINVOKE.RakNetPageRow_next_set(swigCPtr, getCPtr(value));
			}
		}

		public RakNetPageRow previous
		{
			get
			{
				IntPtr intPtr = RakNetPINVOKE.RakNetPageRow_previous_get(swigCPtr);
				return (intPtr == IntPtr.Zero) ? null : new RakNetPageRow(intPtr, false);
			}
			set
			{
				RakNetPINVOKE.RakNetPageRow_previous_set(swigCPtr, getCPtr(value));
			}
		}

		internal RakNetPageRow(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(RakNetPageRow obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~RakNetPageRow()
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
						RakNetPINVOKE.delete_RakNetPageRow(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		public RakNetPageRow()
			: this(RakNetPINVOKE.new_RakNetPageRow(), true)
		{
		}
	}
}
