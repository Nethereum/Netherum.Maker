using System;
using System.Numerics;

namespace Nethereum.Maker.ERC20Token
{
    public class MakerTokenConvertor
    {

        private const long MakerMeiUnitValue = 1000000000000000000;

        private int CalculateNumberOfDecimalPlaces(decimal value, int currentNumberOfDecimals = 0)
        {
            decimal multiplied = (decimal)((double)value * Math.Pow(10, currentNumberOfDecimals));
            if (Math.Round(multiplied) == multiplied)
                return currentNumberOfDecimals;
            return CalculateNumberOfDecimalPlaces(value, currentNumberOfDecimals + 1);
        }

        /// <summary>
        /// Mei like Wei is the smallest unit for Maker 
        /// </summary>
        /// <param name="makerAmount"></param>
        /// <returns></returns>
        public BigInteger ConvertToMei(decimal makerAmount)
        {
            var decimalPlaces = CalculateNumberOfDecimalPlaces(makerAmount);
            if (decimalPlaces == 0) return BigInteger.Multiply(new BigInteger(makerAmount), MakerMeiUnitValue);

            var decimalConversionUnit = (decimal)Math.Pow(10, decimalPlaces);

            var makerAmountFromDec = new BigInteger(makerAmount * decimalConversionUnit);
            var meiUnitFromDec = new BigInteger(MakerMeiUnitValue / decimalConversionUnit);
            return makerAmountFromDec * meiUnitFromDec;
        }

        public decimal ConvertFromMei(BigInteger meiAmount)
        {
            return (decimal)meiAmount / MakerMeiUnitValue;
        }
    }
}