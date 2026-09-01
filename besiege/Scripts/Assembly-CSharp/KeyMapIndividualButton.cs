using UnityEngine;

public class KeyMapIndividualButton : ClickBehaviour
{
	public KeyMapController KeyMapControllerCode;

	public string myLetter;

	public Renderer myBG;

	public Material RedMaterial;

	public Material DarkMaterial;

	private void Awake()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			if (base.transform.GetChild(i).name == "KeyBG")
			{
				myBG = base.transform.GetChild(i).GetComponent<Renderer>();
			}
			else
			{
				myLetter = base.transform.GetChild(i).GetComponent<TextMesh>().text.ToLower();
			}
		}
	}

	public override void OnClicked()
	{
		KeyMapControllerCode.SetKey(myLetter);
	}

	public void Enabled()
	{
		myBG.GetComponent<Renderer>().material = RedMaterial;
	}

	public void Disabled()
	{
		myBG.GetComponent<Renderer>().material = DarkMaterial;
	}
}
