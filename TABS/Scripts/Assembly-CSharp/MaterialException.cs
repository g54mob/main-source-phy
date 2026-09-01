using UnityEngine;

public class MaterialException : ObjectException
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public override bool TestException(GameObject obj)
	{
		bool result = false;
		if ((bool)obj.GetComponent<Weapon>() && obj != base.gameObject)
		{
			result = true;
		}
		return result;
	}
}
