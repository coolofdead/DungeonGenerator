public interface ICarrier
{
    void Carry(ICarriable carriable);
    public bool CanCarry(ICarriable carriable);
}
