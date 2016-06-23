using System;
using OpenQA.Selenium;

namespace demo.framework.Elements
{
    public class TextBox : BaseElement
    {
        public TextBox(By locator, String name) : base(locator, name){}

        /// <summary>
        /// clear inpuit field and then set text into it
        /// </summary>
        /// <param name="text">text to typing</param>
        public void ClearAndSetText(String text)
        {
            WaitForElementPresent();
            GetElement().Clear();
            Log.Info(String.Format("{0} :: clear text ", GetName()));
            GetElement().SendKeys(text);
            Log.Info(String.Format("{0} :: type text '{1}'", GetName(), text));
        }

        /// <summary>
        /// clear inpuit field and then set text into it with waiting locator for loading
        /// </summary>
        /// <param name="txtBox">where typing</param>
        /// <param name="text">what typing </param>
        /// <param name="locator">what wait</param>
        public void ClearAndSetTextWithDelay( string text, By locator)
        {
            ClearAndSetText(text);
            BaseElement.WaitForElementPresent(locator, string.Concat("Tag ", text));
        }
    }
}
