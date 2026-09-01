using System;
using System.Runtime.InteropServices;

namespace RakNet
{
	public class OnFileStruct : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		private bool fileDataIsCached = false;

		private byte[] fileDataCache;

		public uint fileIndex
		{
			get
			{
				return RakNetPINVOKE.OnFileStruct_fileIndex_get(swigCPtr);
			}
			set
			{
				RakNetPINVOKE.OnFileStruct_fileIndex_set(swigCPtr, value);
			}
		}

		public string fileName
		{
			get
			{
				return RakNetPINVOKE.OnFileStruct_fileName_get(swigCPtr);
			}
			set
			{
				RakNetPINVOKE.OnFileStruct_fileName_set(swigCPtr, value);
			}
		}

		public byte[] fileData
		{
			get
			{
				byte[] array;
				if (!fileDataIsCached)
				{
					IntPtr source = RakNetPINVOKE.OnFileStruct_fileData_get(swigCPtr);
					int num = (int)byteLengthOfThisFile;
					if (num <= 0)
					{
						return null;
					}
					array = new byte[num];
					byte[] array2 = new byte[num];
					Marshal.Copy(source, array2, 0, num);
					array2.CopyTo(array, 0);
					fileDataCache = array;
					fileDataIsCached = true;
				}
				else
				{
					array = fileDataCache;
				}
				return array;
			}
			set
			{
				fileDataCache = value;
				fileDataIsCached = true;
				SetFileData(value, value.Length);
			}
		}

		public uint byteLengthOfThisFile
		{
			get
			{
				return RakNetPINVOKE.OnFileStruct_byteLengthOfThisFile_get(swigCPtr);
			}
			set
			{
				RakNetPINVOKE.OnFileStruct_byteLengthOfThisFile_set(swigCPtr, value);
			}
		}

		public uint bytesDownloadedForThisFile
		{
			get
			{
				return RakNetPINVOKE.OnFileStruct_bytesDownloadedForThisFile_get(swigCPtr);
			}
			set
			{
				RakNetPINVOKE.OnFileStruct_bytesDownloadedForThisFile_set(swigCPtr, value);
			}
		}

		public ushort setID
		{
			get
			{
				return RakNetPINVOKE.OnFileStruct_setID_get(swigCPtr);
			}
			set
			{
				RakNetPINVOKE.OnFileStruct_setID_set(swigCPtr, value);
			}
		}

		public uint numberOfFilesInThisSet
		{
			get
			{
				return RakNetPINVOKE.OnFileStruct_numberOfFilesInThisSet_get(swigCPtr);
			}
			set
			{
				RakNetPINVOKE.OnFileStruct_numberOfFilesInThisSet_set(swigCPtr, value);
			}
		}

		public uint byteLengthOfThisSet
		{
			get
			{
				return RakNetPINVOKE.OnFileStruct_byteLengthOfThisSet_get(swigCPtr);
			}
			set
			{
				RakNetPINVOKE.OnFileStruct_byteLengthOfThisSet_set(swigCPtr, value);
			}
		}

		public uint bytesDownloadedForThisSet
		{
			get
			{
				return RakNetPINVOKE.OnFileStruct_bytesDownloadedForThisSet_get(swigCPtr);
			}
			set
			{
				RakNetPINVOKE.OnFileStruct_bytesDownloadedForThisSet_set(swigCPtr, value);
			}
		}

		public FileListNodeContext context
		{
			get
			{
				IntPtr intPtr = RakNetPINVOKE.OnFileStruct_context_get(swigCPtr);
				return (intPtr == IntPtr.Zero) ? null : new FileListNodeContext(intPtr, false);
			}
			set
			{
				RakNetPINVOKE.OnFileStruct_context_set(swigCPtr, FileListNodeContext.getCPtr(value));
			}
		}

		public SystemAddress senderSystemAddress
		{
			get
			{
				IntPtr intPtr = RakNetPINVOKE.OnFileStruct_senderSystemAddress_get(swigCPtr);
				return (intPtr == IntPtr.Zero) ? null : new SystemAddress(intPtr, false);
			}
			set
			{
				RakNetPINVOKE.OnFileStruct_senderSystemAddress_set(swigCPtr, SystemAddress.getCPtr(value));
			}
		}

		public RakNetGUID senderGuid
		{
			get
			{
				IntPtr intPtr = RakNetPINVOKE.OnFileStruct_senderGuid_get(swigCPtr);
				return (intPtr == IntPtr.Zero) ? null : new RakNetGUID(intPtr, false);
			}
			set
			{
				RakNetPINVOKE.OnFileStruct_senderGuid_set(swigCPtr, RakNetGUID.getCPtr(value));
			}
		}

		internal OnFileStruct(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(OnFileStruct obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~OnFileStruct()
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
						RakNetPINVOKE.delete_OnFileStruct(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		public OnFileStruct()
			: this(RakNetPINVOKE.new_OnFileStruct(), true)
		{
		}

		private void SetFileData(byte[] inByteArray, int numBytes)
		{
			RakNetPINVOKE.OnFileStruct_SetFileData(swigCPtr, inByteArray, numBytes);
		}
	}
}
