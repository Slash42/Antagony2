using Godot;

public partial class Player : CharacterBody2D
{
	[Export] public float speed = 300f;
	public float maxHealth = 100;

	public override void _PhysicsProcess(double delta) {

		LookAt(GetGlobalMousePosition());

		Vector2 move_input = Input.GetVector("left", "right", "up", "down");

		Velocity = move_input * speed;

		MoveAndSlide();
			  

	}
}
