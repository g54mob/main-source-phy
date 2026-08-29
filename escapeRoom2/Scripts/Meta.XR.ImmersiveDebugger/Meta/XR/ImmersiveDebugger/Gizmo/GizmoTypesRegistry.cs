using System;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.XR.ImmersiveDebugger.Gizmo
{
	internal static class GizmoTypesRegistry
	{
		private static readonly Dictionary<(DebugGizmoType, Type), GizmoTypeInfo> GizmoTypeInfos = new Dictionary<(DebugGizmoType, Type), GizmoTypeInfo>();

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init()
		{
			GizmoTypeInfos?.Clear();
		}

		public static void RegisterGizmoType(DebugGizmoType gizmoType, Type dataSourceType, Action<object> renderDelegate)
		{
			GizmoTypeInfos.Add((gizmoType, dataSourceType), new GizmoTypeInfo(renderDelegate));
		}

		public static void InitGizmos()
		{
			RegisterGizmoType(DebugGizmoType.Axis, typeof(Pose), delegate(object dataSource)
			{
				if (dataSource is Pose pose)
				{
					DebugGizmos.DrawAxis(pose);
				}
			});
			RegisterGizmoType(DebugGizmoType.Axis, typeof(Transform), delegate(object dataSource)
			{
				if (dataSource is Transform t)
				{
					DebugGizmos.DrawAxis(t);
				}
			});
			RegisterGizmoType(DebugGizmoType.Point, typeof(Vector3), delegate(object dataSource)
			{
				if (dataSource is Vector3 p)
				{
					DebugGizmos.DrawPoint(p);
				}
			});
			RegisterGizmoType(DebugGizmoType.Point, typeof(Transform), delegate(object dataSource)
			{
				if (dataSource is Transform transform)
				{
					DebugGizmos.DrawPoint(transform.position);
				}
			});
			RegisterGizmoType(DebugGizmoType.Line, typeof(Tuple<Vector3, Vector3>), delegate(object dataSource)
			{
				if (dataSource is Tuple<Vector3, Vector3> tuple)
				{
					DebugGizmos.DrawLine(tuple.Item1, tuple.Item2);
				}
			});
			RegisterGizmoType(DebugGizmoType.Lines, typeof(Vector3[]), delegate(object dataSource)
			{
				if (dataSource is Vector3[] array)
				{
					for (int i = 1; i < array.Length; i++)
					{
						DebugGizmos.DrawLine(array[i - 1], array[i]);
					}
				}
			});
			RegisterGizmoType(DebugGizmoType.Plane, typeof(Tuple<Pose, float, float>), delegate(object dataSource)
			{
				if (dataSource is Tuple<Pose, float, float> tuple)
				{
					DebugGizmos.DrawPlane(tuple.Item1, tuple.Item2, tuple.Item3);
				}
			});
			RegisterGizmoType(DebugGizmoType.Cube, typeof(Tuple<Vector3, float>), delegate(object dataSource)
			{
				if (dataSource is Tuple<Vector3, float> tuple)
				{
					DebugGizmos.DrawWireCube(tuple.Item1, tuple.Item2);
				}
			});
			RegisterGizmoType(DebugGizmoType.TopCenterBox, typeof(Tuple<Pose, float, float, float>), delegate(object dataSource)
			{
				if (dataSource is Tuple<Pose, float, float, float> tuple)
				{
					DebugGizmos.DrawBox(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, isPivotTopSurface: true);
				}
			});
			RegisterGizmoType(DebugGizmoType.Box, typeof(Tuple<Pose, float, float, float>), delegate(object dataSource)
			{
				if (dataSource is Tuple<Pose, float, float, float> tuple)
				{
					DebugGizmos.DrawBox(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
				}
			});
		}

		public static bool IsValidDataTypeForGizmoType(Type type, DebugGizmoType gizmoType)
		{
			if (GizmoTypeInfos.TryGetValue((gizmoType, type), out var _))
			{
				return true;
			}
			Debug.LogWarning($"{gizmoType} not found in GizmoTypeInfos, please registerGizmoType.");
			return false;
		}

		public static void RenderGizmo(DebugGizmoType type, object dataSource)
		{
			if (dataSource != null && GizmoTypeInfos.TryGetValue((type, dataSource.GetType()), out var value))
			{
				value.RenderDelegate(dataSource);
			}
		}
	}
}
