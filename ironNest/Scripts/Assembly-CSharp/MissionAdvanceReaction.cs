using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SleepyNodes;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Missions/Mission Advance Reaction (Simple)")]
public class MissionAdvanceReaction : MonoBehaviour
{
	public enum Trigger
	{
		OnMissionChanging = 0,
		OnMissionChanged = 1,
		OnPhaseChanged = 2
	}

	public enum PhaseFilter
	{
		Any = 0,
		EnterMainMenu = 1,
		EnterBrowsingMap = 2,
		EnterMissionActive = 3
	}

	public enum ExitPhaseFilter
	{
		Any = 0,
		ExitMainMenu = 1,
		ExitBrowsingMap = 2,
		ExitMissionActive = 3
	}

	public enum TargetSelection
	{
		SelfOnly = 0,
		ChildrenOnly = 1,
		SelfAndChildren = 2
	}

	public enum ActionType
	{
		None = 0,
		Disable = 1,
		Destroy = 2
	}

	[Serializable]
	public class GameObjectEvent : UnityEvent<GameObject>
	{
	}

	[CompilerGenerated]
	private sealed class _003CRetrySubscribeRoutine_003Ed__33 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MissionAdvanceReaction _003C_003E4__this;

		private int _003Cattempts_003E5__2;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CRetrySubscribeRoutine_003Ed__33(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[Header("When To React")]
	[Tooltip("When to fire this reaction in the mission lifecycle.\n- OnMissionChanging: Fires before unloading the previous mission and before loading the new one. Use for end-of-mission cleanup.\n- OnMissionChanged: Fires after the new mission scene has finished loading. Use for post-load setup.\n- OnPhaseChanged: Fires when the top-level GamePhase changes (MainMenu / BrowsingMap / MissionActive). Use 'Phase Filter' below to target a specific phase.\n\nNote: Returning to Main Menu is NOT a MissionChanging/MissionChanged event in MissionManager by default.\nUse 'Treat Return To Main Menu As Mission Change' below to opt into reacting to menu transitions.")]
	[SerializeField]
	private Trigger trigger;

	[Tooltip("Only used when Trigger = OnPhaseChanged.\nFilters by which phase is being ENTERED (next). Combined with Exit Phase Filter using AND — both must pass.\n\n- Any: passes for every phase change.\n- EnterMainMenu: passes when transitioning TO MainMenu.\n- EnterBrowsingMap: passes when transitioning TO BrowsingMap (player returning to map).\n- EnterMissionActive: passes when transitioning TO MissionActive (mission starting).\n\nExample: EnterBrowsingMap + ExitPhaseFilter=Any → fires every time the player lands on the map.\nExample: EnterMissionActive + ExitPhaseFilter=ExitBrowsingMap → fires only when starting a mission from the map (not from menu).")]
	[SerializeField]
	private PhaseFilter phaseFilter;

	[Tooltip("Only used when Trigger = OnPhaseChanged.\nFilters by which phase is being LEFT (prev). Combined with Phase Filter using AND — both must pass.\n\n- Any: passes regardless of which phase is being left.\n- ExitMainMenu: passes only when leaving MainMenu.\n- ExitBrowsingMap: passes only when leaving BrowsingMap (e.g. player just launched a mission from the map).\n- ExitMissionActive: passes only when leaving MissionActive (e.g. mission just ended — player going to map or menu).\n\nExample: PhaseFilter=Any + ExitBrowsingMap → fires whenever the player leaves the map, regardless of destination.\nExample: PhaseFilter=EnterBrowsingMap + ExitMissionActive → fires only when coming back to map after a mission (not first load).")]
	[SerializeField]
	private ExitPhaseFilter exitPhaseFilter;

	[Tooltip("If true (default), returning to the Main Menu will be treated as a mission change for this component.\nThis is implemented by additionally listening to MissionManager's Main Menu events:\n- Trigger=OnMissionChanging: reacts on MainMenuLoading\n- Trigger=OnMissionChanged: reacts on MainMenuLoaded\n\nWhy this exists: MissionManager.EndOperationAndReturnToMenu() unloads the current mission and loads the menu,\nbut does not invoke MissionChanging/MissionChanged. This option bridges that gap.\n\nSafe defaults:\n- The callback indices passed into the handler will be:\n  fromIndex = MissionManager.CurrentMissionIndex (at the time of the menu event; often -1 after cleanup)\n  toIndex = -1 (a sentinel meaning 'Main Menu / No Mission').")]
	[SerializeField]
	private bool treatReturnToMainMenuAsMissionChange;

	[Header("What To Affect")]
	[Tooltip("Root of the selection. If left empty, this GameObject is used.\nIMPORTANT: This component's GameObject must be active to receive mission events.")]
	[SerializeField]
	private GameObject root;

	[Tooltip("Which objects to act on relative to the Root.\n- SelfOnly: only the Root.\n- ChildrenOnly: only children of the Root.\n- SelfAndChildren: the Root and its children.")]
	[SerializeField]
	private TargetSelection targetSelection;

	[Tooltip("If true, traverse all descendants (entire hierarchy under Root). If false, only direct children are processed.\nApplies only when the selection includes children.")]
	[SerializeField]
	private bool recursiveChildren;

	[Tooltip("If true, children are included even if inactive in the hierarchy. If false, only active-in-hierarchy children are included.\nNote: This component itself must still be active to receive mission events, regardless of this setting.")]
	[SerializeField]
	private bool includeInactiveChildren;

	[Tooltip("If true, skip any child that also has a MissionAdvanceReaction to avoid double-processing from nested components.\nThe Root is never skipped by this rule.")]
	[SerializeField]
	private bool skipChildrenWithReaction;

	[Header("Action")]
	[Tooltip("Built-in action to perform on each selected object when the trigger fires.\n- None: only invoke events.\n- Disable: SetActive(false).\n- Destroy: Destroy(obj) (end-of-frame).")]
	[SerializeField]
	private ActionType action;

	[Tooltip("If true, this reaction will run once and then automatically unsubscribe from mission events.\nRecommended for cleanup behaviors that should not repeat.\n\nNote: If 'Treat Return To Main Menu As Mission Change' is enabled, this also unsubscribes from Main Menu events.")]
	[SerializeField]
	private bool runOnce;

	[Tooltip("If true, skip the first mission event this component receives.\nAfter skipping the first event, subsequent triggers run normally.\n\nNote: If 'Treat Return To Main Menu As Mission Change' is enabled, the first *menu* event also counts toward this 'first' trigger.")]
	[SerializeField]
	private bool ignoreFirstTrigger;

	[Header("Events")]
	[Tooltip("Invoked once when the reaction triggers, before any per-object actions.\nUse for additional logic or cross-scene signaling.")]
	[SerializeField]
	private UnityEvent onTriggered;

	[Tooltip("Invoked for each selected object just BEFORE performing the built-in action. Receives the target GameObject.")]
	[SerializeField]
	private GameObjectEvent onBeforeEach;

	[Tooltip("Invoked for each selected object just AFTER performing the built-in action. Receives the target GameObject.\nNote: If Action = Destroy, the object is scheduled for destruction at end-of-frame and is still non-null here.")]
	[SerializeField]
	private GameObjectEvent onAfterEach;

	[Tooltip("Invoked once after all selected objects have been processed. Useful for chaining or summary actions.")]
	[SerializeField]
	private UnityEvent onCompleted;

	[Header("Debug")]
	[Tooltip("If true, prints detailed logs to help diagnose selection and timing.\nTip: Disable in production for performance and clean logs.")]
	[SerializeField]
	private bool verbose;

	private bool subscribed;

	private bool hasRun;

	private bool hasIgnoredFirst;

	private Coroutine subscribeRetryRoutine;

	private const int MainMenuSentinelIndex = -1;

	private void Reset()
	{
	}

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void TrySubscribeOrRetry()
	{
	}

	[IteratorStateMachine(typeof(_003CRetrySubscribeRoutine_003Ed__33))]
	private IEnumerator RetrySubscribeRoutine()
	{
		return null;
	}

	private void StopRetry()
	{
	}

	private void Subscribe()
	{
	}

	private void Unsubscribe()
	{
	}

	private void HandleMissionEvent(MissionGraph fromIndex, MissionGraph toIndex)
	{
	}

	private void HandleMainMenuLoading(string sceneName)
	{
	}

	private void HandleMainMenuLoaded(string sceneName)
	{
	}

	private void HandlePhaseChanged(MissionManager.GamePhase prev, MissionManager.GamePhase next)
	{
	}

	private void HandleTriggerInternal(MissionGraph fromIndex, MissionGraph toIndex, bool isMainMenuTransition)
	{
	}

	private List<GameObject> BuildTargetList()
	{
		return null;
	}

	private void CollectChildren(Transform rootTransform, List<GameObject> results)
	{
	}

	private void ExecuteAction(GameObject obj)
	{
	}

	private static void SafeInvoke(UnityEvent evt, string label)
	{
	}

	private static void SafeInvoke(GameObjectEvent evt, string label, GameObject arg)
	{
	}

	private static string SafeName(GameObject go)
	{
		return null;
	}

	[ContextMenu("Test Trigger Now (No Mission Indices)")]
	private void TestTriggerNow()
	{
	}
}
