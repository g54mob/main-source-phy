using System;
using System.Collections;
using UnityEngine;
using VInspector;

[SelectionBase]
public class ItemController : MonoBehaviour, IHighlight
{
	public ItemType itemType;

	public PrefabTypes prefabType;

	public TutorialStepType pickUpTutorialStepType;

	[SerializeField]
	private GameObject highlightIndicatorObject;

	[Space(5f)]
	[Foldout("Item Components")]
	[Space]
	[SerializeField]
	protected Rigidbody rbody;

	[SerializeField]
	protected Collider col;

	[SerializeField]
	private Outline outline;

	[SerializeField]
	private float dropForce = 3f;

	public TweenDuration itemMoveTweenDuration = TweenDuration.Super_Short;

	public float itemMoveTweenDurationfloat = 0.25f;

	public Vector3 pickupPotionRotationAngle;

	public Vector3 startingScale;

	[Space]
	[SerializeField]
	[ReadOnly]
	public bool isPickedUp;

	[SerializeField]
	[ReadOnly]
	private bool isHighlighted;

	[SerializeField]
	[ReadOnly]
	private bool isHintHighlighted;

	protected int currentLayer;

	private bool isGroundHit;

	private bool isWallHit;

	private int groundLayer = 11;

	private int wallLayer = 12;

	private Action OnMoveCompleteAction;

	[EndFoldout]
	protected virtual void Start()
	{
		itemMoveTweenDurationfloat = TweenDataStore.GetTweenDuration(itemMoveTweenDuration);
		currentLayer = base.gameObject.layer;
		rbody = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
		outline = GetComponent<Outline>();
		if (rbody == null)
		{
			Debug.LogError("rbody == null");
		}
		if (col == null)
		{
			Debug.LogError("col == null");
		}
		if (outline == null)
		{
			Debug.LogError("outline == null");
		}
		SetHighlight(flag: false);
		outline.enabled = false;
	}

	public virtual void LoadItem(ItemSaveData itemSaveData)
	{
		currentLayer = base.gameObject.layer;
		DisablePhysics(instantDisable: true);
		base.transform.position = itemSaveData.position;
		base.transform.eulerAngles = itemSaveData.eulerAngles;
		if (itemSaveData.isInInventory)
		{
			SceneSingleton<CharacterInventory>.Instance.AddItemToInventory(this);
		}
	}

	public void SetHintHighlight(bool flag)
	{
		if (highlightIndicatorObject != null)
		{
			MasterPool.ReturnToPoolTransform(highlightIndicatorObject, PrefabTypes.highlightIndicatorObject);
		}
		highlightIndicatorObject = null;
		if (flag && CanShowHighlithIndicator())
		{
			highlightIndicatorObject = MasterPool.Get(PrefabTypes.highlightIndicatorObject, base.transform.position);
		}
		isHintHighlighted = flag;
		UpdateOutline();
	}

	public void SetHighlight(bool flag)
	{
		isHighlighted = flag;
		UpdateOutline();
	}

	protected void UpdateOutline()
	{
		outline.enabled = (isHighlighted || isHintHighlighted) && !isPickedUp;
		if (isHighlighted)
		{
			outline.OutlineColor = Color.white;
		}
		else if (isHintHighlighted)
		{
			outline.OutlineColor = Color.purple;
		}
	}

	protected virtual bool CanShowHighlithIndicator()
	{
		return !isPickedUp;
	}

	public virtual void PickItem()
	{
		if (highlightIndicatorObject != null)
		{
			MasterPool.ReturnToPoolTransform(highlightIndicatorObject, PrefabTypes.highlightIndicatorObject);
		}
		highlightIndicatorObject = null;
		isPickedUp = true;
		UpdateOutline();
		StopAllCoroutines();
		if (pickUpTutorialStepType != TutorialStepType.None)
		{
			SceneSingleton<TutorialManager>.Instance.CompleteTutorialStep(pickUpTutorialStepType);
		}
		col.enabled = false;
		rbody.isKinematic = true;
	}

	public virtual void DropItem()
	{
		if (pickUpTutorialStepType == TutorialStepType.Tutorial_Potion_PickUp)
		{
			SceneSingleton<TutorialManager>.Instance.CompleteTutorialStep(TutorialStepType.Tutorial_Potion_Drop);
		}
		isPickedUp = false;
		isGroundHit = true;
		isWallHit = true;
		UpdateOutline();
		EnablePhysics();
		Vector3 forward = Camera.main.transform.forward;
		rbody.AddForce(forward.normalized * dropForce, ForceMode.Impulse);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == groundLayer)
		{
			if (!isGroundHit)
			{
				return;
			}
			isGroundHit = false;
			MasterPool.Get(PrefabTypes.Bottle_Dust, base.transform.position);
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Potion_Drop);
		}
		if (collision.gameObject.layer == wallLayer && isWallHit)
		{
			isWallHit = false;
			MasterPool.Get(PrefabTypes.Bottle_Dust, base.transform.position);
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Potion_Drop);
		}
	}

	public void EnablePhysics()
	{
		rbody.linearVelocity = Vector3.zero;
		rbody.angularVelocity = Vector3.zero;
		rbody.isKinematic = false;
		col.enabled = true;
		DisablePhysics();
	}

	public void DisablePhysics(bool instantDisable = false)
	{
		StopAllCoroutines();
		if (instantDisable)
		{
			rbody.isKinematic = true;
		}
		else
		{
			StartCoroutine(DisablePhysicsCoroutine());
		}
	}

	protected IEnumerator DisablePhysicsCoroutine()
	{
		yield return new WaitForSeconds(3f);
		rbody.isKinematic = true;
	}

	public virtual void SetItemLayer(int layer)
	{
		currentLayer = layer;
		base.gameObject.layer = layer;
		foreach (Transform item in base.gameObject.transform)
		{
			item.gameObject.layer = layer;
		}
	}

	public void MoveItem(Transform targetTransform, Action OnMoveCompleteAction = null)
	{
		this.OnMoveCompleteAction = OnMoveCompleteAction;
		TweenController.KillTweens(base.gameObject);
		TweenController.DOMoveTransform(base.transform, targetTransform, itemMoveTweenDuration, OnMoveComplete);
	}

	public void MoveItem(Vector3 targetPosition, Vector3 targetAngles, Vector3 targetScale, Action OnMoveCompleteAction = null)
	{
		this.OnMoveCompleteAction = OnMoveCompleteAction;
		TweenController.KillTweens(base.gameObject);
		TweenController.DOMoveTransform(base.transform, targetPosition, targetAngles, targetScale, itemMoveTweenDuration, OnMoveComplete);
	}

	public virtual void OnMoveComplete()
	{
		if (OnMoveCompleteAction != null)
		{
			OnMoveCompleteAction();
		}
	}
}
