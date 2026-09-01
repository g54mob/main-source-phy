using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TMPTextBaker : MonoBehaviour
{
	[Tooltip("Wont bake text with localization components")]
	public bool skipLocText;

	[Tooltip("Include inactive TMP text objects when baking.")]
	public bool includeInactive;

	[Tooltip("If true, only bake 3D TextMeshPro (not TextMeshProUGUI).")]
	public bool only3DText;

	[Tooltip("Name for the generated baked child GameObject.")]
	public string bakedChildName;

	[Tooltip("Emit verbose debug logs during bake.")]
	public bool verboseLogging;

	[SerializeField]
	[Tooltip("True if a bake currently exists.")]
	private bool baked;

	[SerializeField]
	private List<GameObject> disabledOriginals;

	[SerializeField]
	private GameObject bakedChild;

	public bool IsBaked => false;

	public void Bake()
	{
	}

	public void Unbake()
	{
	}
}
