using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerArmorHUD : MonoBehaviour
{
    public Image armorIconImage;

    private void Start()
    {
        if (armorIconImage == null)
        {
            Debug.LogError("ArmorIconImage is not assigned in the PlayerArmorHUD script!");
        }

        // Subskrybuj zdarzenie ArmorChanged z ArmorManager
        ArmorManager.Instance.OnArmorChanged += UpdateArmorHUD;
    }

    private void OnDestroy()
    {
        // Odsubskrybuj zdarzenie ArmorChanged z ArmorManager
        ArmorManager.Instance.OnArmorChanged -= UpdateArmorHUD;
    }

    public void UpdateArmorHUD(ArmorItem armor)
    {
        if (armorIconImage != null)
        {
            armorIconImage.sprite = armor.icon;
        }
        else
        {
            Debug.LogWarning("ArmorIconImage is not assigned!");
        }
    }
}

