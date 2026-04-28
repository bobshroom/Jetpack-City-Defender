using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class EnemyBoss1 : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float delay;
    [SerializeField] private float bulletSize;
    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject chargeEffect;
    [SerializeField] private GameObject chargeLaserEffect;
    [SerializeField] private float rotateVelocity;
    private GameObject maincamera;
    private bool isInWindow = false;
    private bool isMoveTrigger = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maincamera = GameObject.FindGameObjectWithTag("MainCamera");
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
            bullet.transform.position += transform.right * -1f;
            bulletCompo.speed = speed;
            bulletCompo.size = bulletSize;
            float angle = 180f + transform.rotation.eulerAngles.z - rotate / 2 + rotate / (way-1) * i;
            if (way == 1) angle = 180f + transform.rotation.eulerAngles.z;
            bulletCompo.angle = angle;
        }
    }

    void laserCharge(float ratio)
    {
        chargeEffect.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Math.Min(1, ratio * 2));
        chargeEffect.transform.rotation = Quaternion.Euler(0, 0, chargeEffect.transform.rotation.eulerAngles.z + 1800 * Time.deltaTime);

        if (ratio >= 0.5f)
        {
            chargeLaserEffect.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Math.Min(1, (ratio-0.5f) * 3));
            chargeLaserEffect.SetActive(true);
        }
    }

    void screenShake(float duration)
    {
        maincamera.transform.position = new Vector3(UnityEngine.Random.Range(-duration, duration), UnityEngine.Random.Range(-duration, duration), -10);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "window")
        {
            isInWindow = true;
        }
    }

    void OnDestroy()
    {
        screenShake(0.0f);
    }

    IEnumerator moveCoroutine()
    {
        float goalY = transform.position.y;
        transform.position += Vector3.up * 2;

        do
        {
            float angle = goalY - transform.position.y * -30;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.position += -transform.right * Time.deltaTime * 8;
            yield return null;
        } while (transform.rotation.eulerAngles.z > 5f && transform.rotation.eulerAngles.z < 355f);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        float speed = 8;
        while (speed > 0)
        {
            transform.position += -transform.right * Time.deltaTime * speed;
            speed -= Time.deltaTime * 16;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        float time = 0;
        float rotateSpeed = 0;
        while (time < 0.4f)
        {
            rotateSpeed -= 0.02f * rotateVelocity;
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotateSpeed);

            time += 0.02f;
            yield return new WaitForSeconds(0.02f);
        }
        time = 0;
        while (time < 1f)
        {
            rotateSpeed += 0.02f * rotateVelocity * 4;
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotateSpeed);

            time += 0.02f;
            yield return new WaitForSeconds(0.02f);
        }
        time = 0;
        //Coroutine coroutine = StartCoroutine(machineGun());
        while (time < 2f)
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotateSpeed);

            time += 0.02f;
            shot(1, 0);
            yield return new WaitForSeconds(0.02f);
        }
        //StopCoroutine(coroutine);
        while (rotateSpeed > 2)
        {
            rotateSpeed -= 0.02f * rotateVelocity * 2;
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotateSpeed);
            yield return new WaitForSeconds(0.02f);
        }
        time = 0;
        while (time < 4f)
        {
            // プレイヤーに背を向けるようにする
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Vector3 direction = player.transform.position - transform.position;
                float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f; // 180度反転
                float currentAngle = transform.rotation.eulerAngles.z;

                // 角度の差分を計算 (-180 から 180 の範囲に正規化)
                float angleDiff = targetAngle - currentAngle;
                while (angleDiff > 180) angleDiff -= 360;
                while (angleDiff < -180) angleDiff += 360;

                // 差分に比例した回転速度
                float rotateS = angleDiff * 3f * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 0, currentAngle + rotateS);
            }

            if (time > 1f)
            {
                laserCharge((time-1)/5);
            }

            time += Time.deltaTime;
            yield return null;
        }
        while (time < 6f) {
            laserCharge((time-1)/5);
            
            time += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Laser());
        
        yield return new WaitForSeconds(7f);

        float currentSpeed = 0;
        float currentRotate = transform.rotation.eulerAngles.z;
        float addRotate = 0;
        int exitRotateVec = UnityEngine.Random.Range(0f ,1f) > 0.5 ? 1 : -1;
        int exitRotate = 160;

        while(true)
        {
            transform.position += -transform.right * Time.deltaTime * currentSpeed;
            transform.rotation = Quaternion.Euler(0, 0, currentRotate + addRotate);
            if (Math.Abs(addRotate) < exitRotate) addRotate += exitRotateVec * Time.deltaTime * exitRotate;
            if (currentSpeed < 5) currentSpeed += Time.deltaTime * 5;
            yield return null;
        }
    }

    
    IEnumerator Laser()
    {
        laser.SetActive(true);
        chargeEffect.SetActive(false);
        chargeLaserEffect.SetActive(false);

        float scaleGoal = laser.transform.localScale.y;
        float scale = 0;

        while (scale < scaleGoal)
        {
            scale += Time.deltaTime * 10 * scaleGoal;
            laser.transform.localScale = new Vector3(laser.transform.localScale.x, scale, laser.transform.localScale.z);
            yield return null;
        }
        float time = 0;
        while (time < 2f)
        {
            transform.position += transform.right * Time.deltaTime * 1f;
            transform.Translate(UnityEngine.Random.Range(-0.05f, 0.05f), UnityEngine.Random.Range(-0.05f, 0.05f), 0);
            laser.transform.localScale = new Vector3(laser.transform.localScale.x, scaleGoal + Mathf.Sin(time * 100) * 0.1f, laser.transform.localScale.z);
            screenShake(0.1f);
            time += Time.deltaTime;
            yield return null;
        }
        while (scale > 0)
        {
            scale -= Time.deltaTime * 1 * scaleGoal;
            laser.transform.localScale = new Vector3(laser.transform.localScale.x, scale, laser.transform.localScale.z);
            screenShake(0.1f * scale / scaleGoal);
            yield return null;
        }
        laser.SetActive(false);

        yield return new WaitForSeconds(1f);
        laser.SetActive(false);
    }
}
