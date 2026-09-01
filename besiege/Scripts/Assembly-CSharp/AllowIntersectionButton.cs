using UnityEngine;

[AddComponentMenu("UI/Tools/Allow Intersection Button")]
public class AllowIntersectionButton : ClickBehaviour
{
	public Renderer bgRend;

	private void Awake()
	{
		bgRend.gameObject.SetActive(StatMaster.Mode.allowIntersection);
	}

	public override void OnClicked()
	{
		if (!base.enabled)
		{
			bgRend.gameObject.SetActive(false);
			return;
		}
		Machine machine = Machine.Active();
		if ((bool)machine && !machine.isSimulating && machine.CanModify)
		{
			if (StatMaster.Mode.allowIntersection)
			{
				EnableIntersectionBlock();
			}
			else
			{
				DisableIntersectionBlock();
			}
			ReferenceMaster.ResetLevelEditor();
		}
	}

	public void DisableIntersectionBlock()
	{
		bgRend.gameObject.SetActive(true);
		StatMaster.Mode.allowIntersection = true;
	}

	public void EnableIntersectionBlock()
	{
		EnableExternal();
	}

	public void EnableExternal()
	{
		bgRend.gameObject.SetActive(false);
		StatMaster.Mode.allowIntersection = false;
	}
}
