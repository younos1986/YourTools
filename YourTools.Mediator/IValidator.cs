namespace YourTools.Mediator;

public interface IValidator<in T>
{
    void Validate(T request);
}