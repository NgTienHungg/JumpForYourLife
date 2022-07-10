using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float targetPointY = 2.5f;
    [SerializeField] private float timeOnPlatform = 0.03f;

    private GameObject currentPlatform;
    private float jumpingDistance;
    private float onPlatformTimer = 0f;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !GameManager.IsPointerOverUIObject() && OnPlatform() && !currentPlatform.GetComponent<PlatformMovement>().IsMovingUp)
            Jump();
    }

    private void Jump()
    {
        //AudioManager.instance.PlaySound("Jump");
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
            transform.parent = collision.gameObject.transform;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            if (onPlatformTimer >= timeOnPlatform)
                return;

            // dem thoi gian dung tren Platform
            onPlatformTimer += Time.deltaTime;
            if (onPlatformTimer >= timeOnPlatform)
            {
                if (collision.gameObject.name != "StartPlatform")
                    AudioManager.instance.PlaySound("OnGround");

                currentPlatform = collision.gameObject;
                jumpingDistance = targetPointY - currentPlatform.GetComponent<PlatformMovement>().PivotY;
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            transform.parent = null;
            currentPlatform = null;
            onPlatformTimer = 0f;

            // tat BoxCollider cua Platform
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

            // tat BoxCollider cua Player -> thua luon
            if (collision.gameObject.name != "StartPlatform" && collision.gameObject.GetComponent<PlatformStatus>().WasBroken())
                boxCollider.isTrigger = true;
        }
    }

    public bool OnPlatform()
    {
        return currentPlatform != null;
    }

    public bool JustJumped()
    {
        return currentPlatform.GetComponent<PlatformMovement>().PivotY < targetPointY;
    }

    public float JumpingDistance()
    {
        return jumpingDistance;
    }
}