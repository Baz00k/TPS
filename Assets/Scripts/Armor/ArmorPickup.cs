using UnityEngine;

public class ArmorPickup : MonoBehaviour
{
    public ArmorItem armorItem;

    public void Pickup()
    {
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
    }
}
