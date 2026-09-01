using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kamgam.UGUIComponentsForSettings
{
	[Serializable]
	public class InputBindingForInputSystem : IInputBindingForGUI
	{
		public enum LocalConfigBehaviours
		{
			OverrideGlobalIfLocalExists = 0,
			AppendLocalToGlobal = 1
		}

		public delegate bool CheckBindingPathDelegate(string previousPath, string path);

		public delegate Action OnBeforeRebindStartDelegate(InputActionRebindingExtensions.RebindingOperation rebindingOperation);

		public static char CompositeControlSeparator;

		public static float WaitForKeyComboDuration;

		public static string[] GlobalIgnoreControlPaths;

		public static string[] GlobalAbortControlPaths;

		public static string GlobalControlsHavingToMatchPath;

		[Tooltip("Defines how the paths configs should be combined with the GLOBAL path configs. See IputBindingUGUI.cs")]
		public LocalConfigBehaviours LocalConfigBehaviour;

		[Tooltip("Local ignore control paths. These are handed to RebindingOperation.WithControlsExcluding().")]
		public string[] IgnoreControlPaths;

		[Tooltip("Local abort control paths. These are handed to RebindingOperation.WithCancelingThrough().")]
		public string[] AbortControlPaths;

		public string ControlsHavingToMatchPath;

		[Tooltip("Local match paths. Used to limit the controls which react to this input.\nExample: Set to <Keyboard>/* to limit to keyboard inputs only or <Gamepad>/<Button> for gamepad buttons.")]
		public string[] MatchControlPaths;

		[Tooltip("At runtime this is the selected binding path.")]
		[SerializeField]
		protected string _bindingPath;

		[NonSerialized]
		public bool AllowComposite;

		public CheckBindingPathDelegate CheckBindingPathFunc;

		public OnBeforeRebindStartDelegate OnBeforeRebindStart;

		protected InputActionRebindingExtensions.RebindingOperation _rebindingOperation;

		public event Action OnComplete
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

		public event Action OnCanceled
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

		public void CopyFrom(InputBindingForInputSystem other)
		{
		}

		public string GetBindingPath()
		{
			return null;
		}

		public void SetBindingPath(string path)
		{
		}

		public void AddOnCompleteCallback(Action callback)
		{
		}

		public void RemoveOnCompleteCallback(Action callback)
		{
		}

		public void AddOnCanceledCallback(Action callback)
		{
		}

		public void RemoveOnCanceledCallback(Action callback)
		{
		}

		public void StartListening()
		{
		}

		protected string[] resolveConfigStrings(string[] globals, string[] locals)
		{
			return null;
		}

		protected string resolveConfigString(string global, string local)
		{
			return null;
		}

		public void OnEnable()
		{
		}

		public void OnDisable()
		{
		}
	}
}
