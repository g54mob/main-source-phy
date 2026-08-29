using UnityEngine;

namespace FullSail
{
	public class TestImpacts : MonoBehaviour
	{
		[Range(0f, 1f)]
		public float minSize = 0.1f;

		[Range(0f, 1f)]
		public float maxSize = 0.3f;

		public GameObject ball;

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				GameObject obj = Object.Instantiate(ball);
				ball.transform.position = base.transform.position;
				obj.GetComponent<Rigidbody>().AddForce(Camera.main.ScreenPointToRay(Input.mousePosition).direction * 1600f, ForceMode.Impulse);
			}
			if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo, 100f))
			{
				Sail component = hitInfo.collider.GetComponent<Sail>();
				if ((bool)component)
				{
					component.AddImpact(hitInfo.textureCoord.x, hitInfo.textureCoord.y, Random.Range(minSize, maxSize), 0);
					Vector4 val = new Vector4(hitInfo.textureCoord.x, hitInfo.textureCoord.y, Time.timeSinceLevelLoad, 0.5f);
					component.SetValue(SailParamID.ImpactLocation, val);
				}
			}
			if (Input.GetMouseButtonDown(1) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo2, 100f))
			{
				Sail component2 = hitInfo2.collider.GetComponent<Sail>();
				if ((bool)component2)
				{
					component2.PatchImpact();
				}
			}
			if (Input.GetKeyDown(KeyCode.R) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo3, 100f))
			{
				Sail component3 = hitInfo3.collider.GetComponent<Sail>();
				if ((bool)component3)
				{
					component3.RemoveImpact();
				}
			}
		}
	}
}
