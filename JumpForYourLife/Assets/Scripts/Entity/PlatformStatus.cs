using UnityEngine;

public enum EPlatformPrefabType
{
    Short,
    Medium,
    Long
}

public class PlatformStatus : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite spriteEntire;
    public Sprite spriteCrack;

    private SpriteRenderer spriteRender;
    private BoxCollider2D boxCollider;
    private int collisionWithWall = 0;
    private bool playerLanded = false;

    private EPlatformPrefabType type;
    public EPlatformPrefabType Type
    {
        get { return type; }
        set { type = value; }
    }

    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRender.sprite = spriteEntire;

        if (gameObject.name == "ShortPlatform(Clone)")
            type = EPlatformPrefabType.Short;
        else if (gameObject.name == "MediumPlatform(Clone)")
            type = EPlatformPrefabType.Medium;
        else if (gameObject.name == "LongPlatform(Clone)")
            type = EPlatformPrefabType.Long;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            playerLanded = true;

        if (playerLanded && collision.gameObject.tag == "Wall")
        {
            // so lan va cham voi Wall
            collisionWithWall++;

            if (collisionWithWall == 1)
            {
                AudioManager.instance.PlaySound("Crack");
                spriteRender.sprite = spriteCrack;
            }
            else
            {
                AudioManager.instance.PlaySound("Break");
                spriteRender.enabled = false;
                boxCollider.enabled = false;
            }
        }
    }

    public bool WasBroken()
    {
        return collisionWithWall == 2;
    }
}