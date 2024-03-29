using System;
using Godot;

public partial class Player : CharacterBody2D
{

	[Export] public float base_speed = 300f;
	[Export] public float accel = 2f;
	[Export] public float dash_speed = 5000f;
	[Export] public float dash_duration = 0.2f;
	public float maxHealth = 100f;
	public float speed;
	
	AnimationTree animationTree;
	Node2D acidShooter;
	CollisionShape2D hitbox;
	Dash dash;

	Vector2 direction = new Vector2(0,0);
	Vector2 hitboxpos = new Vector2(0,0);

    public override void _Ready()
    {
		dash = (Dash)GetTree().Root.GetNode("Main Game").GetNode("Player").GetNode("Dash");
        animationTree = GetNode<AnimationTree>("AnimationTree");
		acidShooter = GetNode<Node2D>("AntAcidShooter");
		hitbox = GetNode<CollisionShape2D>("AntHitbox");
    }

    public override void _PhysicsProcess(double delta) {
		
		AnimateChar();
		RotateChar();

		if (Input.IsActionJustPressed("dash") && (dash.can_dash == true)) {
			dash.StartDash(dash_duration);
		}

		if (dash.IsDashing() == true) {
			speed = dash_speed;
		} else if (dash.IsDashing() == false) {
			speed = base_speed;
		}

		acidShooter.Position = direction;
		hitbox.Position = hitboxpos;
		
		Vector2 move_input = Input.GetVector("left", "right", "up", "down");
		move_input = move_input.Normalized();

		Velocity = Velocity.Lerp(move_input * speed, (float)delta * accel);

		MoveAndSlide();

	}

	public void AnimateChar() {
		Vector2 mousePos = GetGlobalMousePosition();
		Vector2 wayToLook = (mousePos - GlobalPosition).Normalized();
			if ((Math.Abs(Velocity.X) < 2) && (Math.Abs(Velocity.Y) < 2)) {
			AnimationNodeStateMachinePlayback stateMachine = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
			stateMachine.Travel("Idle");
			animationTree.Set("parameters/Idle/blend_position", wayToLook);
		} else {
			AnimationNodeStateMachinePlayback stateMachine = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
			stateMachine.Travel("Walk");
			animationTree.Set("parameters/Walk/blend_position", wayToLook);
		}
	}

	public void RotateChar() {
		Vector2 mousePos = GetGlobalMousePosition();
		Vector2 wayToLook = (mousePos - GlobalPosition).Normalized();
			if (Math.Abs(wayToLook.X) > Math.Abs(wayToLook.Y)) {
			hitbox.RotationDegrees = 90;
			hitboxpos.Y = 32;
			hitboxpos.X = -30;
			if (wayToLook.X >= 0) {
				acidShooter.RotationDegrees = 0;
				direction.X = 38;
				direction.Y = 40;
			} else {
				acidShooter.RotationDegrees = 180;
				direction.X = -98;
				direction.Y = 52;
			}
		} else {
			hitbox.RotationDegrees = 0;
			hitboxpos.Y = 0;
			hitboxpos.X = -30;
			if (wayToLook.Y > 0) {
				acidShooter.RotationDegrees = 90;
				direction.X = -25;
				direction.Y = 88;
			} else {
				acidShooter.RotationDegrees = 270;
				direction.X = -36;
				direction.Y = -64;
			}
		}
	}
}
