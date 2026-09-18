using UnityEngine;

public class PotionSignController : MonoBehaviour
{
	[SerializeField]
	private CabinetController cabinetController;

	[SerializeField]
	private MeshRenderer meshRenderer;

	private static MaterialPropertyBlock mpb;

	public void SetSignColor(Color color)
	{
		meshRenderer.material.color = color;
	}

	public void UpdateColorBlind(bool flag)
	{
		if (mpb == null)
		{
			mpb = new MaterialPropertyBlock();
		}
		meshRenderer.GetPropertyBlock(mpb);
		if (flag)
		{
			mpb.SetTexture("_PatternTex", Singleton<PotionsDataHolder>.Instance.GetCatagoryColorBlindSprite(cabinetController.potionCategoryType));
			mpb.SetColor("_PatternColor", Singleton<PotionsDataHolder>.Instance.GetCatagoryColorBlindSpriteColor(cabinetController.potionCategoryType));
			mpb.SetFloat("_PatternOpacity", Singleton<PotionsDataHolder>.Instance.GetCatagoryColorBlindSpriteOpacity(cabinetController.potionCategoryType));
			PotionPrefabColorblindData potionPrefabColorblindData = Singleton<PotionsDataHolder>.Instance.GetPotionPrefabColorblindData(PrefabTypes.Bottle_D);
			mpb.SetFloat("_PatternScale", potionPrefabColorblindData.patternCount);
			mpb.SetFloat("_PatternSpacing", potionPrefabColorblindData.patternSpacing);
			mpb.SetVector("_PatternOffset", potionPrefabColorblindData.offset);
		}
		else
		{
			mpb.SetFloat("_PatternOpacity", 0f);
		}
		meshRenderer.SetPropertyBlock(mpb);
	}
}
