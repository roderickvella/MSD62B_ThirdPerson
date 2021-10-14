using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Tooltip("The minimum amount of items the player can have in the inventory")]
    public int minItems = 5;

    [Tooltip("The maximum amount of items the player can have in the inventory")]
    public int maxItems = 10;

    [Tooltip("Items selection Panel")]
    public GameObject itemsSelectionPanel;

    [Tooltip("List of Items")]
    public List<ItemScriptableObject> itemsAvailable;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
