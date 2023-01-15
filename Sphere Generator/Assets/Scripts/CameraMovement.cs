using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera camera;
    public GameObject parent;

    public float zoomSensitivity = 20.0f;
    public float zoomSpeed = 20.0f;
    public float zoomMin = 5.0f;
    publci float zoomMax = 80.0f;

    privat float zoom;

    // Start is called before the first frame update
    void Start()
    {
            zoom = camera.fieldOfView;
            camera.transform.LookAt(parent.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            float h = 5 * Input.GetAxis("Mouse y");
            float y = (5 * Input.GetAxis("Mouse X")) * -1;
        }
    }
}
