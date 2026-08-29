using UnityEngine;

namespace FullSail
{
	[HelpURL("http://www.macspeedee.com/full-sail/")]
	public class SailBounds : MonoBehaviour
	{
		public Vector3 size = new Vector3(20f, 10f, 20f);

		private void Start()
		{
			MeshFilter component = GetComponent<MeshFilter>();
			if ((bool)component)
			{
				Mesh sharedMesh = component.sharedMesh;
				if ((bool)sharedMesh)
				{
					Bounds bounds = sharedMesh.bounds;
					bounds.size = size;
					sharedMesh.bounds = bounds;
				}
			}
		}
	}
}
