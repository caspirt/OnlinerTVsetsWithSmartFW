using demo.framework.forms;
using OpenQA.Selenium;

namespace demo.framework.Forms
{
    public class ProductPage : BaseForm
    {
        //locator for detecting some TV page
        private static readonly By ProductPageLocator = By.XPath("//div[contains(@class,'product-specs')]");
        //locator for detecting Year of TV producing
        private static By byProductYear = By.XPath("//*[@id='specs']/div[2]/div/table/tbody[1]/tr[2]/td[2]/span");

        public ProductPage() : base(ProductPageLocator, "product Page")
        {
        }
        /// <summary>
        /// Get Year of manufacturing TV
        /// </summary>
        /// <returns></returns>
        public string  GetYear ()
        {
            //count number of results in search
            var Year = Browser.GetDriver().FindElements(byProductYear);
            int count = Year.Count;
            //return if result present
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
