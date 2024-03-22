using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmorPickup : MonoBehaviour
{
    public ArmorItem armorItem;

    void Update()
    {
        // Sprawdź czy został naciśnięty przycisk F
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            Pickup();
        }
    }

    void Pickup()
    {
        ArmorManager.Instance.Add(armorItem);
        Destroy(gameObject);
    }
}
