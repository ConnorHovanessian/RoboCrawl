using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50;

    public void takeDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
