using UnityEngine;
using System.Collections;

public enum Direction
{
    forward, left, right,none, idleleft,idleright, sharpleft,sharpright
};

public class Unit : MonoBehaviour
{
    public Transform hip;
    public Transform target;
    public float speed = 10;
    Vector3[] path;
    int targetIndex;

    //dont need to touch the waypoint itself, just an area around it
    public float areaTolerance;

    void Start()
    {
        RequestPath();
    }

    public void RequestPath()
    {
        this.GetComponent<AnimationManager>().forcestop = false;
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            if (Vector3.Distance(transform.position, currentWaypoint) <= areaTolerance) 
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    this.GetComponent<AnimationManager>().forcestop = true;                    
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }


           
            
            Direction dir = getDirection(currentWaypoint);
            this.GetComponent<AnimationManager>().ChangeAnim(dir);            
            yield return null;

        }
       
    }

    Direction getDirection(Vector3 currentTarget)
    {
        Direction d;
        Vector3 targetDirection = currentTarget - this.transform.position;
        Vector3.Normalize(targetDirection);
        float angle = Vector3.SignedAngle(hip.forward, targetDirection, Vector3.up);

        

        if (angle < 15 && angle > -15)
        {
            d = Direction.forward;
        }
        else if (angle >= 0)
        {
            if (angle >= 150)
            {
                d = Direction.idleright;
            }
            else if (angle >= 80)
            {
                d = Direction.sharpright;
            }
            else 
            {
                d = Direction.right;
            }
        }
        else
        {
            if (angle <= -150)
            {
                d = Direction.idleleft;
            }
            else if (angle <= -80)
            {
                d = Direction.sharpleft;
            }
            else
            {
                d = Direction.left;
            }
        }

        return d;
    }

    public void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawRay(transform.position, hip.forward * 5);

        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawSphere(path[i],areaTolerance);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
