using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace XGamingRuntime.Interop
{
	public static class Converters
	{
		public static IntPtr Offset(this IntPtr ptr, long that)
		{
			return new IntPtr(ptr.ToInt64() + that);
		}

		public static int GetSizeRequiredToEncodeStringToUTF8(string str)
		{
			return Encoding.UTF8.GetByteCount(str) + Encoding.UTF8.GetPreamble().Length;
		}

		public static DisposableBuffer StringArrayToUTF8StringArray(string[] strings)
		{
			if (strings == null)
			{
				return new DisposableBuffer();
			}
			List<byte[]> list = new List<byte[]>(strings.Length);
			int num = 0;
			checked
			{
				foreach (string str in strings)
				{
					byte[] array = StringToNullTerminatedUTF8ByteArray(str);
					list.Add(array);
					if (array != null)
					{
						num += array.Length;
					}
				}
				int num2 = Marshal.SizeOf(typeof(IntPtr));
				int num3 = num2 * strings.Length;
				num += num3;
				DisposableBuffer disposableBuffer = new DisposableBuffer(num);
				IntPtr ptr = disposableBuffer.IntPtr;
				IntPtr intPtr = ptr.Offset(num3);
				foreach (byte[] item in list)
				{
					if (item != null)
					{
						Marshal.WriteIntPtr(ptr, intPtr);
						Marshal.Copy(item, 0, intPtr, item.Length);
						intPtr = intPtr.Offset(item.Length);
					}
					else
					{
						Marshal.WriteIntPtr(ptr, IntPtr.Zero);
					}
					ptr = ptr.Offset(num2);
				}
				return disposableBuffer;
			}
		}

		public static IntPtr StringArrayToUTF8StringArray(string[] strings, DisposableCollection disposableCollection, out SizeT count)
		{
			if (strings == null)
			{
				count = new SizeT(0);
				return IntPtr.Zero;
			}
			count = new SizeT(strings.Length);
			return disposableCollection.Add(StringArrayToUTF8StringArray(strings)).IntPtr;
		}

		public static byte[] StringToNullTerminatedUTF8ByteArray(string str)
		{
			return StringToNullTerminatedUTF8ByteArrayInternal(str, -1);
		}

		public static byte[] StringToNullTerminatedUTF8ByteArray(string str, int requiredByteArrayLength)
		{
			return StringToNullTerminatedUTF8ByteArrayInternal(str, requiredByteArrayLength);
		}

		private static byte[] StringToNullTerminatedUTF8ByteArrayInternal(string str, int requiredByteArrayLength)
		{
			if (str == null)
			{
				return null;
			}
			if (requiredByteArrayLength == -1)
			{
				return Encoding.UTF8.GetBytes(str + '\0');
			}
			byte[] array = new byte[requiredByteArrayLength];
			Encoding.UTF8.GetBytes(str + '\0', 0, str.Length + 1, array, 0);
			return array;
		}

		public unsafe static void StringToNullTerminatedUTF8FixedPointer(string str, byte* bytePointer, int length)
		{
			byte[] source = StringToNullTerminatedUTF8ByteArray(str, length);
			Marshal.Copy(source, 0, (IntPtr)bytePointer, length);
		}

		public unsafe static string BytePointerToString(byte* bytePointer, int length)
		{
			byte[] array = new byte[length];
			byte[] destination = array;
			Marshal.Copy((IntPtr)bytePointer, destination, 0, length);
			return ByteArrayToString(array);
		}

		public unsafe static string NullTerminatedBytePointerToString(byte* bytePointer)
		{
			int num = 0;
			byte* ptr = bytePointer;
			while (*ptr != 0)
			{
				ptr++;
				num++;
			}
			return BytePointerToString(bytePointer, num);
		}

		public static string ByteArrayToString(byte[] arr)
		{
			string text = Encoding.UTF8.GetString(arr);
			int num = text.IndexOf('\0');
			return (num < 0) ? text : text.Substring(0, num);
		}

		public static string ByteArrayToString(byte[] arr, int index, int count)
		{
			return Encoding.UTF8.GetString(arr, index, count).TrimEnd(default(char));
		}

		public static string PtrToStringUTF8(IntPtr rawPtr)
		{
			if (rawPtr == IntPtr.Zero)
			{
				return null;
			}
			List<byte> list = new List<byte>();
			while (true)
			{
				byte b = Marshal.ReadByte(rawPtr);
				if (b == 0)
				{
					break;
				}
				list.Add(b);
				rawPtr = rawPtr.Offset(1L);
			}
			return Encoding.UTF8.GetString(list.ToArray());
		}

		public static ClassType PtrToClass<ClassType, InteropStructType>(IntPtr rawPtr, Func<InteropStructType, ClassType> ctor) where ClassType : class where InteropStructType : struct
		{
			if (rawPtr == IntPtr.Zero)
			{
				return (ClassType)null;
			}
			return ctor((InteropStructType)Marshal.PtrToStructure(rawPtr, typeof(InteropStructType)));
		}

		public static ClassType[] PtrToClassArray<ClassType, InteropStructType>(IntPtr rawPtr, SizeT count, Func<InteropStructType, ClassType> ctor)
		{
			return PtrToClassArray(rawPtr, count.ToUInt32(), ctor);
		}

		public static ClassType[] PtrToClassArray<ClassType, InteropStructType>(IntPtr rawPtr, uint count, Func<InteropStructType, ClassType> ctor)
		{
			ClassType[] array = new ClassType[count];
			if (IntPtr.Zero != rawPtr)
			{
				int num = Marshal.SizeOf(typeof(InteropStructType));
				for (int i = 0; i < count; i++)
				{
					InteropStructType arg = (InteropStructType)Marshal.PtrToStructure(rawPtr.Offset(i * num), typeof(InteropStructType));
					array[i] = ctor(arg);
				}
			}
			return array;
		}

		public static IntPtr ClassArrayToPtr<ClassType, InteropStructType>(ClassType[] inputTypes, Func<ClassType, DisposableCollection, InteropStructType> converter, DisposableCollection disposableCollection, out uint arrayCount)
		{
			SizeT arrayCount2;
			IntPtr result = ClassArrayToPtr(inputTypes, converter, disposableCollection, out arrayCount2);
			arrayCount = arrayCount2.ToUInt32();
			return result;
		}

		public static IntPtr ClassArrayToPtr<ClassType, InteropStructType>(ClassType[] inputTypes, Func<ClassType, DisposableCollection, InteropStructType> converter, DisposableCollection disposableCollection, out SizeT arrayCount)
		{
			if (inputTypes == null)
			{
				arrayCount = new SizeT(0);
				return IntPtr.Zero;
			}
			bool isEnum = typeof(InteropStructType).IsEnum;
			int num = Marshal.SizeOf((!isEnum) ? typeof(InteropStructType) : Enum.GetUnderlyingType(typeof(InteropStructType)));
			DisposableBuffer disposableBuffer = disposableCollection.Add(new DisposableBuffer(checked(num * inputTypes.Length)));
			IntPtr ptr = disposableBuffer.IntPtr;
			foreach (ClassType arg in inputTypes)
			{
				object structure = ((!isEnum) ? ((object)converter(arg, disposableCollection)) : Convert.ChangeType(converter(arg, disposableCollection), Enum.GetUnderlyingType(typeof(InteropStructType))));
				Marshal.StructureToPtr(structure, ptr, false);
				ptr = ptr.Offset(num);
			}
			arrayCount = new SizeT(inputTypes.Length);
			return disposableBuffer.IntPtr;
		}

		public static InteropStructType[] ConvertArrayToFixedLength<ClassType, InteropStructType>(ClassType[] classes, int length, Func<ClassType, InteropStructType> ctor)
		{
			InteropStructType[] array = new InteropStructType[length];
			int num = Math.Min(length, classes.Length);
			for (int i = 0; i < num; i++)
			{
				array[i] = ctor(classes[i]);
			}
			return array;
		}
	}
}
