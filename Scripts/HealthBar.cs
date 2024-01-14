using Godot;
using System;

public partial class HealthBar : TextureProgressBar
{
	Health health;

	public override void _Ready() {
		health = (Health)GetTree().Root.GetNode("Main Game").GetNode("Player").GetNode("Health");	
	}

	public override void _Process(double delta)
	{
		health.healthChange += () => Update();
		Update();
	}

	public void Update() {
		Value = health.health * 100 / health.maxHealth;
	}
}
