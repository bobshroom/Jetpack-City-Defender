using System;
using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab; // 爆発エフェクトのプレハブ
    [SerializeField] private float explosionSize = 1.75f; // 爆発エフェクトのサイズ
    [SerializeField] int hp = 100; // 敵の体力
    private SpriteRenderer spriteRenderer;
    public bool isExplosion = true; // 爆発エフェクトが生成されるかどうか
    public bool isInWindow = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "bullet" && isInWindow)
        {
            Destroy(other.gameObject);
            StartCoroutine(damageEffect());
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            hp -= bullet.damage; // 弾のダメージを敵の体力から引く
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnDestroy()
    {
        if (!isExplosion) return;
        Debug.Log("爆発エフェクト生成");
        GameObject instant = Instantiate(explosionPrefab); // 爆発エフェクトを生成
        instant.transform.position = transform.position; // 爆発エフェクトの位置を敵の位置に設定
        instant.transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(0,360)); // 爆発エフェクトの回転をランダムに設定
        instant.transform.localScale = new Vector3(explosionSize, explosionSize, explosionSize);
        Destroy(instant, 0.5f); // 0.5秒後に爆発エフェクトを破壊
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "window")
        {
            isInWindow = true;
        }
    }

    IEnumerator damageEffect()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0); // ダメージを受けたときの色
        yield return new WaitForSeconds(0.02f); // 0.02秒待つ
        spriteRenderer.color = Color.white; // 元の色に戻す
    }
}
