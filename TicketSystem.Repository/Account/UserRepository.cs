using Dapper;
using TicketSystem.Common.DataAccess;
using TicketSystem.Common.Models.Entity;
using TicketSystem.Repository.Interfaces;

namespace TicketSystem.Repository.Account
{
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Query(string name)
        {
            Dictionary<int, User> lookup = new Dictionary<int, User>();

            var sql = @"SELECT u.userID, u.Name, r.roleID, r.Name roleName
                        FROM user u
                        LEFT JOIN userRole ur ON u.userID = ur.userID
                        LEFT JOIN role r ON r.roleID = ur.roleID
                        WHERE u.Name = @name";

            var users = await _unitOfWork.DbConnection.QueryAsync<(int userID, string userName, int roleID, string roleName)>(sql, new { name });

            foreach (var u in users)
            {
                if (!lookup.TryGetValue(u.userID, out User tempUser))
                {
                    tempUser = new User
                    {
                        UserID = u.userID,
                        Name = u.userName,
                    };

                    lookup.Add(u.userID, tempUser);
                }

                tempUser.Roles.Add(new Role
                {
                    RoleID = u.roleID,
                    Name = u.roleName,
                });
            }

            var user = lookup.FirstOrDefault().Value;
            return user;
        }
    }
}
