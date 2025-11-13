using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedsInventory : MonoBehaviour
{
    public static SeedsInventory instance;
    public Dictionary<Seed, int> inventorySeeds;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        inventorySeeds = new Dictionary<Seed, int>()
        {
            { Seed.ColMorada, 1 },
            { Seed.ColBlanca, 1 },
            { Seed.LechugaRepollo, 1 },
            { Seed.LechugaCrespa, 1 },
            { Seed.Remolacha, 1 },
            { Seed.Culantro, 1 },
            { Seed.Perejil, 1 },
            { Seed.Apio, 1 },
            { Seed.Calabaza, 1 },
            { Seed.Pimiento, 1 },
            { Seed.Zanahoria, 1 },
            { Seed.Acelga, 1 },
            { Seed.Camote, 1 },
            { Seed.Amaranto, 1 },
            { Seed.Mashua, 1 },
            { Seed.Ocas, 1 }
        };
    }
    public bool HaveSeeds(Seed seedSelected)
    {
        if (inventorySeeds.ContainsKey(seedSelected))
        {
            return inventorySeeds[seedSelected] > 0;
        }
        else
        {
            return false;
        }
    }
    public void ReduceSeeds(Seed seedSelected)
    {
        if (inventorySeeds.ContainsKey(seedSelected) && inventorySeeds[seedSelected] > 0)
        {
            inventorySeeds[seedSelected] --;
        }
    }
}
public enum Seed
{
    ColMorada,
    ColBlanca,
    LechugaRepollo,
    LechugaCrespa,
    Remolacha,
    Culantro,
    Perejil,
    Apio,
    Calabaza,
    Pimiento,
    Zanahoria,
    Acelga,
    Camote,
    Amaranto,
    Mashua,
    Ocas
}


