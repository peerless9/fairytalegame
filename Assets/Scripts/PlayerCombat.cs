using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        // Call the Attack method on the weapon
        Weapon weapon = GetComponentInChildren<Weapon>();
        if (weapon != null)
        {
            weapon.Attack();
        }
    }
}
