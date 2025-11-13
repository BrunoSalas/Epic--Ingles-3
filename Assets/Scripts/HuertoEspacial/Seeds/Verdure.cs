using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verdure : MonoBehaviour
{
    public Products typeProduct;
    public int productsQuantity;
    private bool inZone;
    public TypeSeed typeSeed;
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E) && inZone)
        //{
        //        Harvest();
        //}
    }
    public void Harvest()
    {
        Products harvestedProduct = typeProduct;

        if (ProductsInventory.instance.inventarioProductos.ContainsKey(harvestedProduct))
        {
            ProductsInventory.instance.inventarioProductos[harvestedProduct] += productsQuantity;
        }

        Debug.Log("Producto cosechado: " + harvestedProduct + ". Cantidad actual: " + ProductsInventory.instance.inventarioProductos[harvestedProduct]);
        //PlantInteraction.instance.ClosePlantRecipesUI();
        typeSeed.verdureInZone = false;
        gameObject.SetActive(false);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AvatarLocal"))
        {
            inZone = true;
            //PlantInteraction.instance.DisplayPlantRecipesUI(typeProduct);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AvatarLocal"))
        {
            inZone = false;
            //PlantInteraction.instance.ClosePlantRecipesUI();
        }
    }
}
