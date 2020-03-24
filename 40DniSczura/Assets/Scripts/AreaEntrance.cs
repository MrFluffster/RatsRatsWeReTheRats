using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public string transitionID;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.instance.transitioning && PlayerController.instance.transitionID == transitionID)
        {
            Debug.Log("AAAAAAAAAA");
            //PlayerController.instance.gameObject.SetActive(false);
            PlayerController.instance.transform.position = transform.position;
            //PlayerController.instance.gameObject.SetActive(true);
            CameraContorller.instance.transform.position = new Vector3(transform.position.x, transform.position.y, CameraContorller.instance.transform.position.z);
            PlayerController.instance.transitioning = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
