using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

    public GameplayManager manager;
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {            
            var left = Input.mousePosition.x < (Screen.width / 2);
            manager.OnClick(left);
        }
	}
}
