using UnityEngine;

public class EnemyBomber : MonoBehaviour
{
    EnemyManager enemyManager;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed;
    [SerializeField] private GameObject[] children;
    [SerializeField] private float power;
    [SerializeField] private float rotate;
    private bool isInWindow = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInWindow)
        {
            transform.Translate(-2 * Time.deltaTime, 0, 0);
            return;
        }
        transform.Translate(-speed * Time.deltaTime, 0, 0);
        if (UnityEngine.Random.Range(0, 1000) < 2) dropBomb(); // ランダムで爆弾を落とす
    }

    void dropBomb()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0); // 敵を透明にする
        for (int i = 0; i < 3; i++)
        {
            children[i].SetActive(true);
            children[i].transform.parent = null; // 子オブジェクトの親を解除
            Rigidbody2D rb = children[i].GetComponent<Rigidbody2D>();
            if (i == 0)
            {
                rb.AddForce(new Vector2(-power * UnityEngine.Random.Range(0.8f,1.2f), 0), ForceMode2D.Impulse);
                rb.AddTorque(UnityEngine.Random.Range(-rotate, rotate), ForceMode2D.Impulse);
            }
            else if (i == 2)
            {
                rb.AddForce(new Vector2(power * UnityEngine.Random.Range(0.8f,1.2f), 0), ForceMode2D.Impulse);
                rb.AddTorque(UnityEngine.Random.Range(-rotate, rotate), ForceMode2D.Impulse);
            }
        }
        enemyManager.isExplosion = false; // 爆発エフェクトを生成しないようにする
        Destroy(gameObject); // 敵を破壊
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "window")
        {
            isInWindow = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "window")
        {
            isInWindow = false;
        }
    }
}
