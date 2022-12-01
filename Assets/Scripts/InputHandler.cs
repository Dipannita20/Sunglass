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

    public List<Sprite> imageList = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        Panel.SetActive(false);

        //ProductName = Panel.transform.Find("RoundedCorner").Find("ProductName").GetComponent<TextMeshProUGUI>();
        //ProductDescription = Panel.transform.Find("RoundedCorner").Find("ProductDescription").GetComponent<TextMeshProUGUI>();
        //ProductPrice = Panel.transform.Find("RoundedCorner").Find("ProductPrice").GetComponent<TextMeshProUGUI>();
        //ProductColor = Panel.transform.Find("RoundedCorner").Find("ProductColor").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!FPSController.activeInHierarchy){
                FPSController.SetActive(true);
                staticCamera.SetActive(false);
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
                    StartCoroutine(UIController.instance.StartQuestion());
                }
                else
                {

                    //Make Panel active
                    if (Panel != null)
                    {
                        Panel.SetActive(true);
                    }

                    //Fetch product detail
                    var product = hit.transform.GetComponent<ProductDetail>();
                    ProductName.text = product.pruductname;
                    ProductDescription.text = product.pruductdescription;
                    ProductColor.text = "Color: " + product.productColor;
                    ProductPrice.text = $"Rs {product.price.ToString()}";
                    Debug.Log(hit.transform.name);
                    if (!string.IsNullOrEmpty(product.imageURL))
                    {
                        productImage.sprite = imageList.Find(a => a.name == product.imageURL);
                    }
                    else
                    {
                        productImage.sprite = fallbackSprite;
                    }
                }
            }
        }
    }
}
