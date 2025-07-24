using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage; //set by the script that spawns the projectile
    public float lifeTime = 2f; // How long the projectile lasts before being destroyed
    public GameObject sender;

    void Start()
    {
        // Destroy the projectile after a certain time to prevent it from existing indefinitely
        Destroy(gameObject, lifeTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        print(sender.tag);
        print(collision.tag);
        if (!collision.CompareTag(sender.tag))
        {
            collision.GetComponent<Entity>()?.TakeDamage(damage);
            Destroy(gameObject); // Destroy the projectile after it hits something
        }
    }
}
