using UnityEngine;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(SpriteRenderer))]
	[AddComponentMenu("More Mountains/Tools/Sprites/MMAutoOrderInLayer")]
	public class MMAutoOrderInLayer : MonoBehaviour
	{
		private static int CurrentMaxCharacterOrderInLayer;

		[Header("Global Counter")]
		[MMInformation("Add this component to an object with a sprite renderer, and it'll give it a new order in layer based on the settings defined here. First is the global counter increment, or how much you'd like to increment the layer order between two objects on that same layer.", MMInformationAttribute.InformationType.Info, false)]
		public int GlobalCounterIncrement;

		[Header("Parent")]
		[MMInformation("You can also decide to determine the new layer order based on the parent sprite's order (it'll have to be on the same layer).", MMInformationAttribute.InformationType.Info, false)]
		public bool BasedOnParentOrder;

		public int ParentIncrement;

		[Header("Children")]
		[MMInformation("And here you can decide to apply your new layer order to all children.", MMInformationAttribute.InformationType.Info, false)]
		public bool ApplyNewOrderToChildren;

		public int ChildrenIncrement;

		protected SpriteRenderer _spriteRenderer;

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void AutomateLayerOrder()
		{
		}
	}
}
