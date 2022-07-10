using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [Header("Horizontal Movement")]
    [SerializeField] private float horizontalSpeed = 1.2f;
    public float HorizontalSpeed
    {
        get { return horizontalSpeed; }
        set { horizontalSpeed = value; }
    }

    private bool isMovingRight;
    public bool IsMovingRight
    {
        get { return isMovingRight; }
        set { isMovingRight = value; }
    }

    [Header("Move Up")]
    [SerializeField] private float speedMoveUp = 18f;
    private float targetPositionY;

    private bool isMovingUp = false;
    public bool IsMovingUp
    {
        get { return isMovingUp; }
    }

    private float pivotY;
    public float PivotY
    {
        get { return pivotY; }
        set { pivotY = value; }
    }

    private void Awake()
    {
        PivotY = transform.position.y;
    }

    private void Update()
    {
        int direction = isMovingRight ? 1 : -1;
        transform.Translate(new Vector3(direction, 0, 0) * horizontalSpeed * Time.deltaTime);

        if (isMovingUp)
        {
            transform.Translate(Vector3.up * speedMoveUp * Time.deltaTime);
            if (transform.position.y >= targetPositionY)
            {
                Vector3 position = transform.position;
                transform.position = new Vector3(position.x, targetPositionY, position.z);
                isMovingUp = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            ChangeDirection();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // sau khi 'Player' da nhay khoi 'Platform' va BoxCollider chuyen thanh Trigger
        if (collision.gameObject.tag == "Wall")
            ChangeDirection();
    }

    private void ChangeDirection()
    {
        isMovingRight = !isMovingRight;
    }

    public void MoveUp(float distance)
    {
        isMovingUp = true;
        targetPositionY = transform.position.y + distance;
        pivotY += distance;
    }
}