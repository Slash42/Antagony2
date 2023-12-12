using Godot;
using System;

public partial class ant_acid : RigidBody2D
{
    [Export] public float dmg = 10;
    public override void _Ready() {
        Timer timer = GetNode<Timer>("Timer");
        timer.Timeout += () => QueueFree();
    }

    public void OnBodyEntered(Node2D body) {
        if (body.IsInGroup("enemy")) {
            body.GetNode<Health>("Health").Dmg(dmg);
        }
        QueueFree();
    }
}
