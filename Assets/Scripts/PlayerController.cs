using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private WeaponController weaponController;

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            weaponController.Fire();
        }
    }
}
