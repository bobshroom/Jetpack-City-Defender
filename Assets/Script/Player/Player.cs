using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private float speed = 6;
    [SerializeField] private GameObject hpBar;
    private UnityEngine.UI.Image hpBarImage;
    private Vector3 hpBarPos;
    private float time;

    [SerializeField] private float floatingSpeed = 10f;
    [SerializeField] private float floatingRange = 1f;
    [SerializeReference] private float maxHp;
    private float hp;
    [SerializeField] private float invincibleTimeMax;
    private bool isInvincible = false;
    private float invincibleTime = 0;
    public float currentHp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        hp = maxHp;
        hpBarImage = hpBar.GetComponent<UnityEngine.UI.Image>();
        hpBarPos = hpBar.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        bool isMoveToHori = false;
        float angle = transform.rotation.eulerAngles.z;
        if (angle > 20) angle -= 360;
        if (angle < -20) angle += 360;

        time += Time.deltaTime;
        invincibleTime -= Time.deltaTime;
        
        if (Keyboard.current.upArrowKey.isPressed && transform.position.y < 4.3f)
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (Keyboard.current.downArrowKey.isPressed && transform.position.y > -4.3f)
        {
            transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
        }
        if (Keyboard.current.rightArrowKey.isPressed && transform.position.x < 8f)
        {
            isMoveToHori = true;
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Max(angle - Time.deltaTime * 50f, -10f));
        }
        if (Keyboard.current.leftArrowKey.isPressed && transform.position.x > -8f)
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

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "refLaser")
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            hp -= bullet.damage * Time.deltaTime;
            setHpBar(hp / maxHp, bullet.damage * Time.deltaTime);
        }
        if (invincibleTime > 0) return;
        if (collision.tag == "enemyBullet")
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            hp -= bullet.damage;
            invincibleTime = 2f;
            StartCoroutine(invisible());
            setHpBar(hp / maxHp, bullet.damage);
        }
    }

    void setHpBar(float ratio, float damage)
    {
        hpBarImage.fillAmount = ratio;
        StartCoroutine(damageBarShake(damage));
        if (ratio > 0.8f) hpBarImage.color = new Color(0, 1, 0);
        else if (ratio > 0.5f)
        {
            float rat = (ratio - 0.5f) * (1f/0.3f);
            hpBarImage.color = new Color(1-rat, 1, 0);
        }
        else if (ratio > 0.2f)
        {
            float rat = (ratio - 0.2f) * (1f/0.3f);
            hpBarImage.color = new Color(1, rat, 0);            
        }
        else hpBarImage.color = new Color(1, 0, 0);
    }

    IEnumerator invisible()
    {
        if (isInvincible) yield break;
        isInvincible = true;
        while (true) {
            sr.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.075f);
            sr.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.075f);
            if (invincibleTime < 0)
            {
                isInvincible = false;
                yield break;
            }
        }
    }

    IEnumerator damageBarShake(float damage)
    {
        float shake = damage * 0.005f;
        hpBar.transform.position = hpBarPos + new Vector3(UnityEngine.Random.Range(-shake, shake), UnityEngine.Random.Range(-shake, shake), 0);
        yield return new WaitForSeconds(0.05f);
        hpBar.transform.position = hpBarPos;
    }
}