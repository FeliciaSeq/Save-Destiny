using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip replaceableAttackAnim;

    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;

    const float locomotionAnimationSmoothTime = 0.1f;


    //Reference to the NavMeshAgent
    NavMeshAgent agent;

    // Reference to the animator component
    protected Animator animator;

    protected CharacterCombat combat;
    public AnimatorOverrideController overrideController;






    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        if(overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }




        
        animator.runtimeAnimatorController = overrideController;

        currentAttackAnimSet = defaultAttackAnimSet;
        combat.OnAttack += OnAttack;
    }




    // Update is called once per frame
    void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

        animator.SetBool("inCombat", combat.InCombat);
    }

    protected virtual void OnAttack()
    {
        if (replaceableAttackAnim != null)
        {
            Debug.Log("replaceableAttackAnim assigned: " + replaceableAttackAnim.name);
            animator.SetTrigger("attack");

            // Check if the array has elements before accessing an index
            if (currentAttackAnimSet.Length > 0)
            {
                int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
                overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attackIndex];
            }
            else
            {
                Debug.LogError("currentAttackAnimSet is empty!");
            }
        }
        else
        {
            Debug.LogError("replaceableAttackAnim is not assigned.");
        }
    }
}

