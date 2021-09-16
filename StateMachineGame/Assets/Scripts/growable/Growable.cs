using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
//[RequireComponent(typeof(SpriteRenderer))]
public class Growable : MonoBehaviour
{
    public delegate void StageChanged(object sender, System.EventArgs args);
    public delegate void GrowableDestroyed(System.EventArgs args);


    public event StageChanged OnStageUp;
    //event StageChanged OnStageDown;
    public event StageChanged OnFinalStage;
    public event GrowableDestroyed OnDestroyed;

    //animate the growable
    public Animator animator;

    //sprite renderer
    public SpriteRenderer spriteRenderer;

    //coroutines
    protected Dictionary<string, Coroutine> coroutines = new Dictionary<string, Coroutine>();

    //specific coroutine
    int growIndex = 0;

    //plants starting health
    public float startHealth = 10;

    //plant's current health
    public float health
    {
        get
        {
            return _health;
        }
        protected set
        {
            if (value > _health)
            {
                OnHealthChanged();
                OnHeal();
            }
            else
            if (value < _health)
            {
                OnHealthChanged();
                OnHurt();
            }
            _health = value >= maxHealth ? maxHealth : value <= 0 ? 0 : value;
            if (_health <= 0)
            {
                //growable has died
                OnGrowableDestroyed();
            }
        }
    }
    [SerializeField]
    protected float _health = 10f;

    //max health of plant. Based on starting health, growth stage, and health modifier.
    public float maxHealth
    {
        get
        {
            return startHealth + (stage * healthModifier);
        }
    }
    protected float healthModifier;

    //current stage of growth. Doesn't necesarilly mean it is ready to be harvested
    public int stage = 0;
    //how many stages of growth there are.
    public int stages = 5;
    //how much the plant has to grow on the first stage
    public float startGrowth = 10;
    //a modifier that changes growth amount per stage
    public float growthModifier = 1.2f;
    //determines base line for how much plant grows per cycle
    public float baseCycleGrowth = 1f;
    //a modifier that changes how much plant grows per cycle
    public float baseGrowthCycleModifier = 1f;
    public float growthCycleModifier = 1f;

    //how long a growth cycle takes in seconds
    public float cycleLength = 1f;
    public float stageGrowthSize //determines how much more the plant will have to grow each consecutive stage
    {
        get
        {
            return startGrowth + (stage * growthModifier);
        }
    }

    //whether can grow or not
    public bool canGrow = true;

    //how far plant is from finishing current stage of growth
    public float growth
    {
        get
        {
            return _growth;
        }
        protected set
        {
            if (value <= 0)
            {
                _growth = 0;
            }
            if (value >= stageGrowthSize) //next stage
            {
                _growth = 0;
                stage++;
                if (OnStageUp != null)
                {
                    OnStageUp(this, new System.EventArgs());
                }
                growthCycleModifier += UnityEngine.Random.Range(0f, 1f)*0.1f;
                if (stage >= stages)
                {
                    //stop growing
                    canGrow = false;
                    if (OnFinalStage != null)
                    {
                        OnFinalStage(this, new System.EventArgs());
                    }
                }
            }
            else
            {
                _growth = value;
            }
        }
    }
    [SerializeField]
    protected float _growth;

    protected virtual void Awake()
    {
        if (spriteRenderer == null)
        {
            GameObject gobject = GameObject.Instantiate(new GameObject(), transform);
            gobject.name = "Sprite";
            spriteRenderer = gobject.AddComponent<SpriteRenderer>();
            
        }
        StartGrow();
    }

    protected virtual void StageUp(object sender, System.EventArgs e)
    {
        //yay!
        //trigger animations here
    }

    protected virtual void FinalStage(object sender, System.EventArgs e)
    {
        //stop growing
        StopGrow();
    }

    protected virtual void Subscribe()
    {
        OnStageUp += StageUp;
        OnFinalStage += FinalStage;
        StartRoutine("grow", Grow());
    }
    protected virtual void Unsubscribe()
    {
        OnStageUp -= StageUp;
        OnFinalStage -= FinalStage;
        StopRoutines();
    }

    protected virtual void StartRoutine(string name, IEnumerator routine)
    {
        coroutines[name] = StartCoroutine(routine);
    }

    protected virtual void StopRoutine(string name)
    {
        StopCoroutine(coroutines[name]);
        coroutines.Remove(name);
    }

    protected virtual void StopRoutines()
    {
        List<string> remove = new List<string>();
        for (var i = coroutines.Values.Count-1; i>=0; i--)
        {
            var pair = coroutines.ToList()[i];
            var routine = pair.Value;
            if (routine != null)
            {
                StopCoroutine(routine);
            }
            remove.Add(pair.Key);
        }
        foreach (var key in remove)
        {
            coroutines.Remove(key);
        }
    }

    protected virtual void StartGrow()
    {
        StartRoutine("grow",Grow());

    }

    protected virtual void StopGrow()
    {
        StopRoutine("grow");
    }

    protected virtual IEnumerator Grow()
    {
        while (true)
        {
            if (canGrow)
            {
                growth += baseCycleGrowth * (UnityEngine.Random.Range(0.5f, 1.5f)*growthCycleModifier);
            }
            yield return new WaitForSeconds(cycleLength);
        }
    }

    protected virtual void OnHealthChanged()
    {

    }

    protected virtual void OnHeal()
    {

    }

    protected virtual void OnHurt()
    {

    }

    protected virtual void OnGrowableDestroyed()
    {
        Destroy(gameObject);
    }


    private void OnDestroy()
    {
        Unsubscribe();
        if (OnDestroyed != null)
        {
            OnDestroyed(new System.EventArgs());
            OnDestroyed = null;
        }
    }
}