using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways] // 편집 모드와 플레이 모드 모두에서 실행되는 스크립트 설정
public class Coordinate_Labler : MonoBehaviour
{

    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();

    WayPoint waypoint;

    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        waypoint = GetComponentInParent<WayPoint>();
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
        }

        ColorCoordinate();
        ToggleLabels();

    }

    private void DisplayCoordinateLabel()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = "(" + coordinates.x + "," + coordinates.y + ")";

    }

    void UpdateTileName()
    {
        transform.parent.name = coordinates.ToString();

    }

    void ColorCoordinate()
    {
        if (waypoint.isPlaceAble)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
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
