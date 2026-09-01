using System;
using System.Collections.Generic;
using Localisation;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class SimpleQueuedTextEmitter : MonoBehaviour
{
	public enum StartStrategy
	{
		DirectTypewriter = 0,
		UseTriggerComponent = 1
	}

	[Header("Print Queue Target")]
	[Tooltip("Teleprinter that receives the submitted lines.")]
	public Teleprinter.Teleprinters PrinterOutput;

	[Tooltip("Source ID attached to the print job. Use this to distinguish different emitters in your pipeline (e.g., 'SimpleText', 'Narration', 'MissionBanner').")]
	public string sourceId;

	[Header("Text")]
	[TextArea(2, 10)]
	[Tooltip("Text to emit. This is split into lines at newline characters and submitted as a single print job.\nIf 'Process Fire Mission Tokens' is enabled, the following tokens are supported and resolved using the assigned FireMissionSceneTemplate:\n  - [GRID <point>]        e.g., [GRID Alpha], [GRID @Target]\n  - [BEARING A B]         e.g., [BEARING Alpha @Target]\n  - [DIR [1|2|3] A B]     e.g., [DIR 1 Alpha @Target]\n  - [DIST A B]            e.g., [DIST Alpha @Ally]\n  - [POINT idOrRoleExpr]  e.g., [POINT Alpha], [POINT @Enemy]\n  - Remaining counts: [targetsremaining], [alliesremaining], [optionaltargetsremaining], [enemiesremaining]\nSelection expressions support roles and IDs: '@Target', '@Ally', '@OptionalTarget', '@Enemy', or explicit IDs.\nSafe examples:\n  'Operation begins at [GRID Alpha].'\n  'Nearest target distance: [DIST Alpha @Target].'")]
	[Obsolete("Use 'Text' Instead - Remove me once we have copied the text over to localisation ")]
	public string text;

	[Header("Text New")]
	[Tooltip("Text to emit. This is split into lines at newline characters and submitted as a single print job.\nIf 'Process Fire Mission Tokens' is enabled, the following tokens are supported and resolved using the assigned FireMissionSceneTemplate:\n  - [GRID <point>]        e.g., [GRID Alpha], [GRID @Target]\n  - [BEARING A B]         e.g., [BEARING Alpha @Target]\n  - [DIR [1|2|3] A B]     e.g., [DIR 1 Alpha @Target]\n  - [DIST A B]            e.g., [DIST Alpha @Ally]\n  - [POINT idOrRoleExpr]  e.g., [POINT Alpha], [POINT @Enemy]\n  - Remaining counts: [targetsremaining], [alliesremaining], [optionaltargetsremaining], [enemiesremaining]\nSelection expressions support roles and IDs: '@Target', '@Ally', '@OptionalTarget', '@Enemy', or explicit IDs.\nSafe examples:\n  'Operation begins at [GRID Alpha].'\n  'Nearest target distance: [DIST Alpha @Target].'")]
	public TextIdentifier Text;

	[Header("Token Processing (Optional)")]
	[Tooltip("If true, Fire Mission tokens in 'Text' are processed before submission using 'Mission Template' + its current instance. If no active instance is available, the raw text is submitted instead (a warning is logged).")]
	public bool processFireMissionTokens;

	[Tooltip("Mission template used to resolve tokens and grid codes when 'Process Fire Mission Tokens' is enabled. If left null, the script will attempt to FindObjectOfType at runtime.")]
	public FireMission missionTemplate;

	[Header("When To Emit")]
	[Tooltip("If true, the text is emitted automatically when this component is enabled.")]
	public bool emitOnEnable;

	[Tooltip("If true, this component will emit only once for its lifetime. Additional triggers (OnEnable or manual) will be ignored after the first submission.")]
	public bool onlyOnce;

	[Tooltip("If true, no submission occurs when 'Text' is null or whitespace.")]
	public bool skipIfEmpty;

	[Header("Advanced")]
	[Tooltip("Optional metadata attached to the PrintJob.userData field. This is not used by the typewriter but can be read by other systems.")]
	public UnityEngine.Object userData;

	[Header("Start Typewriter After Emit (Optional)")]
	[Tooltip("If true, start a Teleprinter after submitting this job. Choose a strategy below: direct start on the typewriter or via a TeleprinterStartTrigger.")]
	public bool startTypewriterAfterEmit;

	[Tooltip("Choose how to start:\n- DirectTypewriter: call TeleprinterQueueTypewriter.TryStart immediately.\n- UseTriggerComponent: call TeleprinterStartTrigger.ArmProgrammatically() to reuse trigger gating logic.")]
	public StartStrategy startStrategy;

	[Tooltip("When starting the Teleprinter directly, if true the emitter passes ignoreInitialDelay=true to TryStart so the typewriter bypasses its own initialStartDelay.")]
	public bool bypassTypewriterInitialDelay;

	[Tooltip("TeleprinterStartTrigger to use when Start Strategy is 'UseTriggerComponent'. If null and auto-find is enabled, the first instance (can include inactive) will be used.")]
	public NewTeleprinterStartTrigger startTrigger;

	[Tooltip("If true and 'Start Trigger' is not assigned, the script will attempt to auto-find a TeleprinterStartTrigger.\nIf 'Start Trigger Tag' is provided, it searches by tag first; otherwise it searches the scene for the first instance (can include inactive).")]
	public bool autoFindStartTrigger;

	[Tooltip("Optional tag used when auto-finding the TeleprinterStartTrigger. Leave blank to search all objects.")]
	public string startTriggerTag;

	[Header("Events")]
	[Tooltip("Invoked right before the text has been submitted to the print queue.")]
	public UnityEvent onEmitting;

	[Tooltip("Invoked after the text has been submitted to the print queue.")]
	public UnityEvent onEmitted;

	private bool _hasEmitted;

	private void OnValidate()
	{
	}

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	[ContextMenu("Trigger Output Now")]
	public void TriggerOutput()
	{
	}

	private void StartTeleprinterAfterEmit()
	{
	}

	private NewTeleprinterStartTrigger ResolveStartTrigger()
	{
		return null;
	}

	private NewTeleprinterStartTrigger TryAutoFindStartTrigger()
	{
		return null;
	}

	private void EnsureMissionTemplate()
	{
	}

	private static List<string> BuildLines(string block)
	{
		return null;
	}
}
