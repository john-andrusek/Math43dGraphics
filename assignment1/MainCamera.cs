using Godot;
using System;

public class MainCamera : InterpolatedCamera
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
    public void _on_CameraXSlider_value_changed(float value) {
       
        
    }    
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta2)
  {
        Vector3 delta = new Vector3(0.0f,0.0f,0.0f);
        if (Input.IsKeyPressed((int)Godot.KeyList.Kp4)) {
            delta.x = 0.01f;
        } else if (Input.IsKeyPressed((int)Godot.KeyList.Kp6)) {
            delta.x = -0.01f;
        } else if ((Input.IsKeyPressed((int)Godot.KeyList.Kp8)) && (Input.IsKeyPressed((int)Godot.KeyList.Shift))) {
            delta.z = -0.01f;
        } else if ((Input.IsKeyPressed((int)Godot.KeyList.Kp2)) && (Input.IsKeyPressed((int)Godot.KeyList.Shift))) {
            delta.z = 0.01f;
        } else if (Input.IsKeyPressed((int)Godot.KeyList.Kp8)) {
            delta.y = -0.01f;
        } else if (Input.IsKeyPressed((int)Godot.KeyList.Kp2)) {
            delta.y = 0.01f;
        } 
        this.TranslateObjectLocal(delta);      
  }
}
