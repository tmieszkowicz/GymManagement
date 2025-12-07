namespace GymManagement.MediatorLibrary;

//TODO: maybe swap to ICommandHandler and IQueryHandler?
public interface IHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResultBase
{
    Task<TResponse> Handle(TRequest request);
}
