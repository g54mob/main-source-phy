using System.Collections.Generic;
using Poly.Collide;
using Poly.Math;

namespace Poly.PortToJS
{
	public class PolygonData
	{
		public static PolygonData[] m_ScenePolygonData;

		public Vec2[] verts;

		public float[] invLengths;

		public float radius;

		public QueryFilter filter;

		public static PolygonData FromCircle(Vec2 position, float radius)
		{
			PolygonData polygonData = new PolygonData();
			polygonData.verts = new Vec2[1] { position };
			polygonData.invLengths = new float[1];
			polygonData.radius = radius;
			polygonData.filter = QueryFilter.NoChecks;
			return polygonData;
		}

		public static PolygonData FromSegment(Vec2 endpoint0, Vec2 endpoint1, float radius)
		{
			float num = Vec2.Distance(in endpoint0, in endpoint1);
			float num2 = ((num > 1E-12f) ? (1f / num) : 0f);
			PolygonData polygonData = new PolygonData();
			polygonData.verts = new Vec2[2] { endpoint0, endpoint1 };
			polygonData.invLengths = new float[2] { num2, num2 };
			polygonData.radius = radius;
			polygonData.filter = QueryFilter.NoChecks;
			return polygonData;
		}

		public static PolygonCollisionProcess CreateCollisionProcess(PolygonData polyA, PolygonData polyB)
		{
			return new PolygonCollisionProcess
			{
				aTb = Transform2.identity,
				vA = polyA.verts,
				vB = polyB.verts,
				vB_Count = polyB.verts.Length,
				invLengthsA = polyA.invLengths,
				invLengthsB = polyB.invLengths,
				radiusA = polyA.radius,
				radiusB = polyB.radius
			};
		}

		public static byte[] SerializeAllPolygons()
		{
			List<byte> list = new List<byte>();
			PolygonData[] array = PolygonDataUtil.GatherPolygonDataFromScene();
			list.AddRange(ByteSerializer.SerializeInt(array.Length));
			PolygonData[] array2 = array;
			foreach (PolygonData polygonData in array2)
			{
				list.AddRange(polygonData.Serialize());
			}
			return list.ToArray();
		}

		public static void DeserializeAllPolygons(byte[] bytes, ref int offset)
		{
			int num = ByteSerializer.DeserializeInt(bytes, ref offset);
			List<PolygonData> list = new List<PolygonData>();
			for (int i = 0; i < num; i++)
			{
				PolygonData polygonData = new PolygonData();
				polygonData.Deserialize(bytes, ref offset);
				list.Add(polygonData);
			}
			m_ScenePolygonData = list.ToArray();
		}

		public byte[] Serialize()
		{
			List<byte> list = new List<byte>();
			list.AddRange(ByteSerializer.SerializeInt(verts.Length));
			Vec2[] array = verts;
			foreach (Vec2 vec in array)
			{
				list.AddRange(ByteSerializer.SerializeVector2(vec));
			}
			list.AddRange(ByteSerializer.SerializeInt(invLengths.Length));
			float[] array2 = invLengths;
			foreach (float value in array2)
			{
				list.AddRange(ByteSerializer.SerializeFloat(value));
			}
			list.AddRange(ByteSerializer.SerializeFloat(radius));
			list.AddRange(ByteSerializer.SerializeInt((int)filter));
			return list.ToArray();
		}

		public void Deserialize(byte[] bytes, ref int offset)
		{
			int num = ByteSerializer.DeserializeInt(bytes, ref offset);
			List<Vec2> list = new List<Vec2>();
			for (int i = 0; i < num; i++)
			{
				list.Add(ByteSerializer.DeserializeVector2(bytes, ref offset));
			}
			verts = list.ToArray();
			int num2 = ByteSerializer.DeserializeInt(bytes, ref offset);
			List<float> list2 = new List<float>();
			for (int j = 0; j < num2; j++)
			{
				list2.Add(ByteSerializer.DeserializeFloat(bytes, ref offset));
			}
			invLengths = list2.ToArray();
			radius = ByteSerializer.DeserializeFloat(bytes, ref offset);
			filter = (QueryFilter)ByteSerializer.DeserializeInt(bytes, ref offset);
		}
	}
}
