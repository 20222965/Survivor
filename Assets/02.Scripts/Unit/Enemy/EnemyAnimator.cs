using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public enum State { Idle, Hit, Dead }
    public State CurState { get; set; }


    [SerializeField] RuntimeAnimatorController[] animatorController;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Transform target;

    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DeadHash = Animator.StringToHash("Dead");


    void Awake()
    {
        if (target == null) target = GameManager.Instance.Player.transform;
        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.IsPause) return;

        spriteRenderer.flipX = target.position.x < transform.position.x;
    }

    public void SetAnimatorController(int idx)
    {
        if(animatorController != null && animatorController.Length > idx)
            animator.runtimeAnimatorController = animatorController[idx];
    }

    public bool SetState(State nextState)
    {
        if (CurState == nextState) return false;

        switch (CurState)
        {
            case State.Dead:
                animator.SetBool(DeadHash, false);
                if (spriteRenderer.sortingOrder <= 1)
                    spriteRenderer.sortingOrder = 2;
                break;
            default:
                break;
        }

        switch (nextState)
        {
            case State.Hit:
                animator.SetTrigger(HitHash);
                break;
            case State.Dead:
                animator.SetBool(DeadHash, true);
                spriteRenderer.sortingOrder = 1;
                break;
            default:
                break;
        }

        CurState = nextState;

        return true;
    }

    public bool IsHit()
    {
        return animator.GetCurrentAnimatorStateInfo(0).shortNameHash == HitHash;
    }
}
