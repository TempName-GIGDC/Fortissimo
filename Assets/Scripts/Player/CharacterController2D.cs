using System.Collections;
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

    public void Dash(Vector3 velocity)
    {
        UpdateRaycastOrigins();

        // 기존 정보들 리셋
        collisions.Reset();
        collisions.velocityOld = velocity;

        HorizontalCheck(ref velocity);
        VerticalCheck(ref velocity);

        if (collisions.floor || collisions.climbingSlope || collisions.descendingSlope)
            velocity.y = collisions.velocityOld.y;

        // 최종 움직임
        transform.Translate(velocity);
    }

    public void DownJump()
    {
        UpdateRaycastOrigins();
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.bottomLeft;

            rayOrigin += Vector2.right * (verticalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, skinWidth * 7f, collisionMask);

            if (hit && !hit.transform.CompareTag("Wall"))
            {
                StartCoroutine(SwitchLayer(hit.transform.gameObject));
            }
        }
    }

    IEnumerator SwitchLayer(GameObject g)
    {
        g.layer = 0;
        yield return new WaitForSeconds(0.3f);
        g.layer = 6;
    }

    public void Move(Vector3 velocity)
    {
        UpdateRaycastOrigins();

        // 기존 정보들 리셋
        collisions.Reset();
        collisions.velocityOld = velocity;

        HorizontalCheck(ref velocity);
        VerticalCheck(ref velocity);

        if (collisions.climbingSlope)
        {
            float distance = Mathf.Abs(velocity.x);
            velocity.y = Mathf.Sin(collisions.slopeAngle * Mathf.Deg2Rad) * distance;
            velocity.x = Mathf.Cos(collisions.slopeAngle * Mathf.Deg2Rad) * distance * Mathf.Sign(velocity.x);
        }
        else if (collisions.descendingSlope)
        {
            float distance = Mathf.Abs(velocity.x);
            velocity.y = -Mathf.Sin(collisions.slopeAngle * Mathf.Deg2Rad) * distance;
            velocity.x = Mathf.Cos(collisions.slopeAngle * Mathf.Deg2Rad) * distance * Mathf.Sign(velocity.x);
        }
        // 최종 움직임
        transform.Translate(velocity);
    }

    void HorizontalCheck(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = skinWidth * 6f;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;

            rayOrigin += Vector2.up * (horizontalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {

                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                print(slopeAngle);
                if (hit.transform.CompareTag("Wall"))
                {
                    if (directionX == -1)
                        collisions.left = true;
                    else if (directionX == 1)
                        collisions.right = true;

                    velocity.x = (hit.distance - skinWidth) * directionX;
                }
                else if (i == 0 && velocity.y < 0f && slopeAngle <= 90f)
                {
                    collisions.climbingSlope = true;
                    collisions.slopeAngle = slopeAngle;
                }
            }
        }
    }

    void VerticalCheck(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = skinWidth * 6f;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;

            rayOrigin += Vector2.right * (verticalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {
                if (directionY == -1)
                {
                    float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                    if (slopeAngle != 0 && slopeAngle <= maxDescendAngle)
                    {
                        if (i == 0)
                        {
                            rayOrigin = raycastOrigins.bottomLeft;
                            hit = Physics2D.Raycast(rayOrigin, Vector2.left, rayLength, collisionMask);
                            if (hit)
                            {
                                collisions.descendingSlope = true;
                                collisions.slopeAngle = slopeAngle;
                            }
                        }
                        else if (i == verticalRayCount - 1)
                        {
                            rayOrigin = raycastOrigins.bottomRight;
                            hit = Physics2D.Raycast(rayOrigin, Vector2.right, rayLength * 2f, collisionMask);
                            if (hit)
                            {
                                collisions.descendingSlope = true;
                                collisions.slopeAngle = slopeAngle;
                            }
                        }
                    }
                    else if (slopeAngle == 0 && !hit.transform.CompareTag("Stair"))
                    {
                        if (hit.transform.CompareTag("Wall"))
                            collisions.below = true;
                        else if (hit.transform.CompareTag("Floor"))
                            collisions.floor = true;
                        velocity.y = (hit.distance - skinWidth) * directionY;
                    }
                }
                else if (directionY == 1 && hit.transform.CompareTag("Wall"))
                {
                    collisions.above = true;
                    velocity.y = (hit.distance - skinWidth) * directionY;
                }
            }
        }
    }

    // 충돌 정보
    public struct CollisionInfo
    {
        // 하좌우 충돌 여부
        public bool above, below, floor;
        public bool left, right;

        // 오르내리기 가능 여부
        public bool climbingSlope;
        public bool descendingSlope;

        // 경사각
        public float slopeAngle, slopeAngleOld;
        public Vector3 velocityOld;

        public void Reset()
        {
            above = below = floor = false;
            left = right = false;
            climbingSlope = false;
            descendingSlope = false;

            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }
}