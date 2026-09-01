using UnityEngine;

public class InsigniaTriggerInvisible : InsigniaTrigger
{
	public MeshRenderer[] vis;

	public Collider col;

	public GameObject icon;

	private MToggle hide;

	private MToggle collision;

	public PulseAlpha alphaScript;

	public TileTextureOnScale tileOnScale;

	public SetIconVirtualTrigger setTriggerIcon;

	private bool lastEnabled = true;

	public override void Init()
	{
		if (!isInitialized)
		{
			base.Init();
			fadeOutFlash.DisplayInMapper = false;
			hide = AddToggle(2499, "hide-visual", true);
			collision = AddToggle(3425, "enable-collision", false);
			collision.Toggled += ToggleCollider;
			hide.Toggled += ToggleIcon;
			ToggleCollider(collision.IsActive);
			UpdateVisibility(!hide.IsActive, false);
		}
	}

	public override bool TriggerEvaluate()
	{
		return collision.IsActive || base.TriggerEvaluate();
	}

	protected void ToggleIcon(bool t)
	{
		UpdateVisibility(!hide.IsActive, isSimulating);
		icon.SetActive(t);
	}

	protected void ToggleCollider(bool t)
	{
		col.isTrigger = !t;
	}

	protected override void Start()
	{
		base.Start();
		UpdateVisibility(!hide.IsActive, isSimulating);
	}

	public void UpdateVisibility(bool show, bool sim)
	{
		icon.SetActive(StatMaster.Mode.levelEdit && !sim && !show);
		bool flag = (StatMaster.Mode.levelEdit && !sim) || show;
		if (lastEnabled != flag)
		{
			alphaScript.enabled = flag;
			tileOnScale.enabled = flag;
			setTriggerIcon.enabled = flag;
			for (int i = 0; i < pulseAlpha.Length; i++)
			{
				pulseAlpha[i].enabled = flag;
			}
			for (int i = 0; i < vis.Length; i++)
			{
				vis[i].enabled = flag;
			}
			lastEnabled = flag;
		}
	}

	protected void OnCollisionEnter(Collision collision)
	{
		triggerObject.OnTriggerEnter(collision.collider);
	}

	protected void OnCollisionExit(Collision collision)
	{
		triggerObject.OnTriggerExit(collision.collider);
	}
}
