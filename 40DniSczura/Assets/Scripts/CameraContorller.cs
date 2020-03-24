using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContorller : MonoBehaviour
{
    public Transform anchor;
    public float cameraSize;

    public static CameraContorller instance;
    public static Camera mainCamera;

    //The speed with which the camera follows its anchor
    public float lerpSpeed;

    public bool testing;

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
        DontDestroyOnLoad(gameObject);
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

        if(testing)
        {
            if (Input.GetKey("i"))
            {
                cameraSize += 0.2f;
            }
            if (Input.GetKey("u"))
            {
                cameraSize -= 0.2f;
            }
            if (Input.GetKeyDown("o"))
            {
                cameraSize = 5f;
            }
        }  
    }
}
