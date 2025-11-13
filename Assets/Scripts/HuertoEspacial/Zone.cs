using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public SelectSeed selectSeed;
    public SeedsInventory seedsInventory;
    //public GameObject seed;
    public TypeSeed typeSeedObject;
    //public SpatialNetworkObject seedPrefab;
    //public SpawnNetworkObjectRequest request;
    public bool inZone;
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E) && inZone && !typeSeedObject.verdureInZone && !typeSeedObject.isActivate)
        //{
        //    Plant();
        //}

    }

    public void PlantInteract()
    {
        if (!typeSeedObject.verdureInZone && !typeSeedObject.isActivate)
        {
            Plant();
        }
    }

    public void Plant()
    {
        bool haveSeeds= seedsInventory.HaveSeeds(selectSeed.actualSeed);
        if (haveSeeds && typeSeedObject.prefabsVerdure[(int)selectSeed.actualSeed])
        {
            Debug.Log("Hay semillas disponibles para: " + selectSeed.actualSeed);
            //seed.SetActive(true);
            typeSeedObject.typeSeed = selectSeed.actualSeed;
            typeSeedObject.isActivate = true;
            //typeSeedObject = Instantiate(seedPrefab, transform.position, Quaternion.identity).GetComponent<TypeSeed>();
            //typeSeedObject.Zone = this; 
            //seedsInventory.ReduceSeeds(selectSeed.actualSeed);
            //request = SpatialBridge.spaceContentService.SpawnNetworkObject(seedPrefab, transform.position, Quaternion.identity);
            //seedPlant.GetComponent<TypeSeed>().SelectTypeSeed(selectSeed.actualSeed);
        }
        else
        {
            Debug.Log("No hay semillas disponibles para: " + selectSeed.actualSeed);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AvatarLocal")) 
        {
            inZone = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AvatarLocal"))
        {
            inZone = false;
        }
    }
}
