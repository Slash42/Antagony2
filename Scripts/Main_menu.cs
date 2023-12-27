using Godot;
using System;

public partial class Main_menu : Control
{
	public void _on_play_pressed() {
		GetTree().ChangeSceneToFile("res://main_game.tscn");
	}
	public void _on_options_pressed() {
		GetTree().ChangeSceneToFile("res://Scenes/Options_menu.tscn");
	}
	public void _on_quit_pressed() {
		GetTree().Quit();
	}
}
