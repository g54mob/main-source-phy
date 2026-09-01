using System.Collections;
using BlockMapperInternal;
using Selectors;
using UnityEngine;

[AddComponentMenu("UI/Transform Widget")]
public class TransformWidget : ParameterWidget
{
	public ValueHolder[] PosHolders;

	public ValueHolder[] RotHolders;

	public ValueHolder[] ScaleHolders;

	public UIButton copy;

	public UIButton paste;

	public UIButton reset;

	public MeshRenderer[] posFlashs;

	public MeshRenderer[] rotFlashs;

	public MeshRenderer[] scaleFlashs;

	public AudioSource audioSource;

	private Vector3 lastPosition;

	private Quaternion lastRotation;

	private Vector3 lastScale;

	private bool _lock;

	protected LevelEntity entity;

	public bool PositionSelected
	{
		get
		{
			for (int i = 0; i < PosHolders.Length; i++)
			{
				if (PosHolders[i].IsFocused && !PosHolders[i].IsSelecting)
				{
					return true;
				}
			}
			return false;
		}
	}

	public bool RotationSelected
	{
		get
		{
			for (int i = 0; i < RotHolders.Length; i++)
			{
				if (RotHolders[i].IsFocused && !RotHolders[i].IsSelecting)
				{
					return true;
				}
			}
			return false;
		}
	}

	public bool ScaleSelected
	{
		get
		{
			for (int i = 0; i < ScaleHolders.Length; i++)
			{
				if (ScaleHolders[i].IsFocused && !ScaleHolders[i].IsSelecting)
				{
					return true;
				}
			}
			return false;
		}
	}

	public void Awake()
	{
		if (PosHolders.Length != 3)
		{
			Debug.LogError("Not the right amount of pos holders!");
			return;
		}
		PosHolders[0].ValueChanged += OnPosX;
		PosHolders[1].ValueChanged += OnPosY;
		PosHolders[2].ValueChanged += OnPosZ;
		RotHolders[0].ValueChanged += OnRotX;
		RotHolders[1].ValueChanged += OnRotY;
		RotHolders[2].ValueChanged += OnRotZ;
		ScaleHolders[0].ValueChanged += OnScaleX;
		ScaleHolders[1].ValueChanged += OnScaleY;
		ScaleHolders[2].ValueChanged += OnScaleZ;
		copy.Down += CopyAll;
		paste.Down += PasteAll;
		reset.Down += ResetAll;
	}

	public override void Init(int i, object parameter)
	{
		base.Init(i, parameter);
		GenericEntity genericEntity = parameter as GenericEntity;
		entity = genericEntity.entity;
		lastPosition = entity.Position;
		lastRotation = entity.Rotation;
		lastScale = entity.Scale;
		UpdateVisual();
	}

	public void Update()
	{
		if (InputManager.CopyKeys())
		{
			if (PositionSelected)
			{
				ReferenceMaster.Clipboard.position = new Vector3(PosHolders[0].GetValue(), PosHolders[1].GetValue(), PosHolders[2].GetValue());
				if (base.gameObject.activeInHierarchy)
				{
					StopAllCoroutines();
					StartCoroutine(AnimateCopy(posFlashs, 0.25f));
				}
				audioSource.Play();
			}
			else if (RotationSelected)
			{
				ReferenceMaster.Clipboard.euler = new Vector3(RotHolders[0].GetValue(), RotHolders[1].GetValue(), RotHolders[2].GetValue());
				if (base.gameObject.activeInHierarchy)
				{
					StopAllCoroutines();
					StartCoroutine(AnimateCopy(rotFlashs, 0.25f));
				}
				audioSource.Play();
			}
			else if (ScaleSelected)
			{
				ReferenceMaster.Clipboard.scale = new Vector3(ScaleHolders[0].GetValue(), ScaleHolders[1].GetValue(), ScaleHolders[2].GetValue());
				if (base.gameObject.activeInHierarchy)
				{
					StopAllCoroutines();
					StartCoroutine(AnimateCopy(scaleFlashs, 0.25f));
				}
				audioSource.Play();
			}
		}
		else if (InputManager.PasteKeys())
		{
			if (PositionSelected)
			{
				_lock = true;
				PastePosition();
				audioSource.Play();
				_lock = false;
				AddUndo(CreatePosUndo());
			}
			else if (RotationSelected)
			{
				_lock = true;
				PasteRotation();
				audioSource.Play();
				_lock = false;
				AddUndo(CreateRotUndo());
			}
			else if (ScaleSelected)
			{
				_lock = true;
				PasteScale();
				audioSource.Play();
				_lock = false;
				AddUndo(CreateScaleUndo());
			}
		}
	}

