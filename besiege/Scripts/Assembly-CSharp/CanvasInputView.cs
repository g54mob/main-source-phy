using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasInputView : MonoBehaviour, ICanvasInputView
{
	protected ICanvasInputController controller;

	[SerializeField]
	protected GameObject viewContainer;

	[SerializeField]
	private bool shouldSetInMenu;

	[SerializeField]
	private InputField inputField;

	[SerializeField]
	private GameObject textEntryTemplate;

	private string lastInputText = string.Empty;

	private ScrollRect scrollRect;

	private FixedSizedQueue<GameObject> textEntries;

	private bool hasFocus;

	protected bool hasToggleViewKey;

	private bool wasPreviouslyEnabled;

	private bool lastFrameStopHotkey;

	public bool IsVisible
	{
		get
		{
			return viewContainer.activeSelf;
		}
	}

	protected virtual bool toggleViewKey { get; private set; }

	public virtual void SetVisibility(bool visible)
	{
		if (visible)
		{
			if (shouldSetInMenu)
			{
				StatMaster.SetInMenu(true);
			}
			Invoke("FocusInputField", 0.2f);
		}
		else
		{
			inputField.text = string.Empty;
			if (shouldSetInMenu)
			{
				StatMaster.SetInMenu(false);
			}
		}
		viewContainer.SetActive(visible);
		controller.OnVisibilityChanged(visible);
	}

	public void AddTextEntry(string newTextEntry)
	{
		if (!string.IsNullOrEmpty(newTextEntry))
		{
			if (textEntryTemplate == null)
			{
				Debug.LogWarning("textEntryTemplate is not set, please assign it");
				return;
			}
			CreateTextEntry(newTextEntry);
			ScrollToBottom();
		}
	}

	protected virtual void Initialize()
	{
		controller = new CanvasInputController();
		controller.Initialize(this);
	}

	protected virtual void LateUpdate()
	{
		if (Debug.isDebugBuild && SingleInstance<StatMaster>.Instance.isDebug && SRDebug.Instance.IsDebugPanelVisible)
		{
			if (viewContainer.activeSelf)
			{
				SetVisibility(false);
			}
			return;
		}
		bool flag = (!lastFrameStopHotkey && !StatMaster.stopHotkeys && !StatMaster.inMenu) || viewContainer.activeSelf;
		if (hasToggleViewKey && toggleViewKey && flag)
		{
			ToggleVisibility();
		}
		if (inputField.isFocused != hasFocus)
		{
			StatMaster.StopHotKeys(inputField.isFocused);
			hasFocus = inputField.isFocused;
		}
		controller.OnUpdate();
		lastFrameStopHotkey = StatMaster.stopHotkeys;
	}

	protected virtual void OnDestroy()
	{
		if (controller != null)
		{
			controller.Dispose();
		}
		InputField obj = inputField;
		obj.onValidateInput = (InputField.OnValidateInput)Delegate.Remove(obj.onValidateInput, new InputField.OnValidateInput(OnValidateTextField));
		inputField.onEndEdit.RemoveListener(OnEditComplete);
		textEntries.OnItemDequeue = null;
	}

	protected void ScrollToBottom()
	{
		Canvas.ForceUpdateCanvases();
		scrollRect.verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
	}

	private void CreateTextEntry(string newTextEntry)
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(textEntryTemplate, textEntryTemplate.transform.parent);
		Text component = gameObject.GetComponent<Text>();
		component.text = newTextEntry;
		gameObject.SetActive(true);
		textEntries.Enqueue(gameObject);
	}

	public void Clear()
	{
		GameObject result;
		while (textEntries.TryDequeue(out result))
		{
			UnityEngine.Object.Destroy(result);
		}
	}

	private void ResetInputField()
	{
		inputField.text = string.Empty;
		FocusInputField();
	}

	private void OnEditComplete(string inputText)
	{
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			string text = StaticSettings.SanatizeString(inputText);
			if (!lastInputText.Equals(text) && !string.IsNullOrEmpty(text))
			{
				controller.HandleInput(text);
				ResetInputField();
			}
		}
	}

	private void FocusInputField()
	{
		inputField.ActivateInputField();
	}

	private void Awake()
	{
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
		inputField.onEndEdit.AddListener(OnEditComplete);
		Initialize();
	}

	private char OnValidateTextField(string input, int charIndex, char addedChar)
	{
		addedChar = ReferenceMaster.StripSurrogateFromTextField(input, charIndex, addedChar);
		return addedChar;
	}

	private void ToggleVisibility()
	{
		SetVisibility(!viewContainer.activeSelf);
	}

	private void OnVisibilityChanged(bool visible)
	{
		SetVisibility(visible);
	}

	private void OnLogChanged(string newLogEntry)
	{
		AddTextEntry(newLogEntry);
	}

	private void OnDisable()
	{
		wasPreviouslyEnabled = viewContainer.activeSelf;
		if (wasPreviouslyEnabled)
		{
			SetVisibility(false);
		}
	}

	private void OnEnable()
	{
		if (wasPreviouslyEnabled)
		{
			SetVisibility(true);
		}
	}
}
