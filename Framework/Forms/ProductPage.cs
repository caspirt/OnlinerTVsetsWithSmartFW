using demo.framework.forms;
using OpenQA.Selenium;

namespace demo.framework.Forms
{
    public class ProductPage : BaseForm
    {

        private static readonly By ProductPageLocator = By.XPath("//div[contains(@class,'product-specs')]");

        private static By byProductYear = By.XPath("//*[@id='specs']/div[2]/div/table/tbody[1]/tr[2]/td[2]/span");


        public ProductPage() : base(ProductPageLocator, "product Page")
        {
        }


        public string  GetYear ()
        {
            var Year = Browser.GetDriver().FindElements(byProductYear);

            int count = Year.Count;

            if (count > 0)
            {
                return Year[0].Text;
            }
            else
            {
                return "0";
            }

        }
    }
}
