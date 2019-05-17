using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Common.HorseGame;

public class CoroutineLauncher : MonoBehaviour {
    
    public void LaunchCoroutine(coroutine coroutine)
    {
        StartCoroutine(coroutine());
        
    }

    public IEnumerator test()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("coucou");
    }
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
