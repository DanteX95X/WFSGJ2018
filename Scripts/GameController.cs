using Godot;
using System.Collections.Generic;

namespace WFS
{
	internal class MockPlayer : IActionProvider
	{
		public Action ProvideAction()
		{
			return Action.Timeout;
		}
	}
	
	public class GameController : Node
	{
		private State state;
		private List<Action> actions = new List<Action>();
		
		
		public override void _Ready()
		{
			GD.Print("Controller started");
			
			state = new RecordActionsState(new MockPlayer(), 4, ref actions);
		}
		
		public override void _Process(float delta)
		{
			state?.Update(delta);
		}
	}
}