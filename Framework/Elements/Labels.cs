using System;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using demo.framework.Forms;

namespace demo.framework.Elements
{
    public class Labels : BaseElement
    {
        public Labels (By locator, string name) : base(locator, name){}

        private ProductPage TVpage;

        public string [,] GetLabels(string byProductName, string byProductDescription, string locatorPrice, string patternFirstWord, string patterSecondWord, string patternPrice)
        {

            var elementsName = Browser.GetDriver().FindElements(By.XPath(byProductName));

            //string year;
            int count = elementsName.Count;
            string [,] results = new string[count,5];
            if (count > 0)
            {

                for (int i = 0; i < count; i++)
                {
                    var elementsDescription = Browser.GetDriver().FindElements(By.XPath(byProductDescription));
                    var elementsPrices = Browser.GetDriver().FindElements(By.XPath(locatorPrice));
                    elementsName = Browser.GetDriver().FindElements(By.XPath(byProductName));

                    String Name = elementsName[i].Text;
                    String Description = elementsDescription[i].Text;
                    String Price = elementsDescription[i].Text;

                    Browser.GetDriver().FindElements(By.XPath(byProductName))[i].Click();
                    ProductPage TVpage = new ProductPage();
                    var year = TVpage.GetYear();
                    Browser.GetDriver().Navigate().Back();
                    WaitForElementPresent (By.XPath(byProductName), string.Concat("Year for ", i.ToString()," page is processed "));

                    var manufacturer = Regex.Match(Name, @patternFirstWord);
                    var model = Regex.Match(Name, @patterSecondWord);
                    var diagonal = Regex.Match(Description, @patternFirstWord);
                    var price = Regex.Match(Price, @patternPrice);
                    var yearOnly = Regex.Match(year, @patternFirstWord);

                    results[i, 0] = manufacturer.ToString();
                    results[i, 1] = model.ToString();
                    results[i, 2] = string.Concat((diagonal.ToString())[0].ToString(),
                        (diagonal.ToString())[1].ToString());
                    results[i, 3] = price.ToString();
                    results[i, 4] = yearOnly.ToString();
                }
            }
            return results;
        }

    }
}
