using System.Collections;
using UnityEngine;

public class DestroyPhysNodeTileOnCompletion : MonoBehaviour
{
	public PhysNodeTile myTile;

	private bool broken;

	private void Update()
	{
		if (StatMaster.levelSimulating && WinCondition.hasWon && !broken && Time.time - PhysNodeTile.MomentOfLastTileDestroyed < 5f)
		{
			broken = true;
			Vector3 position = Machine.Active().FirstBlock.transform.position;
			Vector3 position2 = base.transform.position;
			float value = Vector3.Distance(position2, position);
			float num = Mathf.InverseLerp(0f, 50f, value);
			StartCoroutine(DelayedBreak(num, num * 2f));
		}
	}

	public IEnumerator DelayedBreak(float min, float max)
	{
		yield return new WaitForSeconds(Random.Range(min, max));
		Break();
	}

	public void Break()
	{
		for (int i = 0; i < myTile.nodes.Length; i++)
		{
			myTile.BreakNode(myTile.nodes[i], Vector3.zero);
		}
		Object.Destroy(this);
	}
}
