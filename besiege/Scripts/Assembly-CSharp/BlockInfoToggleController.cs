using UnityEngine;

public class BlockInfoToggleController : ClickBehaviour
{
	public Renderer bgRendy;

	public Material startMat;

	public Material redMat;

	public bool isActive;

	public override void OnClicked()
	{
		Set(!isActive);
	}

	private void Set(bool toggle)
	{
		isActive = toggle;
		if (isActive)
		{
			bgRendy.material = redMat;
		}
		else
		{
			bgRendy.material = startMat;
		}
	}
}
