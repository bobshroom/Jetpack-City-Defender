using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int type;
    public int damage = 100; // 弾のダメージ
    public float speed = 0;
    public float angle; // 弾の角度
    public float size = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (type == 1) return;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.localScale = new Vector3(size, size, 1);

        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (type == 1) return;
        transform.Translate(speed * Time.deltaTime, 0, 0);

        if (transform.position.x > 15)
        {
            Destroy(gameObject);
        }
    }
}