using UnityEngine;

[AddComponentMenu("UI/UI Button (Extended)")]
public class UIButtonExtended : UIButton
{
	public GameObject BG;

	public MonoBehaviour[] scripts;

	public Renderer icon;

	public bool IsBGActive
	{
		get
		{
			return BG.activeSelf;
		}
	}

	public event DownRef DownRef;

	public void EnableScripts()
	{
		for (int i = 0; i < scripts.Length; i++)
		{
			scripts[i].enabled = true;
		}
		ToggleButton(true);
	}

	public void DisableScripts()
	{
		for (int i = 0; i < scripts.Length; i++)
		{
			scripts[i].enabled = false;
		}
		ToggleButton(false);
	}

	public void SetIconAlpha(float alpha)
	{
		string propertyName = "_TintColor";
		Material material = icon.material;
		if ((bool)icon && material.HasProperty(propertyName))
		{
			Color color = material.GetColor(propertyName);
			material.SetColor(propertyName, new Color(color.r, color.g, color.b, alpha));
		}
	}

	protected override bool _InvokeOnDown()
	{
		if (base._InvokeOnDown())
		{
			DownRef downRef = this.DownRef;
			if (downRef != null)
			{
				downRef(this);
			}
			return true;
		}
		return false;
	}

	public void ToggleBG(bool toggle)
	{
		if (BG.activeSelf != toggle)
		{
			BG.SetActive(toggle);
		}
	}

	public override void ResetDelegates()
	{
		base.ResetDelegates();
		this.DownRef = null;
	}
}
