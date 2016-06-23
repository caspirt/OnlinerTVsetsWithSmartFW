using System;

namespace demo.framework.Models
{
    public class TVset
    {
        public readonly String diagonal, model, manufacturer, price, year;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <param name="model"></param>
        /// <param name="diagonal"></param>
        /// <param name="price"></param>
        /// <param name="year"></param>
        public TVset(string manufacturer, string model, string diagonal, string price, string year)
        {
            this.diagonal = diagonal;
            this.model = model;
            this.manufacturer = manufacturer;
            this.year = year;
            this.price = price;
        }
    }
}
