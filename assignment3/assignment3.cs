using Godot;
using System;

public class assignment3 : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
	ARVRCamera[] cameras = new  ARVRCamera[5];

	AABB[] boxes = new AABB[5];

	public Vector3[] positions = new Vector3[5];

	theCylinder[] cylinders = new theCylinder[5];

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
			VisibilityNotifier aBX = (VisibilityNotifier) node.GetNode("./theCylinder/VisibilityNotifier");
			aBX.SetTranslation(newPos);
			
			boxes[i] = aBX.GetAabb();
			aNode.setWorld(this);
			AddChild(node);
			this.cylinders[i] = aNode;
			positions[i] = newPos;
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

		for(int i=0; i < 1;i++) {
			ARVRCamera aCam = (ARVRCamera)(this.cameras[i]);
			Vector3 pos = aCam.GetTranslation();
			Transform t = this.cameras[i].GetTransform();  
			
			
			if (delta != Vector3.Zero) {
				t.Translated((delta * deltaT));
				aCam.SetTransform(t);
			}	
		}
		return delta;
   }
	int[] computeALLMostPositivePoints(AABB abox) {
		Godot.Collections.Array planes = (Godot.Collections.Array)(this.cameras[0].GetFrustum());
		int[] indexOfMostPositivePoint = new int[6];
		for (int index = 0; index < 6; index++) {		
			 indexOfMostPositivePoint [index] =
		     computeIndexOfMostPositivePoint (abox, (Plane)planes[index]);	
		}
		return indexOfMostPositivePoint;
	}

	public bool isPointInside(int boxIndex, int pointInd,Godot.Collections.Array planes) {
			AABB box = this.boxes[boxIndex];
	 		for (int planeIndex=0;planeIndex < 6;planeIndex++){
				 Plane plane = (Plane)planes [planeIndex];
				 Vector3 aPoint = box.GetEndpoint(pointInd) + positions[boxIndex];
				if (plane.DistanceTo(aPoint) >= 0) {
					return false;
				}
			}
			return true;
	}
	bool isInside (int boxIndex, int[] indexOfMostPositivePoint) {
	 	AABB box = this.boxes[boxIndex];
	 	Godot.Collections.Array planes = (Godot.Collections.Array)(this.cameras[0].GetFrustum());
		
		for (int pointInd=0;pointInd < 8;pointInd++) {
			if (this.isPointInside(boxIndex, pointInd, planes)) {
				return true;
			}
		}	
		return false;
	}

	
	int computeIndexOfMostPositivePoint (AABB anyBox, Plane plane) {
			float distance = plane.DistanceTo(anyBox.GetEndpoint(0));
			int indexOfMostPositive = 0; //So far…
			for (int index = 1; index < 8; index++) {		
				float d = plane.DistanceTo (anyBox.GetEndpoint (index));
				if (d > distance) {
					distance = d; indexOfMostPositive = index;
				}
			}
		return indexOfMostPositive ; 
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	 this.applyTransaltion(delta);
	 int[] points = this.computeALLMostPositivePoints(this.boxes[0]);
	 for (int i=0;i<5;i++) {
		 cylinders[i].isVisible = this.isInside(i, points);
	 }

  }
}

