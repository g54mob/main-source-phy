using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	public struct Geometry
	{
		public IntPtr handle;

		public RESULT release()
		{
			return default(RESULT);
		}

		public RESULT addPolygon(float directocclusion, float reverbocclusion, bool doublesided, int numvertices, VECTOR[] vertices, out int polygonindex)
		{
			polygonindex = default(int);
			return default(RESULT);
		}

		public RESULT getNumPolygons(out int numpolygons)
		{
			numpolygons = default(int);
			return default(RESULT);
		}

		public RESULT getMaxPolygons(out int maxpolygons, out int maxvertices)
		{
			maxpolygons = default(int);
			maxvertices = default(int);
			return default(RESULT);
		}

		public RESULT getPolygonNumVertices(int index, out int numvertices)
		{
			numvertices = default(int);
			return default(RESULT);
		}

		public RESULT setPolygonVertex(int index, int vertexindex, ref VECTOR vertex)
		{
			return default(RESULT);
		}

		public RESULT getPolygonVertex(int index, int vertexindex, out VECTOR vertex)
		{
			vertex = default(VECTOR);
			return default(RESULT);
		}

		public RESULT setPolygonAttributes(int index, float directocclusion, float reverbocclusion, bool doublesided)
		{
			return default(RESULT);
		}

		public RESULT getPolygonAttributes(int index, out float directocclusion, out float reverbocclusion, out bool doublesided)
		{
			directocclusion = default(float);
			reverbocclusion = default(float);
			doublesided = default(bool);
			return default(RESULT);
		}

		public RESULT setActive(bool active)
		{
			return default(RESULT);
		}

		public RESULT getActive(out bool active)
		{
			active = default(bool);
			return default(RESULT);
		}

		public RESULT setRotation(ref VECTOR forward, ref VECTOR up)
		{
			return default(RESULT);
		}

		public RESULT getRotation(out VECTOR forward, out VECTOR up)
		{
			forward = default(VECTOR);
			up = default(VECTOR);
			return default(RESULT);
		}

		public RESULT setPosition(ref VECTOR position)
		{
			return default(RESULT);
		}

		public RESULT getPosition(out VECTOR position)
		{
			position = default(VECTOR);
			return default(RESULT);
		}

		public RESULT setScale(ref VECTOR scale)
		{
			return default(RESULT);
		}

		public RESULT getScale(out VECTOR scale)
		{
			scale = default(VECTOR);
			return default(RESULT);
		}

		public RESULT save(IntPtr data, out int datasize)
		{
			datasize = default(int);
			return default(RESULT);
		}

		public RESULT setUserData(IntPtr userdata)
		{
			return default(RESULT);
		}

		public RESULT getUserData(out IntPtr userdata)
		{
			userdata = default(IntPtr);
			return default(RESULT);
		}

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_Release(IntPtr geometry);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_AddPolygon(IntPtr geometry, float directocclusion, float reverbocclusion, bool doublesided, int numvertices, VECTOR[] vertices, out int polygonindex);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_GetNumPolygons(IntPtr geometry, out int numpolygons);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_GetMaxPolygons(IntPtr geometry, out int maxpolygons, out int maxvertices);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_GetPolygonNumVertices(IntPtr geometry, int index, out int numvertices);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_SetPolygonVertex(IntPtr geometry, int index, int vertexindex, ref VECTOR vertex);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_GetPolygonVertex(IntPtr geometry, int index, int vertexindex, out VECTOR vertex);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_SetPolygonAttributes(IntPtr geometry, int index, float directocclusion, float reverbocclusion, bool doublesided);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_GetPolygonAttributes(IntPtr geometry, int index, out float directocclusion, out float reverbocclusion, out bool doublesided);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_SetActive(IntPtr geometry, bool active);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_GetActive(IntPtr geometry, out bool active);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_SetRotation(IntPtr geometry, ref VECTOR forward, ref VECTOR up);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_GetRotation(IntPtr geometry, out VECTOR forward, out VECTOR up);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_SetPosition(IntPtr geometry, ref VECTOR position);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_GetPosition(IntPtr geometry, out VECTOR position);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_SetScale(IntPtr geometry, ref VECTOR scale);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_GetScale(IntPtr geometry, out VECTOR scale);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_Save(IntPtr geometry, IntPtr data, out int datasize);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_SetUserData(IntPtr geometry, IntPtr userdata);

		[PreserveSig]
		private static extern RESULT FMOD5_Geometry_GetUserData(IntPtr geometry, out IntPtr userdata);

		public Geometry(IntPtr ptr)
		{
			handle = (IntPtr)0;
		}

		public bool hasHandle()
		{
			return false;
		}

		public void clearHandle()
		{
		}
	}
}
