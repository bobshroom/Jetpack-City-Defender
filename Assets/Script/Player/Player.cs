using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 6;
    private float time;

    [SerializeField] private float floatingSpeed = 10f;
    [SerializeField] private float floatingRange = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isMoveToHori = false;
        float angle = transform.rotation.eulerAngles.z;
        if (angle > 20) angle -= 360;
        if (angle < -20) angle += 360;
        Debug.Log(angle);

        time += Time.deltaTime;
        if (Keyboard.current.upArrowKey.isPressed && transform.position.y < 4.5f)
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (Keyboard.current.downArrowKey.isPressed && transform.position.y > -4.5f)
        {
            transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
        }
        if (Keyboard.current.rightArrowKey.isPressed && transform.position.x < 10f)
        {
            isMoveToHori = true;
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Max(angle - Time.deltaTime * 50f, -10f));
        }
        if (Keyboard.current.leftArrowKey.isPressed && transform.position.x > -10f)
        {
            isMoveToHori = true;
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Min(angle + Time.deltaTime * 50f, 10f));
        }

        if (!isMoveToHori)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle * 0.95f);
        }

        transform.Translate(0, Mathf.Sin(time*floatingSpeed) * Time.deltaTime * floatingRange, 0);
    }
}