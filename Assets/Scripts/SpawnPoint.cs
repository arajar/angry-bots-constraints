using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

// all possible types of content
public enum ContentType
{
    Spider, EnemyMech, ConfusedMech, Buzzers, LargeBarrel, SmallBarrel, CrateA, CrateATall, CrateSmall, SmallHealth, MediumHealth, LargeHealth, None
}

public class SpawnPoint : MonoBehaviour
{
    // 

    // enemies
    public GameObject Spider;
    public GameObject EnemyMech;
    public GameObject ConfusedMech;
    public GameObject Buzzers;
    
    // static obstacles
    public GameObject LargeBarrel;
    public GameObject SmallBarrel;
    public GameObject CrateA;
    public GameObject CrateATall;
    public GameObject CrateSmall;

    // health packs
    public GameObject SmallHealth;
    public GameObject MediumHealth;
    public GameObject LargeHealth;

    private Dictionary<ContentType, GameObject> _contentTypeToContent;

	// Use this for initialization
	void Start () {
        _contentTypeToContent = new Dictionary<ContentType, GameObject>()
            {
                { ContentType.Buzzers, Buzzers },
                { ContentType.ConfusedMech, ConfusedMech },
                { ContentType.CrateA, CrateA },
                { ContentType.CrateATall, CrateATall },
                { ContentType.CrateSmall, CrateSmall },
                { ContentType.EnemyMech, EnemyMech },
                { ContentType.LargeBarrel, LargeBarrel },
                { ContentType.LargeHealth, LargeHealth },
                { ContentType.MediumHealth, MediumHealth },
                { ContentType.SmallBarrel, SmallBarrel },
                { ContentType.SmallHealth, SmallHealth },
                { ContentType.Spider, Spider },
                { ContentType.None, null },
            };
	}


    private GameObject _instantiated;

    /// <summary>
    /// Instantiates a type of content.
    /// </summary>
    public void Instantiate(ContentType type)
    {
        // if there is already an instantiated object, kill it
        if (_instantiated != null) Destroy(_instantiated);

        GameObject prefab;
        _contentTypeToContent.TryGetValue(type, out prefab);

        if (prefab != null)
        {
            _instantiated = (GameObject) Instantiate(prefab, transform.position, transform.rotation);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
