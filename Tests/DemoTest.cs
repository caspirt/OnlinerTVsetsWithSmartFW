using System;
using NUnit.Framework;
using demo.framework;
using demo.framework.Forms;
using Assert = NUnit.Framework.Assert;

namespace demo.tests
{
    public class TestOnliner :   BaseTest
    {
        public static String _model = RunConfigurator.GetValue("manufacturer");
        public static String _diagonalUpperLimit = RunConfigurator.GetValue("diagonalUpperLimit");
        public static String _diagonalLowerLimit = RunConfigurator.GetValue("diagonalLowerLimit");
        public static String _YearLowerLimit = RunConfigurator.GetValue("YearLowerLimit");
        public static String _YearUpperLimit = RunConfigurator.GetValue("YearUpperLimit");
        public static String _priceUpperLimit = RunConfigurator.GetValue("priceUpperLimit");
        public static String _priceLowerLimit = RunConfigurator.GetValue("priceLowerLimit");


        [Test]
        public void RunTest()
        {
            int step = 1;

            Log.Step(step++, "Open main onliner page");
            Onliner mainForm = new Onliner();

            Log.Step(step++, "Go to catalog page");
            mainForm.NavigateToCatalog();

            Log.Step(step++, "Click TVsets link");
            CatalogPage catalogForm = new CatalogPage();
            catalogForm.NavigateToTVsets();

            Log.Step(step++, "Set manufacturer search");
            SearchResultPage TVpage = new SearchResultPage();
            TVpage.SearchInManufacturer();

            Log.Step(step++, "Set price and Year search");
            TVpage.SearchInTextsRange();

            Log.Step(step++, "Set diagonal search");
            TVpage.SearchInSelectBoxesRange();

            Log.Step(step++, "Get data for all models available at first page result");
            TVpage.SearchInSelectBoxesRange();

            Log.Step(step++, "Create list of model's description");
            var tvs = TVpage.GetTvs();

            Log.Step(step++, "Check that all collected values are correct");
            foreach (var tv in tvs)
            {
                if ( _model !="")
                    Assert.AreEqual(_model, tv.manufacturer,string.Concat("manufacturer ", _model, " is correct"));
                if (_diagonalLowerLimit != "")
                    Assert.GreaterOrEqual(tv.diagonal,_diagonalLowerLimit, string.Concat("diagonal", tv.diagonal, "inside range "));
                if (_diagonalUpperLimit != "")
                    Assert.LessOrEqual(tv.diagonal, _diagonalUpperLimit, string.Concat("diagonal", tv.diagonal, "inside range "));
                if (_priceUpperLimit != "")
                    Assert.LessOrEqual(tv.price,_priceUpperLimit, string.Concat("price", tv.price, "inside range "));
                if (_priceLowerLimit != "")
                    Assert.GreaterOrEqual(tv.price, _priceLowerLimit, string.Concat("price", tv.price, "inside range "));
                if (_YearLowerLimit != "")
                    Assert.GreaterOrEqual(tv.year, _YearLowerLimit, string.Concat("Year ", tv.year, "inside range "));
                if (_YearUpperLimit != "")
                    Assert.LessOrEqual(tv.year, _YearLowerLimit, string.Concat("Year ", tv.year, "inside range "));
            }
        }
    }
}
