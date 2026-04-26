using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 6;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.upArrowKey.isPressed && transform.position.y < 4.5f)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        if (Keyboard.current.downArrowKey.isPressed && transform.position.y > -4.5f)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        if (Keyboard.current.rightArrowKey.isPressed && transform.position.x < 10f)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (Keyboard.current.leftArrowKey.isPressed && transform.position.x > -10f)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
    }
}