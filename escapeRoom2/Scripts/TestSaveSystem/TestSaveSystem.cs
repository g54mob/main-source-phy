using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class TestSaveSystem : MonoBehaviour
{
	private struct BasicStruct
	{
		public int ID;

		public float Value;

		public short Flag;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	private struct BasicStructWithArray
	{
		public int ID;

		public float Value;

		public short Flag;

		public unsafe fixed byte Data[4];
	}

	private void Start()
	{
		runFastSerializationTest();
	}

	private void runBasicStructTest()
	{
		byte[] array = serialize(new BasicStruct
		{
			ID = 42,
			Value = 3.14f,
			Flag = 7
		});
		Debug.Log($"Serialized size: {array.Length}, bytes: {BitConverter.ToString(array)}");
		BasicStruct basicStruct = deserialize<BasicStruct>(array);
		Debug.Log($"ID: {basicStruct.ID}, Value: {basicStruct.Value}, Flag: {basicStruct.Flag}");
	}

	private unsafe void runBasicStructWithArrayTest()
	{
		BasicStructWithArray blittableStruct = new BasicStructWithArray
		{
			ID = 42,
			Value = 3.14f,
			Flag = 7
		};
		blittableStruct.Data[0] = 1;
		blittableStruct.Data[1] = 2;
		blittableStruct.Data[2] = 3;
		blittableStruct.Data[3] = 4;
		byte[] array = serialize(blittableStruct);
		Debug.Log($"Serialized size: {array.Length}, bytes: {BitConverter.ToString(array)}");
		BasicStructWithArray basicStructWithArray = deserialize<BasicStructWithArray>(array);
		string text = blittableStruct.Data[0] + ", " + blittableStruct.Data[1] + ", " + blittableStruct.Data[2] + ", " + blittableStruct.Data[3];
		Debug.Log($"ID: {basicStructWithArray.ID}, Value: {basicStructWithArray.Value}, Flag: {basicStructWithArray.Flag}, Data: {text}");
	}

	private static byte[] serialize<T>(T blittableStruct) where T : struct
	{
		byte[] array = new byte[Marshal.SizeOf<T>()];
		MemoryMarshal.Write(array, ref blittableStruct);
		return array;
	}

	private static T deserialize<T>(byte[] data) where T : struct
	{
		return MemoryMarshal.Read<T>(data);
	}

	private void runFastSerializationTest()
	{
		using FastBinaryWriter fastBinaryWriter = new FastBinaryWriter(1024, 4096);
		fastBinaryWriter.WriteUnchecked<int>(1234, default(FastBinaryWriter.ForPrimitives));
		fastBinaryWriter.WriteUnchecked<bool>(true, default(FastBinaryWriter.ForPrimitives));
		fastBinaryWriter.WriteUnchecked<Vector3>(new Vector3(2f, 4f, 6f), default(FastBinaryWriter.ForPrimitives));
		fastBinaryWriter.WriteUnchecked(new int[3] { 1, 2, 3 }, default(FastBinaryWriter.ForPrimitives));
		byte[] array = fastBinaryWriter.ToArray();
		Debug.Log($"Bytes ({array.Length}): " + BitConverter.ToString(array));
		using FastBinaryReader fastBinaryReader = new FastBinaryReader(array);
		int num = fastBinaryReader.ReadSafe<int>(default(FastBinaryWriter.ForPrimitives));
		bool flag = fastBinaryReader.ReadSafe<bool>(default(FastBinaryWriter.ForPrimitives));
		Vector3 vector = fastBinaryReader.ReadSafe<Vector3>(default(FastBinaryWriter.ForPrimitives));
		int[] values = fastBinaryReader.ReadArraySafe<int>(default(FastBinaryWriter.ForPrimitives));
		Debug.Log(string.Format("Values: {0}, {1}, {2}, [{3}]", num, flag, vector, string.Join(", ", values)));
	}
}
