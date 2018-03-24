using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsMenu : MenuBase
{
    public Button[] inputsFilds;
    public Button setup;

    int editing = -1;

    public override void Start()
    {
        base.Start();

        // Adds listener for all keycode buttons
        for (int i = 0; i < inputsFilds.Length; i++)
        {
            int ii = i;
            inputsFilds[i].onClick.AddListener(delegate { inputControll(ii); });
        }
        
        // sets up base key codes
        ControlBank.Setup();

        // intal sync of values
        SyncValues();
    }

    public void inputControll(int i) // called by button press in GUI and sets buton for editing
    {
        if(editing == -1)
        {
            editing = i;
            inputsFilds[i].image.color = new Color(0.9f,0.9f,1);
        }
    }

    public void SyncValues() // update display based off controll values
    {
        for (int i = 0; i < inputsFilds.Length; i++)
        {
            inputsFilds[i].transform.Find("Text").GetComponent<Text>().text = ControlBank.controlls[i].ToString();
            inputsFilds[i].image.color = Color.white;
        }
    }

    private void OnGUI()
    {
        // selector for new key
        if(editing != -1)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                ControlBank.controlls[editing] = e.keyCode;
                editing = -1;
                SyncValues();
            }
        }
    }   
}

public static class ControlBank 
{
    // holds all the keycodes for the controlls
    public static KeyCode[] controlls = new KeyCode[4];

    private static bool isSetup = false;
    
    public static void Setup()
    {
        if (!isSetup)
        {
            isSetup = true;
            // basic movement controlls
            controlls[0] = KeyCode.W; // up
            controlls[1] = KeyCode.S; // down
            controlls[2] = KeyCode.D; // right
            controlls[3] = KeyCode.A; // left
        }
    }
}

