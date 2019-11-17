using Godot;
using System;

public class world : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	ARVRCamera[] cameras = new  ARVRCamera[2];
	public string currentCameraId = "0";
		
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cameras[1] = (ARVRCamera)GetNode("./origin/SecondCamera");
		cameras[0] = (ARVRCamera)GetNode("./origin/MainCamera");
		
	}
	public void updateCamera(string id)
	{
		this.currentCameraId = id;
		this.cameras[0].Current = id == "0";
		this.cameras[1].Current = id == "1";
	}


	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	  public override void _Process(float delta)
	  {
		 
	  }
}
