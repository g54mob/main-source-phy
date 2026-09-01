using System.Collections;
using UnityEngine;

[AddComponentMenu("UI/Tools/Bin Button")]
public class BinButton : ClickBehaviour
{
	public Renderer bgRend;

	public Material clickedMaterial;

	public Transform sureBox;

	public bool toggleBox;

	public float startPosY = 2.1f;

	public float endPosY = -1f;

	public float lerpInSpeed = 0.1f;

	public bool activey;

	private Material startMaterial;

	private void Start()
	{
		startMaterial = bgRend.material;
	}

	protected bool CanInteract()
	{
		Machine machine = Machine.Active();
		if (machine == null)
		{
			return false;
		}
		if (SelectionTool.BatchChange)
		{
			return false;
		}
		return machine.ReadyForSim;
	}

	public override void OnClicked()
	{
		Machine machine = Machine.Active();
		if (!machine || machine.isSimulating || !machine.CanModify || SelectionTool.BatchChange || !machine.ReadyForSim)
		{
			if (activey)
			{
				CloseAll();
			}
		}
		else if (!activey)
		{
			if (AdvancedBlockEditor.Instance.selectionController.Count > 0)
			{
				ReferenceMaster.ResetLevelEditor();
				DestroyMachine();
				CloseAll();
				return;
			}
			activey = true;
			bgRend.material = clickedMaterial;
			if (!toggleBox)
			{
				StartCoroutine(LerpPosIn());
			}
			sureBox.gameObject.SetActive(true);
			ReferenceMaster.ResetLevelEditor();
		}
		else
		{
			CloseAll();
		}
	}

	public void DestroyMachine()
	{
		BlockSelectionTool selectionController = AdvancedBlockEditor.Instance.selectionController;
		if (selectionController.CanSelect() && selectionController.Count > 0)
		{
			selectionController.RemoveSelection(false);
			return;
		}
		Machine machine = Machine.Active();
		if ((bool)machine && !machine.isSimulating && machine.CanModify)
		{
			StatMaster.Bounding.inLeftWall = false;
			StatMaster.Bounding.inRightWall = false;
			StatMaster.Bounding.inGround = false;
			StatMaster.Bounding.inRoof = false;
			StatMaster.Bounding.inBackWall = false;
			StatMaster.Bounding.inFrontWall = false;
			SingleInstance<MachineObjectTracker>.Instance.CreateNewMachine();
		}
	}

	public void CloseAll()
	{
		activey = false;
		if (!toggleBox)
		{
			StartCoroutine(LerpPosOut());
		}
		else
		{
			sureBox.gameObject.SetActive(false);
		}
		bgRend.material = startMaterial;
	}

	private IEnumerator LerpPosIn()
	{
		float cTime = 0f;
		float rate = 1f / lerpInSpeed;
		float startPosy = sureBox.localPosition.y;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			sureBox.localPosition = new Vector3(sureBox.localPosition.x, Mathf.Lerp(startPosy, endPosY, cTime), sureBox.localPosition.z);
			yield return null;
		}
	}

	private IEnumerator LerpPosOut()
	{
		float cTime = 0f;
		float rate = 1f / lerpInSpeed;
		float startPosy = sureBox.localPosition.y;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			sureBox.localPosition = new Vector3(sureBox.localPosition.x, Mathf.Lerp(startPosy, startPosY, cTime), sureBox.localPosition.z);
			yield return null;
		}
	}
}
