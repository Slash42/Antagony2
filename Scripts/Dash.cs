using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Dash : Node2D
{

    Timer duration_timer;
    float dash_delay = 1f;
    public bool can_dash = true;

    public override void _Ready()
    {
        duration_timer = GetNode<Timer>("DurationTimer");
    }
    
    public void StartDash(double duration) {
        duration_timer.Start(duration);
    }

    public bool IsDashing() {
        if (duration_timer.TimeLeft == 0) {
            return false;
        } else {
            return true;
        }
    }

    public async void EndDash() {
        can_dash = false;
        await ToSignal(GetTree().CreateTimer(dash_delay), "timeout");
        can_dash = true; 
    }

}
