using System.Collections.Generic;
using UnityEngine;

public class MyTextField : ClickBehaviour
{
	public enum FieldType
	{
		Generic = 0,
		PlayerName = 1,
		IP = 2,
		MachineName = 3,
		LevelName = 4,
		DirectoryName = 5
	}

	public TextMesh TextMeshy;

	public static string lastNameUsed;

	public static string lastMachineUsed;

	public static string lastLevelLoaded;

	public static string lastIpUsed;

	public static List<MyTextField> activeFields = new List<MyTextField>();

	public FieldType type;

	public string fileName;

	public float lineFlashTime = 0.1f;

	public bool isFlashing;

	public Transform maxLengthPos;

	public Transform suffix;

	public Transform blockFlash;

	public float flashBlockOffset = 0.001f;

	public bool allowPeriods;

	public bool setLastUsed = true;

	public bool canFocus;

	public static MyTextField activeField
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

	private void Start()
	{
		InvokeRepeating("FlashLine", lineFlashTime, lineFlashTime);
		SetFlashLine();
		SetFileName();
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

	public void ToggleFocus(bool toggle)
	{
		Renderer component = blockFlash.GetComponent<Renderer>();
		if (activeFields.Contains(this))
		{
			activeFields.Remove(this);
		}
		if (toggle)
		{
			activeFields.Add(this);
		}
		component.enabled = hasFocus;
	}

	private void OnEnable()
	{
		if (setLastUsed)
		{
			switch (type)
			{
			case FieldType.PlayerName:
				SetName(lastNameUsed);
				break;
			case FieldType.IP:
				SetName(lastIpUsed);
				break;
			case FieldType.MachineName:
				SetName(lastMachineUsed);
				break;
			case FieldType.LevelName:
				SetName(lastLevelLoaded);
				break;
			default:
				SetName(string.Empty);
				break;
			}
		}
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
				if (TextMeshy.text.Length > 0)
				{
					TextMeshy.text = TextMeshy.text.Substring(0, TextMeshy.text.Length - 1);
					SetFlashLine();
					SetSuffix();
					SetFileName();
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
					GUIUtility.systemCopyBuffer = TextMeshy.text;
					break;
				case 'v':
				{
					string systemCopyBuffer = GUIUtility.systemCopyBuffer;
					FillTextField(systemCopyBuffer);
					break;
				}
				}
			}
			else if ((allowPeriods || c != '.') && c != ',' && c != '|' && !flag)
			{
				string text = TextMeshy.text + c;
				FillTextField(text);
			}
		}
	}

	private void AddLetter(string letter)
	{
		if (TextMeshy.GetComponent<Renderer>().bounds.max.x < maxLengthPos.position.x)
		{
			TextMeshy.text += letter;
		}
	}

	private void SetSuffix()
	{
		suffix.position = new Vector3(TextMeshy.GetComponent<Renderer>().bounds.max.x, suffix.position.y, suffix.position.z);
	}

	public void SetFlashLine()
	{
		blockFlash.position = new Vector3(TextMeshy.GetComponent<Renderer>().bounds.max.x + flashBlockOffset, blockFlash.position.y, blockFlash.position.z);
	}

	private void FlashLine()
	{
		Renderer component = blockFlash.GetComponent<Renderer>();
		component.enabled = hasFocus && !component.enabled;
	}

	public void SetName(string namey)
	{
		FillTextField(namey);
	}

	private void FillTextField(string fileName)
	{
		TextMeshy.text = string.Empty;
		if (fileName == null)
		{
			fileName = string.Empty;
		}
		for (int i = 0; i < fileName.Length; i++)
		{
			AddLetter(fileName[i].ToString());
		}
		SetSuffix();
		SetFlashLine();
		SetFileName();
	}

	private void SetFileName()
	{
		switch (type)
		{
		case FieldType.PlayerName:
			fileName = TextMeshy.text;
			lastNameUsed = fileName;
			break;
		case FieldType.IP:
			fileName = TextMeshy.text;
			lastIpUsed = fileName;
			break;
		case FieldType.MachineName:
			fileName = StaticSettings.SanatizeFileName(TextMeshy.text);
			lastMachineUsed = fileName;
			break;
		case FieldType.LevelName:
			fileName = StaticSettings.SanatizeFileName(TextMeshy.text);
			lastLevelLoaded = fileName;
			break;
		case FieldType.DirectoryName:
			fileName = StaticSettings.SanatizeFileName(TextMeshy.text);
			break;
		}
	}
}
