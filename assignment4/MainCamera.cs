using Godot;
using System;



public class MainCamera : ARVRCamera
{
	private float ticksPerFrame = 550.0f;
	// private string b = "text";
	private Vector2 fpsTranslation;

	private Vector3 applyTransaltion(float deltaT)
	{

		CSGSphere player = (CSGSphere)GetNode("../../Player");
		Vector3 delta = new Vector3(0.0f, 0.0f, 0.0f);
		if (Input.IsKeyPressed((int)Godot.KeyList.Right))
		{
			delta.x = this.fpsTranslation.x;
		}
		else if (Input.IsKeyPressed((int)Godot.KeyList.Left))
		{
			delta.x = -this.fpsTranslation.x;
		}
		else if ((Input.IsKeyPressed((int)Godot.KeyList.Up)) && (Input.IsKeyPressed((int)Godot.KeyList.Shift)))
		{
			delta.z = -this.fpsTranslation.y;
		}
		else if ((Input.IsKeyPressed((int)Godot.KeyList.Down)) && (Input.IsKeyPressed((int)Godot.KeyList.Shift)))
		{
			delta.z = this.fpsTranslation.y;
		}
		else if (Input.IsKeyPressed((int)Godot.KeyList.Up))
		{
			delta.y = this.fpsTranslation.y;
		}
		else if (Input.IsKeyPressed((int)Godot.KeyList.Down))
		{
			delta.y = -this.fpsTranslation.y;
		}
      
		
			Transform t = this.GetTransform();
			Transform t2 = player.GetTransform();
			if (delta != Vector3.Zero)
			{
				t.Translated((delta * deltaT));
				t2.Translated((delta * deltaT));
				player.SetTransform(t2);
				this.SetTransform(t);
			}
		
		return delta;
	}
	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	  public override void _Process(float delta)
	  {
		  this.applyTransaltion(delta);
	  }

	  public override void _Ready()
    {
        Vector2 screenSize = this.GetViewport().GetVisibleRect().Size;
        this.fpsTranslation = screenSize / this.ticksPerFrame;
   	}
}
