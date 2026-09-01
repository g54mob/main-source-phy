using UnityEngine;

public class LevelSwitchOnClick : ClickBehaviour
{
	public Transform ActiveSwitch;

	public bool SwitchState;

	private bool move;

	private float interpolate;

	private void Update()
	{
		if (move)
		{
			interpolate += Time.deltaTime * 2f;
			ActiveSwitch.position = Vector3.Lerp(ActiveSwitch.position, base.transform.position, interpolate);
			ActiveSwitch.localPosition = new Vector3(ActiveSwitch.localPosition.x, ActiveSwitch.localPosition.y, -0.02f);
			if (interpolate >= 1f)
			{
				move = false;
				ActiveSwitch.GetComponent<ActiveSwitch>().CurrentState = SwitchState;
			}
		}
	}

	public override void OnClicked()
	{
		interpolate = 0f;
		move = true;
	}
}
