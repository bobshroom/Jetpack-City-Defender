using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    float speed = 0.02f;
    public GameObject bulletPrefab; // 弾のプレハブを格納する変数
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* Keyboard.current.upArrowKey.isPressedで上矢印キーが押されているかを確認し、
           transform.position.y < 4.5fでプレイヤーのy座標が4.5未満であることを確認しています。
           これにより、プレイヤーが画面の上限を超えないようにしています。*/
        if (Keyboard.current.upArrowKey.isPressed && transform.position.y < 4.5f)
        {
            transform.Translate(0, speed, 0);
        }


        // スペースキーが押された際にshot()関数を呼び出す
        if (Keyboard.current.spaceKey.isPressed)
        {
            shot();
        }
    }


    void shot()
    {
        // ここにスペースキーが押された際の挙動を記述していく
        
    }
}