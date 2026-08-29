using UnityEngine;

namespace ExitGames.Demos.DemoPunVoice
{
	public class FirstPersonController : BaseController
	{
		[SerializeField]
		private MouseLookHelper mouseLook = new MouseLookHelper();

		private float oldYRotation;

		private Quaternion velRotation;

		public Vector3 Velocity => rigidBody.linearVelocity;

		protected override void SetCamera()
		{
			base.SetCamera();
			mouseLook.Init(base.transform, camTrans);
		}

		protected override void Move(float h, float v)
		{
			Vector3 linearVelocity = camTrans.forward * v + camTrans.right * h;
			linearVelocity.x *= speed;
			linearVelocity.z *= speed;
			linearVelocity.y = 0f;
			rigidBody.linearVelocity = linearVelocity;
		}

		private void Update()
		{
			RotateView();
		}

		private void RotateView()
		{
			oldYRotation = base.transform.eulerAngles.y;
			mouseLook.LookRotation(base.transform, camTrans);
			velRotation = Quaternion.AngleAxis(base.transform.eulerAngles.y - oldYRotation, Vector3.up);
			rigidBody.linearVelocity = velRotation * rigidBody.linearVelocity;
		}
	}
}
