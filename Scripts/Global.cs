using Godot;
using System.Collections.Generic;

namespace WFS
{
	public class Global : Node
	{
		static public ConfigFile config;

		public override void _Ready()
		{
			config = new ConfigFile();
			config.Load("res://GameConfig.cfg");
			// config.GetValue("Config", "AttackTime");
		}
	}
}