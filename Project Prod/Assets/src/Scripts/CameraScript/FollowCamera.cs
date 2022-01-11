using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{   
    public GameObject target;
    Vector3 offset;
    private float rotationSpeed = 5.0f;

    [Range(0.01f, 1.0f)]
    [SerializeField]
    float smooth;

    private float horizontal;


    
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 2, -7);
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 cameraPosition = target.transform.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, cameraPosition, smooth);
        horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
        target.transform.Rotate(0, horizontal, 0);
        float desiredAngle = target.transform.eulerAngles.y; 
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * offset);
        
        transform.position = cameraPosition;
        //transform.LookAt(target.transform);
    }
     
}
