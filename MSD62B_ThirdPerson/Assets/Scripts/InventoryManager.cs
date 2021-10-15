using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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

    [Tooltip("Selected Item Colour")]
    public Color selectedColour;

    [Tooltip("Not Selected Item Colour")]
    public Color notSelectedColour;


    private List<InventoryItem> itemsForPlayer;

    // Start is called before the first frame update
    void Start()
    {
        itemsForPlayer = new List<InventoryItem>();

        PopulateInventorySpawn();
        RefreshInventoryGUI();
    }

    private void OnEnable()
    {
        InputManager.KeyDown += ObservorKeyDown;
    }

    private void OnDisable()
    {
        InputManager.KeyDown -= ObservorKeyDown;
    }

    private void ObservorKeyDown(KeyCode key)
    {
        if(key == KeyCode.LeftArrow || key == KeyCode.RightArrow)
        {
            print("user just pressed left or right key");
        }
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

       //set active false redundant buttons
       for (int i=buttonId; i < 6; i++)
       {
            itemsSelectionPanel.transform.Find("Button" + i).gameObject.SetActive(false);
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
            //check whether objItem exists in itemsForPlayer. So basically we need to count how many items
            //we've got of type objItem inside itemsForPlayer
            int countItems = itemsForPlayer.Where(x => x.item == objItem).ToList().Count;
            if (countItems == 0)
            {
                //add objItem with quantity 1 because it is the first type inside itemsForPlayer
                itemsForPlayer.Add(new InventoryItem() { item = objItem, quantity = 1 });
            }
            else
            {
                //return the first object inside itemsForPlayer that is exactly like objItem
                var item = itemsForPlayer.First(x => x.item == objItem);
                //modify and increase the quantity by 1
                item.quantity += 1;
            }


           
        }
    }

    public class InventoryItem
    {
        public ItemScriptableObject item { get; set; }
        public int quantity { get; set; }
    }

}
