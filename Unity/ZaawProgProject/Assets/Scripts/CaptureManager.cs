using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.IO;
//using System.Drawing.Imaging;

public class CaptureManager : MonoBehaviour
{
    [Header("Recognition bools")]
    public bool palmFound = false;
    public bool fistFound = false;
    public bool pointingRightFound = false;
    public bool pointingLeftFound = false;
    private bool isPhotoTaken = false;
    public bool CanTakePhotos = false;

    [Header("Components")]
    private WebCamTexture webCamTexture;
    private string filePath = "Assets/HandFiles/handPhoto.png";

    [Header("Timer")]
    const float timer = 10.0f;
    float currentTime = 10.0f;

    void Start()
    {
        TestGestureRecognition();

        webCamTexture = new WebCamTexture();

        if (WebCamTexture.devices.Length > 0)
        {
            Debug.Log("Camera found!");
            CanTakePhotos = true;
        }
        else Debug.Log("Camera not found!");

        if (CanTakePhotos)
        {
            GetComponent<Renderer>().material.mainTexture = webCamTexture;
            webCamTexture.Play();
            currentTime = timer;
        }
    }

    private void OnApplicationQuit()
    {
        if (CanTakePhotos)
            webCamTexture.Stop();
    }

    private void TestGestureRecognition()
    {
        Debug.Log("aleft_palm.bmp");
        GetPalm("Assets/HandFiles/aleft_palm.bmp");
        GetFist("Assets/HandFiles/aleft_palm.bmp");
        GetLeft("Assets/HandFiles/aleft_palm.bmp");
        GetRight("Assets/HandFiles/aleft_palm.bmp");

        Debug.Log("fist.bmp");
        GetPalm("Assets/HandFiles/fist.bmp");
        GetFist("Assets/HandFiles/fist.bmp");
        GetLeft("Assets/HandFiles/fist.bmp");
        GetRight("Assets/HandFiles/fist.bmp");

        Debug.Log("Assets/HandFiles/lpalm.jpg");
        GetPalm("Assets/HandFiles/lpalm.jpg");
        GetFist("Assets/HandFiles/lpalm.jpg");
        GetLeft("Assets/HandFiles/lpalm.jpg");
        GetRight("Assets/HandFiles/lpalm.jpg");

        Debug.Log("Assets/HandFiles/rpalm.jpg");
        GetPalm("Assets/HandFiles/rpalm.jpg");
        GetFist("Assets/HandFiles/rpalm.jpg");
        GetLeft("Assets/HandFiles/rpalm.jpg");
        GetRight("Assets/HandFiles/rpalm.jpg");
    }

    private bool GetPalm(string filePath)
    {
        Image<Bgr, byte> imageFrame = new Image<Bgr, byte>(filePath); // path can be absolute or relative.
        Image<Gray, byte> grayFrame = imageFrame.Convert<Gray, byte>();

        CascadeClassifier haar = new CascadeClassifier("Assets/HandFiles/rpalm.xml");

        var hands = haar.DetectMultiScale(grayFrame);
        foreach (var hand in hands)
        {
            Debug.Log("Palm R found!");
            return true;
        }

        haar = new CascadeClassifier("Assets/HandFiles/lpalm.xml");
        foreach (var hand in hands)
        {
            Debug.Log("Palm L found!");
            return true;
        }

        return false;
    }

    private bool GetFist(string filePath)
    {
        Image<Bgr, byte> imageFrame = new Image<Bgr, byte>(filePath); // path can be absolute or relative.
        Image<Gray, byte> grayFrame = imageFrame.Convert<Gray, byte>();

        CascadeClassifier haar = new CascadeClassifier("Assets/HandFiles/fist.xml");

        var hands = haar.DetectMultiScale(grayFrame);
        foreach (var hand in hands)
        {
            Debug.Log("Fist found!");
            return true;
        }

        return false;
    }

    private bool GetLeft(string filePath)
    {
        Image<Bgr, byte> imageFrame = new Image<Bgr, byte>(filePath); // path can be absolute or relative.
        Image<Gray, byte> grayFrame = imageFrame.Convert<Gray, byte>();

        CascadeClassifier haar = new CascadeClassifier("Assets/HandFiles/left.xml");

        var hands = haar.DetectMultiScale(grayFrame);
        foreach (var hand in hands)
        {
            Debug.Log("Pointing left found!");
            return true;
        }
        return false;
    }

    private bool GetRight(string filePath)
    {
        Image<Bgr, byte> imageFrame = new Image<Bgr, byte>(filePath); // path can be absolute or relative.
        Image<Gray, byte> grayFrame = imageFrame.Convert<Gray, byte>();

        CascadeClassifier haar = new CascadeClassifier("Assets/HandFiles/right.xml");

        var hands = haar.DetectMultiScale(grayFrame);
        foreach (var hand in hands)
        {
            Debug.Log("Pointing right found!");
            return true;
        }
        return false;
    }

    //https://stackoverflow.com/questions/24496438/can-i-take-a-photo-in-unity-using-the-devices-camera
    IEnumerator TakePhoto()
    {
        yield return new WaitForEndOfFrame();

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        byte[] bytes = photo.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
        isPhotoTaken = true;
        Debug.Log("Photo taken!");
    }

    public void ResetGestureRec()
    {
        palmFound = false;
        fistFound = false;
        pointingRightFound = false;
        pointingLeftFound = false;
    }

    private void Update()
    {
        if (WebCamTexture.devices.Length <= 0)
        {
            webCamTexture.Stop();
            CanTakePhotos = false;
        }

        if (CanTakePhotos)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0.2f)
                Debug.Log("Get ready!");

            if (currentTime < 0)
            {
                StartCoroutine(TakePhoto());
                currentTime = timer;
            }

            if (isPhotoTaken)
            {
                palmFound = GetPalm(filePath);
                fistFound = GetFist(filePath);
                pointingRightFound = GetLeft(filePath);
                pointingLeftFound = GetRight(filePath);
                isPhotoTaken = false;
            }
        }
    }
}
