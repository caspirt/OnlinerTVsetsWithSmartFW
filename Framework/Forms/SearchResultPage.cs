using System;
using System.Collections.Generic;
using demo.framework.forms;
using demo.framework.Models;
using demo.framework.Elements;
using OpenQA.Selenium;
using static demo.tests.TestOnliner;

namespace demo.framework.Forms
{
    public class SearchResultPage : BaseForm
    {

        private static readonly By SearchPageLocator = By.XPath("//h1[text()='Телевизоры']");

        private string byProductName = "//span[contains(@data-bind,'product.full_name')]";

        private string byProductDescription = "//span[contains(@data-bind,'product.description')]";

        private string locatorPrice = "//span[contains(@data-bind,'BYN')]";

        private static By byManufacturer =
            By.XPath(string.Concat(@"//span[contains(@data-bind,'item.name') and text()='", 
                _model,
                @"']/..//span[contains(@class,'i-checkbox__faux')]"));
        private static By byManufacturerTag =
            By.XPath(string.Concat("//span[contains(@data-bind,'html: tag.text') and text()='", 
                _model,
                "' ]"));

        private static By byPriceLower = By.XPath("//input[contains(@class,'number-input_price') and @placeholder = 'от']");
        private static By byPriceUpper = By.XPath("//input[contains(@class,'number-input_price') and @placeholder = 'до']");

        private static By byPriceLowerTag = By.XPath("//div[contains(@title,'Минимальная')]/span");
        private static By byPriceUpperTag = By.XPath("//div[contains(@title,'Минимальная')]/span");

        private static By byYearLower = By.XPath("//input[contains(@data-bind,'facet.value.from') and @placeholder = '2012']");
        private static By byYearUpper = By.XPath("//input[contains(@data-bind,'facet.value.from') and @placeholder = '2016']");

        private static By byYearLowerTag = By.XPath("//div[contains(@title,'Дата выхода на рынок')]/span");
        private static By byYearUpperTag = By.XPath("//div[contains(@title,'Дата выхода на рынок')]/span");


        private static By byDiagLower = By.XPath("//select[contains(@data-bind,'value: facet.value.from')]/option[contains(@value,'385')]/../../*");
        private static By byDiagUpper = By.XPath("//select[contains(@data-bind,'value: facet.value.to')]/option[contains(@value,'385')]/../../*");

        
        private static By byDiagLowerTag = By.XPath("//div[contains(@title,'Диагональ')]/span");
        private static By byDiagUpperTag = By.XPath("//div[contains(@title,'Диагональ')]/span");


        private Checkbox сhkManufacturer = new Checkbox(byManufacturer, "Manufacturer checkbox");

        private TextBox txtPriceLower = new TextBox(byPriceLower, "Lower price textbox");

        private TextBox txtPriceUpper = new TextBox(byPriceUpper, "Upper price textbox");

        private TextBox txtYearLower = new TextBox(byYearLower, "Lower Year textbox");

        private TextBox txtYearUpper = new TextBox(byYearUpper, "Upper Year textbox");

        private SelectBox slkDiagLower = new SelectBox(byDiagLower, "Lower Diag selectbox");

        private SelectBox slkDiagUpper = new SelectBox(byDiagUpper, "Upper Diag selectbox");

        //private Labels lblProductName = new Labels(By.XPath(byProductName), "manufacturer");

        private string patternFirstWord = @"(?:\S+\s+){0}(\S+)";

        private string patternSecondWord = @"(?:\S+\s+){1}(\S+)";

        private string patternPrice = @"/([^,]+)/";

        public SearchResultPage() : base(SearchPageLocator, "Search Result Page")
        {
        }


        public void SearchInManufacturer()
        {
            if (_model != "")
            {
                сhkManufacturer.CheckWithDelay(true, byManufacturerTag);
            }
        }

        public void SearchInTextBox(TextBox txtBox, String text, By locator)
        {
            if ( text != "")
            {
                txtBox.ClearAndSetTextWithDelay (text, locator);
            }
        }

        public void SearchInTextsRange()
        {
            SearchInTextBox(txtPriceLower, _priceLowerLimit, byPriceLowerTag);
            SearchInTextBox(txtPriceUpper, _priceUpperLimit, byPriceUpperTag);
            SearchInTextBox(txtYearLower, _YearLowerLimit, byYearLowerTag);
            SearchInTextBox(txtYearUpper, _YearUpperLimit, byYearUpperTag);
        }

        public void SearchInSelectBox(SelectBox slkBox, String text, By locator)
        {
            if ((text != "") || (text != "\""))
            {
                slkBox.SelectOptionWithDelay(text, locator);
            }
        }

        public void SearchInSelectBoxesRange()
        {
            SearchInSelectBox(slkDiagLower, string.Concat(_diagonalLowerLimit,"\""), byDiagLowerTag);
            SearchInSelectBox(slkDiagUpper, string.Concat(_diagonalUpperLimit, "\""), byDiagUpperTag);
        }



        public List<TVset> GetTvs()
        {
            List<TVset> tv = new List<TVset>();

            Labels TVinfo = new Labels(By.XPath(byProductName), "Product names");

            var results = TVinfo.GetLabels (byProductName, byProductDescription, locatorPrice, patternFirstWord, patternSecondWord, patternPrice);

            int count = results.GetLength(0);

            for (int i = 0; i < count; i++)
            {
                tv.Add(new TVset(results[i, 0], results[i, 1], results[i, 2], results[i, 3], results[i, 4]));
            };

            return tv;

        }
    }
}
