using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.2f, 0, 0);
        //transform.position += transform.right * 0.2f; // この書き方でも可。散弾等が作れる。

        if (transform.position.x > 15)
        {
            Destroy(gameObject);
        }
    }
}