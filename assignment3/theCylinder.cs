using Godot;
using System;

public class theCylinder : CSGCylinder
{
	assignment3 aWorld;

	public bool isVisible = false;

	public void setWorld(assignment3 aWorld) {
		this.aWorld = aWorld;
	}

	private void _on_VisibilityNotifier_camera_entered(object camera)
	{
		//ARVRCamera aCam = (ARVRCamera)camera;
		//if ((camera.GetType().ToString() == "MainCamera") && (aWorld.currentCameraId == "0")) {
	//		this.isVisible = true;
	//	}	
			
	}


	private void _on_VisibilityNotifier_camera_exited(object camera)
	{
		//ARVRCamera aCam = (ARVRCamera)camera;
		//if ((camera.GetType().ToString() == "MainCamera") && (aWorld.currentCameraId == "0")) {
	//		this.isVisible = false;
	//	}
	}


    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	  	if (this.isVisible) {
			 SpatialMaterial aMat = new SpatialMaterial();
			 aMat.SetAlbedo(new Color(0.92549f, 0.164706f, 0.086275f));
			 this.SetMaterial(aMat);
		} else {
			SpatialMaterial aMat = new SpatialMaterial();
			 aMat.SetAlbedo(new Color(0.086275f, 0.099387f, 0.92549f));
			 this.SetMaterial(aMat);
		}	     
  }
}


