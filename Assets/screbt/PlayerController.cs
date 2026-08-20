using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 10f;
    public float sideSpeed = 8f;
    public float jumpForce = 8f;

    [Header("UI Settings")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI speedText;

    private Rigidbody rb;
    [SerializeField] private Animator anim;

    public bool isGrounded = true;
    private bool canMove = true;

    private float speedBoostTimer = 0f;
    private float totalTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!canMove) return;

        float dt = Time.deltaTime;
        speedBoostTimer += dt;
        totalTime += dt;

        if (speedBoostTimer >= 5f)
        {
            forwardSpeed += 2f;
            speedBoostTimer = 0f;
        }

        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.FloorToInt(totalTime).ToString() + "s";
        }

        if (speedText != null)
        {
            speedText.text = "Speed: " + forwardSpeed.ToString();
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            anim.SetTrigger("Jump");
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        Vector3 forwardMove = transform.forward * forwardSpeed * Time.fixedDeltaTime;
        float sideInput = Input.GetAxis("Horizontal");
        Vector3 sideMove = transform.right * sideInput * sideSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + forwardMove + sideMove);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Track"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Track"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Track"))
        {
            isGrounded = false;
        }
    }

    public void StopPlayer()
    {
        canMove = false;
        rb.isKinematic = true;
    }

    public void Die()
    {
        StopPlayer();

        if (anim != null)
        {
            anim.SetTrigger("isDead");
        }
        GameManager.Instance.ShowLose();
    }
}