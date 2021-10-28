using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject Canvas;

    public GameObject PandoraBoxPrefab;
    public List<Vector3> PandoraSpawnPositions;

    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
        SpawnPandoraBoxes();
    }

    private void OnEnable()
    {
        InputManager.KeyDown += ObserverKeyPress;
    }

    private void OnDisable()
    {
        InputManager.KeyDown -= ObserverKeyPress;
    }

    private void ObserverKeyPress(KeyCode key)
    {
        if(key == KeyCode.Tab)
        {
            ShowToggleInventory();
        }
    }

    private void SpawnPandoraBoxes()
    {
        GameObject pandoraBoxesParent = GameObject.Find("PandoraBoxes");

        for(int i=0; i < PandoraSpawnPositions.Count; i++)
        {
            GameObject spawnedBox = Instantiate(PandoraBoxPrefab, PandoraSpawnPositions[i], Quaternion.identity);
            spawnedBox.transform.parent = pandoraBoxesParent.transform;
        }
    }

    private void ShowToggleInventory()
    {
        //I need to call a method inside InventoryManager to toggle the inventory window's animations
        Canvas.GetComponentInChildren<InventoryManager>().ShowToggleInventory(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
