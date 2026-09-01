using UnityEngine;

public class ScreenSpaceParticleOnClick : ClickBehaviour
{
	public Transform particleTrans;

	public ParticleSystem particleSys;

	public Camera cam;

	public float zPos = 10f;

	public override void OnClicked()
	{
		Vector3 vector = cam.ScreenToWorldPoint(InputManager.CursorPosition());
		particleTrans.position = new Vector3(vector.x, vector.y, zPos);
		particleSys.Play();
	}
}
