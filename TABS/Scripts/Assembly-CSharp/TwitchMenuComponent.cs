using GamepadUI.StateManager.Core;
using UnityEngine;

public class TwitchMenuComponent : UIComponent
{
	[SerializeField]
	private CodeAnimation m_CodeAnimationRoot;

	[SerializeField]
	private CodeAnimation m_AdditionalSettingsCogButtonAnimator;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	protected override void OnOpen()
	{
		base.OnOpen();
		if (m_CodeAnimationRoot != null)
		{
			m_CodeAnimationRoot.PlayIn();
		}
		if (m_AdditionalSettingsCogButtonAnimator != null)
		{
			m_AdditionalSettingsCogButtonAnimator.PlayOut();
		}
	}

	protected override void OnClose()
	{
		base.OnClose();
		if (m_CodeAnimationRoot != null)
		{
			m_CodeAnimationRoot.PlayOut();
		}
		if (m_AdditionalSettingsCogButtonAnimator != null)
		{
			m_AdditionalSettingsCogButtonAnimator.PlayIn();
		}
	}

	protected override void Update()
	{
		base.Update();
	}
}
