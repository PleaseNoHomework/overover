using UnityEngine;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("게임 설정")]
    public float payloadSpeed = 2f;
    public float payloadMaxHealth = 100f;
    
    [Header("팀 설정")]
    public int attackerUnitCount = 3;
    public int defenderUnitCount = 3;
    
    private float currentPayloadHealth;
    private List<Unit> attackerUnits = new List<Unit>();
    private List<Unit> defenderUnits = new List<Unit>();
    
    public enum GameState { Preparing, Playing, AttackerWin, DefenderWin }
    public GameState currentState = GameState.Preparing;
    
    public event Action<GameState> OnGameStateChanged;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    private void Start()
    {
        currentPayloadHealth = payloadMaxHealth;
        StartGame();
    }
    
    public void StartGame()
    {
        currentState = GameState.Playing;
        OnGameStateChanged?.Invoke(currentState);
    }
    
    public void RegisterUnit(Unit unit, bool isAttacker)
    {
        if (isAttacker)
            attackerUnits.Add(unit);
        else
            defenderUnits.Add(unit);
    }
    
    public void UnregisterUnit(Unit unit, bool isAttacker)
    {
        if (isAttacker)
        {
            attackerUnits.Remove(unit);
            if (attackerUnits.Count == 0 && currentState == GameState.Playing)
                EndGame(false);
        }
        else
        {
            defenderUnits.Remove(unit);
            if (defenderUnits.Count == 0 && currentState == GameState.Playing)
                EndGame(true);
        }
    }
    
    public void DamagePayload(float damage)
    {
        currentPayloadHealth -= damage;
        if (currentPayloadHealth <= 0 && currentState == GameState.Playing)
        {
            currentPayloadHealth = 0;
            EndGame(true);
        }
    }
    
    public void PayloadReachedDestination()
    {
        if (currentState == GameState.Playing)
            EndGame(false);
    }
    
    private void EndGame(bool attackerWins)
    {
        currentState = attackerWins ? GameState.AttackerWin : GameState.DefenderWin;
        OnGameStateChanged?.Invoke(currentState);
        Debug.Log($"게임 종료! {(attackerWins ? "공격팀" : "수비팀")} 승리!");
    }
    
    public float GetPayloadHealthPercentage()
    {
        return currentPayloadHealth / payloadMaxHealth;
    }
}
