using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUIController : MonoBehaviour
{
    [SerializeField]
    private Canvas worldCanvas;
    void Start()
    {
        foreach(Camera camera in Camera.allCameras)
        {
            if(camera.name == "WorldCamera")
            {
                worldCanvas.worldCamera = camera;
                return;
            }
        }
        
    }
}
