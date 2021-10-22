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

    public int currentSelectedIndex = 0; //by default start/select the first button

    [Tooltip("Show Inventory On GUI")]
    public bool showInventory = false;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //load the controller so that we can play the animations (inventoryIn/inventoryOut)
        animator = itemsSelectionPanel.GetComponent<Animator>();

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
        if(key == KeyCode.J || key == KeyCode.K)
        {
            ChangeSelection(key);
        }
        else if(key == KeyCode.Return)
        {
            ConfirmSelection();
        }
    }

    public void ShowToggleInventory()
    {
        if (showInventory == false)
        {
            showInventory = true;
            animator.SetTrigger("InventoryIn");
        }
        else
        {
            showInventory = false;
            animator.SetTrigger("InventoryOut");
        }
    }

    private void ConfirmSelection()
    {
        //get the item from the itemsForPlayer list using the currentSelectedIndex
        InventoryItem inventoryItem = itemsForPlayer[currentSelectedIndex];
        print("Item Selected is:" + inventoryItem.item.name);
        //reduce the quantity
        inventoryItem.quantity -= 1;

        //check if the quantity is 0. if it is 0, then remove from itemsForPlayer list
        if (inventoryItem.quantity == 0)
            itemsForPlayer.RemoveAt(currentSelectedIndex);

        RefreshInventoryGUI();
    }

    private void ChangeSelection(KeyCode key)
    {
        if(key == KeyCode.J)
        {
            currentSelectedIndex -= 1;
        }
        else if(key == KeyCode.K)
        {
            currentSelectedIndex += 1;
        }

        //check boundaries
        if (currentSelectedIndex < 0)
            currentSelectedIndex = 0;

        if (currentSelectedIndex == itemsForPlayer.Count)
            currentSelectedIndex = currentSelectedIndex - 1;



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

            if(buttonId == currentSelectedIndex)
            {
                button.GetComponent<Image>().color = selectedColour;
            }
            else
            {
                button.GetComponent<Image>().color = notSelectedColour;
            }


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
