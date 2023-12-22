using Godot;

public partial class Player : CharacterBody2D
{
	[Export] public float maxSpeed = 300f;
	[Export] public float acceleration = 500f;
	[Export] public float deceleration = 800f;


	public override void _Process(double delta) {

		LookAt(GetGlobalMousePosition());

		Vector2 move_input = Input.GetVector("left", "right", "up", "down");

		Velocity = move_input * maxSpeed;

		MoveAndSlide();

		Vector2 targetVelocity = Velocity;
              
		if (Velocity != Vector2.Zero) {

			float speed = Velocity.Length();
            Vector2 direction = Velocity.Normalized();

            Vector2 accelerationVector = direction * acceleration * (float)delta;
            Velocity += accelerationVector;

			if (Velocity.Length() > maxSpeed) {
				Velocity = Velocity.Normalized() * maxSpeed;
			}

		} else {

			Vector2 decVector = -Velocity.Normalized() * deceleration * (float)delta;

			if (Velocity.Length() < decVector.Length()) {
				Velocity = Vector2.Zero;
			} else {
				Velocity += decVector;
			}
		}
		Translate(Velocity * (float)delta);
	}
}