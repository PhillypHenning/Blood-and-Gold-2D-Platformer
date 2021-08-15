using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private List<Vector3> path;
    [SerializeField] private float minDistanceToPoint = 0.1f;


    private Vector3 currentPosition;
    private Vector3 startPosition;
    // IEnumerators are used to fragment large collections or files, and to enable "iteration" features.
    private IEnumerator<Vector3> currentPoint;
    private float distanceToPoint;
    private bool gameStarted;

    public Vector3 CurrentPoint => startPosition + currentPoint.Current;

    // Start is called before the first frame update
    private void Start()
    {
        startPosition = transform.position;

        currentPoint = GetPoint();
        currentPoint.MoveNext();

        currentPosition = transform.position;
        transform.position = currentPosition + currentPoint.Current;

        gameStarted = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if(path != null || path.Count > 0)
        {
            ComputePath();
        }
    }

    private void ComputePath(){
        distanceToPoint = (transform.position - (currentPosition + currentPoint.Current)).magnitude;
    
        if (distanceToPoint < minDistanceToPoint)
        {
            currentPoint.MoveNext();
        }
    }

    public IEnumerator<Vector3> GetPoint(){
        int index = 0;
        while(true){
            yield return path[index];
            
            // If there is another iterable (Vector3) in "path", continue to the next Vector3
            if(path.Count <= 1 ){
                continue;
            }

            // This seems wrong to me? Should it not go up there ^^
            index++;

            // If there are no more interables in path, move index to last point in Path
            if(index < 0){
                index = path.Count - 1;
            
            // End of Iterables, reset to 0
            }else if(index > path.Count - 1){
                index = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(gameStarted){
            return;
        }

        for (int i = 0; i < path.Count; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + path[i], 0.3f);

            if(i < path.Count - 1){
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position + path[i], transform.position + path[i+1]);
            }

            if(i == path.Count - 1){
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position + path[i], transform.position + path[0]);
            }
        }
    }
}
