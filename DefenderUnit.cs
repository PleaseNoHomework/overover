using UnityEngine;

public class DefenderUnit : Unit
{
    protected override void Start()
    {
        base.Start();
        isAttacker = false;
    }
    
    protected override void FindTarget()
    {
        // 수비자는 가장 가까운 공격자를 목표로 함
        GameObject[] attackers = GameObject.FindGameObjectsWithTag("Attacker");
        float closestDistance = float.MaxValue;
        GameObject closestAttacker = null;
        
        foreach (var attacker in attackers)
        {
            float distance = Vector2.Distance(transform.position, attacker.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestAttacker = attacker;
            }
        }
        
        currentTarget = closestAttacker;
    }
    
    protected override void Attack()
    {
        if (currentTarget == null) return;
        
        lastAttackTime = Time.time;
        
        if (currentTarget.TryGetComponent<Unit>(out Unit targetUnit))
        {
            targetUnit.TakeDamage(attackDamage);
        }
    }
}
