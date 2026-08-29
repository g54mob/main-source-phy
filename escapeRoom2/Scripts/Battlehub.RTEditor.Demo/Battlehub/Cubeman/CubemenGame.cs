using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battlehub.RTCommon;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battlehub.Cubeman
{
	public class CubemenGame : MonoBehaviour
	{
		public Text TxtScore;

		public Text TxtCompleted;

		public Text TxtTip;

		public Button BtnReplay;

		public GameObject GameUI;

		private int m_score;

		private int m_total;

		private bool m_gameOver;

		[SerializeField]
		private GameCharacter[] m_storedCharacters;

		private GameCharacter m_current;

		private int m_currentIndex = -1;

		private List<GameCharacter> m_activeCharacters;

		private GameCameraFollow m_playerCamera;

		[SerializeField]
		private bool m_cameraFollow = true;

		private bool m_isGameRunning;

		private static CubemenGame m_instance;

		public int CurrentIndex
		{
			get
			{
				return m_currentIndex;
			}
			set
			{
				m_currentIndex = value;
			}
		}

		public bool CameraFollow
		{
			get
			{
				return m_cameraFollow;
			}
			set
			{
				m_cameraFollow = value;
			}
		}

		public bool IsGameRunning => m_isGameRunning;

		private void RuntimeAwake()
		{
			if (m_instance != null)
			{
				Debug.LogWarning("Another instance of Cubemen game exist");
				Object.Destroy(m_instance);
			}
			else
			{
				m_instance = this;
			}
		}

		private void RuntimeStart()
		{
			m_isGameRunning = true;
			StartCoroutine(StartGame());
		}

		private void OnRuntimeDestroy()
		{
			if (m_instance == this)
			{
				m_instance = null;
			}
		}

		private void OnRuntimeActivate()
		{
			m_isGameRunning = true;
			if (m_current != null)
			{
				m_current.HandleInput = true;
			}
		}

		private void OnRuntimeDeactivate()
		{
			m_isGameRunning = false;
			if (m_current != null)
			{
				m_current.HandleInput = false;
			}
		}

		private void OnRuntimeEditorOpened()
		{
			StopGame();
			m_isGameRunning = false;
		}

		private void OnRuntimeEditorClosed()
		{
			m_isGameRunning = true;
			StartCoroutine(StartGame());
		}

		private void Start()
		{
			IRTEState iRTEState = IOC.Resolve<IRTEState>();
			if (iRTEState == null || !iRTEState.IsCreated)
			{
				m_isGameRunning = true;
			}
			if (UnityObjectExt.FindAnyObjectByType<EventSystem>() == null && GameUI != null)
			{
				GameUI.AddComponent<EventSystem>();
				GameUI.AddComponent<StandaloneInputModule>();
			}
			if (BtnReplay != null)
			{
				BtnReplay.onClick.AddListener(RestartGame);
			}
			if (GameUI != null)
			{
				GameUI.SetActive(value: false);
			}
			if (IsGameRunning)
			{
				StartCoroutine(StartGame());
			}
		}

		private void OnDestroy()
		{
			if (m_instance == this)
			{
				m_instance = null;
			}
			if (BtnReplay != null)
			{
				BtnReplay.onClick.RemoveListener(RestartGame);
			}
		}

		private void Update()
		{
			if (m_isGameRunning)
			{
				if (Input.GetKeyDown(KeyCode.Return))
				{
					SwitchPlayer(m_current, 0f, next: true);
				}
				else if (Input.GetKeyDown(KeyCode.Backspace))
				{
					SwitchPlayer(m_current, 0f, next: false);
				}
			}
		}

		private IEnumerator StartGame()
		{
			DestroyStoredCharacters();
			yield return new WaitForEndOfFrame();
			if (GameUI != null)
			{
				GameUI.SetActive(value: true);
			}
			GameCharacter[] array = (from c in UnityObjectExt.FindObjectsByType<GameCharacter>().Where(delegate(GameCharacter g)
				{
					ExposeToEditor component = g.GetComponent<ExposeToEditor>();
					return component != null && !component.MarkAsDestroyed;
				})
				orderby c.name
				select c).ToArray();
			for (int num = 0; num < array.Length; num++)
			{
				array[num].Game = this;
				array[num].IsActive = true;
			}
			SaveCharactersInInitalState(array);
			InitializeGame(array, m_currentIndex);
		}

		private void DestroyStoredCharacters()
		{
			if (m_storedCharacters == null)
			{
				return;
			}
			for (int i = 0; i < m_storedCharacters.Length; i++)
			{
				GameCharacter gameCharacter = m_storedCharacters[i];
				if (gameCharacter != null)
				{
					Object.Destroy(gameCharacter.gameObject);
				}
			}
		}

		private void StopGame()
		{
			RestartGame();
			for (int i = 0; i < m_activeCharacters.Count; i++)
			{
				m_activeCharacters[i].IsActive = false;
				Object.Destroy(m_activeCharacters[i].gameObject);
			}
			if (m_playerCamera != null)
			{
				m_playerCamera.target = null;
			}
			if (GameUI != null)
			{
				GameUI.SetActive(value: false);
			}
		}

		private void RestartGame()
		{
			if (m_activeCharacters != null)
			{
				for (int i = 0; i < m_activeCharacters.Count; i++)
				{
					Object.Destroy(m_activeCharacters[i].gameObject);
				}
			}
			GameCharacter[] storedCharacters = m_storedCharacters;
			SaveCharactersInInitalState(storedCharacters);
			InitializeGame(storedCharacters, -1);
		}

		private void InitializeGame(GameCharacter[] characters, int activeCharacterIndex)
		{
			m_gameOver = false;
			if (m_cameraFollow)
			{
				m_playerCamera = UnityObjectExt.FindAnyObjectByType<GameCameraFollow>();
				if (m_playerCamera == null && Camera.main != null)
				{
					m_playerCamera = Camera.main.gameObject.AddComponent<GameCameraFollow>();
				}
				if (m_playerCamera != null)
				{
					Canvas componentInChildren = GetComponentInChildren<Canvas>();
					componentInChildren.renderMode = RenderMode.ScreenSpaceCamera;
					Camera camera = (componentInChildren.worldCamera = m_playerCamera.GetComponent<Camera>());
					componentInChildren.planeDistance = camera.nearClipPlane + 0.05f;
				}
			}
			m_activeCharacters = new List<GameCharacter>();
			foreach (GameCharacter gameCharacter in characters)
			{
				gameCharacter.transform.SetParent(base.transform.parent);
				gameCharacter.gameObject.SetActive(value: true);
				gameCharacter.HandleInput = false;
				if (!gameCharacter.GetComponent<ExposeToEditor>())
				{
					gameCharacter.gameObject.AddComponent<ExposeToEditor>();
				}
				else
				{
					ExposeToEditor[] componentsInChildren = gameCharacter.GetComponentsInChildren<ExposeToEditor>(includeInactive: true);
					for (int j = 0; j < componentsInChildren.Length; j++)
					{
						componentsInChildren[j].MarkAsDestroyed = false;
					}
				}
				m_activeCharacters.Add(gameCharacter);
			}
			m_total = m_activeCharacters.Count;
			m_score = 0;
			if (m_total == 0)
			{
				TxtCompleted?.gameObject.SetActive(value: true);
				TxtScore?.gameObject.SetActive(value: false);
				TxtTip?.gameObject.SetActive(value: false);
				if (TxtCompleted != null)
				{
					TxtCompleted.text = "Game Over!";
				}
				m_gameOver = true;
				return;
			}
			TxtCompleted?.gameObject.SetActive(value: false);
			TxtScore?.gameObject.SetActive(value: true);
			TxtTip?.gameObject.SetActive(value: true);
			UpdateScore();
			if (activeCharacterIndex >= 0)
			{
				m_current = m_activeCharacters[activeCharacterIndex];
				if (m_current != null)
				{
					ActivatePlayer();
				}
				else
				{
					SwitchPlayer(null, 0f, next: true);
				}
			}
			else
			{
				SwitchPlayer(null, 0f, next: true);
			}
		}

		private void SaveCharactersInInitalState(GameCharacter[] characters)
		{
			GameCharacter[] array = new GameCharacter[characters.Length];
			for (int i = 0; i < characters.Length; i++)
			{
				GameCharacter gameCharacter = characters[i];
				bool activeSelf = gameCharacter.gameObject.activeSelf;
				gameCharacter.gameObject.SetActive(value: false);
				GameCharacter gameCharacter2 = Object.Instantiate(gameCharacter, gameCharacter.transform.position, gameCharacter.transform.rotation);
				gameCharacter2.name = gameCharacter.name;
				gameCharacter.gameObject.SetActive(activeSelf);
				ExposeToEditor[] componentsInChildren = gameCharacter2.GetComponentsInChildren<ExposeToEditor>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					componentsInChildren[j].MarkAsDestroyed = true;
				}
				gameCharacter2.transform.SetParent(base.transform);
				array[i] = gameCharacter2;
			}
			m_storedCharacters = array;
		}

		private void UpdateScore()
		{
			if (TxtScore != null)
			{
				TxtScore.text = "Saved : " + m_score + " / " + m_total;
			}
		}

		private bool IsGameCompleted()
		{
			return m_activeCharacters.Count == 0;
		}

		public void OnPlayerFinish(GameCharacter gameCharacter)
		{
			m_score++;
			UpdateScore();
			SwitchPlayer(gameCharacter, 1f, next: true);
			m_activeCharacters.Remove(gameCharacter);
			if (IsGameCompleted())
			{
				m_gameOver = true;
				TxtTip?.gameObject.SetActive(value: false);
				StartCoroutine(ShowText("Congratulation! \n You have completed a great game "));
			}
		}

		private IEnumerator ShowText(string text)
		{
			yield return new WaitForSeconds(1.5f);
			if (m_gameOver)
			{
				TxtScore?.gameObject.SetActive(value: false);
				TxtCompleted?.gameObject.SetActive(value: true);
				if (TxtCompleted != null)
				{
					TxtCompleted.text = text;
				}
			}
		}

		public void OnPlayerDie(GameCharacter gameCharacter)
		{
			m_gameOver = true;
			m_activeCharacters.Remove(gameCharacter);
			TxtTip?.gameObject.SetActive(value: false);
			StartCoroutine(ShowText("Game Over!"));
			for (int i = 0; i < m_activeCharacters.Count; i++)
			{
				m_activeCharacters[i].HandleInput = false;
			}
		}

		public void SwitchPlayer(GameCharacter current, float delay, bool next)
		{
			if (m_gameOver)
			{
				return;
			}
			if (current != null)
			{
				current.HandleInput = false;
				m_currentIndex = m_activeCharacters.IndexOf(current);
				if (next)
				{
					m_currentIndex++;
					if (m_currentIndex >= m_activeCharacters.Count)
					{
						m_currentIndex = 0;
					}
				}
				else
				{
					m_currentIndex--;
					if (m_currentIndex < 0)
					{
						m_currentIndex = m_activeCharacters.Count - 1;
					}
				}
			}
			if (m_currentIndex < 0)
			{
				m_currentIndex = 0;
			}
			m_current = m_activeCharacters[m_currentIndex];
			if (current == null)
			{
				ActivatePlayer();
			}
			else
			{
				StartCoroutine(ActivateNextPlayer(delay));
			}
		}

		private IEnumerator ActivateNextPlayer(float delay)
		{
			yield return new WaitForSeconds(delay);
			if (!m_gameOver)
			{
				ActivatePlayer();
			}
		}

		private void ActivatePlayer()
		{
			if (m_current != null && IsGameRunning)
			{
				m_current.HandleInput = true;
			}
			if (m_playerCamera != null)
			{
				m_playerCamera.target = m_current.transform;
				m_playerCamera.Follow();
				m_current.Camera = m_playerCamera.transform;
			}
		}
	}
}
