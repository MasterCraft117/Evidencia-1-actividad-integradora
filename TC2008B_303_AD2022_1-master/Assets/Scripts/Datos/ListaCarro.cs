using System;

// SERIALIZACIÓN 

[Serializable]
public class ListaCarro
{
    public Carro[] carros;

    public Carro[] GetCarros()
    {
        return carros;
    }
}
