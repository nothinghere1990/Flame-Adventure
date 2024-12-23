using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed, turnSpeed;
    public Transform[] patrolPoints;
    private int currentPatrolPoint;
    private Vector3 moveDirection, lookTarget;
    private float yStore;

    public Rigidbody theRB;

    private PlayerController thePlayer;

    public enum EnemyState
    {
        idle,
        patrolling,
        chasing,
        returning
    };
    public EnemyState currentState;

    public float waitTime, waitChance;
    private float waitCounter;

    public float chaseDistance, chaseSpeed, lostDistance;

    public float waitBeforeReturning;
    private float returnCounter;
    
    private void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        
        foreach (Transform pp in patrolPoints)
        {
            pp.SetParent(null);
        }

        currentState = EnemyState.idle;

        waitCounter = waitTime;
    }

    private void Update()
    {
       switch (currentState)
        {
            case EnemyState.idle:
                
                yStore = theRB.velocity.y;
                theRB.velocity = new Vector3(0f, yStore, 0f);

                waitCounter -= Time.deltaTime;
                if (waitCounter <= 0)
                {
                    currentState = EnemyState.patrolling;
                    
                    NextPatrolPoint();
                }
                
                break;
            
            case EnemyState.patrolling:
                
                yStore = theRB.velocity.y;
                moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;

                //(1, 0, 1)
                moveDirection.y = 0f;
                moveDirection.Normalize();

                theRB.velocity = moveDirection * moveSpeed;
                theRB.velocity = new Vector3(theRB.velocity.x, yStore, theRB.velocity.z);

                if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) <= .1f)
                    NextPatrolPoint();
                else
                    lookTarget = patrolPoints[currentPatrolPoint].position;
                
                break;
            
            case EnemyState.chasing:

                lookTarget = thePlayer.transform.position;

                yStore = theRB.velocity.y;
                moveDirection = thePlayer.transform.position - transform.position;

                moveDirection.y = 0f;
                moveDirection.Normalize();

                theRB.velocity = moveDirection * chaseSpeed;
                theRB.velocity = new Vector3(theRB.velocity.x, yStore, theRB.velocity.z);

                if (Vector3.Distance(thePlayer.transform.position, transform.position) > lostDistance)
                {
                    currentState = EnemyState.returning;

                    returnCounter = waitBeforeReturning;
                }
                
                break;
            
            case EnemyState.returning:

                returnCounter -= Time.deltaTime;
                
                if (returnCounter <= 0)
                    currentState = EnemyState.patrolling;
                
                break;
        }

        if (Vector3.Distance(thePlayer.transform.position, transform.position) < chaseDistance)
            currentState = EnemyState.chasing;
        
        //Rotate
        lookTarget.y = transform.position.y;

        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(lookTarget - transform.position), 
            turnSpeed * Time.deltaTime);
    }

    private void NextPatrolPoint()
    {
        if (Random.Range(0f, 100f) < waitChance)
        {
            waitCounter = waitTime;
            currentState = EnemyState.idle;
        }
        else
        {
            currentPatrolPoint++;
            
            if (currentPatrolPoint >= patrolPoints.Length)
                currentPatrolPoint = 0;
        }
    }
}
