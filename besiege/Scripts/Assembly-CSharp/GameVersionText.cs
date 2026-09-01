using UnityEngine;

public class GameVersionText : MonoBehaviour
{
	public TextMesh versionTextField;

	public string prefix = "v";

	protected void Awake()
	{
		StatMaster.isMainMenu = true;
		if (versionTextField != null)
		{
			string versionString = VersionNumber.GetVersionString();
			versionTextField.text = prefix + versionString;
		}
		else
		{
			Debug.LogWarning("Could not find versionTextField, please assign it.");
		}
	}
}
