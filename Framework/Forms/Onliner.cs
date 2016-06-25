using demo.framework;
using demo.framework.Elements;
using demo.framework.forms;
using OpenQA.Selenium;

public class Onliner : BaseForm {

    //locator for detecting Onliner main page
    private static readonly By MainPageLocator = By.XPath("//img[contains(@class,'onliner_logo')]");
    //locator for detecting Catalog link
    private Link linkCatalog = new Link(By.XPath("//a[contains (@href,'catalog.onliner')]"),"catalog link ");

    public Onliner()   : base(MainPageLocator, "onliner by")
    {
    }

    public void NavigateToCatalog()
    {
        linkCatalog.Click();
        Browser.WaitForPageToLoad();
    }

    
}
