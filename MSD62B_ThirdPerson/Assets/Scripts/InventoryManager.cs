using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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


    private List<InventoryItem> itemsForPlayer;

    // Start is called before the first frame update
    void Start()
    {
        itemsForPlayer = new List<InventoryItem>();

        PopulateInventorySpawn();
        RefreshInventoryGUI();
    }

    private void RefreshInventoryGUI()
    {
        int buttonId = 0;
        foreach(InventoryItem i in itemsForPlayer)
        {
            //load the button
            GameObject button = itemsSelectionPanel.transform.Find("Button" + buttonId).gameObject;

            //search for the child image and change it's sprite
            button.transform.Find("Image").GetComponent<Image>().sprite = i.item.icon;
            //change the quantity
            button.transform.Find("Quantity").GetComponent<TextMeshProUGUI>().text = "x" + i.quantity;

            buttonId += 1;

        }


    }

    private void PopulateInventorySpawn()
    {
        //randomly decide the number of items to create in the inventory
        int numberOfItems = Random.Range(minItems, maxItems);

        for(int i=0; i < numberOfItems; i++)
        {
            //pick random object from itemsAvailable
            ItemScriptableObject objItem = itemsAvailable[Random.Range(0, itemsAvailable.Count)];

            //add this item with quantity
            //InventoryItem objInventoryItem = new InventoryItem();
            //objInventoryItem.item = objItem;
            //objInventoryItem.quantity = 1;

            //itemsForPlayer.Add(objInventoryItem);

            itemsForPlayer.Add(new InventoryItem() { item = objItem, quantity = 1 });
        }
    }

    public class InventoryItem
    {
        public ItemScriptableObject item { get; set; }
        public int quantity { get; set; }
    }

}
