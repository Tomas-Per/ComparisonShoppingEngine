using ModelLibrary;
using PathLibrary;
using System.IO;

namespace EmailSender.EmailTemplates
{
    public static class PriceDroppedEmailTemplate
    {
        public static string GetPriceDroppedEmailTemplate(Item item, double oldPrice, User user)
        {
            string template = File.ReadAllText(MainPath.GetMainPath() + @"\EmailSender\EmailTemplates\PriceDroppedEmailTemplate.txt");

            template = template.Replace("ITEM_IMAGE_LINK", item.ImageLink);
            template = template.Replace("ITEM_NAME", item.Name);
            template = template.Replace("ITEM_OLD_PRICE", oldPrice.ToString());
            template = template.Replace("ITEM_NEW_PRICE", item.Price.ToString());
            template = template.Replace("ITEM_LINK", item.ItemURL);
            template = template.Replace("USER_NAME", user.Username);

            //need to update this since we dont have our web yet
            //template = template.Replace("SHOP_LINK", "https://www.Libra.lt/")

            return template;
        }
    }
}
