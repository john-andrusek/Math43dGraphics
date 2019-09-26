using Godot;
using System;

public class ARVRCamera : Godot.ARVRCamera
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
     private Vector3 camMo;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        camMo.x=-0.1f;
        camMo.y=0.1f;
        camMo.z=0.1f;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    
     this.Translate(camMo);
     //camMo.z += 5;
  }
}
