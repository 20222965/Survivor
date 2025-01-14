using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAnimator : MonoBehaviour
{
    public enum State { Idle, Run, Dead }
    public State CurState { get; set; }

    [SerializeField] public RuntimeAnimatorController[] animatorController;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Player player;

    private static readonly int isRunHash = Animator.StringToHash("isRun");
    private static readonly int DeadHash = Animator.StringToHash("Dead");

    void Awake()
    {
        if(player == null) player = GetComponent<Player>();
        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void LateUpdate()
    {

        if (GameManager.Instance.IsPause)
            return;

        MoveAnimation();
    }

    private void MoveAnimation()
    {
        if (player.MoveDirection != Vector2.zero)
        {
            // 애니메이션 Run
            SetState(State.Run);
            // 왼쪽으로 이동하는 중이면 x flip
            spriteRenderer.flipX = player.MoveDirection.x < 0;
        }
        else
        {
            SetState(State.Idle);
        }
    }

    public void SetAnimatorController(int idx)
    {
        if (animatorController != null && animatorController.Length > idx)
            animator.runtimeAnimatorController = animatorController[idx];
    }

    public bool SetState(State nextState)
    {
        if (CurState == nextState) return false;

        switch (CurState)
        {
            case State.Run:
                animator.SetBool(isRunHash, false);
                break;
            default:
                break;
        }

        switch (nextState)
        {
            case State.Run:
                animator.SetBool(isRunHash, true);
                break;
            case State.Dead:
                animator.SetTrigger(DeadHash);
                break;
            default:
                break;
        }

        CurState = nextState;

        return true;
    }
}
