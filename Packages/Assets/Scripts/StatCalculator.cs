using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCalculator : MonoBehaviour
{
    public static StatCalculator Instance;
    public bool ListsInitialized;

    public double previousHighHealth;
    public double currentHighHealth;
    public double previousHighAbilityPower;
    public double currentHighAbilityPower;
    public double previousHighAtkPower;
    public double currentHighAtkPower;

    public List<double> xpCosts = new List<double>();
    public List<double> atkPowerLevels = new List<double>();
    public List<double> abilityPowerLevels = new List<double>();
    public List<double> healthLevels = new List<double>();

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }


        void Start()
    {
        if (!ListsInitialized)
        {
            InitializeLists();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeLists()
    {
        xpCosts.Add(MagicNumbers.Instance.startingXpCost);
        xpCosts.Add(MagicNumbers.Instance.nextXpCost);
        atkPowerLevels.Add(MagicNumbers.Instance.startingAtkPower);
        atkPowerLevels.Add(MagicNumbers.Instance.nextAtkPower);
        abilityPowerLevels.Add(MagicNumbers.Instance.startingAbilityPower);
        abilityPowerLevels.Add(MagicNumbers.Instance.nextAbilityPower);
        healthLevels.Add(MagicNumbers.Instance.startingHealth);
        healthLevels.Add(MagicNumbers.Instance.nextHealth);

        previousHighHealth = MagicNumbers.Instance.startingHealth;
        currentHighHealth = MagicNumbers.Instance.nextHealth;
        previousHighAbilityPower = MagicNumbers.Instance.startingAbilityPower;
        currentHighAbilityPower = MagicNumbers.Instance.nextAbilityPower;
        previousHighAtkPower = MagicNumbers.Instance.startingAtkPower;
        currentHighAtkPower = MagicNumbers.Instance.nextAtkPower;

        ListsInitialized = true;
    }
}
