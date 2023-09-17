using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioS;

    [SerializeField] float thrustForce;
    [SerializeField] float rotateForce;
    [SerializeField] AudioClip mainEngine;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioS = GetComponent<AudioSource>();
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
            if (!audioS.isPlaying)
            {
                audioS.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audioS.Stop();
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
