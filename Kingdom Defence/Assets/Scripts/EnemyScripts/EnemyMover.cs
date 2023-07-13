using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{

    [SerializeField][Range(0, 5)] float movingSpeed = 1f; // 적의 이동 속도, 0보다 작으면 안되기에 Range로 0을 미니멈으로 지정

    List<Node> pathList = new List<Node>();
    Enemy enemy;
    GridManager gridManager;
    PathFinding pathFinder;

    
    // Start is called before the first frame update
    void Awake()
    {
        enemy = FindObjectOfType<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinding>();
    }
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if(resetPath){
            coordinates = pathFinder.startPos;
        }
        else{
            coordinates = gridManager.GetCoordinatesFromPostion(transform.position);
        }

        StopAllCoroutines();
        pathList.Clear();
        pathList = pathFinder.GetNewPath(coordinates);

        StartCoroutine(FollowPath());

    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.startPos);
    }

    void FinishPath()
    {
        if (enemy != null) enemy.Penalty();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    { // 지정된 경로를 따라 선형적으로 이동하는 함수
        for(int i = 1; i < pathList.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(pathList[i].coordinates);
            float travelPercent = 0;
            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * movingSpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }

        }

        FinishPath();
    }
}
