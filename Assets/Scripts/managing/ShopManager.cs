using UnityEngine;
using System.Collections.Generic;


public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    public GameObject[] allItems; // Assign your 5 item prefabs here in the Inspector
    public Transform[] shopSlots; // Assign the 3 UI slots where the items should appear

    private void Awake()
    {
        DisplayRandomItems();
    }

    public void DisplayRandomItems()
    {
        // Clear any existing items in slots
        foreach (Transform slot in shopSlots)
        {
            foreach (Transform child in slot)
            {
                Destroy(child.gameObject);
            }
        }

        // Pick 3 random unique items
        GameObject[] selectedItems = GetRandomUniqueItems(3);

        // Instantiate the selected items into the slots
        for (int i = 0; i < selectedItems.Length; i++)
        {
            Instantiate(selectedItems[i], shopSlots[i]);
        }
    }

    private GameObject[] GetRandomUniqueItems(int count)
    {
        GameObject[] result = new GameObject[count];
        List<int> usedIndices = new List<int>();

        int i = 0;
        while (i < count)
        {
            int randomIndex = Random.Range(0, allItems.Length);
            if (!usedIndices.Contains(randomIndex))
            {
                usedIndices.Add(randomIndex);
                result[i] = allItems[randomIndex];
                i++;
            }
        }
        return result;
    }
}
