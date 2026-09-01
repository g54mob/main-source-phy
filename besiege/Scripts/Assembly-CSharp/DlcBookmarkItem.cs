using UnityEngine;

[AddComponentMenu("Besiege/FileBrowserView/Dlc/DlcBookmarkItem")]
public class DlcBookmarkItem : MonoBehaviour
{
	[SerializeField]
	private MeshRenderer iconRenderer;

	[SerializeField]
	private TextMesh tooltipTextMesh;

	internal void Setup(string tooltipText, Texture dlcIconTexture)
	{
		iconRenderer.material.mainTexture = dlcIconTexture;
		tooltipTextMesh.text = tooltipText;
	}
}
