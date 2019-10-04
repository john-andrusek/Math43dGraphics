using Godot;
using System;

public class Cube : MeshInstance
{
    // Declare member variables here. Examples:
 private float ticksPerRotation = 360 / 50.0f;


    private bool shouldInvert = false;
    private float test = 0.01f;
    private Vector3 currentPos = new Vector3(0.0f,0.0f,0.0f);

    private Vector3 currentRot = new Vector3(0.0f,0.0f,0.0f);

    private float ticksPerFrame = 150.0f;
    private float rotationSpeed = 30.0f; //degrees per tick

    private float xScale = 1.0f;

    private float yScale = 1.0f;

    private float zScale = 1.0f;

    private string updateParam = "11";

    private Vector2 fpsTranslation;
   
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Vector2 screenSize = this.GetViewport().GetVisibleRect().Size;
        this.fpsTranslation = screenSize / this.ticksPerFrame;
    }

    public void updateDisplayParams(string id) {
       this.updateParam = id;
    }

  
    private float adjustScaleFactor(float val) {
        if (val >= 50) {
            return  1.0f;
        }
        return  val;
    }

    


    private void applyRotation(float deltaT) {

        if (Input.IsKeyPressed((int)Godot.KeyList.X)) {
            this.currentRot.x += this.rotationSpeed * deltaT;
        } else if (Input.IsKeyPressed((int)Godot.KeyList.Y)) {
            this.currentRot.y += this.rotationSpeed * deltaT;
        } else if (Input.IsKeyPressed((int)Godot.KeyList.Z)) {
            this.currentRot.z += this.rotationSpeed * deltaT;
        }     
        
        Transform t = this.GetTransform();
        t = t.Rotated(new Vector3(1.0f, 0.0f,0.0f), Mathf.Deg2Rad(this.currentRot.x));
        t = t.Rotated(new Vector3(0.0f,1.0f,0.0f), Mathf.Deg2Rad(this.currentRot.y));
        t = t.Rotated(new Vector3(0.0f,0.0f,1.0f), Mathf.Deg2Rad(this.currentRot.z));
        this.SetTransform(t);
   }

   private void applyParentRotation(float deltaT) {
        this.currentRot.x += this.rotationSpeed * deltaT;
        this.currentRot.y += this.rotationSpeed * deltaT;
        this.currentRot.z += this.rotationSpeed * deltaT;
        

        Transform t = ((Spatial)this.GetParent()).GetTransform();
        t = t.Rotated(new Vector3(1.0f, 0.0f,0.0f), Mathf.Deg2Rad(this.currentRot.x));
        t = t.Rotated(new Vector3(0.0f,1.0f,0.0f), Mathf.Deg2Rad(this.currentRot.y));
        t = t.Rotated(new Vector3(0.0f,0.0f,1.0f), Mathf.Deg2Rad(this.currentRot.z));
        ((Spatial)this.GetParent()).SetTransform(t);
   }

   private Vector3 applyTransaltion(float deltaT) {
        Vector3 delta = new Vector3(0.0f,0.0f,0.0f);
        if (Input.IsKeyPressed((int)Godot.KeyList.Right)) {
            delta.x = this.fpsTranslation.x;
        } else if (Input.IsKeyPressed((int)Godot.KeyList.Left)) {
            delta.x = -this.fpsTranslation.x;
        } else if ((Input.IsKeyPressed((int)Godot.KeyList.Up)) && (Input.IsKeyPressed((int)Godot.KeyList.Shift))) {
            delta.z = this.fpsTranslation.y;
        } else if ((Input.IsKeyPressed((int)Godot.KeyList.Down)) && (Input.IsKeyPressed((int)Godot.KeyList.Shift))) {
            delta.z = -this.fpsTranslation.y;
        } else if (Input.IsKeyPressed((int)Godot.KeyList.Up)) {
            delta.y = this.fpsTranslation.y;
        } else if (Input.IsKeyPressed((int)Godot.KeyList.Down)) {
            delta.y = -this.fpsTranslation.y;
        } 
        
        Transform t = this.GetTransform();   
        this.currentPos.x += delta.x * deltaT;
        this.currentPos.y += delta.y * deltaT;
        this.currentPos.z += delta.z * deltaT;
        t.Translated(this.currentPos);
      
        this.SetTransform(t);
        return delta;
   }

    private Vector3 applyParentTransaltion(float deltaT) {
        Vector3 delta = new Vector3(0.0f,0.0f,0.0f);
        if (Input.IsKeyPressed((int)Godot.KeyList.Right)) {
            delta.x = this.fpsTranslation.x;
        } else if (Input.IsKeyPressed((int)Godot.KeyList.Left)) {
            delta.x = -this.fpsTranslation.x;
        } else if ((Input.IsKeyPressed((int)Godot.KeyList.Up)) && (Input.IsKeyPressed((int)Godot.KeyList.Shift))) {
            delta.z = this.fpsTranslation.y;
        } else if ((Input.IsKeyPressed((int)Godot.KeyList.Down)) && (Input.IsKeyPressed((int)Godot.KeyList.Shift))) {
            delta.z = -this.fpsTranslation.y;
        } else if (Input.IsKeyPressed((int)Godot.KeyList.Up)) {
            delta.y = this.fpsTranslation.y;
        } else if (Input.IsKeyPressed((int)Godot.KeyList.Down)) {
            delta.y = -this.fpsTranslation.y;
        } 
        
        Transform t = ((Spatial)this.GetParent()).GetTransform();
        this.currentPos.x += delta.x * deltaT;
        this.currentPos.y += delta.y * deltaT;
        this.currentPos.z += delta.z * deltaT;
        t.Translated(this.currentPos);
      
        ((Spatial)this.GetParent()).SetTransform(t);
        return delta;
   }

    private void resetVSlider(string name) {
        Slider aSlider = (VSlider) this.GetNode("../../Control/" + name);
        aSlider.SetValue(50);
    }

    private void resetRotation() {
       this.resetVSlider("rotationSlider");
       this.resetVSlider("yRotatonSlider");
       this.resetVSlider("zRotationSlider");
    
    }

    private void resetScale() {
        CheckBox xcb = (CheckBox) this.GetNode("../../Control/xScaleCB");
        CheckBox ycb = (CheckBox) this.GetNode("../../Control/yScaleCB");
        CheckBox zcb = (CheckBox) this.GetNode("../../Control/zScaleCB");
        xcb.Pressed = false;
        ycb.Pressed = false;
        zcb.Pressed = false;
        
        this.xScale = 1.0f;
        this.yScale = 1.0f;
        this.zScale = 1.0f;
    }
    public void _on_resetButton_pressed() {
        this.resetRotation();
        this.currentRot = new Vector3(0,0,0);
        this.currentPos = new Vector3(0,0,0);
        this.resetScale();
    }
    private void applyScale() {
        Transform t = this.GetTransform();
        t = t.Scaled(new Vector3(this.xScale,this.yScale,this.zScale));
        this.SetTransform(t);
    }

   private void applyParentScale() {
         Transform t = ((Spatial)this.GetParent()).GetTransform();
        t = t.Scaled(new Vector3(this.xScale,this.yScale,this.zScale));
        ((Spatial)this.GetParent()).SetTransform(t);
    }

    public void _on_HSlider_value_changed() {
        
    }

    public void _on_invertTransform_toggled(bool val) {
       this.shouldInvert = val;
    }

   // Called every frame. 'delta' is the elapsed time since the previous frame.
   public override void _Process(float delta)
   {
       this.SetTransform(Transform.Identity);
       ((Spatial)this.GetParent()).SetTransform(Transform.Identity);
        ShaderMaterial spm = (ShaderMaterial)(this.GetSurfaceMaterial(0));
       test += 0.01f;
       var identityTransform = Transform2D.Identity;
       identityTransform.x.x = test;
       Vector2 a  = new Vector2(test,0);
       spm.SetShaderParam("myVec", a);
       spm.SetShaderParam("shouldInvert", this.shouldInvert);
       this.applyScale();
        this.applyRotation(delta);
        this.applyParentTransaltion(delta);
   }   
}
