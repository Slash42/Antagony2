using System;
using Godot;

public partial class Player : CharacterBody2D
{

	[Export] public float speed = 300f;
	public float maxHealth = 100;
	
	AnimatedSprite2D _idle;
	Vector2 direction = new Vector2(0,0);

    public override void _Ready()
    {
        _idle = GetNode<AnimatedSprite2D>("AntIdle");
    }

    public override void _PhysicsProcess(double delta) {
		
		Vector2 mousePos = GetGlobalMousePosition();
		Vector2 wayToLook = (mousePos - GlobalPosition).Normalized();
		

		if (wayToLook.X > 0 && wayToLook.X > Math.Abs(wayToLook.Y)) {
			direction.X = -100;
			direction.Y = 0;	 
		} else if (wayToLook.X < 0 && Math.Abs(wayToLook.X) > Math.Abs(wayToLook.Y)) {
			direction.X = 100;
			direction.Y = 0;
		} else if (wayToLook.Y > 0 && wayToLook.Y > Math.Abs(wayToLook.X)) {
			direction.X = 0;
			direction.Y = -100;
		} else if (wayToLook.Y < 0 && Math.Abs(wayToLook.Y) > Math.Abs(wayToLook.Y)) {
			direction.X = 0;
			direction.Y = 100;
		}

		LookAt(GlobalPosition - direction);

		Vector2 move_input = Input.GetVector("left", "right", "up", "down");

		Velocity = move_input * speed;

		if (Velocity == Vector2.Zero) {	
			_idle.Play("AntIdle");
		}

		MoveAndSlide();


	}
}
