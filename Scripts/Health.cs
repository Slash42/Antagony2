using Godot;

public partial class Health : Node2D
{
	[Export] public float maxHealth = 100f;
	public float health;

	[Signal] public delegate void healthChangeEventHandler();

	public override void _Ready() {
	   health = maxHealth;
	}

	public void Dmg(float dmg) {
		health -= dmg;
		EmitSignal(SignalName.healthChange);
		if (health <= 0) {
		   GetParent().QueueFree(); 
		}
	}
}
