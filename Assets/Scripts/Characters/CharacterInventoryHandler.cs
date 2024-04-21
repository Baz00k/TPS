using System.Collections.Generic;
using UnityEngine;
using TPS.Armor;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(CharacterArmorController))]
public class CharacterInventoryHandler : MonoBehaviour
{
    [Tooltip("The transform where the character's hand is located")]
    [SerializeField]
    private Transform characterHand;

    [Tooltip("The maximum number of items in the inventory")]
    [SerializeField]
    private int inventorySize = 5;

    [Tooltip("The items the character starts with")]
    [SerializeField]
    private List<BaseInventoryItem> startingItems;

    private BaseInventoryItem[] inventoryItems;
    private int activeItemIndex = -1;
    private Collider2D characterCollider;
    private CharacterArmorController armorController;

    private void Awake()
    {
        inventoryItems = new BaseInventoryItem[inventorySize];
        InitializeStartingItems();

        characterCollider = GetComponent<Collider2D>();
        armorController = GetComponent<CharacterArmorController>();
    }

    private void Start()
    {
        if (inventoryItems.Length > 0)
        {
            activeItemIndex = 0;
            SetItemActiveStateAtIndex(activeItemIndex, true);
        }
    }

    private void InitializeStartingItems()
    {
        for (int i = 0; i < startingItems.Count && i < inventorySize; i++)
        {
            if (startingItems[i] != null)
            {
                AddItemToInventory(startingItems[i]);
            }
        }
    }

    public void AddItemToInventory(BaseInventoryItem itemPrefab)
    {
        // Try to find an empty slot
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null)
            {
                InstantiateItemAtIndex(i, itemPrefab);
                return;
            }
        }

        // If no empty slot was found, replace the active item
        if (inventoryItems[activeItemIndex] != null)
        {
            Destroy(inventoryItems[activeItemIndex].gameObject);
        }
        InstantiateItemAtIndex(activeItemIndex, itemPrefab);
    }

    private void InstantiateItemAtIndex(int index, BaseInventoryItem itemPrefab)
    {
        if (index >= 0 && index < inventorySize)
        {
            inventoryItems[index] = Instantiate(itemPrefab, characterHand.position, Quaternion.identity, characterHand);
            inventoryItems[index].gameObject.SetActive(false);
        }
    }

    private void SetItemActiveStateAtIndex(int index, bool active)
    {
        if (index >= 0 && index < inventorySize && inventoryItems[index] != null)
        {
            inventoryItems[index].gameObject.SetActive(active);
            activeItemIndex = index;
        }
    }

    public void ChangeActiveItem(int direction)
    {
        SetItemActiveStateAtIndex(activeItemIndex, false);

        int newIndex;
        if (direction > 0)
        {
            newIndex = (activeItemIndex + 1) % inventorySize;
        }
        else
        {
            newIndex = (activeItemIndex - 1 + inventorySize) % inventorySize;
        }

        SetItemActiveStateAtIndex(newIndex, true);
        activeItemIndex = newIndex;
    }

    public void UseActiveItem()
    {
        if (activeItemIndex >= 0 && activeItemIndex < inventorySize && inventoryItems[activeItemIndex] != null)
        {
            inventoryItems[activeItemIndex].Use();
        }
    }

    public void PickupItem()
    {
        Collider2D[] colliders = new Collider2D[10];
        ContactFilter2D filter = new()
        {
            useLayerMask = true,
            layerMask = 1 << gameObject.layer,
            useTriggers = true,
        };

        characterCollider.OverlapCollider(filter, colliders);

        foreach (Collider2D collider in colliders)
        {
            if (collider == null) break;

            // TODO: Pickup items other than armor

            if (collider.TryGetComponent(out ArmorPickup armorPickup))
            {
                armorController.AddArmor(armorPickup.GetArmorItem());
                Destroy(collider.gameObject);
            }
        }
    }
}
