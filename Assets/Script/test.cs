using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(transform.rotation.z);
        transform.rotation = Quaternion.Euler(0, 0, -20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
