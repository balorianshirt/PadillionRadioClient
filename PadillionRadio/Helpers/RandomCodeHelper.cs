using System;
using System.Threading.Tasks;
using PadillionRadio.Business.Models;
using PadillionRadio.Business.Services;

namespace PadillionRadio.Helpers
{
    public static class RandomCodeHelper
    {
        public static async Task<UserModel> CodeGenerator(this UserModel model, IUserService service)
        {
            var r = new Random();
            
            model.Code = "radio" + Convert.ToChar(r.Next(97, 122)) +
                         r.Next(-100, 100).ToString() +
                         Convert.ToChar(r.Next(97, 122)) +
                         Convert.ToChar(r.Next(97, 122)) +
                         r.Next(-100, 100).ToString() +
                         r.Next(-100, 100).ToString() +
                         Convert.ToChar(r.Next(97, 122));
            
            var user = await service.Create(model);
            
            return model;
        }
    }
}