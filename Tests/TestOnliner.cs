using System;
using NUnit.Framework;
using demo.framework;
using demo.framework.Forms;
using Assert = NUnit.Framework.Assert;

namespace demo.tests
{
    /*
     * Ниже пример работы с абстракцией TV
     */
    public class TestOnliner :   BaseTest
    {
        private readonly String _model = RunConfigurator.GetValue("model");
        private readonly String _diagonalUpperLimit = RunConfigurator.GetValue("diagonalUpperLimit");
        private readonly String _diagonalLowerLimit = RunConfigurator.GetValue("diagonalLowerLimit");
        private readonly String _YearLowerLimit = RunConfigurator.GetValue("YearLowerLimit");
        private readonly String _priceUpperLimit = RunConfigurator.GetValue("priceUpperLimit");


        [Test]
        public void RunTest()
        {
            int step = 1;

            Log.Step(step++, "Open main page");
            Onliner mainForm = new Onliner();
            //additional check for catalog link 
            //to prevent case with disabled catalog
            mainForm.AssertLinkCatalog();


            Log.Step(step++, "Go to catalog");
            mainForm.NavigateToCatalog();

            Log.Step(step++, "Go to TVsets");
            CatalogPage catalogForm = new CatalogPage();
            catalogForm.NavigateToTVsets();

            //var tvs = page.GetTvs();

            //Log.Step(2, "Ппроверяем, что данные соответствует");
            //foreach (var tv in tvs)
            //{
            //    Assert.AreEqual(_model, tv.model);
            //    Assert.GreaterOrEqual(_diagonalLowerLimit, tv.diagonal);
            //    Assert.LessOrEqual(_diagonalUpperLimit, tv.diagonal);
            //    Assert.LessOrEqual(_priceUpperLimit, tv.price);
            //    Assert.GreaterOrEqual(_YearLowerLimit, tv.year);
            //}
        }
    }
}
