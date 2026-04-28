using System;
using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float exitRotate;
    [SerializeField] private int way;
    [SerializeField] private int rotate;
    private bool isInWindow = false;
    private bool isMoveTrigger = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInWindow)
        {
            transform.position += Vector3.left * Time.deltaTime * 2;
            return;
        }
        if (isMoveTrigger) return;
        StartCoroutine(moveCoroutine());
        isMoveTrigger = true;
    }

    void shot(int way, float rotate)
    {
        for (int i = 0; i < way; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet bulletCompo = bullet.GetComponent<Bullet>();
            bullet.transform.position += transform.right * -0.75f;
            bulletCompo.speed = 5;
            float angle = 180f + transform.rotation.eulerAngles.z - rotate / 2 + rotate / (way-1) * i;
            bulletCompo.angle = angle;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "window")
        {
            isInWindow = true;
        }
    }

    IEnumerator moveCoroutine()
    {
        float time = 0;
        while (time < 2f)
        {
            transform.position += -transform.right * Time.deltaTime * (2-time) * 1.5f;
            time += Time.deltaTime;
            yield return null;
        }
        shot(way, rotate);
        yield return new WaitForSeconds(0.5f);
        shot(way-1, rotate*0.75f);
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 1.5f));


        float currentSpeed = 0;
        float currentRotate = transform.rotation.eulerAngles.z;
        float addRotate = 0;
        int exitRotateVec = UnityEngine.Random.Range(0f ,1f) > 0.5 ? 1 : -1;

        while(true)
        {
            transform.position += -transform.right * Time.deltaTime * currentSpeed;
            transform.rotation = Quaternion.Euler(0, 0, currentRotate + addRotate);
            if (Math.Abs(addRotate) < exitRotate) addRotate += exitRotateVec * Time.deltaTime * exitRotate;
            if (currentSpeed < speed) currentSpeed += Time.deltaTime * speed;
            yield return null;
        }
    }
}
