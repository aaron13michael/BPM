using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Player {

	// Use this for initialization
	void Start () {
        base.Start();
        maxAttacks = 3;	
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}
}
