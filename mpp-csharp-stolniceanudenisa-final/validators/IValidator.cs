namespace mpp_csharp_stolniceanudenisa_final.validators
{
    public interface IValidator<T>
    {
        public void Validate(T entity);
    }
}