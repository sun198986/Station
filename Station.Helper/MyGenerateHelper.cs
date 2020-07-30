using System;

namespace Station.Helper
{
    public class MyGenerateHelper
    {
        /// <summary>
        /// 唯一订单号生成
        /// </summary>
        /// <returns></returns>
        public static string GenerateOrder()
        {
            string strDateTimeNumber = DateTime.Now.ToString("yyyyMMddHHmm");
           
            return GenerateOrder(strDateTimeNumber,3);
        }

        /// <summary>
        /// 唯一订单号生成
        /// </summary>
        /// <param name="number">流水号位数 默认3位</param>
        /// <returns></returns>
        public static string GenerateOrder( int number=3 )
        {
            string strDateTimeNumber = DateTime.Now.ToString("yyyyMMddHHmm");

            return GenerateOrder(strDateTimeNumber, number);
        }
        /// <summary>
        /// 根据前缀生成唯一编号
        /// </summary>
        /// <returns></returns>
        public static string GenerateOrder(string number,int numlength)
        {
            string strDateTimeNumber = number+ DateTime.Now.ToString("ssms");
            int max = 1;
            for (int i = 1; i <= max; i++)
            {
                max *= 10;
            }
            string strRandomResult = NextRandom(max, 1).ToString();
            return strDateTimeNumber + strRandomResult.PadLeft(numlength,'0');
        }
        /// <summary>
        /// 参考：msdn上的RNGCryptoServiceProvider例子
        /// </summary>
        /// <param name="numSeeds"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static int NextRandom(int numSeeds, int length)
        {
            // Create a byte array to hold the random value.  
            byte[] randomNumber = new byte[length];
            // Create a new instance of the RNGCryptoServiceProvider.  
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            // Fill the array with a random value.  
            rng.GetBytes(randomNumber);
            // Convert the byte to an uint value to make the modulus operation easier.  
            uint randomResult = 0x0;
            for (int i = 0; i < length; i++)
            {
                randomResult |= ((uint)randomNumber[i] << ((length - 1 - i) * 8));
            }
            return (int)(randomResult % numSeeds) + 1;
        }
    }
}
