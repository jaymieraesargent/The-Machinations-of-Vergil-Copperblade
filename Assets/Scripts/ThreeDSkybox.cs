using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDSkybox : MonoBehaviour
{
    public GameObject Skyboxcamera;
    public GameObject Playercamera;
    public float Scale;
    private void Update()
    {
        if (Playercamera != null)
        {
            Skyboxcamera.transform.localPosition = new Vector3(Playercamera.transform.position.x / Scale, Playercamera.transform.position.y / Scale, Playercamera.transform.position.z / Scale);
            Skyboxcamera.transform.rotation = Playercamera.transform.rotation;
        }
        else if(Camera.main==true)
        {
            Playercamera = Camera.main.gameObject;
            Camera.main.clearFlags = CameraClearFlags.Depth;
        }
    }
}
