using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageController : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown languageDropdown;
    private int languageValue;

    void Awake()
    {
        //Comprobar en las preferencias del jugador si hay una clave definida para el locale, si no poner el predeterminado, 0.
        if (PlayerPrefs.HasKey("LocaleKey"))
        {
            languageValue = PlayerPrefs.GetInt("LocaleKey", 0);
        }
        else
        {
            languageValue = 0;
            PlayerPrefs.SetInt("LocaleKey", languageValue);
        }

        languageDropdown.value = languageValue;
        ChangeLanguage();
    }

    public void ChangeLanguage()
    {
        languageValue = languageDropdown.value;
        //Poner como locale el valor del array de locales que se tiene en la clave.
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageValue];
        //Guardar clave en preferencias de jugador.
        PlayerPrefs.SetInt("LocaleKey", languageValue);
    }
}
