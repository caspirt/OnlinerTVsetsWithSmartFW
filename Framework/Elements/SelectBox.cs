using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace demo.framework.Elements
{
    public class SelectBox : BaseElement
    {
        private SelectElement selector;


        public SelectBox(By locator, String name) : base(locator, name)
        {
            
        }

        /// <summary>
        /// Selecting item from selectbox by Text
        /// </summary>
        /// <param name="state">text to select</param>

        public void SelectOption(string Option)
        {
            WaitForElementPresent();
            var selectElement = new SelectElement(GetElement());
            selectElement.SelectByText( Option);
        }

        /// <summary>
        /// Selecting item from selectbox by Text with delay for loading locator
        /// </summary>
        /// <param name="slkBox">where select</param>
        /// <param name="text">what select </param>
        /// <param name="locator">what wait for loading</param>
        public void SelectOptionWithDelay ( string text, By locator)
        {
            SelectOption(text);
            BaseElement.WaitForElementPresent(locator, string.Concat("Tag ", text));

        }


    }
}
