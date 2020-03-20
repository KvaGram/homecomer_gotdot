using Godot;
using System;

public class ModuleMap : GridContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ShipLocation loc = new ShipLocation(0,0);
        int i = 0;
        foreach( var n in GetChildren())
        {
            ModuleLocationButton mlb = n as ModuleLocationButton;
            if (mlb != null)
            {
                mlb.Location = ShipLocation.FromIndex(i);
                mlb.Connect("ModuleSelected", this, "onInnerModuleSelected");
                i++;
            }
        }
    }
    public void onInnerModuleSelected(int s, int m){
        GD.Print("Hello " + s + " " + m);
        EmitSignal(nameof(onModuleSelected), s, m);
    }
    [Signal]
    public delegate void onModuleSelected(int s, int m);

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
