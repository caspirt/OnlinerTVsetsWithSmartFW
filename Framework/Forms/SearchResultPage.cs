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
        // locator for search result page
        private static readonly By SearchPageLocator = By.XPath("//h1[text()='Телевизоры']");

        // locator for full product name
        private string byProductName = "//span[contains(@data-bind,'product.full_name')]";
        // locator for product description
        private string byProductDescription = "//span[contains(@data-bind,'product.description')]";
        // locator for priduct price
        private string locatorPrice = "//span[contains(@data-bind,'BYN')]";
        // locator for manufacturer
        private static By byManufacturer =
            By.XPath(string.Concat(@"//span[contains(@data-bind,'item.name') and text()='", 
                _model,
                @"']/..//span[contains(@class,'i-checkbox__faux')]"));
        // locator for detecting of manufacturer applying for search
        private static By byManufacturerTag =
            By.XPath(string.Concat("//span[contains(@data-bind,'html: tag.text') and text()='", 
                _model,
                "' ]"));
        // locators for price entering
        private static By byPriceLower = By.XPath("//input[contains(@class,'number-input_price') and @placeholder = 'от']");
        private static By byPriceUpper = By.XPath("//input[contains(@class,'number-input_price') and @placeholder = 'до']");
        // locators for price applying for search
        private static By byPriceLowerTag = By.XPath("//div[contains(@title,'Минимальная')]/span");
        private static By byPriceUpperTag = By.XPath("//div[contains(@title,'Минимальная')]/span");
        // locators for year entering
        private static By byYearLower = By.XPath("//input[contains(@data-bind,'facet.value.from') and @placeholder = '2012']");
        private static By byYearUpper = By.XPath("//input[contains(@data-bind,'facet.value.from') and @placeholder = '2016']");
        // locators for year applying for search
        private static By byYearLowerTag = By.XPath("//div[contains(@title,'Дата выхода на рынок')]/span");
        private static By byYearUpperTag = By.XPath("//div[contains(@title,'Дата выхода на рынок')]/span");
        // locators for diagonal selecting
        private static By byDiagLower = By.XPath("//select[contains(@data-bind,'value: facet.value.from')]/option[contains(@value,'385')]/../../*");
        private static By byDiagUpper = By.XPath("//select[contains(@data-bind,'value: facet.value.to')]/option[contains(@value,'385')]/../../*");
        // locators for diagonal applying for search
        private static By byDiagLowerTag = By.XPath("//div[contains(@title,'Диагональ')]/span");
        private static By byDiagUpperTag = By.XPath("//div[contains(@title,'Диагональ')]/span");

        // manufacturer checkbox
        private Checkbox сhkManufacturer = new Checkbox(byManufacturer, "Manufacturer checkbox");
        // lower price text
        private TextBox txtPriceLower = new TextBox(byPriceLower, "Lower price textbox");
        // upper price text
        private TextBox txtPriceUpper = new TextBox(byPriceUpper, "Upper price textbox");
        // lower year text
        private TextBox txtYearLower = new TextBox(byYearLower, "Lower Year textbox");
        // upper year text
        private TextBox txtYearUpper = new TextBox(byYearUpper, "Upper Year textbox");
        // lower diagonal selectbox
        private SelectBox slkDiagLower = new SelectBox(byDiagLower, "Lower Diag selectbox");
        // upper diagonal selectbox
        private SelectBox slkDiagUpper = new SelectBox(byDiagUpper, "Upper Diag selectbox");

        //regexp for manufacturer matching
        private string patternFirstWord = @"(?:\S+\s+){0}(\S+)";
        //regexp for model matching
        private string patternSecondWord = @"(?:\S+\s+){1}(\S+)";
        //regexp for price matching
        private string patternPrice = @"/([^,]+)/";

        public SearchResultPage() : base(SearchPageLocator, "Search Result Page")
        {
        }

        /// <summary>
        /// marking  manufacturer checkbox
        /// </summary>
        public void SearchInManufacturer()
        {
            if (_model != "")
            {
                сhkManufacturer.CheckWithDelay(true, byManufacturerTag);
            }
        }

        /// <summary>
        /// clear corresponded textbox and enter text if need with detecting for search applying
        /// </summary>
        public void SearchInTextBox(TextBox txtBox, String text, By locator)
        {
            if ( text != "")
            {
                txtBox.ClearAndSetTextWithDelay (text, locator);
            }
        }
        /// <summary>
        /// Make required search in text boxes
        /// </summary>
        public void SearchInTextsRange()
        {
            SearchInTextBox(txtPriceLower, _priceLowerLimit, byPriceLowerTag);
            SearchInTextBox(txtPriceUpper, _priceUpperLimit, byPriceUpperTag);
            SearchInTextBox(txtYearLower, _YearLowerLimit, byYearLowerTag);
            SearchInTextBox(txtYearUpper, _YearUpperLimit, byYearUpperTag);
        }

        /// <summary>
        /// select corresponded selectbox if need with detecting for search applying
        /// </summary>
        public void SearchInSelectBox(SelectBox slkBox, String text, By locator)
        {
            if ((text != "") || (text != "\""))
            {
                slkBox.SelectOptionWithDelay(text, locator);
            }
        }
        /// <summary>
        /// Make required search in selectboxes
        /// </summary>
        public void SearchInSelectBoxesRange()
        {
            SearchInSelectBox(slkDiagLower, string.Concat(_diagonalLowerLimit,"\""), byDiagLowerTag);
            SearchInSelectBox(slkDiagUpper, string.Concat(_diagonalUpperLimit, "\""), byDiagUpperTag);
        }


        /// <summary>
        /// Get all information about all TV in search
        /// </summary>
        public List<TVset> GetTvs()
        {
            List<TVset> tv = new List<TVset>(); //for gathering information into model
            Labels TVinfo = new Labels(By.XPath(byProductName), "Product names");//declare separate TV page
            // collect all required info
            var results = TVinfo.GetAllInfoFromLabels (byProductName, byProductDescription, locatorPrice, patternFirstWord, patternSecondWord, patternPrice);
            //count number of collected info
            int count = results.GetLength(0);
            // mapping collected info into model
            for (int i = 0; i < count; i++)
            {
                tv.Add(new TVset(results[i, 0], results[i, 1], results[i, 2], results[i, 3], results[i, 4]));
            };
            return tv;
        }
    }
}
