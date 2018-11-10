using Godot;
using System.Collections.Generic;

namespace WFS
{
	internal class MockPlayer : IActionProvider
	{
		public bool IsPerformingAction
		{
			get { return false; }
		}
		
		public Action ProvideAction()
		{
			return Action.NegativeFirst;
		}
	}
	
	public class GameController : Node
	{
		private State state;
		
		
		public override void _Ready()
		{
			GD.Print("Controller started");
			
			state = new RecordActionsState(new MockPlayer(), 4);
		}
		
		public override void _Process(float delta)
		{
			state = state?.Update(delta);
		}
	}
}