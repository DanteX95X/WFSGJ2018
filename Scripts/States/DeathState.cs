using Godot;

namespace WFS
{
	public class DeathState : State
	{
		private IActionProvider deadman;
		private BaseController controller;

		private float timer = 3;
		private float timePassed;
		
		private AudioStreamPlayer koSound;
		
		public DeathState(BaseController controller, IActionProvider deadman)
		{
			this.deadman = deadman;
			this.controller = controller;
			timePassed = 0;
			deadman.Reset();
			
			koSound = (AudioStreamPlayer)this.controller.GetNode("Sounds").GetNode("KOSound");
			koSound.Play();
			deadman.Animate(Action.NegativeFirst);
			controller.ResetKOLabel();
		}

		public override State Update(float delta)
		{
			timePassed += delta;
			if (timePassed >= timer)
			{
				Global global = (Global)controller.GetNode("/root/Global");
				global.GotoScene("res://Scenes/MainMenu.tscn");
				return null;
			}

			return this;
		}
	}
}