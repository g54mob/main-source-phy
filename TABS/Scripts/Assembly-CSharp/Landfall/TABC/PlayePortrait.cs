using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABC
{
	public class PlayePortrait : MonoBehaviour
	{
		private bool dead;

		public TextMeshProUGUI nameText;

		public TextMeshProUGUI moneyText;

		public TextMeshProUGUI healthText;

		public Image healthImage;

		public Image moneyImage;

		public Image playerImage;

		public Gradient healthGradient;

		public Populate populate;

		public Populate crossOutPopulate;

		public Transform from;

		public Transform to;

		public Transform fromPort;

		public Transform toPort;

		private int currentHP = 100;

		private int lastHP = 100;

		public RectPositionShake posisionShake;

		public RectPositionShake screenShake;

		public ScaleShake scaleShake;

		public void Init(string playerName)
		{
			nameText.text = playerName;
		}

		private void Start()
		{
			toPort.transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(-45, 45));
		}

		private void Update()
		{
			playerImage.transform.position = Vector3.Lerp(playerImage.transform.position, Vector3.Lerp(toPort.position, fromPort.position, (float)currentHP / 100f), Time.deltaTime * 2f);
			playerImage.transform.rotation = Quaternion.Lerp(playerImage.transform.rotation, Quaternion.Lerp(toPort.rotation, fromPort.rotation, (float)currentHP / 100f), Time.deltaTime * 2f);
		}

		public void PlayerInfoWasUpdated(int newMoney, int newHealth)
		{
			if (lastHP != newHealth)
			{
				TakeDamage(lastHP - newHealth, newHealth);
			}
			lastHP = newHealth;
			moneyText.text = newMoney.ToString();
		}

		private void TakeDamage(int damage, int newHealth)
		{
			if (!dead && damage > 0)
			{
				GameObject obj = populate.DoPopulate()[0];
				float t = Random.Range(0f, 1f);
				obj.transform.position = Vector3.Lerp(from.position, to.position, t);
				obj.transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, t);
				StartCoroutine(DelayHealthUI(newHealth));
			}
		}

		private IEnumerator DelayHealthUI(int newHealth)
		{
			yield return new WaitForSeconds(1.17f);
			healthText.text = newHealth.ToString();
			healthText.color = healthGradient.Evaluate((float)newHealth / 100f);
			healthImage.color = healthGradient.Evaluate((float)newHealth / 100f);
			posisionShake.AddForce(Vector2.left * 5f);
			screenShake?.AddForce(Vector2.left * 5f);
			scaleShake.AddForce(-0.15f);
			currentHP = newHealth;
			if (currentHP <= 0)
			{
				Die();
			}
		}

		private void Die()
		{
			if (!dead)
			{
				dead = true;
				GameObject obj = crossOutPopulate.DoPopulate()[0];
				obj.transform.SetParent(base.transform);
				obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			}
		}
	}
}
