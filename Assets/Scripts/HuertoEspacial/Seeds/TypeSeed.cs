using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using SpatialSys.UnitySDK;
public class TypeSeed : MonoBehaviour
{
    public Seed typeSeed;
    public float timeNeedle;
    private float time;
    public int productsQuantity;

    //public NetworkVariable<string> syncedText = new();

    public TMP_Text textDisplay;
    private string timeLeftText;
    private bool isReady;
    private bool inZone;
    public bool isActivate;
    public bool verdureInZone;
    [HideInInspector] public Zone Zone;
    private bool isWater;

    [Header("Prefabs")]
    public GameObject[] prefabsVerdure;

    // Start is called before the first frame update
    void Start()
    {
        //SelectTypeSeed();
        //syncedText.value = typeSeed.ToString();
        textDisplay.text = typeSeed.ToString();
    }
    public void Update()
    {
        if (isActivate)
        {
            GrowProcces();
            //if (Input.GetKeyDown(KeyCode.E) && inZone && !verdureInZone)
            //{
            //    if (!isReady)
            //        isWater = true;
            //}
        }
        else
        {
            textDisplay.text = "";
        }
        
    }
    public void Water()
    {
        if (!verdureInZone)
        {
            if (!isReady)
                isWater = true;
        }
        
    }
    private void GrowProcces()
    {
        if (isWater && !verdureInZone)
        {
            if (timeNeedle > time)
            {
                timeNeedle -= Time.deltaTime;
                int timeLeftInt = Mathf.FloorToInt(timeNeedle);
                timeLeftText = timeLeftInt.ToString();
                textDisplay.text = typeSeed.ToString() + "\n" + timeLeftText;
            }
            else
            {
                textDisplay.text = typeSeed.ToString() + "\n" + "Listo";
                isReady = true;

                EnablePrefab();
                isActivate = false;
            }
        }
        else
        {
            textDisplay.text = typeSeed.ToString() + " Sin regar";
        }
        
    }
    private void EnablePrefab()
    {
        int seedIndex = (int)typeSeed;

        if (seedIndex >= 0 && seedIndex < prefabsVerdure.Length && prefabsVerdure[seedIndex] != null)
        {
            prefabsVerdure[seedIndex].SetActive(true);
            timeNeedle = 10f;
            isReady = false;
            verdureInZone = true;
        }
        else
        {
            Debug.LogError("No se encontró un prefab para esta semilla.");
        }
    }
    //public void Harvest()
    //{
    //    Products harvestedProduct = (Products)typeSeed;

    //    if (ProductsInventory.instance.inventarioProductos.ContainsKey(harvestedProduct))
    //    {
    //        ProductsInventory.instance.inventarioProductos[harvestedProduct]+= productsQuantity;
    //    }

    //    Debug.Log("Producto cosechado: " + harvestedProduct + ". Cantidad actual: " + ProductsInventory.instance.inventarioProductos[harvestedProduct]);
    //    PlantInteraction.instance.ClosePlantRecipesUI();
    //    timeNeedle = 10f;
    //    isReady= false;
    //    gameObject.SetActive(false);
    //}
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AvatarLocal") && isActivate)
        {
            inZone = true;
            //PlantInteraction.instance.DisplayPlantRecipesUI((Products)typeSeed);
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
