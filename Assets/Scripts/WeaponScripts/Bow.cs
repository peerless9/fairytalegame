using UnityEngine;

public class Bow : Weapon
{
    public GameObject arrowPrefab;
    public float arrowSpeed = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        // Additional initialization for Bow can be done here
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        // Additional update logic for Bow can be added here
    }

    public virtual void shootArrow()
    {
        GameObject newArrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        Projectile arrowScript = newArrow.GetComponent<Projectile>();
        Rigidbody2D rb = newArrow.GetComponent<Rigidbody2D>();
        arrowScript.sender = GameObject.FindGameObjectWithTag("Player");
        rb.linearVelocity = -transform.right * arrowSpeed; // Assuming the arrow moves in the direction the bow is facing
    }
}