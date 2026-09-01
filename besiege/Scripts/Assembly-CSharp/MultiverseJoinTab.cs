using System;
using Localisation;
using UnityEngine;
using UnityEngine.UI;

public class MultiverseJoinTab : MultiverseTab
{
	[SerializeField]
	private InputField nameField;

	[SerializeField]
	private InputField ipField;

	[SerializeField]
	private Button joinBtn;

	[SerializeField]
	private InputField portField;

	[SerializeField]
	private GameObject portItem;

	[SerializeField]
	private Image ipIcon;

	[SerializeField]
	private Sprite playfabIcon;

	[SerializeField]
	private Sprite serverIcon;

	private bool UsePlayfab
	{
		get
		{
			return OptionsMaster.networkType == PlayerNetworkType.Playfab;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		joinBtn.onClick.AddListener(OnStartJoin);
		InputField inputField = nameField;
		inputField.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(inputField.onValidateInput, new InputField.OnValidateInput(ReferenceMaster.StripSurrogateFromTextField));
		InputField inputField2 = ipField;
		inputField2.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(inputField2.onValidateInput, new InputField.OnValidateInput(ReferenceMaster.StripSurrogateFromTextField));
		InputField inputField3 = portField;
		inputField3.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(inputField3.onValidateInput, new InputField.OnValidateInput(ReferenceMaster.StripSurrogateFromTextField));
		InputField inputField4 = ipField;
		inputField4.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(inputField4.onValidateInput, (InputField.OnValidateInput)((string input, int charIndex, char addedChar) => OnValidateIP(addedChar)));
	}

	protected void Start()
	{
		nameField.text = OptionsMaster.BesiegeConfig.PlayerName;
		if (UsePlayfab)
		{
			ipField.text = string.Empty;
		}
		else
		{
			ipField.text = OptionsMaster.BesiegeConfig.LastConnectedAddress;
		}
	}

	protected void OnEnable()
	{
		UpdateUI();
	}

	public override void UpdateUI()
	{
		Text component = ipField.placeholder.GetComponent<Text>();
		if (UsePlayfab)
		{
			if (portItem.gameObject.activeInHierarchy)
			{
				portItem.SetActive(false);
				ipField.text = string.Empty;
			}
			ipIcon.sprite = playfabIcon;
			(ipIcon.transform as RectTransform).sizeDelta = new Vector2(45f, 45f);
			ipIcon.transform.localRotation = Quaternion.identity;
			component.text = LocalisationManager.GetTranslation(16).ToUpper();
		}
		else
		{
			ipIcon.sprite = serverIcon;
			ipIcon.transform.localRotation = Quaternion.Euler(0f, 0f, 45f);
			(ipIcon.transform as RectTransform).sizeDelta = new Vector2(85f, 85f);
			component.text = LocalisationManager.GetTranslation(1903);
			if (!portItem.gameObject.activeInHierarchy)
			{
				portItem.SetActive(true);
				ipField.text = string.Empty;
			}
		}
	}

	protected void OnDisable()
	{
		InputField inputField = nameField;
		inputField.onValidateInput = (InputField.OnValidateInput)Delegate.Remove(inputField.onValidateInput, new InputField.OnValidateInput(ReferenceMaster.StripSurrogateFromTextField));
		InputField inputField2 = ipField;
		inputField2.onValidateInput = (InputField.OnValidateInput)Delegate.Remove(inputField2.onValidateInput, new InputField.OnValidateInput(ReferenceMaster.StripSurrogateFromTextField));
		InputField inputField3 = portField;
		inputField3.onValidateInput = (InputField.OnValidateInput)Delegate.Remove(inputField3.onValidateInput, new InputField.OnValidateInput(ReferenceMaster.StripSurrogateFromTextField));
	}

	private char OnValidateIP(char newChar)
	{
		if (UsePlayfab)
		{
			return (newChar < '\u0080') ? newChar : '\0';
		}
		if (!char.IsDigit(newChar) && newChar != '.')
		{
			newChar = '\0';
		}
		return newChar;
	}

	private void OnStartJoin()
	{
		if (UsePlayfab)
		{
			ReferenceMaster.onJoinPF(nameField.text, ipField.text);
			return;
		}
		int result = 7777;
		int.TryParse(portField.text, out result);
		ReferenceMaster.onJoin(nameField.text, ipField.text, result);
	}
}
