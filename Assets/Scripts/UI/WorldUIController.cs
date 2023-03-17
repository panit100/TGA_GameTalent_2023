using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUIController : MonoBehaviour
{
    [SerializeField]
    private Canvas worldCanvas;
    [SerializeField]
    private bool isFollowObject;
    [SerializeField]
    private GameObject followedObject;

    private Vector3 offset;
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
        offset = followedObject.transform.position - worldCanvas.transform.position;
    }
    private void Update()
    {
        if(isFollowObject)
        {
            worldCanvas.transform.position = followedObject.transform.position - offset;
        }
    }
}
