using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminPlayer : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D interactCollider = Physics2D.OverlapBox(transform.position, new Vector2(1, 1), 0, 256);
        if (interactCollider != null)
        {
            interactCollider.gameObject.GetComponent<InputObjectBase>().Selected();
        }
	}
}
