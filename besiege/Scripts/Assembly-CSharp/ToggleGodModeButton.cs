using UnityEngine;

public class ToggleGodModeButton : ClickBehaviour
{
	public Material redMaterial;

	public Material darkMaterial;

	public GameObject lockIcon;

	public bool isMachinePower;

	public bool toggleMaterial = true;

	public Renderer rendy;

	protected virtual void OnEnable()
	{
		if (rendy == null)
		{
			rendy = GetComponent<Renderer>();
		}
		UpdateVisual();
		if (!StatMaster.isMP)
		{
			ToggleRule(IsRuleOn());
		}
	}

	public virtual string GetModeName()
	{
		return string.Empty;
	}

	public virtual bool IsRuleOn()
	{
		return false;
	}

	public virtual void ToggleRule(bool toggle)
	{
	}

	public bool IsRuleLocked()
	{
		if (StatMaster.isMP && LevelEditor.Instance != null)
		{
			LevelSettings settings = LevelEditor.Instance.Settings;
			return !StatMaster.Mode.levelEdit && settings != null && settings.IsRuleLocked(GetModeName());
		}
		return false;
	}

	public void UpdateGodMode()
	{
		if (isMachinePower)
		{
			ServerMachine serverMachine = Machine.Active() as ServerMachine;
			if (serverMachine != null)
			{
				serverMachine.UpdateGodMode();
			}
		}
	}

	public void UpdateVisual()
	{
		if (lockIcon != null)
		{
			lockIcon.SetActive(IsRuleLocked());
		}
		if (toggleMaterial)
		{
			rendy.material = ((!IsRuleOn()) ? darkMaterial : redMaterial);
		}
		else
		{
			rendy.enabled = IsRuleOn();
		}
	}

	public override void OnClicked()
	{
		if (base.enabled && !IsRuleLocked())
		{
			Set();
			UpdateGodMode();
		}
	}

	public bool Set()
	{
		if (!IsRuleOn())
		{
			StatMaster.GodTools.HasBeenUsed = true;
		}
		ToggleRule(!IsRuleOn());
		UpdateVisual();
		return IsRuleLocked();
	}
}
