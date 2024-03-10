using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private BaseInventoryItem[] inventory = new BaseInventoryItem[3];
    [SerializeField] private Transform hand;


    private int activeItemIndex;
    private BaseInventoryItem ActiveItem => activeItemIndex >= 0 && activeItemIndex < inventory.Length ? inventory[activeItemIndex] : null;
    private GameObject activeItemInstance;
    private bool isUsingItem;


    private void Start()
    {
        UpdateActiveItem();
    }

    public void OnUseItem(InputValue value)
    {
        if (activeItemInstance == null) return;

        isUsingItem = value.isPressed;
        activeItemInstance.GetComponent<BaseInventoryItem>().Use(value.isPressed);
    }

    private void Update()
    {
        // TODO: Rotate the item towards the mouse

        if (!isUsingItem || activeItemInstance == null) return;

        activeItemInstance.GetComponent<BaseInventoryItem>().Use(false);
    }

    public void OnInventoryChange(InputValue value)
    {
        var context = value.Get<float>();
        if (context > 0)
        {
            activeItemIndex = (activeItemIndex + 1) % inventory.Length;
        }
        else if (context < 0)
        {
            activeItemIndex = (activeItemIndex - 1 + inventory.Length) % inventory.Length;
        }

        UpdateActiveItem();
    }

    public void AddItem(BaseInventoryItem item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                break;
            }
        }

        // If the inventory is full, replace the active item with the new item
        inventory[activeItemIndex] = item;
    }

    private void UpdateActiveItem()
    {
        // Destroy any existing item in the hand
        foreach (Transform child in hand)
        {
            Destroy(child.gameObject);
        }

        // If there is an active item, instantiate it and set its parent to the hand
        if (ActiveItem != null)
        {
            GameObject item = Instantiate(ActiveItem.gameObject, hand);
            item.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            activeItemInstance = item;
        }
    }
}
