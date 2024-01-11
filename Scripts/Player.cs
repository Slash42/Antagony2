using System;
using Godot;

public partial class Player : CharacterBody2D
{

	[Export] public float speed = 300f;
	public float maxHealth = 100;
	
	AnimationTree animationTree;
	Node2D acidShooter;
	CollisionShape2D hitbox;
	Vector2 direction = new Vector2(0,0);

    public override void _Ready()
    {
        animationTree = GetNode<AnimationTree>("AnimationTree");
		acidShooter = GetNode<Node2D>("AntAcidShooter");
		hitbox = GetNode<CollisionShape2D>("AntHitbox");
    }

    public override void _PhysicsProcess(double delta) {
		
		Vector2 mousePos = GetGlobalMousePosition();
		Vector2 wayToLook = (mousePos - GlobalPosition).Normalized();		

		if (Velocity == Vector2.Zero) {
			AnimationNodeStateMachinePlayback stateMachine = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
			stateMachine.Travel("Idle");
			animationTree.Set("parameters/Idle/blend_position", wayToLook);
		} else {
			AnimationNodeStateMachinePlayback stateMachine = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
			stateMachine.Travel("Walk");
			animationTree.Set("parameters/Walk/blend_position", wayToLook);
		}
		
		if (Math.Abs(wayToLook.X) > Math.Abs(wayToLook.Y)) {
			hitbox.RotationDegrees = 90;
			if (wayToLook.X >= 0) {
				acidShooter.RotationDegrees = 0;
				direction = Vector2.Right;
			} else {
				acidShooter.RotationDegrees = 180;
				direction = Vector2.Left;
			}
		} else {
			hitbox.RotationDegrees = 0;
			if (wayToLook.Y > 0) {
				acidShooter.RotationDegrees = 90;
				direction = Vector2.Down;
			} else {
				acidShooter.RotationDegrees = 270;
				direction = Vector2.Up;
			}
		}
		
		acidShooter.Position = direction;
		
		Vector2 move_input = Input.GetVector("left", "right", "up", "down");

		Velocity = move_input * speed;

		MoveAndSlide();

	}
}
