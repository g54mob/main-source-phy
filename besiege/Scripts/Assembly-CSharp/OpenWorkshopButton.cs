using UnityEngine;

public abstract class OpenWorkshopButton : SimpleUIButton
{
	protected override void Awake()
	{
		if (Initialize())
		{
			base.Awake();
		}
		else
		{
			Object.DestroyImmediate(base.gameObject);
		}
	}

	protected abstract bool Initialize();
}
