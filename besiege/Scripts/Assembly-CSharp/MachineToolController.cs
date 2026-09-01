using UnityEngine;

[AddComponentMenu("UI/Tools/Machine Tool Controller")]
public class MachineToolController : MonoBehaviour
{
	public TranslateButton translateToolCode;

	public MachineRotation rotateToolCode;

	public MirrorButton mirrorToolCode;

	public EraseButton eraseButtonCode;

	public KeyMapModeButton keyMapButtonCode;

	public SymmetryButton symmetryPivotButtonCode;

	public PaintButton paintToolCode;

	public static MachineToolController Instance;

	private void Awake()
	{
		Instance = this;
		ReferenceMaster.ToolDisable += DisableAll;
	}

	private void OnDestroy()
	{
		ReferenceMaster.ToolDisable -= DisableAll;
	}

	public void EnableTranslate()
	{
		keyMapButtonCode.OffExternal();
		eraseButtonCode.OffExternal();
		symmetryPivotButtonCode.OffExternal();
		if (rotateToolCode != null)
		{
			rotateToolCode.OffExternal();
		}
		if (mirrorToolCode != null)
		{
			mirrorToolCode.OffExternal();
		}
		if (paintToolCode != null)
		{
			paintToolCode.OffExternal();
		}
	}

	public void EnableRotate()
	{
		keyMapButtonCode.OffExternal();
		eraseButtonCode.OffExternal();
		symmetryPivotButtonCode.OffExternal();
		translateToolCode.OffExternal();
		if (mirrorToolCode != null)
		{
			mirrorToolCode.OffExternal();
		}
		if (paintToolCode != null)
		{
			paintToolCode.OffExternal();
		}
	}

	public void EnableMirror()
	{
		keyMapButtonCode.OffExternal();
		eraseButtonCode.OffExternal();
		symmetryPivotButtonCode.OffExternal();
		translateToolCode.OffExternal();
		if (rotateToolCode != null)
		{
			rotateToolCode.OffExternal();
		}
		if (paintToolCode != null)
		{
			paintToolCode.OffExternal();
		}
	}

	public void EnableKeyMap()
	{
		eraseButtonCode.OffExternal();
		translateToolCode.OffExternal();
		symmetryPivotButtonCode.OffExternal();
		if (rotateToolCode != null)
		{
			rotateToolCode.OffExternal();
		}
		if (mirrorToolCode != null)
		{
			mirrorToolCode.OffExternal();
		}
		if (paintToolCode != null)
		{
			paintToolCode.OffExternal();
		}
	}

	public void EnableErase()
	{
		keyMapButtonCode.OffExternal();
		translateToolCode.OffExternal();
		symmetryPivotButtonCode.OffExternal();
		if (rotateToolCode != null)
		{
			rotateToolCode.OffExternal();
		}
		if (mirrorToolCode != null)
		{
			mirrorToolCode.OffExternal();
		}
		if (paintToolCode != null)
		{
			paintToolCode.OffExternal();
		}
	}

	public void EnableSymmetryPivot()
	{
		if (!StatMaster.advancedBuilding)
		{
			translateToolCode.OffExternal();
			if (mirrorToolCode != null)
			{
				mirrorToolCode.OffExternal();
			}
			if (rotateToolCode != null)
			{
				rotateToolCode.OffExternal();
			}
			if (paintToolCode != null)
			{
				paintToolCode.OffExternal();
			}
		}
		keyMapButtonCode.OffExternal();
		eraseButtonCode.OffExternal();
	}

	public void EnablePaint()
	{
		if (keyMapButtonCode != null)
		{
			keyMapButtonCode.OffExternal();
		}
		if (translateToolCode != null)
		{
			translateToolCode.OffExternal();
		}
		if (eraseButtonCode != null)
		{
			eraseButtonCode.OffExternal();
		}
		if (symmetryPivotButtonCode != null)
		{
			symmetryPivotButtonCode.OffExternal();
		}
		if (rotateToolCode != null)
		{
			rotateToolCode.OffExternal();
		}
		if (mirrorToolCode != null)
		{
			mirrorToolCode.OffExternal();
		}
	}

	public void DisableSymmetryPivot()
	{
		symmetryPivotButtonCode.OffExternal();
		EnableSymmetryPivot();
	}

	public void DisableAll()
	{
		StatMaster.Mode.selectedTool = StatMaster.Tool.None;
		if (keyMapButtonCode != null)
		{
			keyMapButtonCode.OffExternal();
		}
		if (translateToolCode != null)
		{
			translateToolCode.OffExternal();
		}
		if (eraseButtonCode != null)
		{
			eraseButtonCode.OffExternal();
		}
		if (symmetryPivotButtonCode != null)
		{
			symmetryPivotButtonCode.OffExternal();
		}
		if (rotateToolCode != null)
		{
			rotateToolCode.OffExternal();
		}
		if (mirrorToolCode != null)
		{
			mirrorToolCode.OffExternal();
		}
		if (paintToolCode != null)
		{
			paintToolCode.OffExternal();
		}
	}
}
