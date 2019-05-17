using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    public Text fpsText;
    public float deltaTime;

    void Start()
    {
        if (GetComponent<Text>() != null) Debug.Log("qqchose");
        else Debug.Log("rien");
        fpsText = GetComponent<Text>();
        
    }


    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();
    }
}