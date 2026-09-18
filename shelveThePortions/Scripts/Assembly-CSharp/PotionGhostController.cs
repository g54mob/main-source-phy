using UnityEngine;

public class PotionGhostController : MonoBehaviour
{
	[SerializeField]
	private GameObject corkObject;

	[Space]
	[SerializeField]
	private MeshFilter bodyMeshFilter;

	[SerializeField]
	private MeshFilter corkMeshFilter;

	[SerializeField]
	private MeshFilter liquidMeshFilter;

	[SerializeField]
	private MeshRenderer liquedMeshRenderer;

	public void GhostPotion(PotionController potionController, ShelfSpaceController spaceShelf)
	{
		base.transform.position += new Vector3(0f, 0.01f, 0f);
		base.transform.localScale = potionController.startingScale;
		bodyMeshFilter.mesh = potionController.bodyMeshFilter.mesh;
		corkMeshFilter.mesh = potionController.corkMeshFilter.mesh;
		liquidMeshFilter.mesh = potionController.liquidMeshFilter.mesh;
		liquedMeshRenderer.material.color = AlphaSystem.Alphalizer(potionController.liquedMeshRenderer.material.color, 0.1f);
		corkObject.transform.localPosition = potionController.potionCork.transform.localPosition;
		base.transform.localEulerAngles = new Vector3(0f, spaceShelf.transform.eulerAngles.y + 180f + potionController.pickupPotionRotationAngle.y, 0f);
	}
}
