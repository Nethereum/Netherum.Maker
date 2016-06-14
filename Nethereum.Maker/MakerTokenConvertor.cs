using System;
using System.Numerics;
using Nethereum.Web3;

namespace Nethereum.Maker.ERC20Token
{
    public class MakerTokenConvertor
    {

        private const long MakerMeiUnitValue = 18;
        /// <summary>
        /// Mei like Wei is the smallest unit for Maker 
        /// </summary>
        /// <param name="makerAmount"></param>
        /// <returns></returns>
        public BigInteger ConvertToMei(decimal makerAmount)
        {
            return UnitConversion.Convert.ToWei(makerAmount, 18);
        }

        public decimal ConvertFromMei(BigInteger meiAmount)
        {
            return UnitConversion.Convert.FromWei(meiAmount, 18);
        }
    }
}