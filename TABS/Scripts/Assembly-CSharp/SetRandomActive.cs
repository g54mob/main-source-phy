using UnityEngine;

public class SetRandomActive : MonoBehaviour
{
	public bool PlayOnAwake = true;

	private void Start()
	{
		if (PlayOnAwake)
		{
			Go();
		}
	}

	public void Go()
	{
		float num = 0f;
		for (int i = 0; i < base.transform.childCount; i++)
		{
			Rarity component = base.transform.GetChild(0).GetComponent<Rarity>();
			num = ((!component) ? (num + 1f) : (num + component.rarity));
		}
		float num2 = Random.Range(0f, num);
		float num3 = 0f;
		for (int j = 0; j < base.transform.childCount; j++)
		{
			Rarity component2 = base.transform.GetChild(0).GetComponent<Rarity>();
			num3 = ((!component2) ? (num3 + 1f) : (num3 + component2.rarity));
			if (num3 > num2)
			{
				base.transform.GetChild(j).gameObject.SetActive(value: true);
				break;
			}
		}
	}
}
