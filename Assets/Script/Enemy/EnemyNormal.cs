using UnityEngine;

public class EnemyNormal : MonoBehaviour
{
    
    [SerializeField] float speed = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
    }
}
