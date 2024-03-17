using UnityEngine;

public abstract class BaseInventoryItem : MonoBehaviour
{
    public string itemName;
    [TextArea(3, 10)] public string description;
    public Sprite icon;

    public abstract void Use();
}
