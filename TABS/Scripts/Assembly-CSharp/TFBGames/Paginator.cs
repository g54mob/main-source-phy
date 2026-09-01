using GamepadUI.StateManager.Core;

namespace TFBGames
{
	public abstract class Paginator : UIComponent
	{
		protected int currentPage;

		protected int totalPages;

		protected const int MaxItemsPerPage = 12;

		public bool IsOpen { get; protected set; }

		public virtual void OpenPage()
		{
			IsOpen = true;
			Clear();
			Populate();
		}

		public virtual void ClosePage()
		{
			IsOpen = false;
			Clear();
		}

		public virtual void ChangePage(int newPage)
		{
			if (newPage >= 0 && newPage < totalPages)
			{
				Populate(newPage);
			}
		}

		public virtual void NextPage()
		{
			ChangePage(currentPage + 1);
		}

		public virtual void PreviousPage()
		{
			ChangePage(currentPage - 1);
		}

		protected virtual void Populate(int newPage = 0)
		{
			currentPage = newPage;
		}

		protected virtual void Clear()
		{
		}
	}
}
