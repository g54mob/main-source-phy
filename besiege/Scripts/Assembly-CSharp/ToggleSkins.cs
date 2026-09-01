using UnityEngine;

public class ToggleSkins : ToggleSetting
{
	public TextMesh text;

	private bool assigned;

	public override bool IsActive
	{
		get
		{
			return OptionsMaster.skinsEnabled;
		}
		set
		{
			OptionsMaster.skinsEnabled = value;
		}
	}

	protected override void Awake()
	{
		BlockSkinLoader.SkinModified += Modified;
		assigned = true;
		base.Awake();
	}

	protected override void OnDestroy()
	{
		if (assigned)
		{
			BlockSkinLoader.SkinModified -= Modified;
		}
	}

	private void Modified(BlockSkinLoader.SModifier m)
	{
		if (m == BlockSkinLoader.UpdateAll)
		{
			Set();
		}
	}

	public override void OnClicked()
	{
		base.OnClicked();
	}
}
