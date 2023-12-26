using Godot;

public partial class Health : Node2D
{
	[Export] public float max_health = 100f;
	float health;

	public override void _Ready() {
	   health = max_health;
	}

	public void Dmg(float dmg) {
		health -= dmg;

		if (health <= 0) {
		   GetParent().QueueFree(); 
		}
	}
}
