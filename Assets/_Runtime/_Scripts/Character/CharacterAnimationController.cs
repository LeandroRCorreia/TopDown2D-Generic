using UnityEngine;

public static class CharacterStringsConstrains
{
    public static readonly string SpeedX = "SpeedX";
    public static readonly string SpeedY = "SpeedY";
    public static readonly string IsOnGround = "IsOnGround";
    public static readonly string IsDead = "IsDead";
    public static readonly string DeathTrigger = "DeathTrigger";

}

public class CharacterAnimationController : MonoBehaviour
{
    private ICharacter character;
    protected Animator animator;

    #region AnimatorHashKeys

    private int SpeedX => Animator.StringToHash(CharacterStringsConstrains.SpeedX);
    private int SpeedY => Animator.StringToHash(CharacterStringsConstrains.SpeedY);
    private int IsOnGround => Animator.StringToHash(CharacterStringsConstrains.IsOnGround);
    private int IsDead => Animator.StringToHash(CharacterStringsConstrains.IsDead);
    private int DeathTrigger => Animator.StringToHash(CharacterStringsConstrains.DeathTrigger);

    #endregion

    protected virtual void Awake() 
    {
        character = GetComponent<ICharacter>();
        animator = GetComponentInChildren<Animator>();

    }

    private void Start() 
    {
        character.HealthControl.DeathEvent += OnDeath;
    }

    protected virtual void LateUpdate() 
    {
        MovementAnimator();
    }

    private void OnDeath()
    {
        animator.SetTrigger(DeathTrigger);
        animator.SetBool(IsDead, true);
    }

    private void MovementAnimator()
    {
        animator.SetFloat(SpeedX, Mathf.Abs(character.CharacterMovement.VelocityX));
        animator.SetFloat(SpeedY, character.CharacterMovement.SpeedY);
        animator.SetBool(IsOnGround, character.CheckSurrounds.IsTouchingBottom);
    }

}
