using Godot;
using System.Collections.Generic;

namespace WFS
{
    public class Global : Node
    {
        static public ConfigFile config;

        public bool singlePlayer = false;
        public Node CurrentScene { get; set; }

        public string FirstCharacterSpriteFrameSelection { get; set; }
        public string SecondCharacterSpriteFrameSelection { get; set; }

        public override void _Ready()
        {
            config = new ConfigFile();
            config.Load("res://GameConfig.cfg");
            // config.GetValue("Config", "AttackTime");

            Viewport root = GetTree().GetRoot();
            CurrentScene = root.GetChild(root.GetChildCount() - 1);

            FirstCharacterSpriteFrameSelection = "tryout";
            SecondCharacterSpriteFrameSelection = "tryout";
        }

        public void GotoScene(string path)
        {
            CallDeferred(nameof(DeferredGotoScene), path);
        }

        public void DeferredGotoScene(string path)
        {
            // Immediately free the current scene, there is no risk here.
            CurrentScene.Free();

            // Load a new scene.
            var nextScene = (PackedScene)GD.Load(path);

            // Instance the new scene.
            CurrentScene = nextScene.Instance();

            // Add it to the active scene, as child of root.
            GetTree().GetRoot().AddChild(CurrentScene);

            // Optional, to make it compatible with the SceneTree.change_scene() API.
            GetTree().SetCurrentScene(CurrentScene);
        }
    }
}