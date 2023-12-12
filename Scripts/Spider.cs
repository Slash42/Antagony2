using Godot;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public partial class Spider : CharacterBody2D
{
    player player;

    [Export] float speed = 250f;
    [Export] float dmg = 10f;
    [Export] float aps = 10f;

    float attack_speed;
    float time_until_attack;
    bool within_attack_range = false;

    public override void _Ready() {
        player = (player)GetTree().Root.GetNode("MainGame").GetNode("Player");
        Debug.Print(player.Name);
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
    }
    public override void _PhysicsProcess(double delta) {
        if (player != null) {
            LookAt(player.GlobalPosition);
            Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();
            Velocity = direction * speed;
        } else {
            Velocity = Vector2.Zero;
        }
        MoveAndSlide();
    }

    public void Attack() {
        Debug.Print("Attack player");
    }

    public void OnAttackRangeBodyEnter(Node2D body) {
        if (body.IsInGroup("player")) {
            Debug.Print("Player in range");
            within_attack_range = true;
        } 
    }
    public void OnAttackRangeBodyExit(Node2D body) {
        if (body.IsInGroup("player")) {
            within_attack_range = false;
            time_until_attack = attack_speed;
        } 
    }
    
}
