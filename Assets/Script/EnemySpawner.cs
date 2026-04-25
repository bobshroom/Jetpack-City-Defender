using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /* ゲームオブジェクトをシーンに生成するためのコードです。
           Startメソッド内に記述することで、ゲーム開始時に一度だけ実行されます。
           Updateメソッド内に記述すると、毎フレーム実行されてしまうため、敵が大量に生成されてしまいます。*/
        GameObject enemy = Instantiate(enemyPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
