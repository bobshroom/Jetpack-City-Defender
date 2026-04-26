using Unity.VisualScripting;
using UnityEngine;

public class BackGroundMover : MonoBehaviour
{
    [SerializeField] GameObject[] backgrounds; // 背景オブジェクトの配列
    [SerializeField] GameObject[] backgrounds2; // 背景オブジェクトの配列
    [SerializeField] float[] speeds; // 各背景の移動速度の配列
    [SerializeField] float[] imageWidths;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<backgrounds.Length; i++)
        {
            backgrounds[i].transform.Translate(Vector3.left * speeds[i] * Time.deltaTime); // 背景を左に移動
            backgrounds2[i].transform.Translate(Vector3.left * speeds[i] * Time.deltaTime); // 背景を左に移動

            if (backgrounds[i].transform.position.x <= -imageWidths[i]) // 背景が画面外に出たら
            {
                backgrounds[i].transform.position += Vector3.right * imageWidths[i] * 2; // 背景を右に移動してループさせる
                backgrounds2[i].transform.position = new Vector3(0, backgrounds[i].transform.position.y, 0);
            }
            if (backgrounds2[i].transform.position.x <= -imageWidths[i]) // 背景が画面外に出たら
            {
                backgrounds2[i].transform.position += Vector3.right * imageWidths[i] * 2; // 背景を右に移動してループさせる
                backgrounds[i].transform.position = new Vector3(0, backgrounds2[i].transform.position.y, 0);
            }
        }
    }
}
