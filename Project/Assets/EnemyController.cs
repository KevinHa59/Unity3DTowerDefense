using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject[] Path;
    // Start is called before the first frame update
    void Start()
    {
        InitialPath();
        transform.LookAt(Path[nextPos].transform.position);
        MovingSpeed = Random.Range(MovingSpeed-1, MovingSpeed+3);
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    int pathLength;
    void InitialPath() {
        pathLength = GameObject.Find("Path").transform.childCount;
        Path = new GameObject[pathLength];
        for (int i = 0; i < pathLength; i++)
        {
            Path[i] = GameObject.Find("Path").transform.Find(i.ToString()).gameObject;
        }
        nextPos = 1;
        transform.position = Path[0].transform.position;
    }

    int nextPos;
    public int MovingSpeed;
    void Moving() {
        
        if (transform.position != Path[nextPos].transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, Path[nextPos].transform.position, Time.deltaTime * MovingSpeed);
        }
        else {
            
            if (nextPos < pathLength - 1) {
                nextPos++;
                transform.LookAt(Path[nextPos].transform.position);
            }
        }
    }

    void AirMoving() {
        Transform nextP = Path[nextPos].transform;
    }

    
}
