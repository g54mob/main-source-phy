using Landfall.TABS.GameState;
using LevelCreator;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Landfall.TABS.TeamEdge
{
	public sealed class TeamEdgeRenderer : PostProcessEffectRenderer<TeamEdge>
	{
		private SettingsInstance m_colorFlipOption;

		public override void Init()
		{
			base.Init();
			if (Application.isPlaying)
			{
				m_colorFlipOption = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
			}
		}

		public override void Render(PostProcessRenderContext context)
		{
			context.command.BeginSample("Team Edge");
			PropertySheet propertySheet = null;
			RuntimeUtilities.BlitFullscreenTriangle(propertySheet: ((!SceneSettings.UseNewPlacementSystem || (Application.isPlaying && ServiceLocator.GetService<GameStateManager>().GameState != Landfall.TABS.GameState.GameState.PlacementState) || SandboxPlacementCamera.InFreeLook) && (!(DMEditor.Instance != null) || !DMEditor.Instance.GetShowTeamEdge())) ? RenderNothing(context) : RenderEdge(context), cmd: context.command, source: context.source, destination: context.destination, pass: 0);
			context.command.EndSample("Team Edge");
		}

		public PropertySheet RenderNothing(PostProcessRenderContext context)
		{
			return context.propertySheets.Get(Shader.Find("Hidden/TeamEdge/Nothing"));
		}

		public PropertySheet RenderEdge(PostProcessRenderContext context)
		{
			bool flag = base.settings.m_DisplayWorldCords;
			bool flag2 = base.settings.m_DisplayPerlin;
			bool flag3 = base.settings.m_DisplayPerlinDirection;
			if (flag && !Application.isEditor)
			{
				flag = false;
			}
			if (flag2 && !Application.isEditor)
			{
				flag2 = false;
			}
			if (flag3 && !Application.isEditor)
			{
				flag3 = false;
			}
			PropertySheet propertySheet = null;
			EdgeType edgeType = SceneSettings.Instance.m_EdgeType;
			switch (edgeType)
			{
			case EdgeType.Line:
				propertySheet = context.propertySheets.Get(Shader.Find("Hidden/TeamEdge/Line"));
				break;
			case EdgeType.Circle:
				propertySheet = context.propertySheets.Get(Shader.Find("Hidden/TeamEdge/Circle"));
				break;
			}
			if (propertySheet == null)
			{
				propertySheet = context.propertySheets.Get(Shader.Find("Hidden/TeamEdge/Line"));
			}
			TeamEdgeTypeSettings teamEdgeTypeSettings = GlobalTeamEdgeSettings.GetSettings(edgeType);
			_ = MapSettings.Instance;
			SceneSettings instance = SceneSettings.Instance;
			propertySheet.properties.SetMatrix("_InverseView", context.camera.cameraToWorldMatrix);
			propertySheet.properties.SetVector("_DisplayDebugModes", new Vector4(IsDebugModeOn(flag), IsDebugModeOn(flag2), IsDebugModeOn(flag3), 0f));
			propertySheet.properties.SetVector("_EdgeInformation", MakeVector(instance.Offset, new Vector2(instance.m_Rotation, 0f)));
			propertySheet.properties.SetFloat("_EdgeThickness", teamEdgeTypeSettings.m_LineThickness * 0.5f);
			propertySheet.properties.SetFloat("_EdgePerlinFreq", teamEdgeTypeSettings.m_PerlinFrequency);
			propertySheet.properties.SetFloat("_EdgePerlinSpeed", teamEdgeTypeSettings.m_PerlinScrollSpeed);
			propertySheet.properties.SetFloat("_EdgePerlinAmp", teamEdgeTypeSettings.m_PerlinStrength);
			propertySheet.properties.SetFloat("_DeadzoneThickness", instance.m_DeadzoneSize * 0.5f);
			propertySheet.properties.SetFloat("_DeadZoneRotationOffset", teamEdgeTypeSettings.m_DeadZoneRotationOffset);
			propertySheet.properties.SetFloat("_DeadzoneLineFrequency", teamEdgeTypeSettings.m_DeadzoneLineFrequency);
			propertySheet.properties.SetFloat("_DeadzoneLineWarpingScale", teamEdgeTypeSettings.m_DeadzoneLineWarpingScale);
			propertySheet.properties.SetFloat("_DeadzoneLineWarpingFrequency", teamEdgeTypeSettings.m_DeadzoneLineWarpingFreqeuncy);
			if (Application.isPlaying && m_colorFlipOption.currentValue == 1)
			{
				if (instance.m_TeamIsSwapped)
				{
					propertySheet.properties.SetColor("_BlueColor", teamEdgeTypeSettings.m_BlueColor);
					propertySheet.properties.SetColor("_RedColor", teamEdgeTypeSettings.m_RedColor);
				}
				else
				{
					propertySheet.properties.SetColor("_BlueColor", teamEdgeTypeSettings.m_RedColor);
					propertySheet.properties.SetColor("_RedColor", teamEdgeTypeSettings.m_BlueColor);
				}
			}
			else if (instance.m_TeamIsSwapped)
			{
				propertySheet.properties.SetColor("_BlueColor", teamEdgeTypeSettings.m_RedColor);
				propertySheet.properties.SetColor("_RedColor", teamEdgeTypeSettings.m_BlueColor);
			}
			else
			{
				propertySheet.properties.SetColor("_BlueColor", teamEdgeTypeSettings.m_BlueColor);
				propertySheet.properties.SetColor("_RedColor", teamEdgeTypeSettings.m_RedColor);
			}
			propertySheet.properties.SetFloat("_GradientFalloffDistance", teamEdgeTypeSettings.m_GradientFalloffDistance);
			propertySheet.properties.SetFloat("_GradientFalloffStrength", teamEdgeTypeSettings.m_GradientFalloffStrength);
			propertySheet.properties.SetFloat("_FadeFactor", instance.FadeFator);
			switch (edgeType)
			{
			case EdgeType.Line:
			{
				Vector3 vector = Quaternion.Euler(new Vector3(0f, instance.m_Rotation, 0f)) * Vector3.forward;
				Vector3 vector2 = new Vector3(instance.Offset.x, 0f, instance.Offset.y);
				Vector3 vector3 = vector2 + vector.normalized * 1000f;
				Vector3 vector4 = vector2 - vector.normalized * 1000f;
				propertySheet.properties.SetVector("_LinePoints", MakeVector(MakeTopDownVector(vector3), MakeTopDownVector(vector4)));
				break;
			}
			case EdgeType.Circle:
				propertySheet.properties.SetFloat("_Radius", instance.m_CircleRadius);
				break;
			}
			return propertySheet;
		}

		private int IsDebugModeOn(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		private Vector2 MakeTopDownVector(Vector3 vector)
		{
			return new Vector2(vector.x, vector.z);
		}

		private Vector4 MakeVector(Vector3 v, float w)
		{
			return new Vector4(v.x, v.y, v.z, w);
		}

		private Vector4 MakeVector(Vector2 v1, Vector2 v2)
		{
			return new Vector4(v1.x, v1.y, v2.x, v2.y);
		}

		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth;
		}
	}
}
