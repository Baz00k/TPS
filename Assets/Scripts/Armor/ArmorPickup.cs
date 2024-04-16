using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class ArmorPickup : MonoBehaviour
{
    public ArmorItem armorItem;
    private bool isFKeyPressed = false;

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            isFKeyPressed = true;
            Debug.Log("Key F was pressed.");
        }
        else if (Keyboard.current.fKey.wasReleasedThisFrame)
        {
            isFKeyPressed = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (isFKeyPressed && other.CompareTag("Player"))
        {
            Pickup();
        }
        else
        {
            Debug.Log("Player is not in range or F key was not pressed.");
        }
    }

    void Pickup()
{
    ArmorManager.Instance.RemoveAll(armorItem);
    ArmorManager.Instance.Add(armorItem);

    // Powiadom HUD o zmianie zbroi
    ArmorManager.Instance.NotifyArmorChanged(armorItem);

    Destroy(gameObject);
    Debug.Log("Armor picked up.");
}

    void UpdatePlayerHUD(ArmorItem armor)
    {
    PlayerArmorHUD playerHUD = FindObjectOfType<PlayerArmorHUD>();
    if (playerHUD != null)
    {
        playerHUD.UpdateArmorHUD(armor);
    }
    else
    {
        Debug.LogWarning("PlayerHUD not found in the scene.");
    }
}

}
