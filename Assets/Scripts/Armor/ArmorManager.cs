using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorManager : MonoBehaviour
{
    // Singleton instance
    private static ArmorManager instance;

    // Lista armatek
    public List<ArmorItem> Armors = new List<ArmorItem>();

    public Transform ArmorContent;
    public GameObject ArmorItem;


    // Dostęp do instancji singletona
    public static ArmorManager Instance
    {
        get
        {
            // Jeśli instancja nie została jeszcze utworzona, próbujemy ją znaleźć
            if (instance == null)
            {
                instance = FindObjectOfType<ArmorManager>();

                // Jeśli nie ma istniejącego obiektu ArmorManager w scenie, tworzymy nowy
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("ArmorManager");
                    instance = singletonObject.AddComponent<ArmorManager>();
                }
            }
            return instance;
        }
    }

    // Funkcja Awake wywoływana przy pierwszym utworzeniu obiektu
    private void Awake()
    {
        // Sprawdzamy, czy instancja singletona już istnieje, jeśli tak, usuwamy ten obiekt
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // Ustawiamy bieżący obiekt jako instancję singletona
            instance = this;
            // Zapewniamy, że obiekt nie zostanie usunięty między scenami
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Funkcja dodająca armatkę do listy
    public void Add(ArmorItem armor)
    {
        Armors.Add(armor);
    }

    // Funkcja usuwająca armatkę z listy
  public void RemoveAll(ArmorItem armor)
{
    Armors.Remove(armor);
    Debug.Log("Usuwamy!!!!");
}

    public void ListArmors()
    {
        foreach (var armor in Armors)
        {
            GameObject obj = Instantiate(ArmorItem, ArmorContent);
            var armorName = obj.transform.Find("Armor/ArmorName").GetComponent<Text>();
            var armorIcon = obj.transform.Find("Armor/ArmorIcon").GetComponent<Image>();

            armorName.text = armor.armorName;
            armorIcon.sprite = armor.icon;

        }
    }

}
