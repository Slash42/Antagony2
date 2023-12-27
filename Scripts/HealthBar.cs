using Godot;
using System;

public partial class HealthBar : ProgressBar
{
	Health health;

	public override void _Ready() {
		health = (Health)GetTree().Root.GetNode("Main Game").GetNode("Player").GetNode("Health");
		health.healthChange += () => Update();
		Update();
	}

	public void Update() {
		Value = health.health * 100 / health.maxHealth;
	}
}
