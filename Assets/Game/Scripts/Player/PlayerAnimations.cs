using UnityEngine;

public class PlayerAnimations
{
    private Animator _animator;

    public PlayerAnimations(Animator animator)
    {
        _animator = animator;
    }

    public void MoveAnimation(bool isMoving)
    {
        _animator.SetBool("IsMoving", isMoving);
    }
}
