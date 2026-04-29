using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    private float time;
    [SerializeField] private float speed = 10;
    [SerializeField] private float delay = 0.25f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if ((Keyboard.current.spaceKey.isPressed || Mouse.current.leftButton.isPressed) && time > delay)
        {
            time = 0;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Bullet>().speed = speed;
            bullet.GetComponent<Bullet>().angle = transform.rotation.eulerAngles.z; 
        }
    }
}