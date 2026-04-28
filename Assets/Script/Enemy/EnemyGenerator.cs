using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*timer += Time.deltaTime;
        if (timer > 1f)
        {
            timer = 0;
            GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
            enemy.transform.position = new Vector3(12, UnityEngine.Random.Range(-4, 4), 0);
        }*/
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
            enemy.transform.position = new Vector3(13, 0, 0);
        }
    }
}
