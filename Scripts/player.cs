using Godot;

public partial class player : CharacterBody2D
{
    [Export] public float speed = 300f;

    public override void _PhysicsProcess(double delta) {

        LookAt(GetGlobalMousePosition());

        Vector2 move_input = Input.GetVector("left", "right", "up", "down");

        Velocity = move_input * speed;

        global::System.Object value = MoveAndSlide();

    }
}
