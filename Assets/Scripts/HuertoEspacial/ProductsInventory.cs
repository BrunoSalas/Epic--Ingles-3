using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductsInventory : MonoBehaviour
{
    public static ProductsInventory instance;

    public Dictionary<Products, int> inventarioProductos = new Dictionary<Products, int>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        inventarioProductos = new Dictionary<Products, int>()
        {
            { Products.colMorada, 0 },
            { Products.colBlanca, 0 },
            { Products.lechugaRepollo, 0 },
            { Products.lechugaCrespa, 0 },
            { Products.remolacha, 0 },
            { Products.culantro, 0 },
            { Products.perejil, 0 },
            { Products.apio, 0 },
            { Products.calabaza, 0 },
            { Products.pimiento, 0 },
            { Products.zanahoria, 0 },
            { Products.acelga, 0 },
            { Products.camote, 0 },
            { Products.amaranto, 0 },
            { Products.mashua, 0 },
            { Products.ocas, 0 }
        };
    }

    
}

public enum Products
{
    colMorada,
    colBlanca,
    lechugaRepollo,
    lechugaCrespa,
    remolacha,
    culantro,
    perejil,
    apio,
    calabaza,
    pimiento,
    zanahoria,
    acelga,
    camote,
    amaranto,
    mashua,
    ocas
}
