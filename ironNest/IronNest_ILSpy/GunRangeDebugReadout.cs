using Cpp2ILInjected;
using UnityEngine;

public class GunRangeDebugReadout : MonoBehaviour, IFloatValueProvider
{
	private GunController gun;

	private bool autoFindOnValidate = true;

	private float predictedRangeIfFiredNow;

	private bool clampNonNegative = true;

	public float PredictedRangeIfFiredNow => predictedRangeIfFiredNow;

	public float GetFloatValue()
	{
		return predictedRangeIfFiredNow;
	}

	private void OnValidate()
	{
		if (autoFindOnValidate && gun == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			GunController gunController = default(GunController);
			gun = gunController;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 77 Invalid \"Jump target not found in method: 0x1803FE0F0\"");
	}

	private void Awake()
	{
		UpdateReadout();
	}

	private void Update()
	{
		UpdateReadout();
	}

	private void UpdateReadout()
	{
		//IL_0029: Expected F4, but got I4
		//IL_005a: Expected F4, but got I4
		//IL_0092: Invalid comparison between I4 and F4
		//IL_00a4: Expected F4, but got I4
		bool flag = gun != null;
		bool flag2 = !flag;
		float num = 0f;
		if (!flag2)
		{
			bool canFire = gun.CanFire;
			bool flag3 = !canFire;
			num = 0f;
			if (!flag3)
			{
				GunController gunController = gun;
				num = gunController._003CCurrentRange_003Ek__BackingField;
			}
		}
		if (clampNonNegative)
		{
			bool flag4 = !(0f < num);
			float num2 = 0f;
			if (!flag4)
			{
				num2 = num;
			}
			num = num2;
		}
		predictedRangeIfFiredNow = num;
	}
}
