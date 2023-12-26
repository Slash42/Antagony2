using Godot;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public partial class Spider : CharacterBody2D
{
	Player player;
	AnimatedSprite2D _animatedSprite;

	[Export] float speed = 250f;
	[Export] float dmg = 40f;
	[Export] float aps = 2f;

	float attack_speed;
	float time_until_attack;
	bool within_attack_range = false;
	bool within_vis_range = false;
   
	
	Random random = new Random();

	public override void _Ready() {
		player = (Player)GetTree().Root.GetNode("Main Game").GetNode("Player");
		Debug.Print(player.Name);
		_animatedSprite = GetNode<AnimatedSprite2D>("SpiderWalk");
		attack_speed = 1 / aps;
		time_until_attack = attack_speed;
	}

	public override void _Process(double delta) {
		if (within_attack_range && time_until_attack <= 0) {
			Attack();
			time_until_attack = attack_speed;
		} else {
			time_until_attack -= (float)delta;
		}
		_animatedSprite.Play("SpiderWalk");
	}
	public override void _PhysicsProcess(double delta) {
		if (player != null && within_vis_range) {           
			Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();
			Velocity = direction * speed;
		} else if (player != null) {
			if (time_until_attack <= 0) {
			int x = random.Next(-100, 101);
			int y = random.Next(-100, 101);
			Velocity = new Vector2(x,y);
			time_until_attack = attack_speed;
			} else {
			time_until_attack -= (float)delta;
			}            
		} else {
			Velocity = Vector2.Zero;
		} 
		MoveAndSlide();
	}

	public void Attack() {
		player.GetNode<Health>("Health").Dmg(dmg);
		Debug.Print("Attack player");
	}

	public void OnVisRangeBodyEnter(Node2D body) {
		if (body.IsInGroup("Player")) {
			Debug.Print("Player in range");
			within_vis_range = true;
		} 
	}
	public void OnVisRangeBodyExit(Node2D body) {
		if (body.IsInGroup("Player")) {
			within_vis_range = false;
			time_until_attack = attack_speed;
		} 
	}

	public void OnAttackRangeBodyEnter(Node2D body) {
		if (body.IsInGroup("Player")) {
			Debug.Print("Player in attack range");
			within_attack_range = true;
		} 
	}
	public void OnAttackRangeBodyExit(Node2D body) {
		if (body.IsInGroup("Player")) {
			within_attack_range = false;
			time_until_attack = attack_speed;
		} 
	}
	
}

