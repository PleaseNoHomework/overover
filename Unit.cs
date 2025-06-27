using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [Header("기본 스탯")]
    public float maxHealth = 50f;
    public float moveSpeed = 5f;
    public float attackDamage = 10f;
    public float attackRange = 3f;
    public float attackCooldown = 1f;
    
    [Header("팀")]
    public bool isAttacker;
    
    protected float currentHealth;
    protected float lastAttackTime;
    protected GameObject currentTarget;
    
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        GameManager.Instance.RegisterUnit(this, isAttacker);
    }
    
    protected virtual void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing)
            return;
            
        FindTarget();
        
        if (currentTarget != null)
        {
            float distance = Vector2.Distance(transform.position, currentTarget.transform.position);
            
            if (distance > attackRange)
            {
                MoveTowardsTarget();
            }
            else if (Time.time - lastAttackTime > attackCooldown)
            {
                Attack();
            }
        }
    }
    
    protected abstract void FindTarget();
    
    protected virtual void MoveTowardsTarget()
    {
        if (currentTarget == null) return;
        
        Vector2 direction = (currentTarget.transform.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }
    
    protected abstract void Attack();
    
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    protected virtual void Die()
    {
        GameManager.Instance.UnregisterUnit(this, isAttacker);
        Destroy(gameObject);
    }
    
    protected void OnDrawGizmosSelected()
    {
        // 공격 범위 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
