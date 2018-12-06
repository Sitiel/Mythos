using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleMenu : MonoBehaviour {

    public GameObject lrGameObject;
    public List<Image> imgBtn = new List<Image>();
    private List<MenuButton> buttons = new List<MenuButton>();
    private Vector2 mousePosition;
    private Vector2 fromVector2M = new Vector2(0.5f, 1.0f);
    private Vector2 centerCircle = new Vector2(0.5f, 0.5f);
    private Vector2 toVector2M;

    private int nbMenuItems;
    private int currentMenuItem;
    private int oldMenuItem;

    public Color buttonNormalColor;
    public Color buttonHiglightedColor;
    public Color buttonPressedColor;

    

    // Use this for initialization
    void Start () {
        InitialiseUI();
        currentMenuItem = 0;
        oldMenuItem = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetCurrentMenuItem();
        ButtonAction();
	}
    
    // use to initialse the UI with a given list of image, mostly used when you want to change the UI (like add a new object)
    void InitialiseUI(List<Image> imgs)
    {
        imgBtn = imgs;
        InitialiseUI();
    }

    // use to initialise the UI with the Unity parameter
    void InitialiseUI()
    {
        nbMenuItems = imgBtn.Count;
        for (int i = 0; i < nbMenuItems; i++)
        {
            buttons.Add(new MenuButton(imgBtn[i],buttonNormalColor,buttonHiglightedColor,buttonPressedColor));
        }
        DrawUI();
    }

    // use to draw the UI
    void DrawUI()
    {
        float angleSize = 360 / nbMenuItems;
        float angleStart = 0;
        for (int i =0; i < nbMenuItems; i++)
        {
            buttons[i].SetSceneImage(DrawButton(angleSize, angleStart,buttons[i].objectImage));
            angleStart += angleSize;
        }
        GameObject uiBackground = Instantiate(lrGameObject, transform);
        /*
        LineRenderer lr = uiBackground.GetComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.endWidth = 1f;
        lr.startWidth = 1f;
        float thetaPrecision = 20f;
        float radius = 3f;
        float theta = 0f;
        int nbVertice = (int)((1f / thetaPrecision) + 1f);
        lr.positionCount = nbVertice;
        for (int i = 0; i < nbVertice; i++)
        {

            theta += ((360) * Mathf.PI * thetaPrecision);
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);
            lr.SetPosition(i, new Vector3(x, y, 0));
        }
        uiBackground.transform.localScale = new Vector3(150f, 150f, 0f);*/
        
    }

    // angle is in degree (360 for a full circle)

    // use to draw each button of the UI
    GameObject DrawButton(float angle, float angleStart, Image img)
    {
        float thetaPrecision = 0.05f;
        float radius = 3f;
        float theta = 0f;

        GameObject imgButton = Instantiate(lrGameObject, transform);
        
        LineRenderer lr = imgButton.GetComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.endWidth = 0.01f;
        lr.startWidth = 0.01f;
        lr.loop = false;
        int nbVertice = (int)((1f / thetaPrecision) + 1f);
        lr.positionCount = nbVertice;
        for (int i =0; i < nbVertice; i++)
        {
            theta += ((angle/180) * Mathf.PI * thetaPrecision);
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);
            lr.SetPosition(i, new Vector3(y, x, 0));
        }
        imgButton.transform.localPosition = new Vector3(0f, 0f, 0f);
        imgButton.transform.localScale = new Vector3(50f, 50f, 0f);
        imgButton.transform.localEulerAngles = new Vector3(0f, 0f, angleStart);
        Instantiate(img,imgButton.transform);

        img.rectTransform.sizeDelta = new Vector2(150f, 150f);

        img.transform.localPosition = new Vector3(5f, 5f, 0f);
        
        img.transform.localRotation = Quaternion.identity;
        img.transform.localEulerAngles = new Vector3(0f, 0f, angleStart);
        img.transform.localPosition = new Vector3(Mathf.Cos((angleStart + (angle / 2) / 180) * Mathf.PI) * 5f, Mathf.Sin((angleStart + (angle / 2) / 180) * Mathf.PI) * 5f, 0f);
        img.transform.localScale = new Vector3(0.01f, 0.01f, 0f);
        
        Debug.Log("bbbb");

        return imgButton;
    }

    // use to get the position of the current item get by the mouse
    public void GetCurrentMenuItem()
    {
        mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        float angle = (Mathf.Atan2(Input.mousePosition.y - Screen.height / 2, Input.mousePosition.x - Screen.width / 2) - Mathf.PI / 2) /Mathf.PI*180;


        if(angle < 0)
        {
            angle += 360;            
        }
        currentMenuItem =(int) (angle / (360 / nbMenuItems));

        if (currentMenuItem != oldMenuItem)
        {
            buttons[oldMenuItem].ChangeColor(0);
            oldMenuItem = currentMenuItem;
            buttons[currentMenuItem].ChangeColor(1);
        }
    }

    // use to select the item under the mouse cursor
    public void ButtonAction()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GlobalVariables.player.SwitchWeaponNB(currentMenuItem);
            print(currentMenuItem);
        }
    }
}



// this class is used by the circle menu, it represent each button of the menu

[System.Serializable]
public class MenuButton
{
    private GameObject sceneObject;
    public Image objectImage;
    public Color normalColor = Color.white;
    public Color higlightedColor = Color.grey;
    public Color pressedColor = Color.grey;

    public MenuButton(Image img)
    {
        objectImage = img;
    }

    public MenuButton(Image img, Color normal, Color Higlighted, Color pressed)
    {
        objectImage = img;
        normalColor = normal;
        higlightedColor = Higlighted;
        pressedColor = pressed;
    }

    public void SetSceneImage(GameObject img)
    {
        sceneObject = img;
        LineRenderer gameObjectRenderer = sceneObject.GetComponent<LineRenderer>();
        Material newMat = new Material(Shader.Find("UI/Default"))
        {
            color = normalColor
        };
        gameObjectRenderer.material = newMat;


    }

    //used to change color of the game object, 0 for normal color, 1 for higlighted, 2 for pressed
    public void ChangeColor(int colorType)
    {
        MeshRenderer gameObjectRenderer = sceneObject.GetComponent<MeshRenderer>();
        
        switch (colorType)
        {
            case 0:
                gameObjectRenderer.material.color = normalColor;
                break;
            case 1:
                gameObjectRenderer.material.color = higlightedColor;
                break;
            case 2:
                gameObjectRenderer.material.color = pressedColor;
                break;
        }
        
    }

}
