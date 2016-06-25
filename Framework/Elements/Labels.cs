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

        /// <summary>
        /// Collect all info from labels
        /// </summary>
        /// <param name="byProductName">locator for TV manufacturer + model </param>
        /// <param name="byProductDescription"> locator for TV description</param>
        /// <param name="locatorPrice">locator for Price</param>
        /// <param name="patternFirstWord">first regexp for matching into ProductName </param>
        /// <param name="patternSecondWord">second regexp for matching into ProductName</param>
        /// <param name="patternPrice">regexp for matching into Price</param>
        /// <returns></returns>
        public string [,] GetAllInfoFromLabels(string byProductName, string byProductDescription, string locatorPrice, string patternFirstWord, string patternSecondWord, string patternPrice)
        {
            // count number of results
            var elementsName = Browser.GetDriver().FindElements(By.XPath(byProductName));
            int count = elementsName.Count;
            string [,] results = new string[count,5];
            if (count > 0)
            {
                //for each result 
                for (int i = 0; i < count; i++)
                {
                    //collect description + price + manufacturer
                    var elementsDescription = Browser.GetDriver().FindElements(By.XPath(byProductDescription));
                    var elementsPrices = Browser.GetDriver().FindElements(By.XPath(locatorPrice));
                    elementsName = Browser.GetDriver().FindElements(By.XPath(byProductName));
                    String Name = elementsName[i].Text;
                    String Description = elementsDescription[i].Text;
                    String Price = elementsPrices[i].Text;
                    // go to separate page of model and collect year
                    Browser.GetDriver().FindElements(By.XPath(byProductName))[i].Click();
                    ProductPage TVpage = new ProductPage();
                    var year = TVpage.GetYear();
                    Browser.GetDriver().Navigate().Back();
                    WaitForElementPresent (By.XPath(byProductName), string.Concat("Year for ", i.ToString()," page is processed "));
                    //parsing results
                    var manufacturer = Regex.Match(Name, @patternFirstWord);
                    var model = Regex.Match(Name, patternSecondWord);
                    var diagonal = Regex.Match(Description, @patternFirstWord);
                    var price = Regex.Match(Price, @patternPrice);
                    var yearOnly = Regex.Match(year, @patternFirstWord);
                    //mapping result
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
