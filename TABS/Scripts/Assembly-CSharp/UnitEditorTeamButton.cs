using Landfall.TABS;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitEditorTeamButton : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Team team;

	public UnitEditorTeamToggle Toggle;

	[Space]
	public float m_selectedScale = 1.15f;

	public float m_hoverScaleFactor = 1.1f;

	public float spring = 10f;

	public float dampner = 5f;

	public float boopForce = 5f;

	private bool selected;

	private bool hover;

	private float velocity;

	public void OnPointerClick(PointerEventData eventData)
	{
		Toggle.OnButtonSelected(team);
	}

	public void Select()
	{
		selected = true;
	}

	public void Deselect()
	{
		selected = false;
	}

	private void Update()
	{
		float num = Mathf.Clamp(Time.unscaledDeltaTime, 0f, 0.2f);
		float num2 = 1f;
		if (selected)
		{
			num2 = m_selectedScale;
		}
		if (hover)
		{
			num2 *= m_hoverScaleFactor;
		}
		float x = base.transform.localScale.x;
		velocity += (num2 - x) * spring * num;
		velocity += velocity * (0f - Mathf.Clamp(dampner * num, 0f, 0.8f));
		x += velocity * Time.deltaTime;
		base.transform.localScale = Vector3.one * x;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		hover = false;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		hover = true;
	}
}
