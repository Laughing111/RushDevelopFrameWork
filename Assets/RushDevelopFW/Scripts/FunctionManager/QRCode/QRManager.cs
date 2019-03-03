using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class QRManager : MonoBehaviour
{

    /// <summary>
    /// 生成二维码
    /// </summary>
    /// <param name="textForEncoding">需要写入的字符串</param>
    /// <param name="width">二维码宽度</param>
    /// <param name="height">二维码高度</param>
    /// <returns></returns>
    public static Texture2D Encode(string textForEncoding, int width, int height)
    {  
        ZXing.Common.BitMatrix matrix = new MultiFormatWriter().encode(textForEncoding,BarcodeFormat.QR_CODE, width, height);
        //注意千万不能使用 writer，因为这个组件除了256 大小能正常输出以外，其他都不支持。
        Texture2D te = new Texture2D(width, height);
        // 下面这里按照二维码的算法，逐个生成二维码的图片， 
        // 两个for循环是图片横列扫描的结果 
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                if (matrix[x, y])
                {
                    te.SetPixel(y, x, Color.black);// 0xff000000;
                }
                else
                {
                    te.SetPixel(y, x, Color.white);// 0xffffffff;
                }
            }
        }
        te.Apply();
        return te;
    }
}
