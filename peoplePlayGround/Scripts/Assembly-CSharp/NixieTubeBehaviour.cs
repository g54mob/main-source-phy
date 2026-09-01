using UnityEngine;

[ExecuteAlways]
[SkipSerialisation]
public class NixieTubeBehaviour : MonoBehaviour
{
	public SpriteRenderer[] DigitRenderers;

	private MaterialPropertyBlock properties;

	public void SetValue(int number)
	{
		if (properties == null)
		{
			properties = new MaterialPropertyBlock();
		}
		int digitCount = (int)Mathf.Floor(Mathf.Log10(number)) + 1;
		for (int i = 0; i < DigitRenderers.Length; i++)
		{
			SpriteRenderer spriteRenderer = DigitRenderers[i];
			if (!(spriteRenderer == null))
			{
				int num = getDigit(number, i - (DigitRenderers.Length - digitCount));
				spriteRenderer.GetPropertyBlock(properties);
				properties.SetFloat(ShaderProperties.Get("_DigitIndex"), num);
				spriteRenderer.SetPropertyBlock(properties);
			}
		}
		int getDigit(int x, int num2)
		{
			if (digitCount < num2 || float.IsInfinity(digitCount))
			{
				return 0;
			}
			num2++;
			return (int)Mathf.Floor((float)x / Mathf.Pow(10f, digitCount - num2) % 10f);
		}
	}
}
