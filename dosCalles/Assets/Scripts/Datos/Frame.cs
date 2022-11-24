using System;

// SERIALIZACIÓN 
[Serializable]
public class Frame
{
    public int frame;
    public Carro[] cars;
    public Semaphore[] semaphores;
}
