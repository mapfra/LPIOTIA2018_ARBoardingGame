using Assets.Common.HorseGame;
using Assets.Common.HorseGame.Board;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Horse : MonoBehaviour {
    
    bool isJumping = false;
    float teta = (float)Math.PI / 4;

    public Player Owner;
    public Square ActualSquare;
    public HorseDirection ActualDirection;
    Vector3 target = Vector3.zero;

    Rigidbody myRigidBody;

    bool isMoving = false;

    bool isInit = false;

    List<HorseDirection> workQueue = new List<HorseDirection>();

    public List<HorseDirection> WorkQueue
    {
        get
        {
            return (workQueue == null) ? new List<HorseDirection>() : workQueue;
        }

        set
        {
            workQueue = value;
            if (!IsMoving)
            {
                CheckForWork();
            }
        }
    }

    public Square ActualDestination
    {
        get
        {
            Square ret = ActualSquare;
            if (WorkQueue.Count > 0)
            {
                foreach (var step in WorkQueue)
                {
                    ret = ret.ChangeDirection(step);
                }
            }
            return ret;
        }
    }

    void CheckForWork()
    {

        
        if (WorkQueue.Count > 0 && isInit)
        {
            var TaskToDo = WorkQueue.First();

            ActualDirection = TaskToDo;
            changeDirection();
        }
        
    }

    public bool IsMoving
    {
        get
        {
            return isMoving;
        }
        set
        {
            isMoving = value;
            myRigidBody.isKinematic = !isMoving;
            GetComponent<BoxCollider>().isTrigger = !isMoving;
            if (!isMoving)
            {
                CheckForWork();
            }
        }
    }




	// Use this for initialization
	void Start () {


       

    }

    public void Init(Square InitSquare, Player player)
    {

        myRigidBody = this.GetComponent<Rigidbody>();

        //Effet anti Ragdoll
        myRigidBody.maxAngularVelocity = 0.1f;

        GetComponent<BoxCollider>().isTrigger = false;

        Owner = player;

        ActualSquare = InitSquare;

        transform.localPosition = new Vector3(ActualSquare.position.x, 370, ActualSquare.position.z);

        /*isJumping = false;
        isInit = false;
        IsMoving = false;*/
        isInit = false;
        myRigidBody.isKinematic = false;
        GetComponent<BoxCollider>().isTrigger = false;

        GameContext.Subscribe("VisiblePawnKilled", OnInvisibleCollision);

        target = ActualSquare.position;
        StartCoroutine(Correct());

        //ActualDirection = Direction.next;
        //changeDirection();

        //Debug.Log(transform.localPosition.x / 150f + "; " + transform.localPosition.z / 150f + "; " + ActualSquare.index);
    }

    public void Jump(Vector3 myTarget)
    {

        /*/ DEBUG BOARD

        Debug.Log(ActualSquare.index + " | x:" + ActualSquare.position.x + "; z:" + ActualSquare.position.z);
        if (ActualDirection == Direction.next && ActualSquare.next != null)
        {
            Debug.Log("Suivant: " + ActualSquare.next.index + " | x:" + ActualSquare.next.position.x + "; z:" + ActualSquare.next.position.z);
        }
        else if (ActualDirection == Direction.right && ActualSquare.next != null)
        {
            Debug.Log("Suivant: " + ActualSquare.right.index + " | x:" + ActualSquare.right.position.x + "; z:" + ActualSquare.right.position.z);
        }
        else if (ActualDirection == Direction.back && ActualSquare.next != null)
        {
            Debug.Log("Suivant: " + ActualSquare.back.index + " | x:" + ActualSquare.back.position.x + "; z:" + ActualSquare.back.position.z);
        }
        else
        {
            Debug.Log("Woups, pas de suivant :/");
        }
        Debug.Log("_________________________");
        
        //*/

        //Debug.Log("localPosition | x:" + (transform.localPosition.x / 150f) + "; z:" + (transform.localPosition.z / 150f));
        //Debug.Log("myTarget | x:" + (myTarget.x / 150f) + "; z:" + (myTarget.z / 150f));
        //Debug.Log("__________");


        var DeltaJump = myTarget - transform.localPosition;

        var rotation = Vector3.Cross(Vector3.up, DeltaJump.normalized).normalized;
        rotation = -(Quaternion.AngleAxis(teta, rotation) * DeltaJump).normalized;
        rotation = new Vector3(-rotation.x, rotation.y, -rotation.z);

        var direction = (DeltaJump.normalized + Vector3.up).normalized;
        double CoefVitesse = CalculCoef(DeltaJump.magnitude);

        myRigidBody.isKinematic = false;
        GetComponent<BoxCollider>().isTrigger = false;
        myRigidBody.velocity = (long)CoefVitesse  * rotation;

        //Debug.Log("norme: " + myRigidBody.velocity.magnitude);

        target = myTarget;
        isJumping = true;


        /*/ DEBUG PHYSICS
        
        Debug.Log("Position | x: " + transform.localPosition.x + "; z: " + transform.localPosition.z);
        Debug.Log("Target | x: " + myTarget.x + "; z: " + myTarget.z);
        Debug.Log("Delta | x: " + DeltaJump.x + "; z: " + DeltaJump.z);
        Debug.Log("Direction | x: " + direction.x + "; y=  " + direction.y + "; z: " + direction.z);
        Debug.Log("Vitesse | coef: x:" + rigidbody.velocity.x + "; y:" + rigidbody.velocity.y + "; z:" + rigidbody.velocity.z);
        Debug.Log("_________________________");
        
        //*/

    }
    

    public IEnumerator Correct()
    {
        
        var deltaX = target.x - transform.localPosition.x;
        var deltaZ = target.z - transform.localPosition.z;

        var nbSteps = 40;
        /*Debug.Log("arrivee avant correction: | x:" + (transform.localPosition.x / 150f) + "; z:" + (transform.localPosition.z / 150f));
        Debug.Log("target: | x:" + (target.x / 150f) + "; z:" + (target.z / 150f));*/



        for (int i = 0; i< nbSteps; i++)
        {
            transform.localPosition += deltaX / nbSteps * Vector3.right + deltaZ / nbSteps * Vector3.forward;
            yield return new WaitForSeconds(0.5f / nbSteps);
        }

        /*Debug.Log("arrivee: | x:" + (transform.localPosition.x / 150f) + "; z:" + (transform.localPosition.z / 150f));
        Debug.Log("___________");*/

        int antiRagdollCounter = 0;

        while (myRigidBody.velocity.magnitude > 0.02 || myRigidBody.angularVelocity.magnitude > 0.02)
        {
            
            yield return new WaitForSeconds(0.2f);

            antiRagdollCounter++;
            if (antiRagdollCounter > 4)
            {
                myRigidBody.velocity = Vector3.zero;
                myRigidBody.angularVelocity = Vector3.zero;
            }
        }

        var temp = GameContext.Occupied(ActualSquare);
        if (temp != null)
        {
            if (!(temp is VisiblePawn))
            {
                GameContext.Submit("PawnKilled", temp);
            }
        }
        if (ActualSquare.index == 80)
        {
            Pawn me = Owner.Pawns.FirstOrDefault(p => {
                if (p is VisiblePawn && ((VisiblePawn)p).horse == this)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            });
            Owner.Pawns.Remove(me);
            GameObject.Destroy(this.gameObject);

            if (Owner.Pawns.Count == 0)
            {
                GameContext.Victory = true;
            }
        }
        if (WorkQueue.Count > 0 && isInit) WorkQueue.RemoveAt(0);
        if (!isInit) isInit = true;

        if (WorkQueue.Any())
        {
            CheckForWork();
        }
        else
        {
            IsMoving = false;
            GameContext.Submit("MoveDone");
        }

    }

    public void changeDirection ()
    {
        //Debug.Log("index_source " + ActualSquare.index + " | x:" + ActualSquare.position.x + "; z:" + ActualSquare.position.z);

        //Debug.Log("1: " + ActualSquare.index);

        
        ActualSquare = ActualSquare.ChangeDirection(ActualDirection);
        //Debug.Log("2: " + ActualSquare.index);

        //Debug.Log("index_destination " + ActualSquare.index + " | x:" + ActualSquare.position.x + "; z:" + ActualSquare.position.z);
        //Debug.Log("______________________");


        Jump(ActualSquare.position);
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            
            if (isJumping)
            {
                isJumping = false;

                StartCoroutine(Correct());
            }
        }
       
    }

    void OnTriggerEnter(Collider collider)
    {
        if ((collider.gameObject.tag.Equals("Horse") && myRigidBody.velocity.magnitude == 0) || collider == null) 
        {
            
            WhenKilled();
        }

        
    }

    void OnInvisibleCollision(System.Object parameters)
    {
        var indexParam = (int)parameters;
        if (transform.GetSiblingIndex() == indexParam)
        {
            GameContext.UnSubscribe("VisiblePawnKilled", OnInvisibleCollision);
            WhenKilled();
        }
    }

    void WhenKilled()
    {
        Square SpawnSquare = GameContext.Stables[Owner.boardIndex];

        if (GameContext.Occupied(SpawnSquare) != null)
        {
            SpawnSquare = SpawnSquare.next;
        }

        var newHorse = GameObject.Instantiate(gameObject, GameContext.BoardTransform);

        newHorse.GetComponent<Horse>().Init(SpawnSquare, Owner);
        ((VisiblePawn)Owner.Pawns.FirstOrDefault(p => ((VisiblePawn)p).horse = this)).horse = newHorse.GetComponent<Horse>();

        GameObject.Destroy(gameObject);
    }
    


    public double CalculCoef(double JumpLength)
    {
        return Math.Sqrt((double)JumpLength * (double)Physics.gravity.magnitude / (Math.Sin(2 * (double)teta)))*3d/4d;

    }

    void OnCollisionExit()
    {

    }

    #region DEPRECATED
    /*
        public long calc()
        {
            var V0 = initSpeed;
            var NV0 = Math.Pow(V0.magnitude, 2);
            Debug.Log("V0 ^2 = " + NV0);

            var teta2 = Vector3.Angle(initSpeed, Vector3.right);
            var sin = Math.Sin(teta2 * 4 * Math.PI / 360);
            Debug.Log("sin(2*teta) = " + sin);

            var g = Physics.gravity;
            var GR = g.magnitude;
            Debug.Log("g = " + GR);
            Debug.Log("g = " + GR);

            //Debug.Log("numerateur = " )

            var calcValue = NV0 * sin / GR;

            return (long)calcValue;
        }
    */
    #endregion
}
