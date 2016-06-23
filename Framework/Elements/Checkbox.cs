using System;
using OpenQA.Selenium;

namespace demo.framework.Elements
{
    public class Checkbox : BaseElement
    {
        public Checkbox(By locator, String name) : base(locator, name)
        {
            
        }

        /// <summary>
        /// setting with checking checkbox for required state
        /// </summary>
        /// <param name="state"></param>
        public void Check(bool state)
        {
            WaitForElementPresent();
            if (state && ! GetElement().Selected)
            {
                GetElement().Click();
            }
            else if (!state && GetElement().Selected)
            {
                GetElement().Click();
            }
        }

        /// <summary>
        /// setting with checking checkbox for required state with delay to loading locator
        /// </summary>
        public void CheckWithDelay (bool state, By locator)
        {
            WaitForElementPresent();
            Check(state);
            BaseElement.WaitForElementPresent(locator, string.Concat("Tag for checkbox = ",state));

        }
}
}
