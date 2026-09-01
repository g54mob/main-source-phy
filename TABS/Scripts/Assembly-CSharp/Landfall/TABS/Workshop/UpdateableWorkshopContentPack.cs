using System.Collections.Generic;

namespace Landfall.TABS.Workshop
{
	public class UpdateableWorkshopContentPack
	{
		public List<GenericCustomContentWrapper> CustomContent;

		public BattleCreatorAssetUICellBase AssetCellUI;

		public BattleCreatorAssetUICellBase SelectedContent;

		public ContentTypeFilter ContentType => AssetCellUI.ContentType;
	}
}
