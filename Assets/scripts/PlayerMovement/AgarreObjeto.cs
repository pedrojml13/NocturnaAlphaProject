using UnityEngine;

public class AgarreObjeto : MonoBehaviour
{
    public Transform objeto;         // El objeto que vas a agarrar
    public Transform mano;           // El hueso o transform de la mano
public Transform puntoAnclaje; // Asigna aquí el GameObject vacío
    public bool agarrarConStart = true;

    void Start()
    {
        if (agarrarConStart)
            Agarrar();
    }



public void Agarrar()
{
    objeto.SetParent(puntoAnclaje);
    objeto.localPosition = Vector3.zero;
    objeto.localRotation = Quaternion.identity;
}


    public void Soltar()
    {
        objeto.SetParent(null);
    }
}