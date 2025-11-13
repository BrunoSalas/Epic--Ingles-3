using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerifyProduct : MonoBehaviour
{
    public Image image;
    public Color colorDisable;
    public Color colorEnable;
    public Products product;

    public GameObject foodPrefab;
    private void Start()
    {
        image = GetComponent<Image>();
    }
    private void Update()
    {
        VerifyQuantity();
    }
    public void VerifyQuantity()
    {
        if (ProductsInventory.instance.inventarioProductos[product] > 0)
        {
            image.color = colorEnable;
        }
        else
        {
            image.color = colorDisable;
        }
    }
    public void CreateFood()
    {
        if (ProductsInventory.instance.inventarioProductos[product] > 0)
        {
            foodPrefab.SetActive(true);
        }
    }
}
