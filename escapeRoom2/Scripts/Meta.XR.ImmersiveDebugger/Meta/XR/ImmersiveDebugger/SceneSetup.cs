using System;
using System.Diagnostics;
using Meta.XR.ImmersiveDebugger.Gizmo;
using Meta.XR.ImmersiveDebugger.Manager;
using Meta.XR.ImmersiveDebugger.UserInterface;
using UnityEngine;

namespace Meta.XR.ImmersiveDebugger
{
	internal static class SceneSetup
	{
		[RuntimeInitializeOnLoadMethod]
		private static void OnLoad()
		{
			_ = RuntimeSettings.Instance.ImmersiveDebuggerEnabled;
		}

		[Conditional("UNITY_EDITOR")]
		[Conditional("DEBUG")]
		[Conditional("IMMERSIVE_DEBUGGER_ALLOW_USE_IN_PROD")]
		internal static void SetupImmersiveDebugger()
		{
			GizmoTypesRegistry.InitGizmos();
			GameObject gameObject = new GameObject("ImmersiveDebuggerManager");
			gameObject.AddComponent<DebugManager>();
			GameObject gameObject2 = new GameObject("ImmersiveDebuggerInterface");
			gameObject2.transform.SetParent(gameObject.transform);
			gameObject2.AddComponent<DebugInterface>();
			Type type = Type.GetType(RuntimeSettings.Instance.CustomIntegrationConfigClassName);
			if (RuntimeSettings.Instance.UseCustomIntegrationConfig && type != null)
			{
				if (typeof(MonoBehaviour).IsAssignableFrom(type) && type.IsSubclassOf(typeof(CustomIntegrationConfigBase)))
				{
					gameObject.AddComponent(type);
				}
				else
				{
					UnityEngine.Debug.LogWarning("CustomIntegrationConfig file is not an valid type");
				}
			}
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}
	}
}
