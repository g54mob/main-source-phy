using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsoleView : SingleInstanceFindOnly<ConsoleView>, IDragHandler, IEventSystemHandler
{
	[SerializeField]
	private GameObject viewContainer;

	[SerializeField]
	private Text logTextArea;

	[SerializeField]
	private InputField inputField;

	[SerializeField]
	private GameObject textEntryTemplate;

	private string consoleKey;

	private ConsoleController console;

	private string lastInputText = string.Empty;

	private RectTransform rectTransform;

	private ScrollRect scrollRect;

	private FixedSizedQueue<GameObject> textEntries;

	private List<string> logHistory = new List<string>();

	private Queue<string> linesToAppend = new Queue<string>();

	private bool blockingHotkeys;

	public ConsoleController Controller
	{
		get
		{
			return console;
		}
	}

	public override string Name
	{
		get
		{
			return "ConsoleView";
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		rectTransform.position += new Vector3(eventData.delta.x, eventData.delta.y);
	}

	public void RunCommand()
	{
		string text = StaticSettings.SanatizeString(inputField.text);
		if (!lastInputText.Equals(text) && !string.IsNullOrEmpty(text))
		{
			console.RunCommandString(text);
			inputField.text = string.Empty;
			FocusInputField();
		}
	}

	public void OnEditComplete()
	{
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			RunCommand();
		}
	}

	private void FocusInputField()
	{
		inputField.ActivateInputField();
	}

	protected override void Awake()
	{
		base.Awake();
		textEntries = new FixedSizedQueue<GameObject>(200);
		textEntries.OnItemDequeue = OnTextEntryDequeued;
		if (textEntryTemplate != null)
		{
			textEntryTemplate.SetActive(false);
		}
		else
		{
			Debug.LogWarning("textEntryTemplate is not set, please assign it");
		}
		rectTransform = GetComponent<RectTransform>();
		console = new BesiegeConsoleController();
		console.Initialize();
		ReferenceMaster.ConsoleController = (IConsoleController)console;
	}

	private void OnTextEntryDequeued(GameObject entryGameObject)
	{
		UnityEngine.Object.Destroy(entryGameObject);
	}

	private void Start()
	{
		scrollRect = GetComponentInChildren<ScrollRect>(true);
		InputField obj = inputField;
		obj.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(obj.onValidateInput, new InputField.OnValidateInput(OnValidateTextField));
		ControlScheme.ControlEntry controlEntry = OptionsMaster.CustomControls.General[15];
		consoleKey = controlEntry.Options[0].Keys[0].ToString();
		if (console != null)
		{
			console.VisibilityChanged += OnVisibilityChanged;
			console.LogChanged += OnLogChanged;
			console.RequestClearLog += OnRequestClearLog;
		}
	}

	private void OnRequestClearLog()
	{
		GameObject result;
		while (textEntries.TryDequeue(out result))
		{
			UnityEngine.Object.Destroy(result);
		}
	}

	private char OnValidateTextField(string input, int charIndex, char addedChar)
	{
		addedChar = ReferenceMaster.StripSurrogateFromTextField(input, charIndex, addedChar);
		if (consoleKey.Length == 1 && addedChar == consoleKey[0])
		{
			addedChar = '\0';
		}
		return addedChar;
	}

	private void AutoComplete()
	{
		inputField.text = console.AutoComplete(inputField.text);
	}

	private void ShowPreviousCommand(bool upInHistory)
	{
		inputField.text = console.GetPreviousCommand(upInHistory);
	}

	private void Update()
	{
		if (Debug.isDebugBuild && SingleInstance<StatMaster>.Instance.isDebug && SingleInstanceFindOnly<NetworkAnalyser>.Instance.DoneTesting && SRDebug.Instance.IsDebugPanelVisible)
		{
			if (viewContainer.activeSelf)
			{
				SetVisibility(false);
			}
			return;
		}
		while (linesToAppend.Count > 0)
		{
			UpdateLogStr(linesToAppend.Dequeue());
		}
		if (blockingHotkeys && !inputField.isFocused)
		{
			StatMaster.StopHotKeys(false);
			blockingHotkeys = false;
		}
		else if (!blockingHotkeys && inputField.isFocused)
		{
			StatMaster.StopHotKeys(true);
			blockingHotkeys = true;
		}
		if ((!StatMaster.stopHotkeys || blockingHotkeys) && InputManager.ConsoleKey())
		{
			ToggleVisibility();
		}
		if (viewContainer.activeSelf)
		{
			if (Input.GetKeyUp(KeyCode.Tab))
			{
				AutoComplete();
			}
			else if (Input.GetKeyUp(KeyCode.UpArrow))
			{
				ShowPreviousCommand(true);
			}
			else if (Input.GetKeyUp(KeyCode.DownArrow))
			{
				ShowPreviousCommand(false);
			}
		}
	}

	public void ToggleVisibility()
	{
		SetVisibility(!viewContainer.activeSelf);
	}

	private void SetVisibility(bool visible)
	{
		if (visible)
		{
			Invoke("FocusInputField", 0.2f);
		}
		else
		{
			inputField.text = string.Empty;
		}
		viewContainer.SetActive(visible);
	}

	private void OnVisibilityChanged(bool visible)
	{
		SetVisibility(visible);
	}

	private void OnLogChanged(string newLogEntry)
	{
		linesToAppend.Enqueue(newLogEntry);
	}

	private void UpdateLogStr(string newLogEntry)
	{
		if (string.IsNullOrEmpty(newLogEntry))
		{
			return;
		}
		logHistory.Add(newLogEntry);
		if (textEntryTemplate == null)
		{
			Debug.LogWarning("textEntryTemplate is not set, please assign it");
			return;
		}
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(textEntryTemplate, textEntryTemplate.transform.parent);
		Text component = gameObject.GetComponent<Text>();
		component.text = newLogEntry;
		gameObject.SetActive(true);
		textEntries.Enqueue(gameObject);
		if (viewContainer.activeSelf)
		{
			Canvas.ForceUpdateCanvases();
			scrollRect.verticalNormalizedPosition = 0f;
			Canvas.ForceUpdateCanvases();
		}
	}

	private void OnDestroy()
	{
		console.VisibilityChanged -= OnVisibilityChanged;
		console.LogChanged -= OnLogChanged;
		InputField obj = inputField;
		obj.onValidateInput = (InputField.OnValidateInput)Delegate.Remove(obj.onValidateInput, new InputField.OnValidateInput(OnValidateTextField));
		console.RequestClearLog -= OnRequestClearLog;
		textEntries.OnItemDequeue = null;
	}
}
