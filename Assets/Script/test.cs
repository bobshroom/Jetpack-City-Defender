using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject camera;
    public float shakeDuration = 0.5f; // Duration of the shake effect
    private bool a;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (a) transform.localScale = new Vector3(transform.localScale.x, 2.7f, transform.localScale.z);
        else transform.localScale = new Vector3(transform.localScale.x, 3f, transform.localScale.z);
        a = !a;
        camera.transform.position = new Vector3(UnityEngine.Random.Range(-shakeDuration, shakeDuration), Random.Range(-shakeDuration, shakeDuration), -10);
    }
}
