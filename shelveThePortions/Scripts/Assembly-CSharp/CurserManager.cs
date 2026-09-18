using System;
using System.Collections.Generic;
using UnityEngine;

public class CurserManager : Singleton<CurserManager>
{
	public Action OnCurserMove;

	public Action OnCurserHide;

	[SerializeField]
	private List<CurserData> CurserDatasList;

	private CursorMode cursorMode;

	private CurserTypes baseCurserType = CurserTypes.Normal;

	private CurserTypes currentBaseCurserType = CurserTypes.Normal;

	private CurserTypes lastBaseCurserType;

	public bool isCurserActive;

	private Vector2 mousePosition;

	private float curserStopTime;

	private float maxCurserStopTime = 3f;

	private void Start()
	{
		mousePosition = Vector2.zero;
		SetCurser(CurserTypes.Normal);
		EventManager.MainMenuLoaded += EnableCursor;
		EventManager.GameStart += LockCursor;
		EventManager.GameResume += LockCursor;
		EventManager.GamePause += EnableCursor;
		EventManager.GameEnd += EnableCursor;
		EventManager.PlaytestEnd += EnableCursor;
		EventManager.CatUILoaded += EnableCursor;
		EventManager.PuzzleUILoaded += EnableCursor;
		EventManager.OnControlsChange += OnControlsChange;
	}

	private void OnDisable()
	{
		EventManager.MainMenuLoaded -= EnableCursor;
		EventManager.GameStart -= LockCursor;
		EventManager.GameResume -= LockCursor;
		EventManager.GamePause -= EnableCursor;
		EventManager.GameEnd -= EnableCursor;
		EventManager.PlaytestEnd -= EnableCursor;
		EventManager.CatUILoaded -= EnableCursor;
		EventManager.PuzzleUILoaded -= EnableCursor;
		EventManager.OnControlsChange -= OnControlsChange;
	}

	public void SetBaseCurserToNormal()
	{
		baseCurserType = CurserTypes.Normal;
		SetCurser(baseCurserType);
	}

	public void LockCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void EnableCursor()
	{
		Cursor.lockState = CursorLockMode.None;
		if (!Singleton<InputManager>.Instance.IsUsingGamepad)
		{
			Cursor.visible = true;
		}
		SetBaseCurserToNormal();
	}

	public void SetCurser(CurserTypes curserType)
	{
		if (curserType == CurserTypes.None)
		{
			Cursor.visible = false;
			return;
		}
		foreach (CurserData curserDatas in CurserDatasList)
		{
			if (curserDatas.curserType == curserType)
			{
				Texture2D cursorTexture = curserDatas.cursorTexture;
				Vector2 hotspot = new Vector2((float)cursorTexture.width * curserDatas.hotSpot.x, (float)cursorTexture.height * curserDatas.hotSpot.y);
				lastBaseCurserType = currentBaseCurserType;
				currentBaseCurserType = curserType;
				Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
			}
		}
	}

	public void ReturnToNormalCurser()
	{
		SetCurser(baseCurserType);
	}

	private void Update()
	{
		Vector2 vector = InputManager.GetMousePosition();
		if (!isCurserActive)
		{
			if (mousePosition != vector)
			{
				EnableMouseCurser();
			}
			return;
		}
		if (mousePosition == vector)
		{
			curserStopTime += Time.deltaTime;
		}
		else
		{
			mousePosition = vector;
			curserStopTime = 0f;
		}
		if (curserStopTime >= maxCurserStopTime)
		{
			HideMouseCurser();
		}
	}

	public void HideMouseCurser()
	{
		if (isCurserActive)
		{
			isCurserActive = false;
			mousePosition = InputManager.GetMousePosition();
			Cursor.visible = false;
			if (OnCurserHide != null)
			{
				OnCurserHide();
			}
		}
	}

	public void EnableMouseCurser()
	{
		mousePosition = InputManager.GetMousePosition();
		isCurserActive = true;
		if (!Singleton<InputManager>.Instance.IsUsingGamepad)
		{
			Cursor.visible = true;
		}
		curserStopTime = 0f;
		if (OnCurserMove != null)
		{
			OnCurserMove();
		}
	}

	public void OnControlsChange()
	{
		if (Singleton<InputManager>.Instance.IsUsingGamepad)
		{
			Cursor.visible = false;
		}
		else if (Cursor.lockState != CursorLockMode.Locked)
		{
			Cursor.visible = true;
		}
	}
}
