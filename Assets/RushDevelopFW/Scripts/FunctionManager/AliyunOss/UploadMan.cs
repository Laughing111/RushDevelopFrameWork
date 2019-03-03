using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UploadMan  {
    public string localPath;
    public string fileName;
    public Action<string> Method;
    public string text;
    public UploadMan(string localPath,string fileName,Action<string> Method, string text)
    {
        this.fileName = fileName;
        this.localPath = localPath;
        this.Method = Method;
        this.text = text;
    }
   
}
