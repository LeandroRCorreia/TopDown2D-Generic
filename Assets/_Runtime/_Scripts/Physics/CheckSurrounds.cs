using UnityEngine;
using UnityEngine.Assertions;

[ExecuteAlways]
[RequireComponent(typeof(Collider2D))]
public class CheckSurrounds : MonoBehaviour, IColliderInfo
{

    #region ColliderInfo

    private Collider2D _currentCollider;

    private Collider2D CurrentCollider => _currentCollider ?? GetComponent<Collider2D>();

    public Vector3 ColliderCenter => CurrentCollider.bounds.center;
    public Vector3 ColliderExtents => CurrentCollider.bounds.extents;

    private Vector3[] colLocalPointsRight;
    private Vector3[] colLocalPointsLeft;
    private Vector3[] colLocalPointsDown;
    private Vector3[] colLocalPointsUp;

    public Vector3 GetColliderUp => ColliderCenter + Vector3.up * ColliderExtents.y;

    public Vector3 GetColliderBottom =>  ColliderCenter + Vector3.down * ColliderExtents.y;

    public Vector3 GetColliderRight => ColliderCenter + Vector3.right * ColliderExtents.x;

    public Vector3 GetColliderLeft => ColliderCenter + Vector3.left * ColliderExtents.x;

    #endregion

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] [Range(0.025f, 1f)] private float raysLength = 0.1f;



    public bool IsTouchingTop {get; private set;}
    public bool IsTouchingBottom {get; private set;}

    public bool IsTouchingRight {get; private set;}
    public bool IsTouchingLeft {get; private set;}

    public bool IsTouchingHorizontal => IsTouchingRight || IsTouchingLeft;
    public bool IsTouchingVertical => IsTouchingBottom || IsTouchingTop;

    private void Awake() 
    {
        _currentCollider = GetComponent<Collider2D>();
    }

    private void Start() 
    {
        Assert.IsNotNull(_currentCollider, "_CurrentCollider cannot be null");
        UpdateColliderInfo();
    }

    private void UpdateColliderInfo()
    {
        Vector3 getColliderRightLocalPosition = GetColliderRight - transform.position;
        colLocalPointsRight = new Vector3[]
        {
            getColliderRightLocalPosition + Vector3.up * ColliderExtents.y,
            getColliderRightLocalPosition,
            getColliderRightLocalPosition + Vector3.down * ColliderExtents.y,
        };

        Vector3 getColliderLeftLocalPosition = GetColliderLeft - transform.position;
        colLocalPointsLeft = new Vector3[]
        {
            getColliderLeftLocalPosition + Vector3.up * ColliderExtents.y,
            getColliderLeftLocalPosition,
            getColliderLeftLocalPosition + Vector3.down * ColliderExtents.y,
        };

        Vector3 getColliderBottomLocalPosition = GetColliderBottom - transform.position;
        colLocalPointsDown = new Vector3[]
        {
            getColliderBottomLocalPosition + (Vector3.right * ColliderExtents.x),
            getColliderBottomLocalPosition,
            getColliderBottomLocalPosition + (Vector3.left * ColliderExtents.x)
        };

        Vector3 getColliderUpLocalPosition = GetColliderUp - transform.position;
        colLocalPointsUp = new Vector3[]
        {
            getColliderUpLocalPosition + (Vector3.right * ColliderExtents.x),
            getColliderUpLocalPosition,
            getColliderUpLocalPosition + (Vector3.left * ColliderExtents.x)
        };

    }

    private void Update()
    {
        UpdateCheckSurrounds();

    }

    private void UpdateCheckSurrounds()
    {
        UpdateHorizontal();
        UpdateVertical();
    }

    private void UpdateHorizontal()
    {
        IsTouchingLeft = CheckAround(Vector3.left, colLocalPointsLeft);
        IsTouchingRight = CheckAround(Vector3.right, colLocalPointsRight);

    }

    private void UpdateVertical()
    {
        IsTouchingBottom = CheckAround(Vector3.down, colLocalPointsDown);
        IsTouchingTop = CheckAround(Vector3.up, colLocalPointsUp);
        
    }

    private bool CheckAround(Vector3 rayDir, Vector3[] localPoints)
    {
        int hitCount = 0;
        foreach (var localPoint in localPoints)
        {
            var rayOrigin = transform.position + localPoint;

            if (Physics2D.Raycast(rayOrigin, rayDir, raysLength, whatIsGround))
            {
                Debug.DrawRay(rayOrigin, rayDir * raysLength, Color.green);
                ++hitCount;
                continue;
            }
            Debug.DrawRay(rayOrigin, rayDir * raysLength, Color.red);
        }

        return hitCount > 1;
    }

}
