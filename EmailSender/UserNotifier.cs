using EmailSender.EmailTemplates;
using ModelLibrary;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmailSender
{
    public class UserNotifier
    {
        private EmailSender _emailSender;
        private HttpClient _httpClient;

        public UserNotifier()
        {
            _emailSender = new EmailSender();
            _httpClient = new HttpClient();
        }
        public void NotifyUsersWhenPriceDropped (Item item, double oldPrice, List<User> users)
        {
            users.ForEach(user => _emailSender.SendEmail($"{item.Name} price dropped!", PriceDroppedEmailTemplate.GetPriceDroppedEmailTemplate(item, oldPrice, user), user.Email));
        }

        public async Task<List<User>> GetUsersForNotification(Item item)
        {
            List<User> users = new List<User>();

            var apiUrl = ConfigurationManager.AppSettings["api"] + "api/FavoriteItemUsers/" + item.Id;
            HttpResponseMessage response;
            response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                users = await response.Content.ReadAsAsync<List<User>>();
            }
            return users;
        }

    }
}
