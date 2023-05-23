using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
    // 충돌을 감지할 레이어 마스크
    public LayerMask collisionMask;

    // 두께
    const float skinWidth = 0.015f;

    // 가로 세로 Ray 개수
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    float maxClimbAngle = 80;
    float maxDescendAngle = 80;

    float horizontalRaySpacing;
    float verticalRaySpacing;

    BoxCollider2D collider;
    RaycastOrigins raycastOrigins;
    public CollisionInfo collisions;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    public void Move(Vector3 velocity)
    {
        UpdateRaycastOrigins();

        // 기존 정보들 리셋
        collisions.Reset();
        collisions.velocityOld = velocity;

        // 경사에 따른 중력 적용
        if (velocity.y < 0)
        {
            DescendSlope(ref velocity);
        }
        // x축 움직임 충돌 체크
        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }
        // y축 움직임 충돌 체크
        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }

        // 최종 움직임
        transform.Translate(velocity);
    }

    // 수평 충돌 체크
    void HorizontalCollisions(ref Vector3 velocity)
    {
        // x축 이동 방향
        float directionX = Mathf.Sign(velocity.x);

        // Ray의 길이 = 속도 + 스킨두께
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        // Raycast
        for (int i = 0; i < horizontalRayCount; i++)
        {
            // 이동 방향에 따른 Ray 시작점
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;

            // 사전에 계산된 간격을 통해 Ray 시작점을 이동
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            // 레이 충돌(벽을 만났을 때)
            if (hit)
            {
                // 수직방향과 충돌 지점의 법선 벡터(면에 수직) 사이의 각도를 통한 경사각 계산
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

                // i == 0은 가장 바닥 Ray를 의미, 경사각이 최대 경사각보다 작을 경우
                if (i == 0 && slopeAngle <= maxClimbAngle)
                {
                    // 만약 경사를 내려가는 도중 이였다면
                    if (collisions.descendingSlope)
                    {
                        // 경사를 내려가는 도중이 아니게 됨
                        collisions.descendingSlope = false;
                        // 저장했던 이전 속도로 복원
                        velocity = collisions.velocityOld;
                    }

                    // 경사를 오르기 시작하는 거리
                    float distanceToSlopeStart = 0;

                    // 만약 경사의 각도가 이전 프레임의 경사각과 다르다면
                    if (slopeAngle != collisions.slopeAngleOld)
                    {
                        // 경사와의 거리
                        distanceToSlopeStart = hit.distance - skinWidth;

                        // x축 속도를 조정
                        velocity.x -= distanceToSlopeStart * directionX;
                    }

                    // 경사를 오르기 위해 속도를 조정
                    ClimbSlope(ref velocity, slopeAngle);
                    // distanceToSlopeStart 변수를 통해 줄인만큼 복원
                    velocity.x += distanceToSlopeStart * directionX;
                }

                // 만약 경사를 오르는 중이 아니거나, 경사각이 최대 오를 수 있는 경사각 보다 크면
                if (!collisions.climbingSlope || slopeAngle > maxClimbAngle)
                {
                    // x축 속도 조정(대상의 거리 비례)
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    rayLength = hit.distance;

                    // 경사를 오르는 중이였다면
                    if (collisions.climbingSlope)
                    {
                        // y축 속도 조정
                        velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                    }

                    // 이동 방향에 따른 충돌 체크
                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                }
            }
        }
    }

    // 수직 충돌 체크
    void VerticalCollisions(ref Vector3 velocity)
    {
        // y축 이동 방향
        float directionY = Mathf.Sign(velocity.y);

        // Ray의 길이 = 속도 + 스킨두께
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        // Raycast
        for (int i = 0; i < verticalRayCount; i++)
        {
            // 이동 방향에 따른 Ray 시작점
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;

            // 사전에 계산된 간격을 통해 Ray 시작점을 이동
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            // 레이 충돌
            if (hit)
            {
                // y축 속도 조정(대상의 거리 비례)
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                // 경사 올라가는 중
                if (collisions.climbingSlope)
                {
                    // x축 속도 보정
                    velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                }

                // 이동 방향에 따른 위 아레 충돌 체크
                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }

        // 경사 올라가는 중
        if (collisions.climbingSlope)
        {
            // x축 이동 방향
            float directionX = Mathf.Sign(velocity.x);

            // Ray의 길이
            rayLength = Mathf.Abs(velocity.x) + skinWidth;

            // 방향에 따른 Ray 시작점
            Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * velocity.y;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            // 레이 충돌
            if (hit)
            {
                // 각도 계산
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

                // 현재 각도가 이전 각도와 다르다면
                if (slopeAngle != collisions.slopeAngle)
                {
                    // x축 속도와 현재 각도 저장
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    collisions.slopeAngle = slopeAngle;
                }
            }
        }
    }

    // 경사로 올라갈 때 속도 조절
    void ClimbSlope(ref Vector3 velocity, float slopeAngle)
    {
        // 이동 거리
        float moveDistance = Mathf.Abs(velocity.x);

        // y축 이동 속도
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

        // 현재 y축 이동 속도와 경사각으로 새로 만든 y축 이동 속도 비교
        if (velocity.y <= climbVelocityY)
        {
            // 기존 속도에 덮어 씌우기
            velocity.y = climbVelocityY;

            // 새로 적용된 속도에 따른 x축 이동 속도 갱신
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);

            // 바닥 충돌 체크, 경사로 올라가는 중, 경사각 갱신
            collisions.below = true;
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngle;
        }
    }

    // 경사로 내려갈 때 속도 조절
    void DescendSlope(ref Vector3 velocity)
    {
        // x축 이동 방향
        float directionX = Mathf.Sign(velocity.x);

        // raycast 시작점을 결정하기 위한 삼항연산자(이동 방향의 반대 쪽에 Ray)
        Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

        // 레이 충돌
        if (hit)
        {
            // 수직방향과 충돌 지점의 법선 벡터(면에 수직) 사이의 각도를 통한 경사각 계산
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

            // 해당 경사가 내려갈 수 있는 경사면인지 확인
            if (slopeAngle != 0 && slopeAngle <= maxDescendAngle)
            {
                // 경사 방향과 움직이는 방향이 일치한지 확인
                if (Mathf.Sign(hit.normal.x) == directionX)
                {
                    // x축 이동 속도 * Tan(경사각)하면 경사각 만큼 대각선 이동시 x축 이동거리가 나옴
                    // 해당 물체까지의 거리 <= x축 이동 속도 * Tan(경사각) 
                    if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x))
                    {
                        // 이동거리
                        float moveDistance = Mathf.Abs(velocity.x);

                        // 이동거리에 따른 내려가는 속도
                        float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

                        // x축 이동 속도 설정 = cos(경사각) * 이동거리 * 이동 방향
                        velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                        velocity.y -= descendVelocityY;

                        // 경사각, 내려가는 중, 바닥 여부 설정
                        collisions.slopeAngle = slopeAngle;
                        collisions.descendingSlope = true;
                        collisions.below = true;
                    }
                }
            }
        }
    }

    // 레이 정점들 업데이트
    void UpdateRaycastOrigins()
    {
        // 콜라이더 정점을 가져온 후 skinWidth 만큼 축소
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        // 정점들을 업데이트
        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    // 레이 정점들 간격 계산
    void CalculateRaySpacing()
    {
        // 콜라이더 정점을 가져온 후 skinWidth 만큼 축소
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        // 가로 세로 레이의 개수 보정
        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        // 레이의 간격 계산
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    // 레이 캐스트를 위치를 잡기 위한 정점들
    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    // 충돌 정보
    public struct CollisionInfo
    {
        // 상하좌우 충돌 여부
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