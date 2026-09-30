using UnityEngine;


/// Responsive Camera Scaler

public class CameraAspectRatioScaler : MonoBehaviour
{

   
    /// Reference Resolution like 1920x1080
    public Vector2 ReferenceResolution;

   
    /// Zoom factor to fit different aspect ratios
    public Vector3 ZoomFactor = Vector3.one;

  
    /// Design time position
    [HideInInspector]
    public Vector3 OriginPosition;

 
    /// Start
    void Start()
    {
        OriginPosition = transform.position;
    }

  
    /// Update per Frame
    void Update()
    {

        if (ReferenceResolution.y == 0 || ReferenceResolution.x == 0)
            return;

        var refRatio = ReferenceResolution.x / ReferenceResolution.y;
        var ratio = (float)Screen.width / (float)Screen.height;

        transform.position = OriginPosition + transform.forward * (1f - refRatio / ratio) * ZoomFactor.z
                                            + transform.right * (1f - refRatio / ratio) * ZoomFactor.x
                                            + transform.up * (1f - refRatio / ratio) * ZoomFactor.y;


    }
}