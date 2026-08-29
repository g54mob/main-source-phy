using System;
using System.Collections.Generic;
using Meta.XR.ImmersiveDebugger.Manager;
using Meta.XR.ImmersiveDebugger.Utils;

namespace Meta.XR.ImmersiveDebugger
{
	internal static class Telemetry
	{
		[OVRTelemetry.Markers]
		internal static class MarkerId
		{
			public const int SettingsAccessed = 163059815;

			public const int SettingsChanged = 163065143;

			public const int ComponentTracked = 163059554;

			public const int Run = 163061656;

			public const int FrameUpdate = 163056655;
		}

		internal enum State
		{
			OnStart = 0,
			OnFocusLost = 1,
			OnDisable = 2
		}

		internal enum Method
		{
			Attributes = 0,
			DebugInspector = 1
		}

		internal static class AnnotationType
		{
			public const string Origin = "Origin";

			public const string Type = "Type";

			public const string Value = "Value";

			public const string Method = "Method";

			public const string State = "State";

			public const string Instances = "Instances";

			public const string Gizmos = "Gizmos";

			public const string Watches = "Watches";

			public const string Tweaks = "Tweaks";

			public const string Actions = "Actions";

			public const string IsCustom = "IsCustom";
		}

		internal class TelemetryTracker
		{
			private readonly Method _method;

			private readonly InstanceCache _cache;

			private readonly IEnumerable<IDebugManager> _managers;

			private OVRTelemetryMarker _runTelemetryMarker;

			public static TelemetryTracker Init(Method method, IEnumerable<IDebugManager> managers, InstanceCache cache, DebugManager debugManager)
			{
				TelemetryTracker telemetryTracker = new TelemetryTracker(method, managers, cache);
				debugManager.OnFocusLostAction += telemetryTracker.OnFocusLost;
				debugManager.OnDisableAction += telemetryTracker.OnDisable;
				return telemetryTracker;
			}

			private TelemetryTracker(Method method, IEnumerable<IDebugManager> managers, InstanceCache cache)
			{
				_method = method;
				_cache = cache;
				_managers = managers;
				_runTelemetryMarker = OVRTelemetry.Start(163061656, 0, -1L).AddAnnotation("Method", _method.ToString()).AddAnnotation("State", State.OnStart.ToString())
					.AddPlayModeOrigin();
			}

			public void OnStart()
			{
				SendStart();
				SendComponentTracked(State.OnStart);
			}

			private void OnFocusLost()
			{
				SendComponentTracked(State.OnFocusLost);
			}

			private void OnDisable()
			{
				SendComponentTracked(State.OnDisable);
			}

			private void SendStart()
			{
				_runTelemetryMarker.Send();
			}

			private void SendComponentTracked(State state)
			{
				foreach (var (type2, list2) in _cache.CacheData)
				{
					if (list2.Count <= 0)
					{
						continue;
					}
					OVRTelemetryMarker oVRTelemetryMarker = OVRTelemetry.Start(163059554, 0, -1L).AddPlayModeOrigin().AddAnnotation("State", state.ToString())
						.AddAnnotation("Method", _method.ToString())
						.AddAnnotation("Instances", list2.Count.ToString());
					oVRTelemetryMarker = ((!type2.IsTypeCustom()) ? oVRTelemetryMarker.AddAnnotation("Type", type2.FullName).AddAnnotation("IsCustom", annotationValue: false) : oVRTelemetryMarker.AddAnnotation("Type", type2.GetTypeHash()).AddAnnotation("IsCustom", annotationValue: true));
					foreach (IDebugManager manager in _managers)
					{
						oVRTelemetryMarker = oVRTelemetryMarker.AddAnnotation(manager.TelemetryAnnotation, manager.GetCountPerType(type2).ToString());
					}
					oVRTelemetryMarker.Send();
				}
			}
		}

		private static readonly List<string> NonCustomAssemblies = new List<string> { "Oculus.", "Meta." };

		internal static string GetTypeHash(this Type type)
		{
			int hashCode = type.GetHashCode();
			int num = type.FullName?.GetHashCode() ?? 0;
			return (hashCode ^ num).ToString();
		}

		private static bool IsTypeCustom(this Type type)
		{
			string name = type.Assembly.GetName().Name;
			foreach (string nonCustomAssembly in NonCustomAssemblies)
			{
				if (name.StartsWith(nonCustomAssembly, StringComparison.InvariantCultureIgnoreCase))
				{
					return false;
				}
			}
			return true;
		}
	}
}
