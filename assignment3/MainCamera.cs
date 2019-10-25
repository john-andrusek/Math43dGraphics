using Godot;
using System;

public class MainCamera : ARVRCamera
{
    public Vector3 near1, near2, near3, near4, far1, far2, far3, far4;  
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.calcPoints();
    }


  private void calcPoints() {
	     Godot.Collections.Array fruc = this.GetFrustum();
		 Plane nearPlane = (Plane)fruc[0]; 
		 Plane farPlane = (Plane)fruc[1]; 
		 Plane leftPlane = (Plane)fruc[2];
		 Plane topPlane = (Plane)fruc[3];
		 Plane rightPlane = (Plane)fruc[4];
		 Plane bottomPlane = (Plane)fruc[5];
		 this.near1 = nearPlane.Intersect3(bottomPlane,leftPlane);
		 this.near2 = nearPlane.Intersect3(leftPlane,topPlane);
		 this.near3 = nearPlane.Intersect3(rightPlane,topPlane);
		 this.near4 = nearPlane.Intersect3(rightPlane,bottomPlane);
		 this.far1 = farPlane.Intersect3(bottomPlane,leftPlane);
		 this.far2 = farPlane.Intersect3(leftPlane,topPlane);
		 this.far3 = farPlane.Intersect3(rightPlane,topPlane);
		 this.far4 = farPlane.Intersect3(rightPlane,bottomPlane);

  }	

	private void _on_VisibilityNotifier_camera_entered(object camera)
	{
		//(camera);
	}

  public override void _Process(float delta)
  {
	    this.calcPoints();
  }
}

