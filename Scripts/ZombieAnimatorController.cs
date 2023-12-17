using UnityEngine;

public class ZombieAnimatorController : MonoBehaviour
{
    private Animator zombieAnimator;

    void Start()
    {
        zombieAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TriggerFallAsleepAnimation();
        }
    }

    void TriggerFallAsleepAnimation()
    {
        if (zombieAnimator != null)
        {
            // Assuming you have a trigger parameter named "FallAsleep" in your Animator Controller.
            zombieAnimator.SetTrigger("FallAsleep");
            Debug.Log("FallAsleep trigger set!");
        }
    }
}
