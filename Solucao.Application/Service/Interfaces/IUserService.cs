using Solucao.Application.Contracts;
using Solucao.Application.Contracts.Requests;
using Solucao.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAll();

        Task<UserViewModel> GetById(string Id);

        Task<UserViewModel> GetByName(string Name);

        Task<ValidationResult> Add(User user);

        Task<ValidationResult> Update(User user, string id);

        Task<ValidationResult> ChangeUserPassword(UserViewModel user, string newPassword);

        Task<UserViewModel> Authenticate(string email, string password);

        Task<UserViewModel> GetByEmail(string email);
    }
}
