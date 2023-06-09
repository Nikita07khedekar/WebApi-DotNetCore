using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedProject;
using System.Text;
using System.Windows;
namespace UI.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View("Registration");
        }

        public async Task<UserManageResponse>  Save(RegisterViewModel model)
        {
            HttpClient client = new HttpClient();
            var jsonData =  JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData,Encoding.UTF8,"application/json");
            var response = await client.PostAsync("https://localhost:7140/api/Auth/Register",content);
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<UserManageResponse>(responseBody);

            if (responseObject.IsSuccess)
                return responseObject ;

            return responseObject;
        }
    }
}
