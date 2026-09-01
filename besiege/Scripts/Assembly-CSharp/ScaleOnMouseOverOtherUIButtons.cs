using UnityEngine;

public class ScaleOnMouseOverOtherUIButtons : MonoBehaviour
{
	public float sizeScaler = 1.4f;

	public Transform objToScale;

	public float mousePressedScale = 0.85f;

	public UIButton[] buttons;

	public GameObject[] objsToActive;

	private Vector3 startScale;

	private Vector3 normalScale;

	private Vector3 overScale;

	private bool active = true;

	private void Awake()
	{
		startScale = objToScale.localScale;
		overScale = startScale * sizeScaler;
	}

	private void Start()
	{
		for (int i = 0; i < objsToActive.Length; i++)
		{
			if (objsToActive[i].activeSelf)
			{
				objsToActive[i].SetActive(false);
			}
		}
	}

	protected void LateUpdate()
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			if (buttons[i].IsHovered)
			{
				Enter();
				return;
			}
		}
		Exit();
	}

	private void Enter()
	{
		if (!active)
		{
			return;
		}
		objToScale.localScale = overScale;
		for (int i = 0; i < objsToActive.Length; i++)
		{
			if (!objsToActive[i].activeSelf)
			{
				objsToActive[i].SetActive(true);
			}
		}
	}

	private void Exit()
	{
		if (!active)
		{
			return;
		}
		for (int i = 0; i < objsToActive.Length; i++)
		{
			if (objsToActive[i].activeSelf)
			{
				objsToActive[i].SetActive(false);
			}
		}
		objToScale.localScale = startScale;
	}

	private void OnDisable()
	{
		objToScale.localScale = startScale;
	}

	private void SetEnabledMsg(bool enabled)
	{
		Exit();
		active = enabled;
	}
}
