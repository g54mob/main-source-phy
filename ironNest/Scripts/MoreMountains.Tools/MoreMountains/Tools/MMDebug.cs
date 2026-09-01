using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MoreMountains.Tools
{
	public static class MMDebug
	{
		public struct DebugLogItem
		{
			public object Message;

			public string Color;

			public int Framecount;

			public float Time;

			public int TimePrecision;

			public bool DisplayFrameCount;

			public DebugLogItem(object message, string color, int framecount, float time, int timePrecision, bool displayFrameCount)
			{
				Message = null;
				Color = null;
				Framecount = 0;
				Time = 0f;
				TimePrecision = 0;
				DisplayFrameCount = false;
			}
		}

		[StructLayout((LayoutKind)0, Size = 1)]
		public struct MMDebugLogEvent
		{
			public delegate void Delegate(DebugLogItem item);

			private static event Delegate OnEvent
			{
				[CompilerGenerated]
				add
				{
				}
				[CompilerGenerated]
				remove
				{
				}
			}

			[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
			private static void RuntimeInitialization()
			{
			}

			public static void Register(Delegate callback)
			{
			}

			public static void Unregister(Delegate callback)
			{
			}

			public static void Trigger(DebugLogItem item)
			{
			}
		}

		private static MethodInfo[] _commands;

		private static readonly int _logHistoryMaxLength;

		private static bool _debugDrawEnabled;

		private static bool _debugLogEnabled;

		private static bool _debugLogEnabledSet;

		public static List<DebugLogItem> LogHistory;

		private const string _editorPrefsDebugLogs = "DebugLogsEnabled";

		private const string _editorPrefsDebugDraws = "DebugDrawsEnabled";

		public static MMDebugOnScreenConsole _console;

		private const string _debugConsolePrefabPath = "MMDebugOnScreenConsole";

		public static MethodInfo[] Commands => null;

		public static string LogHistoryText => null;

		public static bool DebugLogsEnabled
		{
			get
			{
				return false;
			}
			private set
			{
			}
		}

		public static bool DebugDrawEnabled
		{
			get
			{
				return false;
			}
			private set
			{
			}
		}

		public static void DebugLogCommand(string command)
		{
		}

		private static void LogCommand(string command, string color)
		{
		}

		public static void DebugLogClear()
		{
		}

		public static void DebugLogInfo(object message, string color = "", int timePrecision = 3, bool displayFrameCount = true)
		{
		}

		public static void DebugLogTime(object message, string color = "", int timePrecision = 3, bool displayFrameCount = true)
		{
		}

		public static DebugLogItem LogDebugToConsole(object message, string color, int timePrecision, bool displayFrameCount)
		{
			return default(DebugLogItem);
		}

		public static void SetDebugLogsEnabled(bool status)
		{
		}

		public static void SetDebugDrawEnabled(bool status)
		{
		}

		public static RaycastHit2D RayCast(Vector2 rayOriginPoint, Vector2 rayDirection, float rayDistance, LayerMask mask, Color color, bool drawGizmo = false)
		{
			return default(RaycastHit2D);
		}

		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float length, LayerMask mask, Color color, bool drawGizmo = false)
		{
			return default(RaycastHit2D);
		}

		public static RaycastHit2D MonoRayCastNonAlloc(RaycastHit2D[] array, Vector2 rayOriginPoint, Vector2 rayDirection, float rayDistance, LayerMask mask, Color color, bool drawGizmo = false)
		{
			return default(RaycastHit2D);
		}

		public static RaycastHit Raycast3D(Vector3 rayOriginPoint, Vector3 rayDirection, float rayDistance, LayerMask mask, Color color, bool drawGizmo = false, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return default(RaycastHit);
		}

		public static void DebugOnScreen(string message)
		{
		}

		public static void DebugOnScreen(string label, object value, int fontSize = 25)
		{
		}

		public static void InstantiateOnScreenConsole(int fontSize = 25)
		{
		}

		public static void SetOnScreenConsole(MMDebugOnScreenConsole newConsole)
		{
		}

		public static void DrawGizmoArrow(Vector3 origin, Vector3 direction, Color color, float arrowHeadLength = 3f, float arrowHeadAngle = 25f)
		{
		}

		public static void DebugDrawArrow(Vector3 origin, Vector3 direction, Color color, float arrowHeadLength = 0.2f, float arrowHeadAngle = 35f)
		{
		}

		public static void DebugDrawArrow(Vector3 origin, Vector3 direction, Color color, float arrowLength, float arrowHeadLength = 0.2f, float arrowHeadAngle = 35f)
		{
		}

		public static void DebugDrawCross(Vector3 spot, float crossSize, Color color)
		{
		}

		private static void DrawArrowEnd(bool drawGizmos, Vector3 arrowEndPosition, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 40f)
		{
		}

		public static void DrawHandlesBounds(Bounds bounds, Color color)
		{
		}

		public static void DrawSolidRectangle(Vector3 position, Vector3 size, Color borderColor, Color solidColor)
		{
		}

		public static void DrawGizmoPoint(Vector3 position, float size, Color color)
		{
		}

		public static void DrawCube(Vector3 position, Color color, Vector3 size)
		{
		}

		public static void DrawGizmoCube(Transform transform, Vector3 offset, Vector3 cubeSize, bool wireOnly)
		{
		}

		public static void DrawGizmoRectangle(Vector2 center, Vector2 size, Color color)
		{
		}

		public static void DrawGizmoRectangle(Vector2 center, Vector2 size, Matrix4x4 rotationMatrix, Color color)
		{
		}

		public static void DrawRectangle(Rect rectangle, Color color)
		{
		}

		public static void DrawRectangle(Vector3 position, Color color, Vector3 size)
		{
		}

		public static void DrawPoint(Vector3 position, Color color, float size)
		{
		}

		public static void DrawGizmoPoint(Vector3 position, Color color, float size)
		{
		}

		public static string GetSystemInfo()
		{
			return null;
		}

		public static void ClearConsole()
		{
		}
	}
}
