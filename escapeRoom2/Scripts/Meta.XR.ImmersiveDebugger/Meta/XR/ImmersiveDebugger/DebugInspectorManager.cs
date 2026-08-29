using System;
using System.Collections.Generic;
using System.Reflection;
using Meta.XR.ImmersiveDebugger.Gizmo;
using Meta.XR.ImmersiveDebugger.Manager;
using Meta.XR.ImmersiveDebugger.UserInterface;
using Meta.XR.ImmersiveDebugger.Utils;
using UnityEngine;

namespace Meta.XR.ImmersiveDebugger
{
	internal class DebugInspectorManager
	{
		private abstract class ManagerFromInspector : IDebugManager
		{
			private readonly Dictionary<Type, List<MemberInfo>> _dictionary = new Dictionary<Type, List<MemberInfo>>();

			private IDebugUIPanel _uiPanel;

			protected InstanceCache InstanceCache;

			public abstract string TelemetryAnnotation { get; }

			public void Setup(IDebugUIPanel panel, InstanceCache cache)
			{
				_uiPanel = panel;
				InstanceCache = cache;
			}

			public void ProcessType(Type type)
			{
				throw new NotImplementedException();
			}

			public void ProcessTypeFromInspector(Type type, InstanceHandle handle, MemberInfo memberInfo, DebugMember memberAttribute)
			{
				IMember member = _uiPanel.RegisterInspector(handle, memberAttribute.Category).RegisterMember(memberInfo, memberAttribute);
				if (RegisterSpecialisedWidget(member, memberInfo, memberAttribute, handle))
				{
					if (!_dictionary.TryGetValue(type, out var value))
					{
						value = new List<MemberInfo>();
						_dictionary.Add(type, value);
					}
					if (!value.Contains(memberInfo))
					{
						value.Add(memberInfo);
					}
				}
			}

			public int GetCountPerType(Type type)
			{
				_dictionary.TryGetValue(type, out var value);
				return value?.Count ?? 0;
			}

			protected abstract bool RegisterSpecialisedWidget(IMember member, MemberInfo memberInfo, DebugMember memberAttribute, InstanceHandle handle);
		}

		private class TweakManagerFromInspector : ManagerFromInspector
		{
			public override string TelemetryAnnotation => "Tweaks";

			protected override bool RegisterSpecialisedWidget(IMember member, MemberInfo memberInfo, DebugMember memberAttribute, InstanceHandle handle)
			{
				if (!memberAttribute.Tweakable || (!TweakUtils.IsTypeSupported((memberInfo as FieldInfo)?.FieldType) && !TweakUtils.IsTypeSupported((memberInfo as PropertyInfo)?.PropertyType) && !memberInfo.IsBaseTypeEqual(typeof(Enum))))
				{
					return false;
				}
				Tweak tweak = member.GetTweak();
				if (tweak == null || !tweak.Matches(memberInfo, handle.Instance))
				{
					if (memberInfo.IsBaseTypeEqual(typeof(Enum)))
					{
						member.RegisterEnum(TweakUtils.Create(memberInfo, memberAttribute, handle.Instance, memberInfo.GetDataType()));
					}
					else
					{
						TweakUtils.ProcessMinMaxRange(memberInfo, memberAttribute, handle.Instance);
						member.RegisterTweak(TweakUtils.Create(memberInfo, memberAttribute, handle.Instance));
					}
				}
				return true;
			}
		}

		private class ActionManagerFromInspector : ManagerFromInspector
		{
			public override string TelemetryAnnotation => "Actions";

			protected override bool RegisterSpecialisedWidget(IMember member, MemberInfo memberInfo, DebugMember memberAttribute, InstanceHandle handle)
			{
				if (memberInfo.MemberType != MemberTypes.Method)
				{
					return false;
				}
				MethodInfo methodInfo = memberInfo as MethodInfo;
				if (methodInfo == null || methodInfo.GetParameters().Length != 0 || methodInfo.ReturnType != typeof(void))
				{
					return false;
				}
				ActionHook action = member.GetAction();
				if (action == null || !action.Matches(memberInfo, handle.Instance))
				{
					member.RegisterAction(new ActionHook(memberInfo, handle.Instance, memberAttribute));
				}
				return true;
			}
		}

		private class WatchManagerFromInspector : ManagerFromInspector
		{
			public override string TelemetryAnnotation => "Watches";

			protected override bool RegisterSpecialisedWidget(IMember member, MemberInfo memberInfo, DebugMember memberAttribute, InstanceHandle handle)
			{
				if (!WatchManager.IsWatchTypeSupported(memberInfo))
				{
					return false;
				}
				Watch watch = member.GetWatch();
				if (watch == null || !watch.Matches(memberInfo, handle.Instance))
				{
					if (memberInfo.IsTypeEqual(typeof(Texture2D)))
					{
						member.RegisterTexture(WatchUtils.Create(memberInfo, handle.Instance, memberAttribute) as WatchTexture);
					}
					else
					{
						member.RegisterWatch(WatchUtils.Create(memberInfo, handle.Instance, memberAttribute));
					}
				}
				return true;
			}
		}

		private class GizmoManagerFromInspector : ManagerFromInspector
		{
			private readonly Dictionary<MemberInfo, GizmoRendererManager> _memberToGizmoRendererManagerDict = new Dictionary<MemberInfo, GizmoRendererManager>();

			public override string TelemetryAnnotation => "Gizmos";

