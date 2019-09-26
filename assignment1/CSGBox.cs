using Godot;
using System;

public class CSGBox : Godot.CSGBox
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

 private float ticksPerRotation = 360 / 50.0f;

    private float ticksPerFrame = 150.0f;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f; 
    private float zRotation = 0.0f;

    private float xScale = 1.0f;

    private Vector2 fpsTranslation;
   
   

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

     private Vector3 applyTransaltion(float deltaT) {
        Vector3 delta = new Vector3(0.0f,0.0f,0.0f);
        if (Input.IsKeyPressed((int)Godot.KeyList.Right)) {
            delta.x = this.fpsTranslation.x;
        } else if (Input.IsKeyPressed((int)Godot.KeyList.Left)) {
            delta.x = -this.fpsTranslation.x;
        } else if ((Input.IsKeyPressed((int)Godot.KeyList.Up)) && (Input.IsKeyPressed((int)Godot.KeyList.Shift))) {
            delta.z = this.fpsTranslation.y;
        } else if ((Input.IsKeyPressed((int)Godot.KeyList.Down)) && (Input.IsKeyPressed((int)Godot.KeyList.Shift))) {
            delta.z = -this.fpsTranslation.y;
        } else if (Input.IsKeyPressed((int)Godot.KeyList.Up)) {
            delta.y = this.fpsTranslation.y;
        } else if (Input.IsKeyPressed((int)Godot.KeyList.Down)) {
            delta.y = -this.fpsTranslation.y;
        } 
        //this.Translate(delta * deltaT); 
        //this.GlobalTranslate(delta * deltaT);
        
        //Transform t = this.GetTransform();
        //t.origin += new Vector3(delta * deltaT);
        //this.SetTransform(t);
        return delta;
   }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
     Vector3 deltaT = this.applyTransaltion(delta);
        Basis identityBasis = Basis.Identity;



         Transform t = this.GetTransform();
         //t.basis = identityBasis;
         //t = t.Rotated(new Vector3(1.0f, 0.0f,0.0f), Mathf.Deg2Rad(this.xRotation * delta));
         
         t = t.Translated(deltaT * delta);
       
       this.SetTransform(t);  
  }
}