	public void CopyAll()
	{
		ReferenceMaster.Clipboard.position = new Vector3(PosHolders[0].GetValue(), PosHolders[1].GetValue(), PosHolders[2].GetValue());
		ReferenceMaster.Clipboard.euler = new Vector3(RotHolders[0].GetValue(), RotHolders[1].GetValue(), RotHolders[2].GetValue());
		ReferenceMaster.Clipboard.scale = new Vector3(ScaleHolders[0].GetValue(), ScaleHolders[1].GetValue(), ScaleHolders[2].GetValue());
		audioSource.Play();
	}

	public void PastePosition()
	{
		Vector3 position = ReferenceMaster.Clipboard.position;
		SetPosition(position);
	}

	public void PasteRotation()
	{
		SetRotation(ReferenceMaster.Clipboard.euler);
	}

	public void PasteScale()
	{
		SetScale(ReferenceMaster.Clipboard.scale);
	}

	public void SetPosition(Vector3 position)
	{
		PosHolders[0].Terminate();
		PosHolders[0].SetValue(position.x);
		OnPosX(position.x);
		PosHolders[1].Terminate();
		PosHolders[1].SetValue(position.y);
		OnPosY(position.y);
		PosHolders[2].Terminate();
		PosHolders[2].SetValue(position.z);
		OnPosZ(position.z);
	}

	public void SetRotation(Vector3 euler)
	{
		RotHolders[0].Terminate();
		RotHolders[0].SetValue(euler.x);
		OnRotX(euler.x);
		RotHolders[1].Terminate();
		RotHolders[1].SetValue(euler.y);
		OnRotY(euler.y);
		RotHolders[2].Terminate();
		RotHolders[2].SetValue(euler.z);
		OnRotZ(euler.z);
	}

	public void SetScale(Vector3 scale)
	{
		ScaleHolders[0].Terminate();
		ScaleHolders[0].SetValue(scale.x);
		OnScaleX(scale.x);
		ScaleHolders[1].Terminate();
		ScaleHolders[1].SetValue(scale.y);
		OnScaleY(scale.y);
		ScaleHolders[2].Terminate();
		ScaleHolders[2].SetValue(scale.z);
		OnScaleZ(scale.z);
	}

	public void PasteAll()
	{
		_lock = true;
		PastePosition();
		PasteRotation();
		PasteScale();
		audioSource.Play();
		_lock = false;
		AddUndo(CreateUndo());
	}

	public void ResetAll()
	{
		_lock = true;
		SetPosition(entity.FirstPosition);
		SetRotation(entity.FirstRotation.eulerAngles);
		SetScale(entity.FirstScale);
		audioSource.Play();
		_lock = false;
		AddUndo(CreateUndo());
	}

