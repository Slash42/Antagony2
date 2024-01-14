using Godot;
using System;

public partial class Textbox : CanvasLayer
{
    MarginContainer textbox_container;
    Label label;

    public override void _Ready() {
        textbox_container = GetNode<MarginContainer>("TextboxContainer");
        label = GetNode<Label>("Label");
        ShowTextbox();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_accept")) {
            HideTextbox();
        }     
    }

    public void HideTextbox() {
        textbox_container.Hide();
    }

    public void ShowTextbox() {
        textbox_container.Show();
    }
}
