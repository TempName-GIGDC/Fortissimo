using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RaycastController : MonoBehaviour
{
    // 충돌을 감지할 레이어 마스크
    public LayerMask collisionMask;

    // 두께
    public const float skinWidth = 0.015f;

    // 가로 세로 Ray 개수
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    [HideInInspector]
    public float horizontalRaySpacing;
    [HideInInspector]
    public float verticalRaySpacing;

    [HideInInspector]
    public BoxCollider2D collider;
    public RaycastOrigins raycastOrigins;

    public virtual void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    // 레이 정점들 업데이트
    public void UpdateRaycastOrigins()
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
    public void CalculateRaySpacing()
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
    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }


}
