using System;

namespace WFS
{
	public interface IActionProvider
	{
		Action ProvideAction();
		bool IsPerformingAction { get; }
		int Health { get; set; }
		void Reset();
		GameController controller { set; }
	}
}