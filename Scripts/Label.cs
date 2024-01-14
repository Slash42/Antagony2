using Godot;
using System;

public partial class Label : Godot.Label
{
    int x = 0;
    public void IncreaseCounter() {
        x++;
        Text = "Spiders killed: " + x + "/100";

        if (x >= 100) {
            Text = "Glückwunsch!";
        }
    }
}
