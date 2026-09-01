using BesiegeDlc;
using UnityEngine;

[AddComponentMenu("Blocks/BlockPrefabContainer")]
public class BlockPrefabContainer : MonoBehaviour
{
	public BlockPrefab Info;

	[SerializeField]
	[EnumMask]
	private DlcManager.DlcType dlcType;
}
