using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-0.01f, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D other)     // 弾が当たった時
    {
        if (other.gameObject.tag == "bullet")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
