using Godot;
using System;

public class CameraSelector : MenuButton
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
     PopupMenu aPop = this.GetPopup();
        aPop.AddItem("Main");
 		aPop.AddItem("Top");
        aPop.Connect("id_pressed", this, "onItemPressed");   
    }

	public void onItemPressed(string id) {
		world aWorld = (world) this.GetNode("..");
		aWorld.updateCamera(id);

    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}