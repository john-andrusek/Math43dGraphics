using Godot;
using System;

public class UpdateOrderMenuButton : MenuButton
{
    private string selectedIndex;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        PopupMenu aPop = this.GetPopup();
        aPop.AddItem("None");    
        aPop.AddItem("Rotate");
        aPop.AddItem("Translate");
        aPop.AddItem("Scale");
        aPop.AddItem("Translate Rotate");
        aPop.AddItem("Rotate Translate");
        aPop.AddItem("Translate Rotate Scale");
        aPop.AddItem("Translate Scale Rotate");
        aPop.AddItem("Rotate Translate  Scale");
        aPop.AddItem("Rotate Scale Translate");
        aPop.AddItem("Scale Rotate Translate");
        aPop.AddItem("Scale Translate Rotate");
        aPop.Connect("id_pressed", this, "onItemPressed");
    }

    public void onItemPressed(string id) {
       this.selectedIndex = id;
       GD.Print("about to");
       Cube aCube = (Cube) this.GetNode("../../Spatial/Cube");
       GD.Print(aCube);
       aCube.updateDisplayParams(id);

    }
}
