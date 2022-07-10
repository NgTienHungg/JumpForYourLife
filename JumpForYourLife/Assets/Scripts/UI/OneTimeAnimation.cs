using UnityEngine;

public class OneTimeAnimation : MonoBehaviour
{
    public void OnEndOfAnimation()
    {
        Destroy(gameObject);
    }
}