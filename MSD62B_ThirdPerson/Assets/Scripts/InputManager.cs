using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance = null;

    public delegate void KeyEvent(KeyCode key);
    public static event KeyEvent KeyDown;
    public static event KeyEvent KeyUp;
    public static event KeyEvent KeyIsPressed;

    //list containing keycodes
    private List<KeyCode> keyCodes;

    // Start is called before the first frame update
    void Start()
    {
        //create a singleton
        if (InputManager.Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        //load all possible keycodes
        keyCodes = new List<KeyCode>((KeyCode[])System.Enum.GetValues(typeof(KeyCode)));
    }

    

    // Update is called once per frame
    void Update()
    {
        if (keyCodes == null)
            return;

        for(int i=0; i < keyCodes.Count; i++)
        {
            if(KeyDown != null && Input.GetKeyDown(keyCodes[i]))
            {
                KeyDown(keyCodes[i]);
            }

            if(KeyUp!=null && Input.GetKeyUp(keyCodes[i]))
            {
                KeyUp(keyCodes[i]);
            }

            if(KeyIsPressed != null && Input.GetKey(keyCodes[i]))
            {
                KeyIsPressed(keyCodes[i]);
            }
        }
    }
}
