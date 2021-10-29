using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameObject nextToBox;

    private Animator animator;

    public GameObject Camera2;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
        else if (keyCode == KeyCode.F)
        {
            StartShooting();
        }
    }

    private void StartShooting()
    {
        //trigger shooting animation
        animator.SetTrigger("WeaponShoot");

        //enable the second camera
        Camera2.SetActive(true);

        //Start Timer
        StartCoroutine(IStopShooting());
    }

    IEnumerator IStopShooting()
    {
        //wait for 5seconds
        yield return new WaitForSeconds(5f);
        Camera2.SetActive(false);
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
