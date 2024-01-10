using System;
using Godot;

public partial class Player : CharacterBody2D
{

	[Export] public float speed = 300f;
	public float maxHealth = 100;
	
	AnimationTree animationTree;
	Vector2 direction = new Vector2(0,0);

    public override void _Ready()
    {
        animationTree = GetNode<AnimationTree>("AnimationTree");
    }

    public override void _PhysicsProcess(double delta) {
		
		Vector2 mousePos = GetGlobalMousePosition();
		Vector2 wayToLook = (mousePos - GlobalPosition).Normalized();		

		if (wayToLook.X > 0 && wayToLook.X > Math.Abs(wayToLook.Y)) {
			direction.X = 1;
			direction.Y = 0;
		} else if (wayToLook.X < 0 && Math.Abs(wayToLook.X) > Math.Abs(wayToLook.Y)) {
			direction.X = -1;
			direction.Y = 0;		
		} else if (wayToLook.Y > 0 && wayToLook.Y > Math.Abs(wayToLook.X)) {
			direction.X = 0;
			direction.Y = -1;
		} else if (wayToLook.Y < 0 && Math.Abs(wayToLook.Y) > Math.Abs(wayToLook.Y)) {
			direction.X = 0;
			direction.Y = 1;
		}

		animationTree.Set("parameters/Idle/blend_position", direction);

		Vector2 move_input = Input.GetVector("left", "right", "up", "down");

		Velocity = move_input * speed;

		MoveAndSlide();


	}
}
