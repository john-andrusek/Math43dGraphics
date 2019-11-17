using Godot;
using System;

public class Sprite : CSGBox
{
    

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {	
	  CSGSphere player = (CSGSphere)GetNode("../Player");
	  this.LookAt(player.Transform.origin, new Vector3(0,1,0));	
 		     
  }
}
