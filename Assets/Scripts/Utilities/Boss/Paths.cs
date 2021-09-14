using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paths : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private List<Vector3> _Paths;
    //[SerializeField] private GameObject _Path;
    private float minDistanceToPoint = 1.5f;
    [SerializeField] private int _StartingPathPoint = 2; // Default start for boss

    public Vector3 CurrentPosition;
    private Vector3 StartPosition;
    // IEnumerators are used to fragment large collections or files, and to enable "iteration" features.
    private IEnumerator<Vector3> CurrentPoint;
    private float distanceToPoint;
    private bool gameStarted;

    public int _PathCount => _Paths.Count;

    //public Vector3 CurrentPoint => StartPosition + currentPoint.Current;

    // Start is called before the first frame update
    private void Start()
    {
        StartPosition = _Paths[_StartingPathPoint];
        CurrentPosition = StartPosition;
        
        //currentPoint = GetPoint();
        //currentPoint.MoveNext();

        //currentPosition = transform.position;
        //transform.position = currentPosition + currentPoint.Current;

        gameStarted = true;
    }

    // Update is called once per frame
    private void Update()
    {
        //if(path != null || path.Count > 0)
        //{
        //    ComputePath();
        //}
    }

    private void ComputePath()
    {
        distanceToPoint = (transform.position - (CurrentPosition + CurrentPoint.Current)).magnitude;

        if (distanceToPoint < minDistanceToPoint)
        {
            CurrentPoint.MoveNext();
        }
    }

    public Vector3 GetPathPoint(int index){
        return _Paths[index];
    }

    public IEnumerator<Vector3> GetPoint()
    {
        int index = 0;
        while (true)
        {
            yield return _Paths[index];

            // If there is another iterable (Vector3) in "path", continue to the next Vector3
            if (_Paths.Count <= 1)
            {
                continue;
            }

            // This seems wrong to me? Should it not go up there ^^
            index++;

            // If there are no more interables in path, move index to last point in Path
            if (index < 0)
            {
                index = _Paths.Count - 1;

                // End of Iterables, reset to 0
            }
            else if (index > _Paths.Count - 1)
            {
                index = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        //if(gameStarted){
        //    return;
        //}

        for (int i = 0; i < _Paths.Count; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + _Paths[i], 0.3f);

            if (i < _Paths.Count - 1)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position + _Paths[i], transform.position + _Paths[i + 1]);
            }

            if (i == _Paths.Count - 1)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position + _Paths[i], transform.position + _Paths[0]);
            }
        }
    }
}



