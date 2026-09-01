using UnityEngine;

public class BowSwitchArrow : MonoBehaviour
{
	private Charges charges;

	private ChargeUpBow chargeUpBow;

	public ArrowInstances[] arrows;

	private int id;

	private void Start()
	{
		charges = GetComponentInChildren<Charges>();
		chargeUpBow = GetComponent<ChargeUpBow>();
		Switch();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse1) && !chargeUpBow.isCharging)
		{
			id++;
			if (id >= arrows.Length)
			{
				id = 0;
			}
			Switch();
		}
	}

	private void Switch()
	{
		for (int i = 0; i < arrows.Length; i++)
		{
			arrows[i].part.SetActive(value: false);
		}
		arrows[id].part.SetActive(value: true);
		chargeUpBow.SetParts(arrows[id].part);
		chargeUpBow.coolPro = arrows[id].proj;
		Renderer[] componentsInChildren = charges.GetComponentsInChildren<Renderer>();
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			componentsInChildren[j].material.color = arrows[id].col * arrows[id].emission;
		}
	}
}
