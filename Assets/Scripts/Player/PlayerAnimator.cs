using UnityEngine;

public enum PlayerAniState { Idle = 0, Walk, Run, Jump, Dash }

public class PlayerAnimator : MonoBehaviour
{
    public PlayerAniState animationState = PlayerAniState.Idle;
    PlayerAniState currentState;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentState = animationState;
    }

    // Update is called once per frame
    void Update()
    {
        if (animationState == currentState)
            return;

        if (animationState != PlayerAniState.Idle)
            animator.SetBool(animationState.ToString(), true);

        if (currentState != PlayerAniState.Idle)
            animator.SetBool(currentState.ToString(), false);

        currentState = animationState;
    }
}
