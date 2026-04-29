using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("0:プレイ中 1:ゲームオーバー 2:ゲームクリア")]
    public int gameState;
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if (gameState == 1) gameOverPanel.SetActive(true);
        if (gameState == 1 && Keyboard.current.rKey.wasPressedThisFrame) SceneManager.LoadScene("SampleScene");
        if (gameState == 2) gameClearPanel.SetActive(true);
    }
}
