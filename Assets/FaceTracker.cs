using UnityEngine;
using UnityEngine.UI;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.ObjdetectModule;
using OpenCVForUnity.UnityUtils;


public class FaceTracker : MonoBehaviour
{
    private WebCamTexture webcamTexture;
    private string path;
    private CascadeClassifier cascade;
    private MatOfRect faces;
    private Texture2D texture;
    private Mat rgbaMat;
    private Mat grayMat;

    private void Start()
    {
        //obtain cameras avialable
        WebCamDevice[] cam_devices = WebCamTexture.devices;
        //create camera texture
        webcamTexture = new WebCamTexture(cam_devices[0].name, 1024, 720, 30);
        //start camera
        webcamTexture.Play();

        //rotate RawImage according to rotation of webcamtexture
        this.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 360 - webcamTexture.videoRotationAngle));
        //store name of xml file
        path = Application.dataPath + "/haarcascade_frontalface_alt.xml";
        //initaliaze cascade classifier
        cascade = new CascadeClassifier();
        //load the xml file data
        if (!cascade.load(path))
        {
            Debug.LogError("The classification XML could not be loaded: " + path);
        }
        //initalize faces matofrect
        faces = new MatOfRect();
        //initialize rgb and gray Mats
        rgbaMat = new Mat(webcamTexture.height, webcamTexture.width, CvType.CV_8UC4);
        grayMat = new Mat(webcamTexture.height, webcamTexture.width, CvType.CV_8UC4);

        //initialize texture2d
        texture = new Texture2D(rgbaMat.cols(), rgbaMat.rows(), TextureFormat.RGBA32, false);
    }

    private void Update()
    {
        // convert webcamtexture to rgb mat
        Utils.webCamTextureToMat(webcamTexture, rgbaMat);
        //convert rgbmat to grayscale
        Imgproc.cvtColor(rgbaMat, grayMat, Imgproc.COLOR_RGBA2GRAY);
        //extract faces
        cascade.detectMultiScale(grayMat, faces);
        //store faces in array
        OpenCVForUnity.CoreModule.Rect[] rects = faces.toArray();
        
        //draw rectangle over all faces
        for (int i = 0; i < rects.Length; i++)
        {
            Debug.Log("detect faces " + rects[i]);
            Imgproc.rectangle(rgbaMat, new Point(rects[i].x, rects[i].y), new Point(rects[i].x + rects[i].width, rects[i].y + rects[i].height), new Scalar(255, 0, 0, 255), 2);
        }
        //convert rgb mat back to texture
        Utils.fastMatToTexture2D(rgbaMat, texture);

        //set rawimage texture
        this.GetComponent<RawImage>().texture = texture;
    }
}