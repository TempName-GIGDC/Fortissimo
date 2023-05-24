using UnityEngine;


public class CharacterController2D : RaycastController
{

    float maxClimbAngle = 80f;
    float maxDescendAngle = 80f;

    public CollisionInfo collisions;

    public override void Start()
    {
        base.Start();
    }

    public void Move(Vector3 velocity)
    {
        UpdateRaycastOrigins();

        // 기존 정보들 리셋
        collisions.Reset();
        collisions.velocityOld = velocity;

        HorizontalCheck(velocity);
        VerticalCheck(velocity);

        if (collisions.left || collisions.right)
            velocity.x = 0;

        if (collisions.above || collisions.below)
            velocity.y = 0;

        // 최종 움직임
        transform.Translate(velocity);
    }

    void HorizontalCheck(Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;

            rayOrigin += Vector2.up * (horizontalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {
                if (hit.transform.CompareTag("Wall"))
                {
                    if (directionX == -1)
                        collisions.left = true;
                    else if (directionX == 1)
                        collisions.right = true;
                }
                else
                {
                    collisions.climbingSlope = true;
                }
            }
        }
    }

    void VerticalCheck(Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;

            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {
                if (directionY == -1)
                    collisions.below = true;
                else if (directionY == 1 && hit.transform.CompareTag("Wall"))
                    collisions.above = true;
            }
        }
    }

    // 충돌 정보
    public struct CollisionInfo
    {
        // 하좌우 충돌 여부
        public bool above, below;
        public bool left, right;

        // 오르내리기 가능 여부
        public bool climbingSlope;
        public bool descendingSlope;

        // 경사각
        public float slopeAngle, slopeAngleOld;
        public Vector3 velocityOld;

        public void Reset()
        {
            above = below = false;
            left = right = false;
            climbingSlope = false;
            descendingSlope = false;

            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }
}