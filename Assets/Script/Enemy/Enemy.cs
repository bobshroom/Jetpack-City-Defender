using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab; // 爆発エフェクトのプレハブ
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-0.01f, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "bullet")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        GameObject instant = Instantiate(explosionPrefab); // 爆発エフェクトを生成
        instant.transform.position = transform.position; // 爆発エフェクトの位置を敵の位置に設定
        instant.transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(0,360)); // 爆発エフェクトの回転をランダムに設定
        instant.transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
        Destroy(instant, 0.5f); // 0.5秒後に爆発エフェクトを破壊
    }
}
