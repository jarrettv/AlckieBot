using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Helpers
{
    public static class RandomHelper
    {
        /// <summary>
        /// Will generate a random number between 1 and the informed max number.
        /// </summary>
        /// <param name="maxNumber">Max number to be generated. Must be greater than 0.</param>
        /// <returns></returns>
        public static int GetRandomNumber(int maxNumber)
        {
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                if (maxNumber <= 0)
                    throw new ArgumentOutOfRangeException(nameof(maxNumber));

                var randomNumber = new byte[1];
                rngCsp.GetBytes(randomNumber);
                return ((randomNumber[0] % maxNumber) + 1);
            }
        }

        /// <summary>
        /// Simulates a roll of a dice.
        /// </summary>
        /// <param name="numberSides">Numbers os sides of the dice.</param>
        /// <returns></returns>
        public static int RollDice(int numberSides)
        {
            if (numberSides <= 0)
                throw new ArgumentOutOfRangeException(nameof(numberSides));

            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Create a byte array to hold the random value.
                var randomNumber = new byte[1];
                do
                {
                    // Fill the array with a random value.
                    rngCsp.GetBytes(randomNumber);
                }
                while (!IsFairRoll(randomNumber[0], numberSides));
                // Return the random number mod the number
                // of sides.  The possible values are zero-
                // based, so we add one.
                return (int)((randomNumber[0] % numberSides) + 1);
            }
        }
        private static bool IsFairRoll(byte roll, int numSides)
        {
            // There are MaxValue / numSides full sets of numbers that can come up
            // in a single byte.  For instance, if we have a 6 sided die, there are
            // 42 full sets of 1-6 that come up.  The 43rd set is incomplete.
            var fullSetsOfValues = Byte.MaxValue / numSides;

            // If the roll is within this range of fair values, then we let it continue.
            // In the 6 sided die case, a roll between 0 and 251 is allowed.  (We use
            // < rather than <= since the = portion allows through an extra 0 value).
            // 252 through 255 would provide an extra 0, 1, 2, 3 so they are not fair
            // to use.
            return roll < numSides * fullSetsOfValues;
        }
    }
}
