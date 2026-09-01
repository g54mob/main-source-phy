using Landfall.TABS_Input;

public class Bug_CameraAbilityPush : Bug_CameraAbility
{
	private PlayerActions m_playerActions;

	public override void Disable()
	{
		base.transform.GetChild(0).gameObject.SetActive(value: false);
	}

	public override void Enable()
	{
	}

	private void Start()
	{
		m_playerActions = PlayerActions.Instance;
	}

	private void Update()
	{
		if (IsActive)
		{
			if (m_playerActions.m_TriggerAbility.IsPressed)
			{
				base.transform.GetChild(0).gameObject.SetActive(value: true);
			}
			else
			{
				base.transform.GetChild(0).gameObject.SetActive(value: false);
			}
		}
	}
}
