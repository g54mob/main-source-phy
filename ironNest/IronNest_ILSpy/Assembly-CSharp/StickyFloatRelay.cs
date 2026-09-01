using Cpp2ILInjected;
using UnityEngine;

public sealed class StickyFloatRelay : MonoBehaviour, IFloatValueProvider
{
	private MonoBehaviour sourceProviderBehaviour;

	private float decayRate = 0.5f;

	private bool clampValues = true;

	private float clampMin;

	private float clampMax = 1f;

	private float initialValue;

	private IFloatValueProvider _source;

	private float _heldValue;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		IFloatValueProvider source = default(IFloatValueProvider);
		_source = source;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		bool flag = !clampValues;
		float num = initialValue;
		_heldValue = initialValue;
		if (flag)
		{
			return;
		}
		if (clampMin > clampMax)
		{
			clampMax = clampMin;
			clampMin = clampMax;
		}
		if (!clampValues)
		{
			return;
		}
		float num2 = clampMin;
		if (!(clampMin > num))
		{
			num2 = clampMax;
			if (!(num > clampMax))
			{
				goto IL_011d;
			}
		}
		num = num2;
		goto IL_011d;
		IL_011d:
		_heldValue = num;
	}

	private void Update()
	{
		//IL_0093: Invalid comparison between F4 and I4
		float num;
		if (_source != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180005E40");
			float num2 = default(float);
			num = num2;
		}
		else
		{
			num = _heldValue;
		}
		if (clampValues)
		{
			if (!(clampMin > num))
			{
				if (num > clampMax)
				{
					num = clampMax;
				}
			}
			else
			{
				num = clampMin;
			}
		}
		if (!(num > _heldValue))
		{
			if (decayRate > 0f)
			{
				float deltaTime = Time.deltaTime;
				float num3 = deltaTime * decayRate;
				if (_heldValue > num)
				{
					float num4 = _heldValue - num3;
					if (num < num4)
					{
						num = num4;
					}
					goto IL_021a;
				}
			}
			goto IL_01ec;
		}
		goto IL_021a;
		IL_020b:
		float heldValue;
		_heldValue = heldValue;
		return;
		IL_021a:
		_heldValue = num;
		goto IL_01ec;
		IL_01ec:
		if (!clampValues)
		{
			return;
		}
		float num5 = clampMin;
		heldValue = _heldValue;
		if (!(clampMin > _heldValue))
		{
			num5 = clampMax;
			if (!(_heldValue > clampMax))
			{
				goto IL_020b;
			}
		}
		heldValue = num5;
		goto IL_020b;
	}

	public float GetFloatValue()
	{
		return _heldValue;
	}

	private float ReadSourceValueOrDefault()
	{
		if (_source != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180005E40");
			float result = default(float);
			return result;
		}
		return _heldValue;
	}

	public void SetSourceProvider(MonoBehaviour providerBehaviour)
	{
		sourceProviderBehaviour = providerBehaviour;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		IFloatValueProvider source = default(IFloatValueProvider);
		_source = source;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
	}

	public void ResetHeldValue(float value)
	{
		float num = default(float);
		if (clampValues && !(clampMin > num) && num > clampMax)
		{
			_heldValue = clampMax;
		}
		else
		{
			_heldValue = clampMin;
		}
	}
}
