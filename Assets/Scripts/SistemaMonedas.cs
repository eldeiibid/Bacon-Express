using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Bienvenidos al script que contiene el mejor sistema monetario $$ hasta la fecha, envidiado por los capitalistas y codiciado por los  grandes empresarios.
// Si Marx lo hubiera conocido se hubiera estado calladito. Trump y Elon Musk me lo quieren quitar de las manos, señores.
// Os presento:
// El Des-socialistador 

[System.Serializable]
public class SistemaMonedas : MonoBehaviour
{
    public static SistemaMonedas Instance;

    public event Action<int> OnCoinsChanged;

    [SerializeField] private TMP_Text coinsText;

    public int coins = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        OnCoinsChanged?.Invoke(coins);
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            OnCoinsChanged?.Invoke(coins);
            return true;
        }

        return false;
    }



    private void Start()
    {
        UpdateCoins(SistemaMonedas.Instance.coins);

        SistemaMonedas.Instance.OnCoinsChanged += UpdateCoins;
    }

    private void OnDestroy()
    {
        if (SistemaMonedas.Instance != null)
            SistemaMonedas.Instance.OnCoinsChanged -= UpdateCoins;
    }

    private void UpdateCoins(int amount)
    {
        coinsText.text = amount + "$";
    }
}

