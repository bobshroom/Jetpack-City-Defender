using System.Collections;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletCount;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float explotionTimeMin;
    [SerializeField] private float explotionTimeMax;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(explodeCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator explodeCoroutine()
    {
        float waitTime = Random.Range(explotionTimeMin, explotionTimeMax);
        yield return new WaitForSeconds(waitTime-0.6f);
        for(int i = 0; i < 3; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        float startAngle = Random.Range(0, 360f);
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet bulletCompo = bullet.GetComponent<Bullet>();
            float angle = 360f / bulletCount * i + startAngle;
            bulletCompo.angle = angle;
            bulletCompo.speed = bulletSpeed;
        }
        GetComponent<EnemyManager>().isExplosion = false;
        Destroy(gameObject);
    }
}
