using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    private CameraManager() { }
    private WebCamDevice[] device;
    private static WebCamTexture te;
    private bool WebTex;
    public int GifIndex = 0;
    private static CameraManager cameraManager;
    public static CameraManager Instan()
    {
        if (cameraManager == null)
        {
            cameraManager = new CameraManager();
        }
        return cameraManager;
    }
    /// <summary>
    /// 摄像头开启
    /// </summary>
    public WebCamTexture CameraOpen(int width, int height, int fps)
    {
        Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            device = WebCamTexture.devices;
            Debug.Log("开启摄像头");
            if (device.Length > 0)
            {
                string camName = device[0].name;
                te = new WebCamTexture(camName, width, height, fps);
                te.wrapMode = TextureWrapMode.Repeat;
                te.Play();
            }
            else
            {
                Debug.Log("没有识别到摄像头");
            }
        } 
        return te;
    }
    /// <summary>
    /// 摄像头暂停
    /// </summary>
    public void CameraPause()
    {
        te.Pause();
    }
    /// <summary>
    /// 摄像头恢复
    /// </summary>
    public void CameraResume()
    {
        te.Play();
    }
    //调整尺寸
    public Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);
        float incX = (1.0f / (float)targetWidth);
        float incY = (1.0f / (float)targetHeight);
        for (int i = 0; i < result.height; ++i)
        {
            for (int j = 0; j < result.width; ++j)
            {
                Color newColor = source.GetPixelBilinear((float)j / (float)result.width, (float)i / (float)result.height);
                result.SetPixel(j, i, newColor);
            }
        }
        result.Apply();
        return result;
    }

    /// <summary>
    /// 摄像机关闭
    /// </summary>
    public void StopCamera()
    {
        te.Stop();
    }
}
