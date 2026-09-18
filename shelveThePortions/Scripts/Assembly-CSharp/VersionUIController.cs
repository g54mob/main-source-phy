using TMPro;
using UnityEngine;

public class VersionUIController : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI versionText;

	private void Start()
	{
		versionText.text = Application.version;
	}
}
