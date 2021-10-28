using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameObject nextToBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        InputManager.KeyDown += ObserverKeyDown;
    }

    private void OnDisable()
    {
        InputManager.KeyDown -= ObserverKeyDown;
    }

    private void ObserverKeyDown(KeyCode keyCode)
    {
        if(keyCode == KeyCode.E)
        {
            DestroyBox();
        }
    }

    private void DestroyBox()
    {
        GameManager.Canvas.GetComponentInChildren<BottomMenuManager>().ShowBottomMenu(false);
        Destroy(nextToBox);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Collision with:" + other.gameObject.name);
        GameManager.Canvas.GetComponentInChildren<BottomMenuManager>().ShowBottomMenu(true, "Press E to collect box");
        nextToBox = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        print("exiting collision");
        GameManager.Canvas.GetComponentInChildren<BottomMenuManager>().ShowBottomMenu(false);
        nextToBox = null;
    }
}
