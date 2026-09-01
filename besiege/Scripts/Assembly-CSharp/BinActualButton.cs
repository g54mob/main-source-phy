using UnityEngine;

[AddComponentMenu("UI/Tools/Bin Actual Button")]
public class BinActualButton : ClickBehaviour
{
	public BinButton binControllerCode;

	public bool killMachine = true;

	public override void OnClicked()
	{
		Machine machine = Machine.Active();
		if (!(machine == null) && !machine.isSimulating && machine.CanModify)
		{
			ReferenceMaster.ResetLevelEditor();
			if (killMachine)
			{
				binControllerCode.DestroyMachine();
				GetComponent<AudioSource>().Play();
			}
			binControllerCode.CloseAll();
		}
	}
}
