using System;
using System.Collections.Generic;
using OpenCVForUnity.DnnModule;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class PlayerController: MonoBehaviour
    {
        public enum ControlStyle{
            Arrow,
            FaceLeaning,
            Face,
        }
        
        private OpenCVForUnity.CoreModule.Rect faceRect ;
        private CharacterController controller;
        private WebCamTexture webcamTexture;
        public ControlStyle controlStyle;
        private GlobalSettings _globalSettings;
        private GUIStyle _controlstyle =  new GUIStyle();
        private GUIStyle _scorestyle =  new GUIStyle();
        private int _score = 0;
        private int webcamWidth = 0;
        private int webcamHeight = 0;


        public void setWebcamDimension(int width, int height)
        {
            webcamWidth = width;
            webcamHeight = height;
        }

        public void setWebcamTexture(WebCamTexture webcamTexture)
        {
            this.webcamTexture = webcamTexture;
        }
        

        public void updateFaceRect(OpenCVForUnity.CoreModule.Rect faceRect)
        {
            this.faceRect = faceRect;
        }

        private float remap(float inputmin, float inputmax, float input, float outputmin, float outputmax)
        {
            float normal = Mathf.InverseLerp(inputmin, inputmax, input);
            float value = Mathf.Lerp(outputmin, outputmax, normal);
            return value;
        }
        
        private void Start()
        {
            _globalSettings = FindObjectOfType<GlobalSettings>();
            faceRect = new OpenCVForUnity.CoreModule.Rect();
            var collider = gameObject.GetComponent<CapsuleCollider>();

            controller = gameObject.AddComponent<CharacterController>();
            controller.height = collider.height;
            controller.radius = collider.radius;
            controller.center = collider.center;

            _controlstyle.fontSize = 20;
            _controlstyle.fontStyle = FontStyle.Bold;
            _controlstyle.normal.textColor = Color.red;
            _controlstyle.alignment = TextAnchor.MiddleCenter;
            
            _scorestyle.fontSize = 50;
            _scorestyle.font = (Font)Resources.Load("SuperMario256");
            _scorestyle.normal.textColor = Color.blue;
            _scorestyle.alignment = TextAnchor.MiddleCenter;
        }
        
        
        void OnGUI()
        {
            String _text = "";
            switch (controlStyle)
            {
                case ControlStyle.Arrow:
                    _text = "Arrow Control";
                    break;
                case ControlStyle.FaceLeaning:
                    _text = "Face + Leaning Control";
                    break;
                case ControlStyle.Face:
                    _text = "Face Control";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            GUI.Label(new Rect(Screen.width/2 -100, Screen.height -20, 200, 20), _text,_controlstyle);
            GUI.Label(new Rect(Screen.width/2 -100, 20, 200, 50), "Score " + _score*10,_scorestyle);
        }
        private void Update()
        {
            if (Input.GetKeyDown("r"))
            {
                webcamTexture.Stop();
                SceneManager.LoadScene("Exercise 03",LoadSceneMode.Single);
            }

            if (Input.GetKeyDown("1"))
            {
                controlStyle = ControlStyle.Arrow;
            }else if (Input.GetKeyDown("2"))
            {
                controlStyle = ControlStyle.FaceLeaning;
            }else if (Input.GetKeyDown("3"))
            {
                controlStyle = ControlStyle.Face;
            }
            
      
            Vector3 move = Vector3.one * 0.0001f;
            switch (controlStyle)
            {
                case ControlStyle.Arrow:
                    move.x = Input.GetAxis("Horizontal");
                    move.y = Input.GetAxis("Vertical");
                    break;
                case ControlStyle.FaceLeaning:
                    if (faceRect != null)
                    {

                        if (faceRect.area() > 0)
                        {
                            Vector3 face_center = new Vector3(faceRect.x + faceRect.width / 2, faceRect.y + faceRect.height / 2,0f);
                            float horizontal = remap(0, webcamWidth, face_center.x, -2, 2);
                            float vertical = remap(0, webcamHeight, faceRect.height, -2, 2);
                            move = new Vector3(-horizontal,-vertical, 0.0001f);
                        }
                    }
                    break;
                case ControlStyle.Face:
                    if (faceRect != null)
                    {

                        if (faceRect.area() > 0)
                        {
                            Vector3 face_center = new Vector3(faceRect.x + faceRect.width / 2, faceRect.y + faceRect.height / 2,0f);
                            float horizontal = remap(0, webcamWidth, face_center.x, -2, 2);
                            float vertical = remap(0, webcamHeight, face_center.y, -2, 2);
                            move = new Vector3(-horizontal,-vertical, 0.0001f);
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            controller.Move(move * (Time.deltaTime * _globalSettings.velocity));
        }
        
        private void FixedUpdate()
        {
            if (controller.collisionFlags != CollisionFlags.None)
            {
   
                //    SceneManager.LoadScene("Exercise 03",LoadSceneMode.Single);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Collectible"))
            {
                _score++;
            }
        }

        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            //Debug.Log(hit.collider.gameObject.layer);
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("SafeCollide"))
            {
                //Debug.Log(hit.collider.gameObject.layer);
            }else {
                _globalSettings.velocity = 0;
            }

            
        }
    }
    
    
}