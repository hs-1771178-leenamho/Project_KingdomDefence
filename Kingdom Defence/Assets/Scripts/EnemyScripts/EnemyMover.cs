using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<WayPoint> pathList = new List<WayPoint>();
    [SerializeField] [Range(0,5)] float movingSpeed = 1f; // 적의 이동 속도, 0보다 작으면 안되기에 Range로 0을 미니멈으로 지정

    Enemy enemy;
    // Start is called before the first frame update
    void Start() {
        enemy = FindObjectOfType<Enemy>();
    }
    void OnEnable()
    {
        
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FindPath(){

        pathList.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach(Transform child in parent.transform){
            WayPoint waypoint = child.GetComponent<WayPoint>();
            if(waypoint != null)
                pathList.Add(waypoint);
        }

    }

    void ReturnToStart(){
        transform.position = pathList[0].transform.position;
    }
    
    void FinishPath(){
        if(enemy != null) enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath(){ // 지정된 경로를 따라 선형적으로 이동하는 함수
        foreach(WayPoint waypoint in pathList){
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0;
            transform.LookAt(endPosition);

            while(travelPercent < 1f){
                travelPercent += Time.deltaTime * movingSpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
            
        }
        
        FinishPath();
    }
}
