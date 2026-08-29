using System;
using UnityEngine;
using UnityEngine.UI;

namespace Knife.HologramEffect
{
	public class Demo : MonoBehaviour
	{
		[Serializable]
		private class GameObjectsGroup
		{
			[SerializeField]
			private GameObject[] gameObjects;

			public void SetActive(bool enabled)
			{
				GameObject[] array = gameObjects;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(enabled);
				}
			}
		}

		[SerializeField]
		private GameObjectsGroup[] groups;

		[SerializeField]
		private Button previousButton;

		[SerializeField]
		private Button nextButton;

		private int currentGroup;

		private void Start()
		{
			currentGroup = 0;
			OpenCurrent();
			previousButton.onClick.AddListener(Previous);
			nextButton.onClick.AddListener(Next);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				Next();
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				Previous();
			}
		}

		private void Next()
		{
			currentGroup++;
			if (currentGroup >= groups.Length)
			{
				currentGroup = 0;
			}
			OpenCurrent();
		}

		private void Previous()
		{
			currentGroup--;
			if (currentGroup < 0)
			{
				currentGroup = groups.Length - 1;
			}
			OpenCurrent();
		}

		private void OpenCurrent()
		{
			GameObjectsGroup[] array = groups;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(enabled: false);
			}
			groups[currentGroup].SetActive(enabled: true);
		}
	}
}
