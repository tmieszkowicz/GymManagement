namespace GymManagement.MediatorLibrary;

public interface IHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request);
}
