using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private string Speed = "speed";
    private string Jump = "jump";
    private string Die = "die";

    public void SetMovement(float speed)
    {
        _animator.SetFloat(Speed, speed);
    }

    public void SetJump()
    {
        _animator.SetTrigger(Jump);
    }

    public void SetDeath(bool isDead)
    {
        _animator.SetBool(Die, isDead);
    }
    
}
