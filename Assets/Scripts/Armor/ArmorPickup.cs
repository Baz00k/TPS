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
    }

    void Pickup()
    {
        //Debug.Log("Trying to remove armor: " + armorItem.armorName);

        // Ustaw durability podnoszonego armoru na 100
        armorItem.durability = 100;

        // Usuń poprzednią zbroję z listy
        ArmorManager.Instance.Remove(armorItem);

        // Dodaj nową zbroję z aktualnym durability ustawionym na 100
        ArmorManager.Instance.Add(armorItem);

        // Powiadom HUD o zmianie zbroi
        ArmorManager.Instance.NotifyArmorChanged(armorItem);

        // Niszczymy obecną zbroję
        Destroy(gameObject);
        //Debug.Log("Armor picked up.");
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
