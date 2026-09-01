using BlockMapperInternal;
using Selectors;
using UnityEngine;

public class LogicWidget : ParameterWidget
{
	public int mask = -1;

	protected LogicSelector logicSelector;

	protected EntityLogic logic;

	protected int index;

	protected GenericEntity entityBehaviour;

	protected EditLogicHandler editLogicHandler;

	protected bool hasHandler;

	protected static Camera hudCam;

	protected Renderer backgroundRenderer;

	protected bool mouseOver;

	public LogicSelector Selector
	{
		get
		{
			return logicSelector;
		}
	}

	public int Index
	{
		get
		{
			return index;
		}
	}

	public EntityLogic Logic
	{
		get
		{
			return logic;
		}
	}

	public override void Init(int i, object parameter)
	{
		if (hudCam == null)
		{
			hudCam = GameObject.Find("HUD Cam").GetComponent<Camera>();
		}
		base.Init(i, parameter);
		ContainerDetails component = GetComponent<ContainerDetails>();
		backgroundRenderer = component.Background.GetComponentInChildren<Renderer>();
		logic = parameter as EntityLogic;
		index = i;
		entityBehaviour = BlockMapper.CurrentInstance.Entity;
		logicSelector = GetComponentInParent<LogicSelector>();
		editLogicHandler = EditLogicHandler.Instance;
		hasHandler = editLogicHandler != null;
		Init();
		UpdateVisual();
		ToggleHover(false);
	}

	protected virtual void Init()
	{
	}

	protected virtual void UpdateVisual()
	{
	}

	protected virtual void ToggleHover(bool toggle)
	{
	}

	protected virtual void OnDisable()
	{
		mouseOver = false;
	}

	protected void Update()
	{
		Vector2 vector = InputManager.CursorPosition();
		if (hudCam != null)
		{
			Vector3 position = hudCam.ScreenToWorldPoint(new Vector3(vector.x, vector.y, 10f));
			Bounds bounds = backgroundRenderer.bounds;
			bool flag = UIMask.InsideMask(mask, position) && position.x > bounds.min.x && position.x < bounds.max.x && position.y > bounds.min.y && position.y < bounds.max.y;
			if (mouseOver != flag)
			{
				ToggleHover(flag);
				mouseOver = flag;
			}
		}
	}
}
