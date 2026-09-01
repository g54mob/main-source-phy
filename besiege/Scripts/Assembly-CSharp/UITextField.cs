using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[AddComponentMenu("UI/UI Text Field")]
[Obsolete("Use TextHolder instead")]
public class UITextField : ClickBehaviour
{
	public Action TextChanged;

	[SerializeField]
	protected Renderer textMeshRenderer;

	private static List<UITextField> activeFields = new List<UITextField>();

	[SerializeField]
	private TextMesh textMesh;

	[SerializeField]
	private TextMesh suffixTextMesh;

	[SerializeField]
	private Transform maxLengthPos;

	[SerializeField]
	private Transform cursorTransform;

	[SerializeField]
	private float flashBlockOffset = 0.025f;

	[SerializeField]
	private float lineFlashTime = 0.5f;

	[SerializeField]
	private bool allowPeriods;

	private bool canFocus;

	private string fileSuffix;

	public string Text
	{
		get
		{
			return textMesh.text;
		}
	}

	private static UITextField activeField
	{
		get
		{
			return (activeFields.Count <= 0) ? null : activeFields[activeFields.Count - 1];
		}
	}

	private bool hasFocus
	{
		get
		{
			return activeField == this;
		}
	}

	public void Clear()
	{
		SetFileSuffix(string.Empty);
		FillTextField(string.Empty);
	}

	public void SetCanFocus(bool canFocus)
	{
		this.canFocus = canFocus;
		ToggleFocus(canFocus);
	}

	public void ToggleFocus(bool toggle)
	{
		Renderer component = cursorTransform.GetComponent<Renderer>();
		if (activeFields.Contains(this))
		{
			activeFields.Remove(this);
		}
		if (toggle)
		{
			activeFields.Add(this);
		}
		component.enabled = hasFocus;
		if (toggle && !allowPeriods)
		{
			SetSuffixText(fileSuffix);
		}
	}

	public void SetName(string fileName)
	{
		string extension = Path.GetExtension(fileName);
		if (!string.IsNullOrEmpty(extension))
		{
			fileName = fileName.Substring(0, fileName.Length - extension.Length);
		}
		FillTextField(fileName);
		SetSuffixText(extension);
	}

	public void SetFileSuffix(string suffix)
	{
		fileSuffix = suffix;
		SetSuffixText(fileSuffix);
	}

	private void Awake()
	{
		if (textMeshRenderer == null)
		{
			Debug.LogError("TextMeshRenderer not assigned to UITextField on " + Machine.GetObjectPath(base.gameObject) + "!");
		}
		textMeshRenderer = textMesh.GetComponent<Renderer>();
	}

	private void Start()
	{
		InvokeRepeating("FlashLine", lineFlashTime, lineFlashTime);
		SetFlashLine();
	}

	public override void OnClicked()
	{
		if (canFocus && !hasFocus)
		{
			if (activeField != null)
			{
				activeField.ToggleFocus(false);
			}
			ToggleFocus(true);
		}
	}

	private void OnEnable()
	{
		if (canFocus)
		{
			ToggleFocus(false);
		}
		else
		{
			ToggleFocus(true);
		}
	}

	public override void OnDisable()
	{
		base.OnDisable();
		ToggleFocus(false);
	}

	private void Update()
	{
		if (!hasFocus)
		{
			return;
		}
		string inputString = Input.inputString;
		foreach (char c in inputString)
		{
			if (c == "\b"[0])
			{
				if (textMesh.text.Length > 0)
				{
					string fileName = textMesh.text.Substring(0, textMesh.text.Length - 1);
					FillTextField(fileName);
				}
				continue;
			}
			bool flag = c == '\n' || c == '\r';
			if (hasFocus && flag)
			{
				if (canFocus)
				{
					ToggleFocus(false);
				}
			}
			else if (InputManager.LeftHotCtrlKey())
			{
				switch (c)
				{
				case 'c':
					GUIUtility.systemCopyBuffer = textMesh.text;
					break;
				case 'v':
				{
					string text = GUIUtility.systemCopyBuffer;
					if (!allowPeriods)
					{
						text = text.Replace(".", string.Empty);
					}
					FillTextField(text);
					break;
				}
				}
			}
			else if ((allowPeriods || c != '.') && c != ',' && c != '|' && !flag)
			{
				string fileName2 = textMesh.text + c;
				FillTextField(fileName2);
			}
		}
	}

	private void AddLetter(string letter)
	{
		if (textMesh.GetComponent<Renderer>().bounds.max.x < maxLengthPos.position.x)
		{
			textMesh.text += letter;
		}
	}

	private void SetSuffixPosition()
	{
		Transform transform = suffixTextMesh.transform;
		transform.position = new Vector3(textMeshRenderer.bounds.max.x, transform.position.y, transform.position.z);
	}

	private void SetSuffixText(string suffixText)
	{
		suffixTextMesh.text = suffixText;
	}

	public void SetFlashLine()
	{
		cursorTransform.position = new Vector3(textMeshRenderer.bounds.max.x + flashBlockOffset, cursorTransform.position.y, cursorTransform.position.z);
	}

	private void FlashLine()
	{
		Renderer component = cursorTransform.GetComponent<Renderer>();
		component.enabled = hasFocus && !component.enabled;
	}

	private void FillTextField(string fileName)
	{
		textMesh.text = string.Empty;
		for (int i = 0; i < fileName.Length; i++)
		{
			AddLetter(fileName[i].ToString());
		}
		SetSuffixPosition();
		SetFlashLine();
		if (TextChanged != null)
		{
			TextChanged();
		}
	}
}
