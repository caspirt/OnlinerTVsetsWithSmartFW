using demo.framework;
using demo.framework.Elements;
using demo.framework.forms;
using OpenQA.Selenium;

public class CatalogPage : BaseForm {

    //locator for detecting Catalog page
    private static readonly By CatalogPageLocator = By.XPath("//div[contains(@class,'catalog-navigation')]");

    //locator for TV link
    private Link linkTVsets = new Link(By.XPath("//a[@class='catalog-bar__link catalog-bar__link_strong' and @href='http://catalog.onliner.by/tv']"), "catalog of TVsets link ");

    public CatalogPage()   : base(CatalogPageLocator, "catalog main page")
    {
    }

    public void NavigateToTVsets()
    {
        linkTVsets.Click();
        Browser.WaitForPageToLoad();
    }

}
