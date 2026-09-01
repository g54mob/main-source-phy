using UnityEngine;

public class SandboxToggleButton : ClickBehaviour
{
	[SerializeField]
	private PlanetSandboxToggler toggler;

	[SerializeField]
	private Vector3 moverOffset = new Vector3(25f, 0f, 0f);

	[SerializeField]
	private MeshRenderer[] toEnable = new MeshRenderer[0];

	[SerializeField]
	private MeshRenderer[] toDisable = new MeshRenderer[0];

	[SerializeField]
	private Animator anim;

	public override void OnClicked()
	{
		if ((bool)anim)
		{
			anim.enabled = false;
		}
		for (int i = 0; i < toEnable.Length; i++)
		{
			toEnable[i].enabled = true;
		}
		for (int j = 0; j < toDisable.Length; j++)
		{
			toDisable[j].enabled = false;
		}
		if (toggler != null)
		{
			toggler.Toggle(moverOffset);
		}
	}
}
