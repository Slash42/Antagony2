using Godot;
using System;

public partial class Options_menu : Control
{

    public void _on_back_pressed() {
		GetTree().ChangeSceneToFile("res://Scenes/Main_menu.tscn");
	}
}
