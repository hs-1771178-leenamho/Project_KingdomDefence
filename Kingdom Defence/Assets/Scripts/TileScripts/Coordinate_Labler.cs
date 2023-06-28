using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
[ExecuteAlways] // 편집 모드와 플레이 모드 모두에서 실행되는 스크립트 설정
public class Coordinate_Labler : MonoBehaviour
{

    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.magenta;
    [SerializeField] Color pathedColor = Color.red;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();

    GridManager gridManager;
    //WayPoint waypoint;

    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        gridManager = FindObjectOfType<GridManager>();
        //waypoint = GetComponentInParent<WayPoint>();
        label.enabled = false;
        DisplayCoordinateLabel();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            // do something
            DisplayCoordinateLabel();
            UpdateTileName();
            label.enabled = true;
        }

        SetLabelColor();
        ToggleLabels();

    }

    private void DisplayCoordinateLabel()
    {
        if(gridManager == null) return;

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.unityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.unityGridSize);

        label.text = "(" + coordinates.x + "," + coordinates.y + ")";

    }

    void UpdateTileName()
    {
        transform.parent.name = coordinates.ToString();

    }

    void SetLabelColor()
    {
        if(gridManager == null) return;
        
        Node node = gridManager.GetNode(coordinates);
        if(node == null) return;
        /*
        if (waypoint.isPlaceAble)
        
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
        }*/

        if(!node.isWalkable){
            label.color = blockedColor;
        }
        else if(node.isPathed){
            label.color = pathedColor;
        }
        else if(node.isExplored){
            label.color = exploredColor;
        }
        else{
            label.color = defaultColor;
        }
        
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
}
