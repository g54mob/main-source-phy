using NaughtyAttributes;
using UnityEngine;

public class ShowAssetPreview : MonoBehaviour
{
	[ShowAssetPreview(64, 64)]
	public Sprite sprite;

	[ShowAssetPreview(96, 96)]
	public GameObject prefab;
}
