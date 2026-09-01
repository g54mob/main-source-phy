using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class CinematicCamCreator : MonoBehaviour
{
	private SSAOPro ssao;

	private OptionsMaster.Tier orgSSAO;

	private int orgCascades;

	private bool setOrgs;

	protected void Awake()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
		base.enabled = false;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		base.enabled = !AddPiece.IsMenuScene(scene.name);
	}

	protected void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void Update()
	{
		if (!StatMaster.inMenu && InputManager.AdvancedBuilding.LeftShiftKey() && InputManager.RightShiftKey())
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				SetSSAO(true);
				SetSetReflection();
				CinematicCam.Create();
			}
			else if (Input.GetKeyDown(KeyCode.O))
			{
				SetSSAO(false);
			}
		}
	}

	private void SetSSAO(bool high)
	{
		if (object.ReferenceEquals(ssao, null))
		{
			ssao = Camera.main.GetComponent<SSAOPro>();
			if (object.ReferenceEquals(ssao, null))
			{
				return;
			}
		}
		if (high)
		{
			setOrgs = true;
			orgSSAO = OptionsMaster.BesiegeConfig.SSAOQuality;
			if ((float)Screen.height >= 2160f)
			{
				OptionsMaster.SetSSAO(5);
			}
			else
			{
				OptionsMaster.SetSSAO(4);
			}
			orgCascades = OptionsMaster.BesiegeConfig.ShadowCascades;
			OptionsMaster.BesiegeConfig.ShadowCascades = 4;
		}
		else if (setOrgs)
		{
			OptionsMaster.BesiegeConfig.SSAOQuality = orgSSAO;
			OptionsMaster.BesiegeConfig.ShadowCascades = orgCascades;
		}
		if (ReferenceMaster.onShadowsChanged != null)
		{
			ReferenceMaster.onShadowsChanged();
		}
	}

	private void SetSetReflection()
	{
		OptionsMaster.BesiegeConfig.Rippling = true;
		OptionsMaster.BesiegeConfig.ReflectionQuality = 2;
		PlanarReflections.UpdateReflectionQuality();
		OptionsMaster.SetShaderRippling();
	}
}
