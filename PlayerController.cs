using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("선택 설정")]
    public LayerMask selectableLayer;
    public GameObject selectionIndicatorPrefab;
    
    private List<Unit> selectedUnits = new List<Unit>();
    private Camera mainCamera;
    
    private void Start()
    {
        mainCamera = Camera.main;
    }
    
    private void Update()
    {
        HandleSelection();
        HandleMovement();
    }
    
    private void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, selectableLayer);
            
            if (hit.collider != null)
            {
                Unit unit = hit.collider.GetComponent<Unit>();
                if (unit != null && !unit.isAttacker) // 수비 유닛만 선택 가능
                {
                    if (!Input.GetKey(KeyCode.LeftShift))
                        ClearSelection();
                        
                    SelectUnit(unit);
                }
            }
            else if (!Input.GetKey(KeyCode.LeftShift))
            {
                ClearSelection();
            }
        }
    }
    
    private void HandleMovement()
    {
        if (Input.GetMouseButtonDown(1) && selectedUnits.Count > 0)
        {
            Vector2 targetPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            
            foreach (var unit in selectedUnits)
            {
                if (unit != null && unit is DefenderUnit)
                {
                    // 간단한 이동 명령 구현
                    // 실제로는 더 복잡한 이동 시스템이 필요할 수 있음
                    unit.GetComponent<DefenderUnit>().SetMoveTarget(targetPos);
                }
            }
        }
    }
    
    private void SelectUnit(Unit unit)
    {
        if (!selectedUnits.Contains(unit))
        {
            selectedUnits.Add(unit);
            // 선택 표시 추가
            unit.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
    
    private void ClearSelection()
    {
        foreach (var unit in selectedUnits)
        {
            if (unit != null)
                unit.GetComponent<SpriteRenderer>().color = Color.white;
        }
        selectedUnits.Clear();
    }
}
