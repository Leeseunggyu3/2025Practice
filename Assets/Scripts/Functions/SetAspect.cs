using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MaintainAspectRatio : MonoBehaviour
{
    [SerializeField]
    private float targetAspect = 16f / 9f; // 원하는 화면 비율 (예: 16:9)

    void Start()
    {
        SetAspectRatio();
    }

    void SetAspectRatio()
    {
        // 현재 화면의 비율 계산
        float windowAspect = (float)Screen.width / Screen.height;

        // 현재 화면 비율과 목표 화면 비율 비교
        float scaleHeight = windowAspect / targetAspect;

        Camera camera = GetComponent<Camera>();

        if (scaleHeight < 1.0f)
        {
            // 세로가 더 길 때: 상하에 레터박스 추가
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            // 가로가 더 길 때: 좌우에 레터박스 추가
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }

    void OnValidate()
    {
        // 인스펙터에서 변경 시 바로 반영
        SetAspectRatio();
    }
}
