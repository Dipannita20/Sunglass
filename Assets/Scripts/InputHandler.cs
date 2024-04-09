using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InputHandler : MonoBehaviour
{
    public GameObject Panel;
    public Image productImage;
    public TextMeshProUGUI ProductName;
    public TextMeshProUGUI ProductDescription;
    public TextMeshProUGUI ProductPrice;
    public TextMeshProUGUI ProductColor;
    public Sprite fallbackSprite;

    public GameObject FPSController;
    public GameObject staticCamera;

   // public GameObject AnimationController;


    public List<Sprite> imageList = new List<Sprite>();

    public float maxSpeed = 1;

    public GameObject TargetPosition;

    private Vector3 centerPos;

    bool ProductClicked;

    private GameObject productGameObject;

    private Vector3 OriginalPosition;

    private Vector3 OriginalRotation;

    private bool ProductSendBack;

    public static InputHandler instance;

    private bool IsStaticCameraActive = false;

    // Start is called before the first frame update
    void Start()
    {
        Panel.SetActive(false);

        instance = this;

        //ProductName = Panel.transform.Find("RoundedCorner").Find("ProductName").GetComponent<TextMeshProUGUI>();
        //ProductDescription = Panel.transform.Find("RoundedCorner").Find("ProductDescription").GetComponent<TextMeshProUGUI>();
        //ProductPrice = Panel.transform.Find("RoundedCorner").Find("ProductPrice").GetComponent<TextMeshProUGUI>();
        //ProductColor = Panel.transform.Find("RoundedCorner").Find("ProductColor").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ProductClicked && productGameObject != null)
        {
            PickUpAnimation();

        }

        if (ProductSendBack && productGameObject != null)
            RevertAnimation();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!FPSController.activeInHierarchy)
            {
                FPSController.SetActive(true);
                staticCamera.SetActive(false);
                IsStaticCameraActive = false;

                StartCoroutine(AnimationControl.instance.PlayAnimationClip(AnimationControl.instance.lip8, AnimationControl.instance.idle1, AnimationControl.instance.lipaudio8, 0.5f));

            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!FPSController.activeSelf)
                return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {

                if (hit.transform.name == "Chair")
                {
                    FPSController.SetActive(false);
                    staticCamera.SetActive(true);
                    IsStaticCameraActive = true;
                    StartCoroutine(UIController.instance.StartQuestion());

                }
                else
                {
                    
                    if (hit.transform.tag != "product" || productGameObject!= null) return;

                    ShowProductDetails(hit.transform);                

                }
            }
        }
    }

    
    public void ShowProductDetails(Transform transform)
    {

        FPSController.GetComponent<FirstPersonController>().enabled = false;

        ProductClicked = true;
        productGameObject = transform.gameObject;
        OriginalPosition = transform.position;
        OriginalRotation = transform.eulerAngles;

        StartCoroutine(RevertAnimationCor());

        //Make Panel active
        if (Panel != null)
        {
            Panel.SetActive(true);
        }

        //Fetch product detail
        var product = transform.GetComponent<ProductDetail>();
        ProductName.text = product.pruductname;
        ProductDescription.text = product.pruductdescription;
        ProductColor.text = "Color: " + product.productColor;
        ProductPrice.text = $"Rs {product.price.ToString()}";
        if (!string.IsNullOrEmpty(product.imageURL))
        {
            productImage.sprite = imageList.Find(a => a.name == product.imageURL);
        }
        else
        {
            productImage.sprite = fallbackSprite;
        }

    }

    private void PickUpAnimation()
    {
        if (productGameObject == null) return;

        if (!IsStaticCameraActive)
        {
            Debug.Log("Main");
            centerPos = Camera.main.ScreenToWorldPoint(
                new Vector3(Screen.width / 2, Screen.height / 2 + 50f, Camera.main.nearClipPlane + .2f));
        }
        else
        {
            Debug.Log("stic");
            centerPos = staticCamera.GetComponent<Camera>().ScreenToWorldPoint(
                new Vector3(Screen.width / 2, Screen.height / 2 + 50f, staticCamera.GetComponent<Camera>().nearClipPlane + .2f));
        }

        productGameObject.transform.position = Vector3.Lerp(productGameObject.transform.position, centerPos, Time.deltaTime * 10);

        productGameObject.transform.Rotate(Vector3.up * Time.deltaTime * 25);


    }

    public IEnumerator RevertAnimationCor()
    {
        yield return new WaitForSeconds(5f);

        ProductSendBack = true;

    }

    private void RevertAnimation()
    {
        FPSController.GetComponent<FirstPersonController>().enabled = true;

        ProductClicked = false;

        productGameObject.transform.position = Vector3.Lerp(productGameObject.transform.position, OriginalPosition, Time.deltaTime * 10);

        productGameObject.transform.eulerAngles = OriginalRotation;

        if (productGameObject.transform.position == OriginalPosition)
        {
            productGameObject = null;
            ProductSendBack = false;

            Panel.SetActive(false);
            
        }
    }
}