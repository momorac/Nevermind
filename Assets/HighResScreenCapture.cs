using UnityEngine;
using System.IO;

public class HighResScreenCapture : MonoBehaviour
{
    public int captureWidth = 3840;  // 캡처할 이미지의 너비 (고화질을 위해 큰 값 설정)
    public int captureHeight = 2160; // 캡처할 이미지의 높이 (고화질을 위해 큰 값 설정)
    public string folderPath = "C:\\Users\\user\\Desktop\\Naeun\\Unity\\Nevermind"; // 저장할 폴더

    void Update()
    {
        // 스페이스바를 누르면 캡처
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CaptureScreenshot();
        }
    }

    public void CaptureScreenshot()
    {
        // RenderTexture 생성
        RenderTexture rt = new RenderTexture(captureWidth, captureHeight, 24);
        Camera.main.targetTexture = rt;

        // 임시로 Camera의 출력물을 RenderTexture에 렌더링
        Texture2D screenShot = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
        Camera.main.Render();

        // RenderTexture에서 텍스처로 읽어들임
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, captureWidth, captureHeight), 0, 0);
        screenShot.Apply();

        // 바이트 배열로 변환 후 저장
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = ScreenShotName(captureWidth, captureHeight);
        File.WriteAllBytes(filename, bytes);

        // RenderTexture와 관련 리소스 해제
        Camera.main.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
        Destroy(screenShot);

        Debug.Log($"Screenshot saved: {filename}");
    }

    private string ScreenShotName(int width, int height)
    {
        // 현재 시간을 기반으로 파일 이름 생성
        return string.Format("{0}/screen_{1}x{2}_{3}.png", folderPath, width, height,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
}
