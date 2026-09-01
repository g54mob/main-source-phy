using UnityEngine;

public class ButtonMaterial : MonoBehaviour
{
	public Material InactiveChoiceMaterial;

	public Material ActiveChoiceMaterial;

	public void SetStatus(bool state)
	{
		if (state)
		{
			GetComponent<Renderer>().material = ActiveChoiceMaterial;
		}
		else
		{
			GetComponent<Renderer>().material = InactiveChoiceMaterial;
		}
	}
}
