using System.Collections.Generic;
using UnityEngine;

public class EditorDisplay : MonoBehaviour
{
	public Lock targetLock;

	public int columns;

	public int rows;

	public Vector2 padding;

	public int spriteSheetColumns;

	public int spriteSheetRows;

	public string spriteSheetFileName;

	public int[] memory;

	public int lockRevisionNumber = -1;

	public List<GameObject> quadCache;

	public Texture2D spriteSheetTexture;

	public Material quadMaterial;
}
