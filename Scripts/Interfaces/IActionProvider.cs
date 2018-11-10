using System;

namespace WFS
{
	public interface IActionProvider
	{
		Action ProvideAction();
		bool IsPerformingAction { get; }
	}
}