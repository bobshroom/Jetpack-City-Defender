using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class showfps : MonoBehaviour
{
    Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(ShowFPS());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowFPS()
    {
        while (true)
        {
            text.text = "FPS: " + (1f / Time.deltaTime).ToString("F3");
            yield return new WaitForSeconds(0.2f);
        }
    }
}
