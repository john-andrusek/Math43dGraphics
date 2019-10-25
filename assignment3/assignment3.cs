using Godot;
using System;

public class assignment3 : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
	ARVRCamera[] cameras = new  ARVRCamera[5];
    private float ticksPerFrame = 550.0f;
	public string currentCameraId = "0";
    
	// private string b = "text";
	private Vector2 fpsTranslation;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Vector2 screenSize = this.GetViewport().GetVisibleRect().Size;
        this.fpsTranslation = screenSize / this.ticksPerFrame;
   		this.cameras[0] = (ARVRCamera) GetNode("./origin/MainCamera");
		this.cameras[1] = (ARVRCamera) GetNode("./origin/backCamera");
		this.cameras[2] = (ARVRCamera) GetNode("./origin/rightCamera");
		this.cameras[3] = (ARVRCamera) GetNode("./origin/leftCamera");
		this.cameras[4] = (ARVRCamera) GetNode("./origin/topCamera");
		this.createCylinders();
	}

	private void createCylinders() {
		RandomNumberGenerator aGen = new RandomNumberGenerator();
		aGen.Randomize();
		for (int i=0;i < 5;i++) {
			Vector3 newPos = new Vector3(0,0,0);
			newPos.x = aGen.RandfRange(-3.0f,3.0f);
			newPos.y = aGen.RandfRange(-3.0f,3.0f);
			newPos.z = aGen.RandfRange(-3.0f,3.0f);
			PackedScene scene = GD.Load<PackedScene>("res://CylinderVis.tscn"); 
			Spatial node = (Spatial)scene.Instance();
			
			node.SetTranslation(newPos);
			theCylinder aNode = (theCylinder)node.GetNode("./theCylinder");
			aNode.setWorld(this);
			AddChild(node);

		}
	}	

	public void updateCamera(string id) {
		ARVRCamera camLeft = (ARVRCamera) GetNode("./origin/leftCamera");
		ARVRCamera camRight = (ARVRCamera) GetNode("./origin/rightCamera");
		ARVRCamera camBack = (ARVRCamera) GetNode("./origin/backCamera");
		ARVRCamera camTop = (ARVRCamera) GetNode("./origin/topCamera");
		ARVRCamera camMain = (ARVRCamera) GetNode("./origin/MainCamera");
		this.currentCameraId = id;
		this.cameras[3].Current = id == "3";
		this.cameras[2].Current = id == "2";
		this.cameras[4].Current = id == "4";
		this.cameras[0].Current = id == "0";
		this.cameras[1].Current = id == "1";
		

	}

private Vector3 applyTransaltion(float deltaT) {
		
		Vector3 delta = new Vector3(0.0f,0.0f,0.0f);
        if (Input.IsKeyPressed((int)Godot.KeyList.Right)) {
            delta.x = this.fpsTranslation.x;
        } else if (Input.IsKeyPressed((int)Godot.KeyList.Left)) {
            delta.x = -this.fpsTranslation.x;
        } else if ((Input.IsKeyPressed((int)Godot.KeyList.Up)) && (Input.IsKeyPressed((int)Godot.KeyList.Shift))) {
            delta.z = -this.fpsTranslation.y;
        } else if ((Input.IsKeyPressed((int)Godot.KeyList.Down)) && (Input.IsKeyPressed((int)Godot.KeyList.Shift))) {
            delta.z = this.fpsTranslation.y;
        } else if (Input.IsKeyPressed((int)Godot.KeyList.Up)) {
            delta.y = this.fpsTranslation.y;
        } else if (Input.IsKeyPressed((int)Godot.KeyList.Down)) {
            delta.y = -this.fpsTranslation.y;
        } 
        ARVRCamera aCam = (ARVRCamera)(this.cameras[int.Parse(this.currentCameraId)]);
		Vector3 pos = aCam.GetTranslation();
        Transform t = this.cameras[int.Parse(this.currentCameraId)].GetTransform();  
		
		if (delta != Vector3.Zero) {
			t.Translated((delta * deltaT));
			aCam.SetTransform(t);
        }
		return delta;
   }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	 this.applyTransaltion(delta);
  }
}

