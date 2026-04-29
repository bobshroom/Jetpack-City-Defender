using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemyboss;
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
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            GameObject enemy = Instantiate(enemy1, transform.position, transform.rotation);
            enemy.transform.position = new Vector3(12, UnityEngine.Random.Range(-4f, 4f), 0);
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            GameObject enemy = Instantiate(enemy2, transform.position, transform.rotation);
            enemy.transform.position = new Vector3(12, UnityEngine.Random.Range(-4f, 4f), 0);
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            GameObject enemy = Instantiate(enemy3, transform.position, transform.rotation);
            enemy.transform.position = new Vector3(12, UnityEngine.Random.Range(-4f, 4f), 0);
        }
        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            GameObject enemy = Instantiate(enemyboss, transform.position, transform.rotation);
            enemy.transform.position = new Vector3(12, UnityEngine.Random.Range(-4f, 4f), 0);
        }
    }
}
