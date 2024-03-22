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
}
