using System;
using System.Collections;
using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitEditorRayCaster : MonoBehaviour
{
	public Camera cam;

	public GraphicRaycaster m_Raycaster;

	private PointerEventData m_PointerEventData;

	public EventSystem m_EventSystem;

	private UnitEditorHandler m_editorHandler;

	private UnitEditorContextMenu m_contextMenu;

	private ObjectPool<Mesh> m_meshPool = new ObjectPool<Mesh>();

	private PlayerActions m_playerActions;

	private float m_dragDelta;

	private Coroutine m_bakeCoroutine;

	private int m_lastEquippedPropsNum;

	private List<Action> m_bakeMeshCallbacks = new List<Action>();

	private void Start()
	{
		m_editorHandler = UnitEditorHandler.Instance;
		m_contextMenu = GetComponent<UnitEditorContextMenu>();
		m_playerActions = PlayerActions.Instance;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Mouse0))
		{
			m_dragDelta += Mathf.Abs(m_playerActions.m_aim.Y) + Mathf.Abs(m_playerActions.m_aim.X);
		}
		bool flag = RaycastUI();
		AttachColliders();
		RaycastHit raycastHit = RaycastProps();
		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			if (m_dragDelta < 1f && !flag)
			{
				if ((bool)raycastHit.collider)
				{
					PropItemProxy componentInParent = raycastHit.collider.GetComponentInParent<PropItemProxy>();
					if ((bool)componentInParent)
					{
						m_contextMenu.OpenContextMenu(componentInParent.m_propItemReference);
					}
					else
					{
						m_contextMenu.CloseContextMenu(removeCurrent: true);
						m_editorHandler.RemoveTemporary();
					}
				}
				else
				{
					m_contextMenu.CloseContextMenu(removeCurrent: true);
					m_editorHandler.RemoveTemporary();
				}
			}
			m_dragDelta = 0f;
		}
		if (!flag && (bool)raycastHit.collider && m_dragDelta < 1f)
		{
			PropItemProxy componentInParent2 = raycastHit.collider.GetComponentInParent<PropItemProxy>();
			if ((bool)componentInParent2)
			{
				componentInParent2.m_propItemReference.Hover();
			}
		}
		RemoveStaticColliders();
	}

	public bool RaycastUI()
	{
		return EventSystem.current.IsPointerOverGameObject();
	}

	public RaycastHit RaycastProps()
	{
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		int mask = LayerMask.GetMask("CharacterProp");
		Physics.Raycast(ray, out var hitInfo, 10000f, mask);
		return hitInfo;
	}

	private void AttachColliders()
	{
		List<CharacterItem> equippedProps = m_editorHandler.EquippedProps;
		if (equippedProps.Count != m_lastEquippedPropsNum)
		{
			m_lastEquippedPropsNum = equippedProps.Count;
			RemoveSkinnedColliders();
			if (m_bakeCoroutine != null)
			{
				StopCoroutine(m_bakeCoroutine);
			}
			m_bakeMeshCallbacks.Clear();
			for (int i = 0; i < equippedProps.Count; i++)
			{
				m_bakeMeshCallbacks.AddRange(equippedProps[i].AttachColliders(m_meshPool, ignoreSkinned: false));
			}
			m_bakeCoroutine = StartCoroutine(BakeCollidersCoroutine());
		}
		else
		{
			for (int j = 0; j < equippedProps.Count; j++)
			{
				m_bakeMeshCallbacks.AddRange(equippedProps[j].AttachColliders(m_meshPool, ignoreSkinned: true));
			}
		}
	}

	private void RemoveStaticColliders()
	{
		List<CharacterItem> equippedProps = m_editorHandler.EquippedProps;
		for (int i = 0; i < equippedProps.Count; i++)
		{
			equippedProps[i].RemoveStaticColliders();
		}
	}

	private void RemoveSkinnedColliders()
	{
		List<CharacterItem> equippedProps = m_editorHandler.EquippedProps;
		for (int i = 0; i < equippedProps.Count; i++)
		{
			equippedProps[i].RemoveSkinnedColliders(m_meshPool);
		}
	}

	private IEnumerator BakeCollidersCoroutine()
	{
		while (true)
		{
			int i = 0;
			while (i < m_bakeMeshCallbacks.Count)
			{
				m_bakeMeshCallbacks[i]();
				yield return null;
				int num = i + 1;
				i = num;
			}
			if (m_bakeMeshCallbacks.Count == 0)
			{
				yield return null;
			}
		}
	}

	private void OnDestroy()
	{
		m_meshPool.ClearPool();
	}
}
