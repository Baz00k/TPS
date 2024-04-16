using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ArmorItem",menuName ="Armor/Create New Armor")]
public class ArmorItem : ScriptableObject
{
    public int id;
    public string armorName;
    public int value;
    public Sprite icon;

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        ArmorItem otherArmor = (ArmorItem)obj;

        // Porównaj wszystkie właściwości obiektów ArmorItem
        return id == otherArmor.id &&
               armorName == otherArmor.armorName &&
               value == otherArmor.value &&
               // Porównaj referencje do sprite'ów
               ReferenceEquals(icon, otherArmor.icon);
    }

    public override int GetHashCode()
    {
        unchecked // Overflow jest dozwolony, ponieważ wartość hash code nie jest obliczana na podstawie obiektów mutable
        {
            int hashCode = id;
            hashCode = (hashCode * 397) ^ (armorName != null ? armorName.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ value;
            hashCode = (hashCode * 397) ^ (icon != null ? icon.GetHashCode() : 0);
            return hashCode;
        }
    }
}
