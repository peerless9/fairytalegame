using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private Transform weaponPivot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponPivot = transform.Find("WeaponPivot");
    }

    private void pointWeapon()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        float zAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 pivotScale = weaponPivot.localScale;
        pivotScale.y = direction.x < 0 ? -Mathf.Abs(pivotScale.x) : Mathf.Abs(pivotScale.x);
        weaponPivot.localScale = pivotScale;
        weaponPivot.rotation = Quaternion.Euler(new Vector3(0, 0, zAngle));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        pointWeapon();
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
