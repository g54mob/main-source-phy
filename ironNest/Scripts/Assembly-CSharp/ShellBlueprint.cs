using UnityEngine;

public class ShellBlueprint : MonoBehaviour
{
	[Header("Prefabs")]
	public GameObject shellVisualPrefab;

	[Header("DEBUG (Runtime Only)")]
	public int currentPowderCharge;

	public ShellDefinition shellDefinition;

	public void Init(ShellDefinition shell)
	{
	}

	public float GetAdjustedShellSpeed()
	{
		return 0f;
	}

	public float GetAdjustedHorizontalDispersion()
	{
		return 0f;
	}

	public float GetAdjustedVerticalDispersion()
	{
		return 0f;
	}

	public void GetRangeForCharge(int chargeLevel, out float minRange, out float maxRange)
	{
		minRange = default(float);
		maxRange = default(float);
	}

	public bool SetPowderCharge(int chargeLevel)
	{
		return false;
	}
}
