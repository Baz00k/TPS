using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmorPickup : MonoBehaviour
{
    public ArmorItem armorItem;
    private bool fKeyPressed = false;

    void Update()
    {

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            fKeyPressed = true;
            Debug.Log("Key F was pressed.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (fKeyPressed && other.CompareTag("Player"))
        {
            Pickup();
            Debug.Log("OnTriggerEnter");
        }
        else
        {
            Debug.Log("Player is not in range or F key was not pressed.");
        }
    }

    void Pickup()
    {
        ArmorManager.Instance.Add(armorItem);
        Destroy(gameObject);
        Debug.Log("Armor picked up.");
    }
}


