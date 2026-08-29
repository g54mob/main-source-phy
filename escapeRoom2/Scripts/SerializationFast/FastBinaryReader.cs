using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public struct FastBinaryReader : IDisposable
{
	private struct ReaderHandle
	{
		internal unsafe byte* BufferPointer;

		internal int Position;

		internal int Length;

		internal Allocator Allocator;
	}

	private unsafe ReaderHandle* Handle;

	public unsafe bool IsInitialized => Handle != null;

	public unsafe int Position
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return Handle->Position;
		}
	}

	public unsafe FastBinaryReader(byte[] buffer, int length = -1, int offset = 0, Allocator allocator = Allocator.Temp)
	{
		if (allocator == Allocator.None)
		{
			throw new NotSupportedException("Allocator.None cannot be used with managed source buffers.");
		}
		fixed (byte* buffer2 = buffer)
		{
			Handle = CreateHandle(buffer2, (length == -1) ? buffer.Length : length, offset, allocator);
		}
	}

	public unsafe FastBinaryReader(Span<byte> buffer, Allocator allocator = Allocator.Temp)
	{
		if (allocator == Allocator.None)
		{
			throw new NotSupportedException("Allocator.None cannot be used with managed source buffers.");
		}
		fixed (byte* buffer2 = buffer)
		{
			Handle = CreateHandle(buffer2, buffer.Length, 0, allocator);
		}
	}

	private unsafe static ReaderHandle* CreateHandle(byte* buffer, int length, int offset, Allocator allocator)
	{
		ReaderHandle* ptr = (ReaderHandle*)UnsafeUtility.Malloc(sizeof(ReaderHandle) + length, UnsafeUtility.AlignOf<byte>(), allocator);
		UnsafeUtility.MemCpy(ptr + 1, buffer + offset, length);
		ptr->BufferPointer = (byte*)ptr + sizeof(ReaderHandle);
		ptr->Position = 0;
		ptr->Length = length;
		ptr->Allocator = allocator;
		return ptr;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void Seek(int where)
	{
		Handle->Position = Math.Min(Handle->Length, where);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe bool TryBeginRead(int byteCount)
	{
		if (Handle->Position + byteCount > Handle->Length)
		{
			return false;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe bool TryBeginReadValue<T>(in T value) where T : unmanaged
	{
		int num = sizeof(T);
		if (Handle->Position + num > Handle->Length)
		{
			return false;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe bool TryBeginReadInternal(int bytes)
	{
		if (Handle->Position + bytes > Handle->Length)
		{
			return false;
		}
		return true;
	}

	public string ReadString()
	{
		int num = ReadInt32();
		if (num >= 0)
		{
			return ReadString(num);
		}
		return null;
	}

	public unsafe string ReadString(int length)
	{
		if (length == 0)
		{
			return string.Empty;
		}
		if (!TryBeginReadInternal(length * 2))
		{
			throw new OverflowException($"Reading past the end of the buffer. Attempted to read {length} chars.");
		}
		string text = "".PadRight(length);
		fixed (char* value = text)
		{
			ReadBytes((byte*)value, text.Length * 2);
		}
		return text;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe byte ReadByte()
	{
		if (!TryBeginReadInternal(1))
		{
			throw new OverflowException("Reading past the end of the buffer.");
		}
		return Handle->BufferPointer[Handle->Position++];
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public sbyte ReadSByte()
	{
		return ReadSafe<sbyte>(default(FastBinaryWriter.ForPrimitives));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool ReadBoolean()
	{
		return ReadSafe<bool>(default(FastBinaryWriter.ForPrimitives));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public char ReadChar()
	{
		return ReadSafe<char>(default(FastBinaryWriter.ForPrimitives));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public short ReadInt16()
	{
		return ReadSafe<short>(default(FastBinaryWriter.ForPrimitives));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int ReadInt32()
	{
		return ReadSafe<int>(default(FastBinaryWriter.ForPrimitives));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public long ReadInt64()
	{
		return ReadSafe<long>(default(FastBinaryWriter.ForPrimitives));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ushort ReadUInt16()
	{
		return ReadSafe<ushort>(default(FastBinaryWriter.ForPrimitives));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public uint ReadUInt32()
	{
		return ReadSafe<uint>(default(FastBinaryWriter.ForPrimitives));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ulong ReadUInt64()
	{
		return ReadSafe<ulong>(default(FastBinaryWriter.ForPrimitives));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public float ReadSingle()
	{
		return ReadSafe<float>(default(FastBinaryWriter.ForPrimitives));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double ReadDouble()
	{
		return ReadSafe<double>(default(FastBinaryWriter.ForPrimitives));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public decimal ReadDecimal()
	{
		return ReadSafe<decimal>(default(FastBinaryWriter.ForPrimitives));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T Read<T>(FastBinaryWriter.ForPrimitives unused = default(FastBinaryWriter.ForPrimitives)) where T : unmanaged
	{
		return ReadUnmanagedSafe<T>();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T ReadSafe<T>(FastBinaryWriter.ForPrimitives unused = default(FastBinaryWriter.ForPrimitives)) where T : unmanaged
	{
		return ReadUnmanagedSafe<T>();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T[] ReadArray<T>(FastBinaryWriter.ForPrimitives unused = default(FastBinaryWriter.ForPrimitives)) where T : unmanaged
	{
		return ReadArrayUnmanagedSafe<T>();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T[] ReadArraySafe<T>(FastBinaryWriter.ForPrimitives unused = default(FastBinaryWriter.ForPrimitives)) where T : unmanaged
	{
		return ReadArrayUnmanagedSafe<T>();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T Read<T>(FastBinaryWriter.ForEnums unused = default(FastBinaryWriter.ForEnums)) where T : unmanaged, Enum
	{
		return ReadUnmanagedSafe<T>();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T ReadSafe<T>(FastBinaryWriter.ForEnums unused = default(FastBinaryWriter.ForEnums)) where T : unmanaged, Enum
	{
		return ReadUnmanagedSafe<T>();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T[] ReadArray<T>(FastBinaryWriter.ForEnums unused = default(FastBinaryWriter.ForEnums)) where T : unmanaged, Enum
	{
		return ReadArrayUnmanagedSafe<T>();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public T[] ReadArraySafe<T>(FastBinaryWriter.ForEnums unused = default(FastBinaryWriter.ForEnums)) where T : unmanaged, Enum
	{
		return ReadArrayUnmanagedSafe<T>();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2 ReadVector2()
	{
		return ReadUnmanagedSafe<Vector2>();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector3 ReadVector3()
	{
		return ReadUnmanagedSafe<Vector3>();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector4 ReadVector4()
	{
		return ReadUnmanagedSafe<Vector4>();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Quaternion ReadQuaternion()
	{
		return ReadUnmanagedSafe<Quaternion>();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Color ReadColor()
	{
		return ReadUnmanagedSafe<Color>();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Ray ReadRay()
	{
		return ReadUnmanagedSafe<Ray>();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe T ReadUnmanaged<T>() where T : unmanaged
	{
		T result = default(T);
		ReadBytes((byte*)(&result), sizeof(T));
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe T ReadUnmanagedSafe<T>() where T : unmanaged
	{
		T result = default(T);
		ReadBytesSafe((byte*)(&result), sizeof(T));
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe T[] ReadArrayUnmanaged<T>() where T : unmanaged
	{
		int num = ReadUnmanaged<int>();
		int size = num * sizeof(T);
		T[] array = new T[num];
		fixed (T* ptr = array)
		{
			byte* value = (byte*)ptr;
			ReadBytes(value, size);
		}
		return array;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe T[] ReadArrayUnmanagedSafe<T>() where T : unmanaged
	{
		int num = ReadUnmanagedSafe<int>();
		int size = num * sizeof(T);
		T[] array = new T[num];
		fixed (T* ptr = array)
		{
			byte* value = (byte*)ptr;
			ReadBytesSafe(value, size);
		}
		return array;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe byte[] ReadBytes(int count)
	{
		byte[] array = new byte[count];
		fixed (byte* value = array)
		{
			ReadBytesSafe(value, count);
		}
		return array;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void ReadBytes(ref byte[] value, int size, int offset = 0)
	{
		fixed (byte* value2 = value)
		{
			ReadBytes(value2, size, offset);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void ReadBytesSafe(ref byte[] value, int size, int offset = 0)
	{
		fixed (byte* value2 = value)
		{
			ReadBytesSafe(value2, size, offset);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe void ReadBytes(byte* value, int size, int offset = 0)
	{
		UnsafeUtility.MemCpy(value + offset, Handle->BufferPointer + Handle->Position, size);
		Handle->Position += size;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe void ReadBytesSafe(byte* value, int size, int offset = 0)
	{
		if (!TryBeginReadInternal(size))
		{
			throw new OverflowException($"Reading past the end of the buffer (attempted to read {size} bytes).");
		}
		UnsafeUtility.MemCpy(value + offset, Handle->BufferPointer + Handle->Position, size);
		Handle->Position += size;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void ReadPartialValue<T>(out T value, int bytesToRead, int offsetBytes = 0) where T : unmanaged
	{
		T val = new T();
		byte* destination = (byte*)(&val) + offsetBytes;
		byte* source = Handle->BufferPointer + Handle->Position;
		UnsafeUtility.MemCpy(destination, source, bytesToRead);
		Handle->Position += bytesToRead;
		value = val;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe byte[] ToArray()
	{
		byte[] array = new byte[Handle->Length];
		fixed (byte* destination = array)
		{
			UnsafeUtility.MemCpy(destination, Handle->BufferPointer, Handle->Length);
		}
		return array;
	}

	public unsafe void Dispose()
	{
		UnsafeUtility.Free(Handle, Handle->Allocator);
		Handle = null;
	}
}
