using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("Game/PatrolOnAir")]
public class PatrolOnAir : GOAction
{

    [InParam("AiController")]
    private EnemyIAController enemyIAController;

    [InParam("CheckSurrounds")]
    private CheckSurrounds checkSurrounds;

    [InParam("PatrolDirection")]
    private bool IsHorizontal = true;

    private Vector3 input;

    public override void OnStart()
    {
        base.OnStart();

        if(IsHorizontal)
        {
            input =  Random.value > 0.5f ? Vector3.right : Vector3.left;
        }
        else
        {
            input =  Random.value > 0.5f ? Vector3.up : Vector3.down;
        }


    }

    public override TaskStatus OnUpdate()
    {
        CheckInvertInput();

        enemyIAController.input = input;

        return TaskStatus.RUNNING;
    }

    private void CheckInvertInput()
    {
        if (checkSurrounds.IsTouchingTop && input.y > 0 || checkSurrounds.IsTouchingBottom && input.y < 0)
            input.y *= -1;
        else if (checkSurrounds.IsTouchingRight && input.x > 0 || checkSurrounds.IsTouchingLeft && input.x < 0)
            input.x *= -1;

    }



}
