using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackTrail : MonoBehaviour {

    public Transform packToFollow;

	void Update ()
    {
        if(packToFollow != null)
            transform.position = Camera.main.ScreenToWorldPoint(packToFollow.position);
	}
}
