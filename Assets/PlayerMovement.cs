using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float sideSpeed = 10f;
    public float jumpForce = 5f;

    public Light winLight;

    private Rigidbody rb;
    private bool isGrounded = true;
    private bool isGameActive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (winLight != null)
        {
            winLight.enabled = false;
        }
    }

    void Update()
    {
       
        if (!isGameActive) return;

        
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 sideMovement = new Vector3(horizontalInput * sideSpeed * Time.deltaTime, rb.linearVelocity.y, rb.linearVelocity.z);
        transform.Translate(sideMovement);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {

        if (!isGameActive) return;
        Vector3 forwardMovement = Vector3.forward * forwardSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMovement);

    
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "track")
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("obstacle"))
        {
            isGameActive = false;
            rb.linearVelocity = Vector3.zero;
            Debug.Log("Game Over! خبطت في حاجز");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            isGameActive = false;
            rb.linearVelocity = Vector3.zero;

            if (winLight != null)
            {
                winLight.enabled = true;
            }

            Debug.Log("You Win! وصلت لخط النهاية");
        }
    }
}