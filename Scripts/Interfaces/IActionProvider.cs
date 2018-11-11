using System;
using Godot;

namespace WFS
{
	public interface IActionProvider
	{
		Action ProvideAction();
		bool IsPerformingAction { get; }
		int Health { get; set; }
		void Reset();
		BaseController controller { set; }
		void Animate(Action action);
		Particles2D Blood { get; set; }
	}
}