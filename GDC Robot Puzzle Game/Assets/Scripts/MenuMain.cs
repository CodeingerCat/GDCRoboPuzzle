using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMain : MenuBase {
    public Button[] buttons;
    public MenuBase[] GUIs;

    private Image image;
    private GameObject[] gameObjects;
    private bool toggle = false;

    public override void Start()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            int iTrick = i;
            buttons[i].onClick.AddListener(delegate { SwitchTo(iTrick); });
        }

        image = gameObject.GetComponent<Image>();
        int temp = gameObject.transform.childCount;
        gameObjects = new GameObject[temp];
        for(int i = 0; i < temp; i++)
        {
            gameObjects[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (toggle)
            {
                if(image)
                    image.enabled = false;
                foreach(GameObject obj in gameObjects)
                {
                    obj.SetActive(false);
                }

                toggle = false;
            }
            else
            {
                if (image)
                    image.enabled = true;
                foreach (GameObject obj in gameObjects)
                {
                    obj.SetActive(true);
                }

                toggle = true;
            }
        }
    }

    public void SwitchTo(int i)
    {
        GUIs[i].gameObject.SetActive(true);
        GUIs[i].lastMenue = this;
        gameObject.SetActive(false);
    }
}
