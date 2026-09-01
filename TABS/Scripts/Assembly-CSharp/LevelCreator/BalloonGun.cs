using Landfall.TABS_Input;
using UnityEngine;

namespace LevelCreator
{
	public class BalloonGun : Tool
	{
		[SerializeField]
		private GameObject m_balloonDart;

		[SerializeField]
		private float m_fireForce;

		[SerializeField]
		private Transform m_firePoint;

		protected override void Start()
		{
			base.Start();
		}

		protected override void AssignInput(PlayerActions actions)
		{
			base.AssignInput(actions);
			m_inputState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				Fire();
			}, m_contextIcons.m_primaryIcon);
		}

		private void Fire()
		{
			GameObject gameObject = Object.Instantiate(m_balloonDart, m_firePoint.position, m_firePoint.rotation);
			gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * m_fireForce, ForceMode.Impulse);
		}
	}
}
