using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorManager : MonoBehaviour
{
    public static ArmorManager Instance;
    public List<ArmorItem> Armors = new List<ArmorItem>();

    private void Awake()
    {
        Instance = this;
    }

    public void Add(ArmorItem armor)
    {
        Armors.Add(armor);
    }

    public void Remove(ArmorItem armor)
    {
        Armors.Remove(armor);
    }
}
