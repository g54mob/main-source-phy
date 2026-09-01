using ModIO;

namespace Landfall.TABS.Workshop
{
	public interface ILoadedCustomContent
	{
		void SetTempModID(int ModID);

		void SetDetails(ModProfile details);

		bool WasChanged();

		void Changed();
	}
}
