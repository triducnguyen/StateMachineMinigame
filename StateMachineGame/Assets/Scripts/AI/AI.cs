using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
[System.Serializable]
public class AI : MonoBehaviour
{
    Coroutine behaviourCheck;

    public GameManager manager;

    //stats
    public float energy = 100;
    public float health = 100;
    public float hunger = 0;

    //position
    public Vector2 position2D
    {
        get => transform.position;
    }

    //behaviour

    //List<string> currentBehNames
    //{
    //    get => _currentBehNames;
    //    set
    //    {
    //        _currentBehNames = value;
    //        activeBehaviours = _currentBehNames.ToArray();
    //    }
    //}
    //List<string> _currentBehNames = new List<string>();
    public List<AIBehaviour> activeBehaviours
    {
        get { return _activeBehaviours; }
        protected set
        {
            if (activeBehaviours is object)
            {
                ExitBehaviours();
            }
            _activeBehaviours = value;
        } //exits the current behaviour before it is changed
    } //Current behaviours tells ai how to act

    public List<AIBehaviour> _activeBehaviours = new List<AIBehaviour>();

    public List<AIBehaviour> behaviours = new List<AIBehaviour>(); //list of possible behaviours

    //a*
    public PathFinder aStar;

    //AI Agro Collider
    public CircleCollider2D agro;



    protected virtual void Awake()
    {
        if (aStar is null)
        {
            aStar = GetComponent<PathFinder>();
        }
        //start behaviour check coroutine to make it happen less often
        behaviourCheck = StartCoroutine(CheckNew());
        if (manager is null)
        {
            manager = GameManager.instance;
        }
        if (!manager.ai.Contains(this))
        {
            manager.ai.Add(this);
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {


    }

    void CheckNewBehviours()
    {
        AIBehaviour next;
        //check enter conditions on all states
        foreach (var behaviour in behaviours)
        {
            if (!activeBehaviours.Contains(behaviour) && behaviour.CheckConditions(behaviour.enterConditions, out next))
            {
                //found a state to enter, add it to active behaviours
                EnterBehaviour(behaviour);
            }
        }

        //check exit conditions for active states
        for (int i = activeBehaviours.Count - 1; i >= 0; i--)
        {
            var behaviour = activeBehaviours[i];
            if (behaviour.CheckConditions(behaviour.exitConditions, out next))
            {
                ExitBehaviour(behaviour);
                if (next is object)
                {
                    EnterBehaviour(next);
                }
            }
        }
    }

    public void EnterBehaviour(AIBehaviour behaviour)
    {
        //add behaviour to active behaviours
        activeBehaviours.Add(behaviour);
        //activate the behaviour
        behaviour.OnEnter();
    }

    public void ExitBehaviour(AIBehaviour behaviour)
    {
        behaviour.OnExit();
        activeBehaviours.RemoveAt(activeBehaviours.IndexOf(behaviour));
    }

    public void ExitBehaviours()
    {
        for ( int i=activeBehaviours.Count; i>=0; i--)
        {
            var behaviour = activeBehaviours[i];
            behaviour.OnExit();
            activeBehaviours.RemoveAt(i);
        }
    }

    IEnumerator CheckNew()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            CheckNewBehviours();
        }
    }
}
