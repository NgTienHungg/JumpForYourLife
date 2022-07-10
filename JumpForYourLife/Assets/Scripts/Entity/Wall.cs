using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 18f;
    [SerializeField] private float limitPositionY = 6f;

    private float resetPostionY;
    private float targetPositionY;
    private bool isMoving = false;

    private void Start()
    {
        resetPostionY = transform.position.y;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.up * movingSpeed * Time.deltaTime);
            if (transform.position.y > targetPositionY)
            {
                if (targetPositionY > limitPositionY)
                {
                    // reset Wall ve vi tri ban dau
                    targetPositionY = resetPostionY;
                }

                transform.position = new Vector3(transform.position.x, targetPositionY, transform.position.z);
                isMoving = false;
            }
        }
    }

    public void MoveUp(float distance)
    {
        isMoving = true;
        targetPositionY = transform.position.y + distance;
    }
}