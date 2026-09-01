using Selectors;
using UnityEngine;

public class KeyContainer : ContainerDetails
{
	[SerializeField]
	private KeySelector leftBigKey;

	[SerializeField]
	private KeySelector rightBigKey;

	public KeySelector LeftMainKey
	{
		get
		{
			return leftBigKey;
		}
	}

	public KeySelector RightMainKey
	{
		get
		{
			return rightBigKey;
		}
	}

	public KeySelector[] LeftKeys
	{
		get
		{
			return new KeySelector[1] { LeftMainKey };
		}
	}

	public KeySelector[] RightKeys
	{
		get
		{
			return new KeySelector[1] { RightMainKey };
		}
	}

	protected void Awake()
	{
		GetHeight = (ContainerDetails c) => ((bool)leftBigKey && leftBigKey.Key != null && leftBigKey.Key.useMessage && leftBigKey.Key.VariableMapperHeight.HasValue) ? (leftBigKey.Key.VariableMapperHeight.Value * c.Background.parent.lossyScale.y) : c.Background.lossyScale.y;
	}

	public void TerminateVariable()
	{
		if (leftBigKey != null)
		{
			leftBigKey.TerminateVariable();
		}
		if (rightBigKey != null)
		{
			rightBigKey.TerminateVariable();
		}
		if (leftBigKey != null)
		{
			leftBigKey.TerminateCleanup();
		}
		if (rightBigKey != null)
		{
			rightBigKey.TerminateCleanup();
		}
	}
}
