using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContorller : MonoBehaviour
{
    public Transform anchor;
    public int cameraSize;

    public static CameraContorller instance;
    public static Camera mainCamera;

    //The speed with which the camera follows its anchor
    public float lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //Making sure ONLY one camera is alive
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(anchor != null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(anchor.position.x, anchor.position.y, transform.position.z), lerpSpeed);
        }
        mainCamera.orthographicSize = cameraSize;
    }
}
