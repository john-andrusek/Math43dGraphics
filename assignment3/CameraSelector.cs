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
 		aPop.AddItem("Back");
        aPop.AddItem("Left");
        aPop.AddItem("Right");
		aPop.AddItem("Top");
        aPop.Connect("id_pressed", this, "onItemPressed");   
    }

	public void onItemPressed(string id) {
		assignment3 aWorld = (assignment3) this.GetNode("..");
		aWorld.updateCamera(id);

    }
}
