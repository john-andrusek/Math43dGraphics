using Godot;
using System;

public class ScenarioPicker : MenuButton
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        PopupMenu aPop = this.GetPopup();
        aPop.AddItem("None");
        aPop.AddItem("Translate texture to the right");
		aPop.AddItem("Translate texture to the left");
		aPop.AddItem("Translate texture up");
		aPop.AddItem("Translate texture down");
		aPop.AddItem("Scale texture bigger");
		aPop.AddItem("Scale texture smaller");
		aPop.AddItem("Rotate texture clockwise");
		aPop.AddItem("Rotate texture counter clockwise");
        aPop.Connect("id_pressed", this, "onItemPressed");
    }

	public void onItemPressed(string id) {
       Cube aCube = (Cube) this.GetNode("../Spatial/Cube");
       aCube.updateDisplayParams(id);

    }


}
