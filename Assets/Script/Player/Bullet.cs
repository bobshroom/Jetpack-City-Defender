using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 100; // 弾のダメージ
    public float speed = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        if (transform.position.x > 15)
        {
            Destroy(gameObject);
        }
    }
}