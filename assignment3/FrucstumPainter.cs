using Godot;
using System;

public class FrucstumPainter : ImmediateGeometry
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

	private MainCamera aCamera;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
		this.aCamera = (MainCamera) GetNode("../origin/MainCamera");
    }

	private void drawFrucstum() {
	  Clear();
	  SetColor(new Color( 1, 1, 0, 1 ));
      Begin(Mesh.PrimitiveType.Lines);
	   	AddVertex(this.aCamera.near1);
		AddVertex(this.aCamera.near2);
		AddVertex(this.aCamera.near2);
		AddVertex(this.aCamera.near3);
		AddVertex(this.aCamera.near3);
		AddVertex(this.aCamera.near4);
		AddVertex(this.aCamera.near4);
		AddVertex(this.aCamera.near1); 
		AddVertex(this.aCamera.far1);
		AddVertex(this.aCamera.far2);
		AddVertex(this.aCamera.far2);
		AddVertex(this.aCamera.far3);
		AddVertex(this.aCamera.far3);
		AddVertex(this.aCamera.far4);
		AddVertex(this.aCamera.far4);
		AddVertex(this.aCamera.far1);
		AddVertex(this.aCamera.near1);
		AddVertex(this.aCamera.far1);
		AddVertex(this.aCamera.near2);
		AddVertex(this.aCamera.far2);
		AddVertex(this.aCamera.near3);
		AddVertex(this.aCamera.far3);
		AddVertex(this.aCamera.near4);
		AddVertex(this.aCamera.far4);
		End();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
  {
	  this.drawFrucstum();
  }
}
