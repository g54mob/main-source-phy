using UnityEngine;
using UnityEngine.UI;

public class ToggleHelper : MonoBehaviour
{
	private Toggle toggle;

	public GameObject checkObject;

	public GameObject uncheckObject;

	private void Awake()
	{
		toggle = GetComponent<Toggle>();
	}

	private void Update()
	{
		checkObject.SetActive(toggle.isOn);
		uncheckObject.SetActive(!toggle.isOn);
	}
}
