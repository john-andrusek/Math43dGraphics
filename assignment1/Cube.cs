using Godot;
using System;



public class Cube : Navigation
{
    private float ticksPerRotation = 360 / 50.0f;

    private Vector3 currentPos = new Vector3(0.0f,0.0f,0.0f);

    private Vector3 currentRot = new Vector3(0.0f,0.0f,0.0f);

    private float ticksPerFrame = 150.0f;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f; 
    private float zRotation = 0.0f;

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

    public void _on_rotationSlider_value_changed(float value) {
      this.xRotation = (value - 50) * this.ticksPerRotation;
    }

    public void _on_yRotatonSlider_value_changed(float value) {
        this.yRotation = (value - 50) * this.ticksPerRotation;
    }

    private float adjustScaleFactor(float val) {
        if (val >= 50) {
            return  1.0f;
        }
        return  val;
    }

    public void _on_yScaleCB_toggled(bool val) {
        if (val) {
            this.yScale = 2.0f;
        } else {
            this.yScale = 1.0f;
        }

       
    }

    public void _on_zScaleCB_toggled(bool val) {
        if (val) {
            this.zScale = 2.0f;
        } else {
            this.zScale = 1.0f;
        }

       
    }

    public void _on_xScaleCB_toggled(bool val) {
        if (val) {
            this.xScale = 2.0f;
        } else {
            this.xScale = 1.0f;
        }

       
    }

    public void _on_XScaleSlider_value_changed(float value) {
        this.xScale = this.adjustScaleFactor(value);
    }

    public void _on_zRotationSlider_value_changed(float value) {
        this.zRotation = (value - 50) * this.ticksPerRotation;
    }

   private void applyRotation(float deltaT) {
        this.currentRot.x += this.xRotation * deltaT;
        this.currentRot.y += this.yRotation * deltaT;
        this.currentRot.z += this.zRotation * deltaT;
        

        Transform t = this.GetTransform();
        t = t.Rotated(new Vector3(1.0f, 0.0f,0.0f), Mathf.Deg2Rad(this.currentRot.x));
        t = t.Rotated(new Vector3(0.0f,1.0f,0.0f), Mathf.Deg2Rad(this.currentRot.y));
        t = t.Rotated(new Vector3(0.0f,0.0f,1.0f), Mathf.Deg2Rad(this.currentRot.z));
        this.SetTransform(t);
   }

   private void applyParentRotation(float deltaT) {
        this.currentRot.x += this.xRotation * deltaT;
        this.currentRot.y += this.yRotation * deltaT;
        this.currentRot.z += this.zRotation * deltaT;
        

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

   // Called every frame. 'delta' is the elapsed time since the previous frame.
   public override void _Process(float delta)
   {
       this.SetTransform(Transform.Identity);
       ((Spatial)this.GetParent()).SetTransform(Transform.Identity);

        switch (this.updateParam)
        {
          case "0":
             break;
          case "1":
            this.applyRotation(delta);
            break;   
          case "2":
            this.applyTransaltion(delta);
            break;   
          case "3":
            this.applyScale();
            break;   
          case "4":
            this.applyTransaltion(delta);
            this.applyRotation(delta);
            break;   
          case "5":
            this.applyRotation(delta);
            this.applyParentTransaltion(delta);
            break;   
          case "6":
            this.applyTransaltion(delta);
            this.applyRotation(delta);
            this.applyScale();
            break; 
          case "7":
            this.applyTransaltion(delta);
            this.applyScale();
            this.applyParentRotation(delta);
            break; 
          case "8":
            this.applyRotation(delta);
            this.applyParentTransaltion(delta);
            this.applyScale();
            break;   
          case "9":
            this.applyRotation(delta);
            this.applyScale();
            this.applyParentTransaltion(delta);
            break;
          case "10":
            this.applyScale();
            this.applyRotation(delta);
            this.applyParentTransaltion(delta);
            break;
          case "11":
            this.applyScale();
            this.applyTransaltion(delta);
            this.applyRotation(delta);
            break;  
        }    
   }
}
