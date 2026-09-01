using System;
using UnityEngine;

namespace Landfall.TABC
{
	public class XPHandlerClient : MonoBehaviour
	{
		public int level = 1;

		public int currentExp;

		public int xpNeededThisLevel;

		public int[] xpNeededPerLevel;

		public bool isMaxLevel;

		public static XPHandlerClient instance;

		public Action AddXPaction;

		public Action<int> LevelUpAction;

		public Action<int> VisualLevelUpAction;

		public Action MaxLevelAction;

		public float sinceLevelUp = 10f;

		public float sinceBuyXP = 10f;

		private int hasVisualized = -1;

		public Action UpdateUIAction;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			xpNeededThisLevel = xpNeededPerLevel[0];
			AddXP(0);
		}

		public void AddFourXP()
		{
			if (WalletHandlerClient.instance.Spend(5))
			{
				AddXP(4);
			}
		}

		public void AddXP(int amount, bool recursiveCall = false)
		{
			if (!isMaxLevel)
			{
				currentExp += amount;
				sinceBuyXP = 0f;
				if (!recursiveCall && AddXPaction != null)
				{
					AddXPaction();
				}
				if (currentExp >= xpNeededThisLevel)
				{
					currentExp -= xpNeededThisLevel;
					LevelUp();
					AddXP(0, recursiveCall: true);
				}
				UpdateUI();
			}
		}

		private void LevelUp()
		{
			sinceLevelUp = 0f;
			if (level < xpNeededPerLevel.Length)
			{
				xpNeededThisLevel = xpNeededPerLevel[level];
			}
			else
			{
				HitMaxLevel();
			}
			level++;
			if (LevelUpAction != null)
			{
				LevelUpAction(xpNeededThisLevel);
			}
		}

		public void DoVisualLevelUp()
		{
			if (hasVisualized != level)
			{
				hasVisualized = level;
				if (VisualLevelUpAction != null)
				{
					VisualLevelUpAction(xpNeededThisLevel);
				}
			}
		}

		private void HitMaxLevel()
		{
			isMaxLevel = true;
			if (MaxLevelAction != null)
			{
				MaxLevelAction();
			}
			currentExp = 100;
			UpdateUI();
		}

		private void UpdateUI()
		{
			if (UpdateUIAction != null)
			{
				UpdateUIAction();
			}
		}

		private void Update()
		{
			sinceLevelUp += Time.deltaTime;
			sinceBuyXP += Time.deltaTime;
		}
	}
}
