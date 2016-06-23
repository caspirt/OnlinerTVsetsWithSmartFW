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

            Log.Step(step++, "Open onliner");
            Onliner mainForm = new Onliner();


            Log.Step(step++, "Go to catalog");
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

            Log.Step(step++, "Get all model");
            TVpage.SearchInSelectBoxesRange();

            Log.Step(step++, "Create list of model description");
            var tvs = TVpage.GetTvs();

            Log.Step(step++, "Ппроверяем, что данные соответствует");
            foreach (var tv in tvs)
            {
                Assert.AreEqual(_model, tv.manufacturer);
                Assert.GreaterOrEqual(tv.diagonal,_diagonalLowerLimit);
                Assert.LessOrEqual(tv.diagonal, _diagonalUpperLimit);
                Assert.LessOrEqual(tv.price,_priceUpperLimit);
                Assert.GreaterOrEqual(tv.year, _YearLowerLimit);
            }
        }
    }
}
