using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebApplication1.models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static readonly IList<User> Users = new List<User>();

        [HttpGet]//Вывести
        public IEnumerable<User> Get()
        {
            return Users;
        }
        [HttpGet("{id:guid}")]//  api/Users/id - bba8385e-cb7f-4b62-8069-084826f8de21
        public User GetUser(Guid id)//Вывести и занести id как параметр
        {
            return Users.FirstOrDefault(x => x.Id == id);
        }
        [HttpGet("stats")]
        public UserStatisticResponseModel GetStatistic()
        {
            var result = new UserStatisticResponseModel()
            {
                Count = Users.Count,
                AvgRate = Users.Average(x => x.AvgRate),
            };
            return result;
        }

        [HttpPost]//Добавить
        public User Add(AddUserRequestModel model)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                AvgRate = model.AvgRate,
            };
            Users.Add(user);
            return user;
        }

        [HttpPut("{id:guid}")]//Изменить
        public User Update([FromRoute] Guid id, [FromBody] AddUserRequestModel model)
        {
            var targetUser = Users.FirstOrDefault(x => x.Id == id);
            if(targetUser != null)
            {
                targetUser.Name = model.Name;
                targetUser.AvgRate = model.AvgRate;
            }
            return targetUser;
        }

        [HttpDelete]//Удалить
        public bool Remove(Guid id)
        {
            var targetUser =  Users.FirstOrDefault(x => x.Id == id);
            if (targetUser != null)
            {
               return Users.Remove(targetUser);
            }
            return false;
        }
    }
}
