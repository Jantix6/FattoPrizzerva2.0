using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudCambioCarasRioTutte : MonoBehaviour
{
    public List<Sprite> caras;
    public enum Caras {NORMAL, SORPRESAREAL, SORPRESAFINGIDO,SERIEDAD, HERIDO, DERROTA };
    public Caras currentCara = Caras.NORMAL;
    public Image image;

    public void ChangeCara(Caras _newCara)
    {
        if (caras.Count > 0)
        {
            switch (currentCara)
            {
                case Caras.NORMAL:
                    image.sprite = caras[0];
                    break;
                case Caras.SORPRESAREAL:
                    image.sprite = caras[1];
                    break;
                case Caras.SORPRESAFINGIDO:
                    image.sprite = caras[2];
                    break;
                case Caras.SERIEDAD:
                    image.sprite = caras[3];
                    break;
                case Caras.HERIDO:
                    image.sprite = caras[4];
                    break;
                case Caras.DERROTA:
                    image.sprite = caras[5];
                    break;
            }
        }
    }
}
