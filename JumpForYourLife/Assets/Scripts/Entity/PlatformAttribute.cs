using UnityEngine;

public enum EPlatformAttributeType
{
    Normal,
    Cross,
    Ziczac,
    Invisible
}

public class PlatformAttribute : MonoBehaviour
{
    [Header("Cross")]
    [SerializeField] private float crossSpeed = 0.35f;
    private bool isMovingUp;

    [Header("Ziczac")]
    [SerializeField] private float ziczacSpeed = 0.7f;
    [SerializeField] private float rangeZiczacY = 0.3f;
    private PlatformMovement platformMovement;
    private float lowerBoundY, upperBoundY;

    [Header("Invisible")]
    [SerializeField] private float visibleDuration = 2.0f;
    [SerializeField] private float invisibleDuration = 0.6f;
    [SerializeField] private float deltaAlpha = 0.008f;
    private SpriteRenderer sprite;
    private float visibleTimer;
    private bool isFading = true;

    private EPlatformAttributeType type;
    public EPlatformAttributeType Type
    {
        get { return type; }
        set { type = value; }
    }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        platformMovement = GetComponent<PlatformMovement>();
        type = EPlatformAttributeType.Normal;
        isMovingUp = Random.Range(0, 1) == 1 ? true : false;
    }

    private void Start()
    {
        if (platformMovement.HorizontalSpeed <= 2f)
        {
            // dau game horizontalSpeed con nho => giam cac speed khac theo
            crossSpeed *= platformMovement.HorizontalSpeed / 2f; // standard speed = 2
            ziczacSpeed *= platformMovement.HorizontalSpeed / 2f;
        }
    }

    private void Update()
    {
        if (type == EPlatformAttributeType.Cross)
            UpdateCrossPlatform();
        else if (type == EPlatformAttributeType.Ziczac)
            UpdateZiczacPlatform();
        else if (type == EPlatformAttributeType.Invisible)
            UpdateInvisiblePlatform();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            isMovingUp = !isMovingUp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            isMovingUp = !isMovingUp;
    }

    private void UpdateCrossPlatform()
    {
        int direction = (isMovingUp ? 1 : -1);
        transform.Translate(new Vector3(0, direction, 0) * crossSpeed * Time.deltaTime);
    }

    private void UpdateZiczacPlatform()
    {
        upperBoundY = platformMovement.PivotY + rangeZiczacY;
        lowerBoundY = platformMovement.PivotY - rangeZiczacY;

        int direction = (isMovingUp ? 1 : -1);
        transform.Translate(new Vector3(0, direction, 0) * ziczacSpeed * Time.deltaTime);

        if (transform.position.y >= upperBoundY || transform.position.y <= lowerBoundY)
            isMovingUp = !isMovingUp;
    }

    private void UpdateInvisiblePlatform()
    {
        visibleTimer = Mathf.Clamp(visibleTimer - Time.deltaTime, 0f, visibleDuration);

        if (visibleTimer == 0f)
        {
            Color color = sprite.color;

            if (isFading)
            {
                color.a = Mathf.Clamp(color.a - deltaAlpha, 0f, 1f);
                if (color.a == 0f)
                {
                    isFading = false;
                    visibleTimer = invisibleDuration;
                }
            }
            else
            {
                color.a = Mathf.Clamp(color.a + deltaAlpha, 0f, 1f);
                if (color.a == 1f)
                {
                    isFading = true;
                    visibleTimer = visibleDuration;
                }
            }

            sprite.color = color;
        }
    }
}