using UnityEngine;

public class AttackerUnit : Unit
{
    private Payload payload;
    
    protected override void Start()
    {
        base.Start();
        isAttacker = true;
        payload = FindObjectOfType<Payload>();
    }
    
    protected override void FindTarget()
    {
        // 공격자는 항상 화물을 목표로 함
        if (payload != null)
        {
            currentTarget = payload.gameObject;
        }
        else
        {
            // 수비 유닛을 찾아서 공격
            GameObject[] defenders = GameObject.FindGameObjectsWithTag("Defender");
            float closestDistance = float.MaxValue;
            GameObject closestDefender = null;
            
            foreach (var defender in defenders)
            {
                float distance = Vector2.Distance(transform.position, defender.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestDefender = defender;
                }
            }
            
            currentTarget = closestDefender;
        }
    }
    
    protected override void Attack()
    {
        if (currentTarget == null) return;
        
        lastAttackTime = Time.time;
        
        if (currentTarget.CompareTag("Payload"))
        {
            payload.TakeDamage(attackDamage);
            Debug.Log($"화물 공격! 남은 체력: {GameManager.Instance.GetPayloadHealthPercentage() * 100}%");
        }
        else if (currentTarget.TryGetComponent<Unit>(out Unit targetUnit))
        {
            targetUnit.TakeDamage(attackDamage);
        }
    }
}
