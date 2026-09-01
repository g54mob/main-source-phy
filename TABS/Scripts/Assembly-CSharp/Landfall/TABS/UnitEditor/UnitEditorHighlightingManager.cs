using TFBGames;
using UnityEngine;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorHighlightingManager : MonoBehaviour
	{
		public Material flashingMaterial;

		private Vector3 flashingHSV;

		private bool isBlinking;

		public UnitEditorColorPalette ColorPalette;

		private void Start()
		{
			flashingMaterial = new Material(flashingMaterial);
			flashingMaterial.name = "Flashing Material";
		}

		public void BlinkClothes(UnitEditorManager.EquipedWrapper equipedItem, int subMeshIndex, bool flash = true)
		{
			UnitEditorManager unitEditorManager = Object.FindObjectOfType<UnitEditorManager>();
			object data;
			Color startColor;
			switch (equipedItem.GetClothingColor(subMeshIndex, ColorPalette, out data))
			{
			case UnitEditorManager.EquipedWrapper.ColorClothingDataType.ColorData:
				startColor = ((ColorPaletteData)data).m_color;
				break;
			case UnitEditorManager.EquipedWrapper.ColorClothingDataType.TeamColorData:
				startColor = ((TeamColorPaletteData)data).GetColor(unitEditorManager.currentTeam);
				break;
			default:
				startColor = ((CharacterItem.RendererMaterialWrapper)data).m_material.SafeColor();
				break;
			}
			BlinkClothes(equipedItem, subMeshIndex, startColor, flash);
		}

		public void BlinkClothes(UnitEditorManager.EquipedWrapper equipedItem, int subMeshIndex, Color startColor, bool flash = true)
		{
			isBlinking = flash;
			Color.RGBToHSV(startColor, out var H, out var S, out var V);
			flashingHSV = new Vector3(H, S, V);
			equipedItem.spawnedProp.SetTemporaryMaterial(flashingMaterial, subMeshIndex);
			flashingMaterial.color = startColor;
		}

		private void Update()
		{
			if (isBlinking)
			{
				float t = (Mathf.Sin(Time.time * 7f) + 1f) * 0.5f;
				flashingMaterial.color = Color.HSVToRGB(flashingHSV.x, flashingHSV.y * Mathf.Lerp(0.9f, 1f, t), flashingHSV.z + Mathf.Lerp(-0.1f, 0.1f, t), hdr: false);
			}
		}

		public void StopBlinking(UnitEditorManager.EquipedWrapper equipedItem, int subMeshIndex)
		{
			equipedItem.spawnedProp.ResetTemporaryMaterial(subMeshIndex);
			isBlinking = false;
		}

		public void CancelBlinking()
		{
			isBlinking = false;
		}
	}
}
