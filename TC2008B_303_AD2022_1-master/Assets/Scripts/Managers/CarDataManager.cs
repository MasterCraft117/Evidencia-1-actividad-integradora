using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CarDataManager : MonoBehaviour
{

    [SerializeField]
    public Carro[] _listaDeCarros;

    public GameObject[] _carrosGO;

    [SerializeField]
    private RequestManager _rM;

    public ListaCarro _muchosCarros;

    public Carro[] _carroses;

    public static CarDataManager Instance;

   
    void Awake()
    {
        Instance = this;
    }

    void Start() 
    {
        _carrosGO = new GameObject[_listaDeCarros.Length];
        
        // activarlos por primera vez
        for(int i = 0; i < _listaDeCarros.Length; i++)
        {
            _carrosGO[i] = CarPoolManager.Instance.Activar(Vector3.zero);
        }

        PosicionarCarros();
    }

    public void PosicionarCarros() {

        _muchosCarros = _rM.darCarros();

        _carroses = _muchosCarros.GetCarros();

        // activar los 10 carritos en las posiciones congruentes
        for (int i = 0; i < _carroses.Length; i++) 
        {

            _carrosGO[i].transform.position = new Vector3(
                _carroses[i].x,
                _carroses[i].y,
                _carroses[i].z
            );
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            PosicionarCarros();
        }
  
    }

    public void EscucharSinArgumentos()
    {
        print("Evento sin argumentos");
    }

    public void EscucharConArgumentos(ListaCarro datos) 
    {
        print("Recibido: " + datos);
    }

}
