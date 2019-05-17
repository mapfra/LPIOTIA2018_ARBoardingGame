using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    [Range(0, 3)] public float speed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y + Time.time * 360 * speed, transform.localRotation.z);
	}
}
