using Godot;
using System;

public class MeshInstance : Godot.MeshInstance
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private int spin;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.spin = 1;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    this.RotateX(Mathf.Deg2Rad(this.spin));
    this.RotateY(Mathf.Deg2Rad(this.spin));
    this.RotateZ(Mathf.Deg2Rad(this.spin));
   // this.spin += 1;   
  }
}
