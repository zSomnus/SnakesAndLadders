using UnityEngine;
using System.Collections;
 
[RequireComponent(typeof(Camera))]
public class ResizeCamera : MonoBehaviour {
 
    public enum CameraView {
        Free = 0,
        Square
    }
 
    [SerializeField]
    CameraView cameraView = CameraView.Square;
    [SerializeField]
    bool center = true;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float scale = 1.0f;
    [SerializeField]
    bool runOnlyOnce = false;
 
    // Internal
    float _cachedHeight = 0.0f;
    float _cachedWidth = 0.0f;
 
    void Start() {
        this.CheckScreenType();
    }
 
    void Update() {
        if(!this.runOnlyOnce) {
            this.CheckScreenType();
        }
    }
 
    void CheckScreenType() {
        switch(cameraView) {
        case CameraView.Square:
            this.SetSquare();
            break;
        case CameraView.Free:
        {
            Camera.main.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        }
            break;
        default:
            break;
        }
    }
 
    /// <summary>
    /// Gets the size of the screen.
    /// </summary>
    void RefreshScreenSize() {
        this._cachedHeight = Screen.height;
        this._cachedWidth = Screen.width;
    }
 
    /// <summary>
    /// Sets the square.
    /// </summary>
    void SetSquare() {
        this.RefreshScreenSize();
        if(this._cachedHeight < this._cachedWidth) {
            float ratio = this._cachedHeight / this._cachedWidth;
 
            Camera.main.rect = new Rect(Camera.main.rect.x, Camera.main.rect.y, ratio, 1.0f);
 
            if(this.center == true) {
                Camera.main.rect = new Rect(((1.0f - ratio * this.scale) / 2), Camera.main.rect.y * this.scale, Camera.main.rect.width * this.scale, Camera.main.rect.height * this.scale);
            }
        } else {
            float ratio = this._cachedWidth / this._cachedHeight;
 
            Camera.main.rect = new Rect(Camera.main.rect.x, Camera.main.rect.y, 1.0f, ratio);
 
            if(this.center == true) {
                Camera.main.rect = new Rect(Camera.main.rect.x, (1.0f - ratio) / 2, Camera.main.rect.width, Camera.main.rect.height);
            }
        }
    }
 
    public void ScrictView(CameraView newCameraView) {
        cameraView = newCameraView;
    }
}
 