	protected IEnumerator AnimateCopy(MeshRenderer[] array, float duration)
	{
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = true;
		}
		for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
		{
			float a = Mathf.Lerp(0.067f, 0f, t / duration);
			for (int j = 0; j < array.Length; j++)
			{
				array[j].material.SetColor("_TintColor", new Color(1f, 1f, 1f, a));
			}
			yield return null;
		}
		for (int k = 0; k < array.Length; k++)
		{
			array[k].material.SetColor("_TintColor", new Color(1f, 1f, 1f, 0f));
			array[k].enabled = false;
		}
	}

	protected void UpdateVisual()
	{
		Vector3 vector = entity.LastPosition;
		Vector3 eulerAngles = entity.LastRotation.eulerAngles;
		Vector3 vector2 = entity.LastScale;
		for (int i = 0; i < 3; i++)
		{
			PosHolders[i].SetText(vector[i]);
			RotHolders[i].SetText(eulerAngles[i]);
			ScaleHolders[i].SetText(vector2[i]);
		}
	}

	private void OnPosX(float posX)
	{
		OnPos(posX, 0);
	}

	private void OnPosY(float posY)
	{
		OnPos(posY, 1);
	}

	private void OnPosZ(float posZ)
	{
		OnPos(posZ, 2);
	}

	private void OnRotX(float rotX)
	{
		OnRot(rotX, 0);
	}

	private void OnRotY(float rotY)
	{
		OnRot(rotY, 1);
	}

	private void OnRotZ(float rotZ)
	{
		OnRot(rotZ, 2);
	}

	private void OnScaleX(float scaleX)
	{
		OnScale(scaleX, 0);
	}

	private void OnScaleY(float scaleY)
	{
		OnScale(scaleY, 1);
	}

	private void OnScaleZ(float scaleZ)
	{
		OnScale(scaleZ, 2);
	}

	private void OnPos(float val, int axis)
	{
		if (isEditing)
		{
			Vector3 position = entity.Position;
			if (entity.Position[axis] != val)
			{
				Vector3 position2 = new Vector3((axis != 0) ? position.x : val, (axis != 1) ? position.y : val, (axis != 2) ? position.z : val);
				entity.SetPosition(position2);
				AddUndo(CreatePosUndo());
			}
		}
	}

	private void OnRot(float val, int axis)
	{
		if (isEditing)
		{
			Quaternion rotation = entity.Rotation;
			if (entity.Rotation.eulerAngles[axis] != val)
			{
				Quaternion rotation2 = Quaternion.Euler((axis != 0) ? rotation.eulerAngles.x : val, (axis != 1) ? rotation.eulerAngles.y : val, (axis != 2) ? rotation.eulerAngles.z : val);
				entity.SetRotation(rotation2);
				AddUndo(CreateRotUndo());
			}
		}
	}

	private void OnScale(float val, int axis)
	{
		if (!isEditing)
		{
			return;
		}
		if (!entity.behaviour.prefab.canScale)
		{
			ScaleHolders[axis].SetText(entity.Scale[axis]);
			return;
		}
		Vector3 scale = entity.Scale;
		float scaleValue = EntityScaleTool.GetScaleValue(val);
		if (scaleValue != val)
		{
			ScaleHolders[axis].SetText(scaleValue);
		}
		if (entity.Scale[axis] != scaleValue)
		{
			Vector3 scale2 = new Vector3((axis != 0) ? scale.x : scaleValue, (axis != 1) ? scale.y : scaleValue, (axis != 2) ? scale.z : scaleValue);
			entity.SetScale(scale2);
			AddUndo(CreateScaleUndo());
		}
	}

	public LevelUndoAction CreateUndo()
	{
		if (_lock)
		{
			return null;
		}
		LevelUndoAction levelUndoAction = null;
		if (entity.Scale == lastScale)
		{
			if (entity.Rotation == lastRotation)
			{
				levelUndoAction = new LUAMoveEntity(entity, lastPosition);
				lastPosition = entity.Position;
			}
			else
			{
				levelUndoAction = new LUARotateEntity(entity, lastRotation, lastPosition);
				lastRotation = entity.Rotation;
				lastPosition = entity.Position;
			}
		}
		else
		{
			levelUndoAction = new LUAScaleEntity(entity, lastPosition, lastRotation, lastScale);
			lastPosition = entity.Position;
			lastRotation = entity.Rotation;
			lastScale = entity.Scale;
		}
		return levelUndoAction;
	}

	protected LevelUndoAction CreatePosUndo()
	{
		if (_lock || entity.Position == lastPosition)
		{
			return null;
		}
		LUAMoveEntity result = new LUAMoveEntity(entity, lastPosition);
		lastPosition = entity.Position;
		return result;
	}

	protected LevelUndoAction CreateRotUndo()
	{
		if (_lock || entity.Rotation == lastRotation)
		{
			return null;
		}
		LUARotateEntity result = new LUARotateEntity(entity, lastRotation, lastPosition);
		lastRotation = entity.Rotation;
		return result;
	}

	protected LevelUndoAction CreateScaleUndo()
	{
		if (_lock || entity.Scale == lastScale)
		{
			return null;
		}
		LUAScaleEntity result = new LUAScaleEntity(entity, lastPosition, lastRotation, lastScale);
		lastScale = entity.Scale;
		return result;
	}

	protected virtual void AddUndo(LevelUndoAction undoAction)
	{
		if (undoAction != null)
		{
			LevelUndoSystem.Add(undoAction);
		}
	}
}
