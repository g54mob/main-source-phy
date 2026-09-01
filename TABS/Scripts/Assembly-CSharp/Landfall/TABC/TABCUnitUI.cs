using System;
using Landfall.TABS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABC
{
	public class TABCUnitUI : MonoBehaviour
	{
		public Transform m_StarHolder;

		public GameObject m_Star;

		public Image m_HealthBar;

		public Image m_WhiteHealthBar;

		public Color m_FriendlyColor = Color.red;

		public Color m_EnemyColor = Color.blue;

		public Color[] m_LevelColors = new Color[0];

		public TextMeshProUGUI Nameplate;

		public Image TwitchUserIcon;

		private DataHandler m_dataHandler;

		private bool done;

		private bool hasBeenInited;

		private float currSizeMultiplier = 1f;

		private Vector3 startScale;

		private SettingsInstance m_healthBarSizeOption;

		public void Init(Unit unit)
		{
			SettingsInstance settingsInstance = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
			m_healthBarSizeOption = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_HEALTHBAR_SIZE");
			m_dataHandler = unit.data;
			for (int i = 0; i < unit.level; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(m_Star);
				gameObject.transform.SetParent(m_StarHolder, worldPositionStays: true);
				gameObject.SetActive(value: true);
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localEulerAngles = Vector3.zero;
				gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0f);
				gameObject.transform.GetChild(0).GetComponent<Image>().color = m_LevelColors[unit.level - 1];
			}
			Populate componentInChildren = GetComponentInChildren<Populate>();
			componentInChildren.times = Mathf.RoundToInt(Mathf.Clamp((unit.data.health - 50f) / 50f, 0f, 10f));
			componentInChildren.DoPopulate();
			CodeAnimation component = GetComponent<CodeAnimation>();
			CodeAnimationInstance[] animations = component.animations;
			for (int j = 0; j < animations.Length; j++)
			{
				animations[j].multiplier = m_healthBarSizeOption.currentSliderValue;
			}
			component.startScale *= 1f + (float)componentInChildren.times * 0.1f;
			if (m_dataHandler.team == Team.Red)
			{
				m_HealthBar.color = ((settingsInstance.currentValue == 0) ? m_FriendlyColor : m_EnemyColor);
			}
			else
			{
				m_HealthBar.color = ((settingsInstance.currentValue == 0) ? m_EnemyColor : m_FriendlyColor);
			}
			startScale = component.startScale;
			hasBeenInited = true;
			unit.InitializeHealthBar(this);
		}

		private void OnTeamColorSwap(int obj)
		{
			throw new NotImplementedException();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			Debug.Log(collision.gameObject.name);
		}

		private void CheckForCorrectScaling()
		{
			if (hasBeenInited)
			{
				float currentSliderValue = m_healthBarSizeOption.currentSliderValue;
				if (currSizeMultiplier != currentSliderValue)
				{
					currSizeMultiplier = currentSliderValue;
					base.transform.localScale = startScale * currSizeMultiplier;
				}
			}
		}

		private void LateUpdate()
		{
			CheckForCorrectScaling();
			if ((m_dataHandler.Dead || m_dataHandler == null) && !done)
			{
				GetComponent<CodeAnimation>().PlayOut();
				done = true;
			}
			if (m_dataHandler != null)
			{
				Vector3 position = m_dataHandler.head.transform.position + Vector3.up * 1.2f;
				if (!(MainCam.instance == null) && !(MainCam.instance.cam == null))
				{
					if (MainCam.instance.cam.transform.InverseTransformPoint(position).z > 0f)
					{
						base.transform.SetPositionAndRotation(position, Quaternion.LookRotation(base.transform.position - MainCam.instance.transform.position));
						float num = m_dataHandler.health / m_dataHandler.maxHealth;
						m_HealthBar.fillAmount = num;
						m_WhiteHealthBar.fillAmount = Mathf.Lerp(m_WhiteHealthBar.fillAmount, num, Time.deltaTime * 5f);
					}
					else
					{
						base.transform.position = Vector3.down * 1000f;
					}
				}
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
