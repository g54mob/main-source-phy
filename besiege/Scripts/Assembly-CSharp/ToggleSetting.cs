using System;
using UnityEngine;

public class ToggleSetting : ClickBehaviour
{
	public Material redMaterial;

	public Material darkMaterial;

	public static Action<ToggleSetting> DisableOthers;

	protected Renderer renderer;

	public virtual bool IsActive { get; set; }

	protected virtual void Awake()
	{
		renderer = GetComponent<Renderer>();
		Set();
	}

	protected virtual void OnDestroy()
	{
		DisableOthers = (Action<ToggleSetting>)Delegate.Remove(DisableOthers, new Action<ToggleSetting>(Disable));
	}

	public override void OnClicked()
	{
		IsActive = !IsActive;
	}

	public virtual void Set()
	{
		if (IsActive)
		{
			renderer.material = redMaterial;
		}
		else
		{
			renderer.material = darkMaterial;
		}
	}

	public void InvokeDisableOthers()
	{
		if (DisableOthers != null)
		{
			DisableOthers(this);
		}
	}

	protected virtual void Disable(ToggleSetting ignore)
	{
	}
}
