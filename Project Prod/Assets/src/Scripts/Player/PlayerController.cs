using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    [SerializeField]
    private Transform playerCamera;




    private Rigidbody RB;
    float rotationSmootTime = 0.1f;
    float rotationSmoothVelocity;
    float speed = 5;
    float horizontal;
    float vertical;
    Vector3 direction;
    float targetAngle;
    float angle;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        direction = new Vector3(horizontal, 0f, vertical);

        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;

        angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSmoothVelocity, rotationSmootTime);

        Debug.Log("targetAngle " + targetAngle + " angle " + angle);
    }
    private void FixedUpdate()
    {

        if (direction.magnitude > 0.1f)
        {
           


            RB.MoveRotation(Quaternion.Euler(0f, targetAngle, 0f));
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; 
            RB.MovePosition(transform.position + moveDir.normalized * speed * Time.fixedDeltaTime);

        }
            
        
    }
}