using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI 요소")]
    public Slider payloadHealthBar;
    public Text gameStateText;
    public Text unitCountText;
    
    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += UpdateGameStateUI;
    }
    
    private void Update()
    {
        UpdatePayloadHealthBar();
        UpdateUnitCount();
    }
    
    private void UpdatePayloadHealthBar()
    {
        if (payloadHealthBar != null)
        {
            payloadHealthBar.value = GameManager.Instance.GetPayloadHealthPercentage();
        }
    }
    
    private void UpdateGameStateUI(GameManager.GameState newState)
    {
        if (gameStateText != null)
        {
            switch (newState)
            {
                case GameManager.GameState.Playing:
                    gameStateText.text = "진행 중";
                    break;
                case GameManager.GameState.AttackerWin:
                    gameStateText.text = "공격팀 승리!";
                    break;
                case GameManager.GameState.DefenderWin:
                    gameStateText.text = "수비팀 승리!";
                    break;
            }
        }
    }
    
    private void UpdateUnitCount()
    {
        // 유닛 수 업데이트 로직
    }
}
