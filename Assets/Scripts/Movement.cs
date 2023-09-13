using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] float thrustForce;
    [SerializeField] float rotateForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(thrustForce * Time.deltaTime * Vector3.up);
        }        
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateForce);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateForce);
        }
    }

    private void ApplyRotation(float rotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(rotation * Time.deltaTime * Vector3.forward);
        rb.freezeRotation = false;
    }
}
