using DM;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS
{
	public class TeamColor : MonoBehaviour
	{
		public int materialID;

		public Material redMaterial;

		public Material blueMaterial;

		private Renderer meshRenderer;

		private CharacterItem.RendererMaterialWrapper m_renderMaterialWrapper;

		private int m_teamColorIndex;

		private Team m_currentTeam;

		private SettingsInstance m_flipColorsSetting;

		public int TeamColorIndex => m_teamColorIndex;

		private void Awake()
		{
			TeamColorPaletteData[] teamColors = ContentDatabase.Instance().GetUnitEditorColorPalette().TeamColors;
			for (int i = 0; i < teamColors.Length; i++)
			{
				if (teamColors[i].m_materialRed == redMaterial)
				{
					m_teamColorIndex = i;
					break;
				}
			}
			m_flipColorsSetting = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
			meshRenderer = GetComponent<Renderer>();
		}

		private void UpdateTeamColors()
		{
			UpdateTeamColors(m_flipColorsSetting.currentValue);
		}

		private void UpdateTeamColors(int value)
		{
			Team team = m_currentTeam;
			if (value == 1)
			{
				switch (team)
				{
				case Team.Red:
					team = Team.Blue;
					break;
				case Team.Blue:
					team = Team.Red;
					break;
				}
			}
			if (m_renderMaterialWrapper != null)
			{
				if (m_renderMaterialWrapper.m_hasTeamColor)
				{
					m_renderMaterialWrapper.SetMaterial(GetTeamMaterial(team), m_teamColorIndex, teamColor: true);
				}
				return;
			}
			if (meshRenderer == null)
			{
				Debug.LogError("Object is missing renderer: " + base.gameObject.name, base.gameObject);
				return;
			}
			Material[] sharedMaterialsNonAlloc = meshRenderer.GetSharedMaterialsNonAlloc();
			if (materialID >= sharedMaterialsNonAlloc.Length)
			{
				Debug.LogError("MaterialID does not match teamcolor material: " + base.transform.parent.name, base.gameObject);
				return;
			}
			sharedMaterialsNonAlloc[materialID] = GetTeamMaterial(team);
			meshRenderer.sharedMaterials = sharedMaterialsNonAlloc;
		}

		public void RegisterRenderersMaterialWrapper(CharacterItem.RendererMaterialWrapper rendererMaterialWrapper)
		{
			m_renderMaterialWrapper = rendererMaterialWrapper;
		}

		public void SetTeamColor(Team team)
		{
			m_currentTeam = team;
			UpdateTeamColors();
		}

		private Material GetTeamMaterial(Team team)
		{
			switch (team)
			{
			case Team.Red:
				return redMaterial;
			case Team.Blue:
				return blueMaterial;
			default:
				return null;
			}
		}
	}
}
