using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    public int health;

    public int OnCollision() {
        gameObject.SetActive(false);
        return health;
    }
}
