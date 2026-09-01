using UnityEngine;

public class IndividualProvinceController : MonoBehaviour
{
	public Color unlockedColor = new Color(0f, 0.8f, 1f);

	public Color lockedColor = new Color(1f, 0f, 0.11f);

	public bool locked = true;

	public bool isConquered;

	public int myIndex;

	public LEVELLORD levelLordCode;

	public Renderer lockRenderer;

	public LoadALevel loadCode;

	public GameObject unbeatenObj;

	public Transform conqueredFlag;

	public Renderer conqueredText;

	public Transform myTooltip;

	public GameObject demoText;

	private HighlightOnMouseOver highlightMouseOver;

	public MeshRenderer intactObj;

	public MeshRenderer diyObj;

	public MeshRenderer achieveObj;

	public MeshRenderer achieveNAObj;

	private void Start()
	{
		if (myTooltip != null)
		{
			lockRenderer = myTooltip.FindChild("LOCK").GetComponent<Renderer>();
			conqueredText = myTooltip.FindChild("CONQUERED").GetComponent<Renderer>();
		}
		highlightMouseOver = GetComponent<HighlightOnMouseOver>();
		CheckUnlock();
		CheckConquered();
	}

	private void OnMouseExit()
	{
		if (locked)
		{
			Lock();
		}
	}

	private void CheckUnlock()
	{
		if (myIndex == 0)
		{
			Unlock();
			return;
		}
		int num = 0;
		int num2 = 0;
		switch (myIndex)
		{
		case 1:
		case 16:
		case 35:
		case 45:
		case 56:
			num = myIndex;
			num2 = num - 1;
			break;
		case 54:
			num = 1;
			num2 = num - 1;
			break;
		case 55:
			num = myIndex;
			num2 = myIndex;
			break;
		case 65:
			num = 61;
			num2 = num - 1;
			break;
		case 62:
			num = 67;
			num2 = num - 1;
			break;
		case 68:
			num = 62;
			num2 = 67;
			break;
		case 63:
			num = 68;
			num2 = 62;
			break;
		case 69:
			num = 64;
			num2 = num - 1;
			break;
		default:
			num = myIndex - 1;
			num2 = num - 1;
			break;
		}
		bool flag = LEVELLORD.levelsComplete[num2] == 1;
		bool flag2 = LEVELLORD.levelsComplete[num] == 1;
		bool flag3 = LEVELLORD.levelsComplete[myIndex] == 1 || myIndex == 55;
		if (flag || flag2 || flag3)
		{
			Unlock();
		}
		else
		{
			Lock();
		}
	}

	private void CheckConquered()
	{
		if (LEVELLORD.levelsComplete[myIndex] == 1)
		{
			conqueredFlag.gameObject.SetActive(true);
			conqueredText.gameObject.SetActive(true);
			if ((bool)unbeatenObj)
			{
				unbeatenObj.SetActive(false);
			}
			if ((bool)intactObj)
			{
				ObjectiveTrackerUI.ToggleObjectives(myIndex, intactObj, diyObj, achieveObj);
			}
		}
		else
		{
			conqueredFlag.gameObject.SetActive(false);
			conqueredText.gameObject.SetActive(false);
			if ((bool)unbeatenObj)
			{
				unbeatenObj.SetActive(true);
			}
			if ((bool)intactObj)
			{
				intactObj.transform.parent.gameObject.SetActive(false);
			}
		}
	}

	private void Lock()
	{
		if (lockRenderer != null)
		{
			lockRenderer.gameObject.SetActive(true);
		}
		if (highlightMouseOver != null)
		{
			highlightMouseOver.colourToLerpTo = lockedColor;
		}
		if (loadCode != null)
		{
			loadCode.unlocked = false;
		}
		locked = true;
	}

	private void Unlock()
	{
		if (lockRenderer != null)
		{
			lockRenderer.gameObject.SetActive(false);
		}
		if (highlightMouseOver != null)
		{
			highlightMouseOver.colourToLerpTo = unlockedColor;
		}
		if (loadCode != null)
		{
			loadCode.unlocked = true;
		}
		locked = false;
	}
}
