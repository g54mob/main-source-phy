using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABC
{
	public class XPHandlerUI : MonoBehaviour
	{
		public TextMeshProUGUI levelText;

		public TextMeshProUGUI xpText;

		public Image xpRing;

		public Image hoverRing;

		public Image darkRing;

		private int myCurrentLevel;

		private XPHandlerClient xpHandler;

		private bool isFading;

		private float xpRingTarget;

		private float hoverRingTarget;

		private float xpRingVel;

		private float hoverRingVel;

		private float drag = 10f;

		private float spring = 200f;

		private bool isHovered;

		private void Awake()
		{
		}

		private void Start()
		{
			xpHandler = XPHandlerClient.instance;
			XPHandlerClient xPHandlerClient = xpHandler;
			xPHandlerClient.UpdateUIAction = (Action)Delegate.Combine(xPHandlerClient.UpdateUIAction, new Action(UpdateUI));
			XPHandlerClient xPHandlerClient2 = xpHandler;
			xPHandlerClient2.VisualLevelUpAction = (Action<int>)Delegate.Combine(xPHandlerClient2.VisualLevelUpAction, new Action<int>(VisualLevelUp));
			XPHandlerClient xPHandlerClient3 = xpHandler;
			xPHandlerClient3.LevelUpAction = (Action<int>)Delegate.Combine(xPHandlerClient3.LevelUpAction, new Action<int>(LevelUp));
			XPHandlerClient xPHandlerClient4 = xpHandler;
			xPHandlerClient4.AddXPaction = (Action)Delegate.Combine(xPHandlerClient4.AddXPaction, new Action(AddXP));
			myCurrentLevel = xpHandler.level;
		}

		private void Update()
		{
			DoSprings();
			DoFade();
		}

		private void UpdateUI()
		{
			xpRingTarget = (float)xpHandler.currentExp / (float)xpHandler.xpNeededThisLevel;
			if (xpHandler.isMaxLevel)
			{
				hoverRingTarget = xpRingTarget;
			}
			else
			{
				hoverRingTarget = (float)(xpHandler.currentExp + (isHovered ? 4 : 0)) / (float)xpHandler.xpNeededThisLevel;
			}
		}

		private void UpdateText()
		{
			levelText.text = xpHandler.level.ToString();
			if (xpHandler.isMaxLevel)
			{
				xpText.text = "HYPE";
			}
			else
			{
				xpText.text = xpHandler.currentExp + " / " + xpHandler.xpNeededThisLevel;
			}
		}

		private void AddXP()
		{
			if (isFading)
			{
				CancelFade();
			}
		}

		private void VisualLevelUp(int xpNeededThisLevel)
		{
			UpdateText();
		}

		private void LevelUp(int xpNeededThisLevel)
		{
			if (xpHandler.level < 10)
			{
				isFading = true;
			}
		}

		private void DoFade()
		{
			if (isFading)
			{
				float num = Mathf.Clamp(3f - xpHandler.sinceLevelUp * 2f, 0f, 1f);
				xpRing.color = new Color(xpRing.color.r, xpRing.color.g, xpRing.color.b, num);
				hoverRing.color = new Color(hoverRing.color.r, hoverRing.color.g, hoverRing.color.b, num);
				if (num <= 0f)
				{
					CancelFade();
				}
			}
		}

		private void CancelFade()
		{
			xpHandler.DoVisualLevelUp();
			xpRingVel = 0f;
			hoverRingVel = 0f;
			isFading = false;
			xpRing.fillAmount = 0f;
			hoverRing.fillAmount = 0f;
			xpRing.color = new Color(xpRing.color.r, xpRing.color.g, xpRing.color.b, 1f);
			hoverRing.color = new Color(hoverRing.color.r, hoverRing.color.g, hoverRing.color.b, 1f);
		}

		private void DoSprings()
		{
			float num = math.clamp(Time.deltaTime, 0f, 0.02f);
			float num2 = xpRingTarget;
			if (isFading)
			{
				num2 = 1f;
			}
			xpRingVel += Mathf.Clamp(num2 - xpRing.fillAmount, -0.15f, 0.5f) * num * spring;
			xpRingVel -= xpRingVel * num * drag;
			xpRing.fillAmount += xpRingVel * num;
			if (xpRingVel > 0f && xpRing.fillAmount < 0.98f && num2 > 0f && xpRing.fillAmount > num2)
			{
				xpRing.fillAmount = num2;
				xpRingVel *= -0.5f;
			}
			if (xpRing.fillAmount >= 1f && myCurrentLevel != xpHandler.level)
			{
				xpHandler.DoVisualLevelUp();
				myCurrentLevel = xpHandler.level;
			}
			if (xpHandler.sinceBuyXP > 0.1f)
			{
				hoverRingVel += Mathf.Clamp(hoverRingTarget - hoverRing.fillAmount, -0.1f, 0.3f) * num * spring * 3f;
			}
			hoverRingVel -= hoverRingVel * num * drag * 3f;
			hoverRing.fillAmount += hoverRingVel * num;
		}

		public void StartHover()
		{
			isHovered = true;
			UpdateUI();
		}

		public void EndHover()
		{
			isHovered = false;
			UpdateUI();
		}
	}
}
