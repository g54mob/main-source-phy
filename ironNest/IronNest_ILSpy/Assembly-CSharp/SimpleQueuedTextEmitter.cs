using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using Localisation;
using UnityEngine;
using UnityEngine.Events;

public class SimpleQueuedTextEmitter : MonoBehaviour
{
	public enum StartStrategy
	{
		DirectTypewriter,
		UseTriggerComponent
	}

	public Teleprinter.Teleprinters PrinterOutput;

	public string sourceId;

	public string text;

	public TextIdentifier Text;

	public bool processFireMissionTokens;

	public FireMission missionTemplate;

	public bool emitOnEnable;

	public bool onlyOnce;

	public bool skipIfEmpty;

	public UnityEngine.Object userData;

	public bool startTypewriterAfterEmit;

	public StartStrategy startStrategy;

	public bool bypassTypewriterInitialDelay;

	public NewTeleprinterStartTrigger startTrigger;

	public bool autoFindStartTrigger;

	public string startTriggerTag;

	public UnityEvent onEmitting;

	public UnityEvent onEmitted;

	private bool _hasEmitted;

	private void OnValidate()
	{
		if (startTypewriterAfterEmit && startStrategy == StartStrategy.UseTriggerComponent && autoFindStartTrigger && startTrigger == null)
		{
			NewTeleprinterStartTrigger newTeleprinterStartTrigger = TryAutoFindStartTrigger();
		}
	}

	private void Awake()
	{
		if (startTypewriterAfterEmit && startStrategy == StartStrategy.UseTriggerComponent && autoFindStartTrigger && startTrigger == null)
		{
			NewTeleprinterStartTrigger newTeleprinterStartTrigger = TryAutoFindStartTrigger();
		}
	}

	private void OnEnable()
	{
		if (emitOnEnable)
		{
			TriggerOutput();
		}
	}

	public void TriggerOutput()
	{
		if (onlyOnce && _hasEmitted)
		{
			return;
		}
		string text = Text.Get();
		if (skipIfEmpty && string.IsNullOrWhiteSpace(text))
		{
			return;
		}
		Teleprinter teleprinter = Teleprinter.GetTeleprinter(PrinterOutput);
		if (teleprinter != null)
		{
			string text2 = text.Replace("\r\n", "\n");
			string[] collection = text2.Split(new char[1] { '\n' }, StringSplitOptions.None);
			List<string> list = new List<string>(collection);
			if (list._size == 0)
			{
				return;
			}
			bool flag = !processFireMissionTokens;
			List<string> lines = list;
			if (!flag)
			{
				if (!missionTemplate)
				{
					FireMission fireMission = UnityEngine.Object.FindObjectOfType<FireMission>();
					missionTemplate = fireMission;
				}
				if (missionTemplate == null)
				{
					Debug.LogWarning("[SimpleQueuedTextEmitter] Token processing enabled but no mission template or active instance found. Submitting raw text.", this);
					lines = list;
				}
				else
				{
					List<string> list2 = FireMissionTokenProcessor.ProcessBlock(text);
					lines = list2;
				}
			}
			if (onEmitting != null)
			{
				onEmitting.Invoke();
			}
			bool waitForTrigger = default(bool);
			PrintJob printJob = teleprinter.SubmitLines(sourceId, lines, userData, waitForTrigger);
			if (onEmitted != null)
			{
				onEmitted.Invoke();
			}
			bool flag2 = !startTypewriterAfterEmit;
			_hasEmitted = true;
			if (!flag2)
			{
				StartTeleprinterAfterEmit();
			}
		}
		else
		{
			Debug.LogWarning("[SimpleQueuedTextEmitter] No Teleprinter Found. Text not submitted.", this);
		}
	}

	private void StartTeleprinterAfterEmit()
	{
		object message;
		if (startStrategy != StartStrategy.UseTriggerComponent)
		{
			Teleprinter teleprinter = Teleprinter.GetTeleprinter(PrinterOutput);
			if (!(teleprinter == null))
			{
				teleprinter.TryStart(bypassTypewriterInitialDelay);
				return;
			}
			message = "[SimpleQueuedTextEmitter] No Teleprinter Found. Text not submitted.";
		}
		else
		{
			bool flag = startTrigger != null;
			NewTeleprinterStartTrigger newTeleprinterStartTrigger2;
			if (!flag && autoFindStartTrigger != flag)
			{
				NewTeleprinterStartTrigger newTeleprinterStartTrigger = TryAutoFindStartTrigger();
				newTeleprinterStartTrigger2 = newTeleprinterStartTrigger;
			}
			else
			{
				newTeleprinterStartTrigger2 = startTrigger;
			}
			if ((bool)newTeleprinterStartTrigger2)
			{
				newTeleprinterStartTrigger2.ArmProgrammatically();
				return;
			}
			message = "[SimpleQueuedTextEmitter] Start strategy 'UseTriggerComponent' is enabled but no TeleprinterStartTrigger was found.";
		}
		Debug.LogWarning(message, this);
	}

	private NewTeleprinterStartTrigger ResolveStartTrigger()
	{
		bool flag = startTrigger != null;
		if (!flag && autoFindStartTrigger != flag)
		{
			return TryAutoFindStartTrigger();
		}
		return startTrigger;
	}

	private NewTeleprinterStartTrigger TryAutoFindStartTrigger()
	{
		if (!string.IsNullOrEmpty(startTriggerTag))
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag(startTriggerTag);
			if (gameObject != null)
			{
				if ((object)gameObject == null)
				{
					return (NewTeleprinterStartTrigger)(object)new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				NewTeleprinterStartTrigger newTeleprinterStartTrigger = default(NewTeleprinterStartTrigger);
				startTrigger = newTeleprinterStartTrigger;
				if ((bool)startTrigger)
				{
					return startTrigger;
				}
			}
		}
		NewTeleprinterStartTrigger newTeleprinterStartTrigger2 = UnityEngine.Object.FindFirstObjectByType<NewTeleprinterStartTrigger>(FindObjectsInactive.Include);
		startTrigger = newTeleprinterStartTrigger2;
		return startTrigger;
	}

	private void EnsureMissionTemplate()
	{
		if (!missionTemplate)
		{
			FireMission fireMission = UnityEngine.Object.FindObjectOfType<FireMission>();
			missionTemplate = fireMission;
		}
	}

	private static List<string> BuildLines(string block)
	{
		string text = block.Replace("\r\n", "\n");
		char[] array = new char[1];
		if (array.Length > 0)
		{
			array[0] = '\n';
			string[] collection = text.Split(array, StringSplitOptions.None);
			return new List<string>(collection);
		}
		return (List<string>)(object)new IndexOutOfRangeException();
	}

	public SimpleQueuedTextEmitter()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A436]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		sourceId = "SimpleText";
		text = "Hello, world.";
		emitOnEnable = true;
		skipIfEmpty = true;
		bypassTypewriterInitialDelay = true;
		startTriggerTag = "";
		base._002Ector();
	}
}
