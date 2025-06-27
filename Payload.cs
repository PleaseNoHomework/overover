//화물 시스템

using UnityEngine;
using System.Collections.Generic;

public class Payload : MonoBehaviour
{
    [Header("경로 설정")]
    public Transform[] waypoints;
    public float moveSpeed = 2f;
    
    [Header("상태")]
    public float health = 100f;
    private int currentWaypointIndex = 0;
    private bool isMoving = false;
    
    [Header("근처 유닛 감지")]
    public float detectionRadius = 5f;
    public LayerMask attackerLayer;
    
    private void Start()
    {
        if (waypoints.Length > 0)
            transform.position = waypoints[0].position;
    }
    
    private void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing)
            return;
            
        CheckNearbyAttackers();
        
        if (isMoving)
            MoveAlongPath();
    }
    
    private void CheckNearbyAttackers()
    {
        Collider2D[] attackers = Physics2D.OverlapCircleAll(transform.position, detectionRadius, attackerLayer);
        isMoving = attackers.Length > 0;
    }
    
    private void MoveAlongPath()
    {
        if (currentWaypointIndex >= waypoints.Length)
            return;
            
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex++;
            
            if (currentWaypointIndex >= waypoints.Length)
            {
                GameManager.Instance.PayloadReachedDestination();
            }
        }
    }
    
    public void TakeDamage(float damage)
    {
        GameManager.Instance.DamagePayload(damage);
    }
    
    private void OnDrawGizmosSelected()
    {
        // 감지 범위 표시
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        
        // 경로 표시
        if (waypoints != null && waypoints.Length > 1)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < waypoints.Length - 1; i++)
            {
                if (waypoints[i] != null && waypoints[i + 1] != null)
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
    }
}