			protected override bool RegisterSpecialisedWidget(IMember member, MemberInfo memberInfo, DebugMember memberAttribute, InstanceHandle handle)
			{
				if (memberAttribute.GizmoType == DebugGizmoType.None)
				{
					return false;
				}
				if (!_memberToGizmoRendererManagerDict.TryGetValue(memberInfo, out var value) && AddGizmo(handle.Type, memberInfo, memberAttribute, out value))
				{
					_memberToGizmoRendererManagerDict[memberInfo] = value;
				}
				if (value == null)
				{
					return false;
				}
				GizmoHook gizmo = member.GetGizmo();
				if (gizmo == null || !gizmo.Matches(memberInfo, handle.Instance))
				{
					member.RegisterGizmo(new GizmoHook(memberInfo, handle.Instance, memberAttribute, OnStateChanged, GetState));
				}
				return true;
				bool GetState()
				{
					return _memberToGizmoRendererManagerDict[memberInfo].GetState(handle.Instance);
				}
				void OnStateChanged(bool state)
				{
					_memberToGizmoRendererManagerDict[memberInfo].SetState(handle.Instance, state);
				}
			}

			private bool AddGizmo(Type type, MemberInfo member, DebugMember gizmoAttribute, out GizmoRendererManager gizmoRendererManager)
			{
				if (!GizmoTypesRegistry.IsValidDataTypeForGizmoType(member.GetDataType(), gizmoAttribute.GizmoType))
				{
					Debug.LogWarning("Invalid registration of gizmo " + member.Name + ": type not matching gizmo type");
					gizmoRendererManager = null;
					return false;
				}
				GameObject gameObject = new GameObject(member.Name + "Gizmo");
				gizmoRendererManager = gameObject.AddComponent<GizmoRendererManager>();
				gizmoRendererManager.Setup(type, member, gizmoAttribute.GizmoType, gizmoAttribute.Color, InstanceCache);
				return true;
			}
		}

		private static DebugInspectorManager _instance;

		private readonly InstanceCache _instanceCache = new InstanceCache();

		private readonly List<IDebugManager> _subDebugManagers = new List<IDebugManager>();

		private readonly List<DebugInspector> _inspectors = new List<DebugInspector>();

		private static IDebugUIPanel _uiPanel;

		public static DebugInspectorManager Instance => _instance ?? (_instance = new DebugInspectorManager());

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init()
		{
			_instance = null;
			_uiPanel = null;
		}

		private DebugInspectorManager()
		{
			if (DebugManager.Instance == null)
			{
				DebugManager.OnReady -= OnReady;
				DebugManager.OnReady += OnReady;
			}
			else
			{
				OnReady(DebugManager.Instance);
			}
		}

		internal static void Destroy()
		{
			if (_instance != null)
			{
				DebugManager.OnReady -= _instance.OnReady;
			}
		}

		private void InitSubManagers()
		{
			RegisterManager<GizmoManagerFromInspector>();
			RegisterManager<WatchManagerFromInspector>();
			RegisterManager<ActionManagerFromInspector>();
			RegisterManager<TweakManagerFromInspector>();
		}

		private void RegisterManager<TManagerType>() where TManagerType : IDebugManager, new()
		{
			TManagerType val = new TManagerType();
			val.Setup(_uiPanel, _instanceCache);
			_subDebugManagers.Add(val);
		}

		public void RegisterInspector(DebugInspector inspector)
		{
			_inspectors.Add(inspector);
			ProcessInspector(inspector);
		}

		public void UnregisterInspector(DebugInspector inspector)
		{
			UnprocessInspector(inspector);
			_inspectors.Remove(inspector);
		}

		private void OnReady(DebugManager debugManager)
		{
			Telemetry.TelemetryTracker telemetryTracker = Telemetry.TelemetryTracker.Init(Telemetry.Method.DebugInspector, _subDebugManagers, _instanceCache, debugManager);
			_uiPanel = debugManager.UiPanel;
			InitSubManagers();
			foreach (DebugInspector inspector in _inspectors)
			{
				ProcessInspector(inspector);
			}
			telemetryTracker.OnStart();
		}

		private void ProcessInspector(DebugInspector inspector)
		{
			if (_uiPanel == null)
			{
				return;
			}
			foreach (InspectedHandle handle in inspector.Registry.Handles)
			{
				if (!handle.Visible)
				{
					continue;
				}
				InstanceHandle instanceHandle = handle.InstanceHandle;
				_instanceCache.RegisterHandle(instanceHandle);
				foreach (InspectedMember inspectedMember in handle.inspectedMembers)
				{
					if (!inspectedMember.Visible)
					{
						continue;
					}
					MemberInfo memberInfo = inspectedMember.MemberInfo;
					if (memberInfo == null)
					{
						continue;
					}
					DebugMember attribute = inspectedMember.attribute;
					if (attribute == null)
					{
						continue;
					}
					_uiPanel.RegisterInspector(instanceHandle, attribute.Category);
					foreach (IDebugManager subDebugManager in _subDebugManagers)
					{
						subDebugManager.ProcessTypeFromInspector(instanceHandle.Type, instanceHandle, memberInfo, attribute);
					}
				}
			}
		}

		private void UnprocessInspector(DebugInspector inspector)
		{
			if (_uiPanel == null)
			{
				return;
			}
			foreach (InspectedHandle handle in inspector.Registry.Handles)
			{
				InstanceHandle instanceHandle = handle.InstanceHandle;
				foreach (InspectedMember inspectedMember in handle.inspectedMembers)
				{
					DebugMember attribute = inspectedMember.attribute;
					if (attribute != null)
					{
						_uiPanel.UnregisterInspector(instanceHandle, attribute.Category, allCategories: false);
					}
				}
				_instanceCache.UnregisterHandle(instanceHandle);
			}
		}
	}
}
