using Localisation;
using UnityEngine;

public class SetBlockMapperTitle : MonoBehaviour
{
	protected void Start()
	{
		BlockMapper component = GetComponent<BlockMapper>();
		string blockName = (component.IsBlock ? ReferenceMaster.TranslateBlockName(component.Block.Prefab.Type) : ((component.IsEntity && component.Entity.entity != null) ? LocalisationManager.GetTranslation(component.Entity.prefab.LocalisationID) : ((!component.IsGenericHolder) ? component.Current.name : component.Holder.Name)));
		component.SetBlockName(blockName);
	}
}
