using UnityEngine;

public class ResolutionNumberField : ClickBehaviour
{
	public TextMesh textMesh;

	public int myValue = 1;

	public string activeString;

	public bool textBoxActive;

	public float flashBlockOffset = 0.1f;

	public float lineFlashTime = 0.1f;

	public Transform blockFlash;

	private bool mouseOver;

	private void Start()
	{
		InvokeRepeating("FlashLine", lineFlashTime, lineFlashTime);
		activeString = myValue + string.Empty;
		SetFlashLine();
	}

	private void OnEnable()
	{
		SetInactive();
		activeString = myValue + string.Empty;
		SetFlashLine();
	}

	private void Update()
	{
		if (!mouseOver && InputManager.LeftMouseButton())
		{
			SetInactive();
		}
		if (textBoxActive)
		{
			CheckKeyPresses();
		}
		if (!textBoxActive)
		{
			activeString = myValue.ToString();
			textMesh.text = string.Empty + activeString;
		}
	}

	private void OnMouseEnter()
	{
		mouseOver = true;
	}

	private void OnMouseExit()
	{
		mouseOver = false;
	}

	public override void OnClicked()
	{
		SetActive();
	}

	private void SetActive()
	{
		textBoxActive = true;
	}

	private void SetInactive()
	{
		textBoxActive = false;
		myValue = int.Parse(activeString);
		SetNumber();
	}

	private void CheckKeyPresses()
	{
		string inputString = Input.inputString;
		for (int i = 0; i < inputString.Length; i++)
		{
			char c = inputString[i];
			switch (c)
			{
			case '\b':
				if (activeString.Length >= 1)
				{
					activeString = activeString.Substring(0, activeString.Length - 1);
					SetNumber();
				}
				break;
			default:
				if (!char.IsDigit(c))
				{
					if (c == '\n' || c == '\r')
					{
						SetInactive();
					}
					break;
				}
				goto case '.';
			case '.':
				if (activeString.Length < 8)
				{
					activeString += c;
					SetNumber();
				}
				break;
			}
		}
	}

	private void SetFlashLine()
	{
		blockFlash.position = new Vector3(textMesh.GetComponent<Renderer>().bounds.max.x + flashBlockOffset, blockFlash.position.y, blockFlash.position.z);
	}

	private void FlashLine()
	{
		Renderer component = blockFlash.GetComponent<Renderer>();
		if (textBoxActive)
		{
			if (component.enabled)
			{
				component.enabled = false;
			}
			else
			{
				component.enabled = true;
			}
		}
		else
		{
			component.enabled = false;
		}
	}

	private void ResetValue()
	{
		myValue = 0;
		activeString = "0";
	}

	private void SetNumber()
	{
		textMesh.text = string.Empty + activeString;
		SetFlashLine();
	}

	private void SetCustomNumber(float num)
	{
		myValue = (int)num;
	}
}
