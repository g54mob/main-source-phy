using System;
using UnityEngine;

[AddComponentMenu("UI/UIManager")]
public class UIManager : SingleInstance<UIManager>
{
	[Flags]
	public enum UIMode
	{
		None = 1,
		Translate = 2,
		Rotate = 4,
		Mirror = 8,
		Erase = 0x10,
		Modify = 0x20,
		Paint = 0x40,
		Simulation = 0x80,
		InMenu = 0x100,
		Loading = 0x200,
		BlockMapper = 0x400
	}

	public static UIMode CurrentUIMode = UIMode.None;

	public static Action<UIMode> onUIModeChanged;

	public override string Name
	{
		get
		{
			return "UIManager";
		}
	}

	public static UIMode GetUIMode
	{
		get
		{
			if (NetworkHUD.connecting)
			{
				return UIMode.InMenu;
			}
			Machine machine = Machine.Active();
			if ((bool)machine && machine.isSimulating)
			{
				return UIMode.Simulation;
			}
			if (StatMaster.inMenu)
			{
				return UIMode.InMenu;
			}
			if (BlockMapper.IsOpen)
			{
				return UIMode.BlockMapper;
			}
			switch (StatMaster.Mode.selectedTool)
			{
			case StatMaster.Tool.Translate:
				return UIMode.Translate;
			case StatMaster.Tool.Rotate:
				return UIMode.Rotate;
			case StatMaster.Tool.Scale:
			case StatMaster.Tool.Mirror:
				return UIMode.Mirror;
			case StatMaster.Tool.Erase:
				return UIMode.Erase;
			case StatMaster.Tool.Modify:
				return UIMode.Modify;
			case StatMaster.Tool.Paint:
				return UIMode.Paint;
			default:
				if (StatMaster.Mode.LevelEditor.selectedTool != StatMaster.Tool.None)
				{
					return UIMode.Loading;
				}
				return UIMode.None;
			}
		}
	}

	protected void Awake()
	{
		SetMode(GetUIMode);
		ReferenceMaster.onLocalMachineSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLocalMachineSimulation, new Action<bool>(OnMachineSimChanged));
		StatMaster.inMenuChanged = (Action)Delegate.Combine(StatMaster.inMenuChanged, new Action(EvaluateUIMode));
		StatMaster.Mode.ToolChanged += EvaluateUIMode;
		ReferenceMaster.onSceneTransition = (Action)Delegate.Combine(ReferenceMaster.onSceneTransition, new Action(SetLoading));
		ReferenceMaster.onSceneLoaded = (Action)Delegate.Combine(ReferenceMaster.onSceneLoaded, new Action(EvaluateUIMode));
		ReferenceMaster.OnConnect += EvaluateUIMode;
		StatMaster.LevelEditingToggled += ToggleLevelEditor;
		BlockMapper.onMapperOpen = (Action)Delegate.Combine(BlockMapper.onMapperOpen, new Action(OnMapperOpen));
		BlockMapper.onMapperClose = (Action)Delegate.Combine(BlockMapper.onMapperClose, new Action(OnMapperClosed));
	}

	private static void EvaluateUIMode(StatMaster.Tool bleh)
	{
		EvaluateUIMode();
	}

	public static void EvaluateUIMode()
	{
		SetMode(GetUIMode);
	}

	public static void SetLoading()
	{
		SetMode(UIMode.Loading);
	}

	public static void ToggleLevelEditor(bool bleh)
	{
		EvaluateUIMode();
	}

	public static void OnMapperOpen()
	{
		EvaluateUIMode();
	}

	public static void OnMapperClosed()
	{
		EvaluateUIMode();
	}

	private static void OnMachineSimChanged(bool toggle)
	{
		if (CurrentUIMode != UIMode.Loading)
		{
			if (toggle)
			{
				SetMode(UIMode.Simulation);
			}
			else
			{
				RestoreMode();
			}
		}
	}

	public static void SetMode(UIMode mode)
	{
		if (mode != CurrentUIMode || mode == UIMode.BlockMapper)
		{
			CurrentUIMode = mode;
			if (onUIModeChanged != null)
			{
				onUIModeChanged(mode);
			}
		}
	}

	public static void RestoreMode()
	{
		SetMode(GetUIMode);
	}

	protected void OnDestroy()
	{
		ReferenceMaster.onLocalMachineSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLocalMachineSimulation, new Action<bool>(OnMachineSimChanged));
		StatMaster.inMenuChanged = (Action)Delegate.Remove(StatMaster.inMenuChanged, new Action(EvaluateUIMode));
		StatMaster.Mode.ToolChanged -= EvaluateUIMode;
		ReferenceMaster.onSceneTransition = (Action)Delegate.Remove(ReferenceMaster.onSceneTransition, new Action(SetLoading));
		ReferenceMaster.onSceneLoaded = (Action)Delegate.Remove(ReferenceMaster.onSceneLoaded, new Action(EvaluateUIMode));
		ReferenceMaster.OnConnect -= EvaluateUIMode;
		StatMaster.LevelEditingToggled -= ToggleLevelEditor;
		BlockMapper.onMapperOpen = (Action)Delegate.Remove(BlockMapper.onMapperOpen, new Action(OnMapperOpen));
		BlockMapper.onMapperClose = (Action)Delegate.Remove(BlockMapper.onMapperClose, new Action(OnMapperClosed));
	}
}
