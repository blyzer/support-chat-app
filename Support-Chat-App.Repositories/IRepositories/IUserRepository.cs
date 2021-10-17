using Support_Chat_App.Data.Dtos;
using Support_Chat_App.Data.RequestModels;
using Support_Chat_App.Data.ResponseModels;
using System.Collections.Generic;

namespace Support_Chat_App.Repositories.IRepositories
{
    public interface IUserRepository
    {
        UserDto GetByEmail(string email);
        IEnumerable<UserDto> GetAll();
        UserDto GetById(long id);
        List<UserWithSeniority> GetByTeamType(long teamTypeId);
    }
}
