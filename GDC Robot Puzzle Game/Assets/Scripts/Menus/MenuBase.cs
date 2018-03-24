using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBase : MonoBehaviour {
    public Button backButton;
    public MenuBase lastMenue;

    public virtual void Start()
    {
        if(backButton)
        backButton.onClick.AddListener(delegate { Back(); });
    }

    public void Back()
    {
        lastMenue.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
