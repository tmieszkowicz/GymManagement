using GymManagement.Domain.Gyms;

namespace GymManagement.Application.Common.Interfaces;

public interface IGymsRepository
{
    Task AddGymAsync(Gym gym);
    Task<Gym?> GetByIdAsync(Guid id);
}
