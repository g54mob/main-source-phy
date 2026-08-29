using UnityEngine;

namespace Battlehub.Cubeman
{
	[DisallowMultipleComponent]
	public class GameCharacter : MonoBehaviour
	{
		public CubemenGame Game;

		private Rigidbody m_rigidBody;

		private CubemanUserControl m_userControl;

		private Transform m_soul;

		private SkinnedMeshRenderer m_skinnedMeshRenderer;

		private bool m_isActive;

		public Transform Camera
		{
			get
			{
				return m_userControl.Cam;
			}
			set
			{
				m_userControl.Cam = value;
			}
		}

		public bool HandleInput
		{
			get
			{
				return m_userControl.HandleInput;
			}
			set
			{
				m_userControl.HandleInput = value;
			}
		}

		public bool IsActive
		{
			get
			{
				return m_isActive;
			}
			set
			{
				m_isActive = value;
				Rigidbody component = base.gameObject.GetComponent<Rigidbody>();
				if ((bool)component)
				{
					component.isKinematic = !m_isActive;
				}
				CubemanCharacter component2 = base.gameObject.GetComponent<CubemanCharacter>();
				if ((bool)component2)
				{
					component2.Enabled = m_isActive;
				}
			}
		}

		private void Awake()
		{
			m_userControl = GetComponent<CubemanUserControl>();
		}

		private void Start()
		{
			m_soul = base.transform.Find("Soul");
			m_skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
			m_rigidBody = GetComponent<Rigidbody>();
			if (Game == null)
			{
				Game = UnityObjectExt.FindAnyObjectByType<CubemenGame>();
			}
		}

		private void Update()
		{
			if (!(Game == null) && Game.IsGameRunning)
			{
				if (base.transform.position.y < -25f)
				{
					Die();
				}
				if (Input.GetKeyDown(KeyCode.K))
				{
					Die();
				}
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if ((!(Game != null) || Game.IsGameRunning) && other.tag == "Finish")
			{
				Game.OnPlayerFinish(this);
				if (m_skinnedMeshRenderer != null)
				{
					m_skinnedMeshRenderer.enabled = false;
				}
				if (m_soul != null)
				{
					m_soul.gameObject.SetActive(value: true);
				}
				Object.Destroy(base.gameObject, 2f);
			}
		}

		private void Die()
		{
			base.enabled = false;
			Game?.OnPlayerDie(this);
			if (m_skinnedMeshRenderer != null)
			{
				m_skinnedMeshRenderer.enabled = false;
			}
			if (m_rigidBody != null)
			{
				m_rigidBody.isKinematic = true;
			}
			if (m_userControl != null)
			{
				m_userControl.HandleInput = false;
			}
			if (m_soul != null)
			{
				m_soul.gameObject.SetActive(value: true);
			}
			Object.Destroy(base.gameObject, 2f);
		}
	}
}
