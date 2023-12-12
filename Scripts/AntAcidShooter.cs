using Godot;
using System;

public partial class AntAcidShooter : Node2D
{
    [Export] PackedScene acid_scene;
    [Export] float acid_speed = 600f;
    [Export] float bps = 2f;
    [Export] float acid_damage = 30f;

    float fire_rate;

    float time_until_fire = 0f;

    public override void _Ready() {
        fire_rate = 1 / bps;
    }

    public override void _Process(double delta) {
        if ((Input.IsActionPressed("space") || Input.IsActionPressed("click")) && time_until_fire > fire_rate) {
            RigidBody2D acid = acid_scene.Instantiate<RigidBody2D>();

            acid.Rotation = GlobalRotation;
            acid.GlobalPosition = GlobalPosition;
            acid.LinearVelocity = acid.Transform.X * acid_speed;

            GetTree().Root.AddChild(acid);

            time_until_fire = 0f;
        } else {
            time_until_fire += (float)delta;
        }
    }
}
