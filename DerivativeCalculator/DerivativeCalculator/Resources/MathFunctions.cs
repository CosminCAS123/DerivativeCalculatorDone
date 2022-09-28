using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AngouriMath;
namespace DerivativeCalculator.Resources
{
    public static class MathFunctions
    {



        //removes extra decimals, making every number in the string at a max of a.bc
        public enum DerivativeType
        {
            FirstDerivative,
            SecondDerivative,
            ThirdDerivative,
            Default
        }
        public static void RemoveExtraDecimals(ref string expression)
        {
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '.')
                {
                    if (char.IsDigit(expression[i + 1]) && char.IsDigit(expression[i + 2]) && char.IsDigit(expression[i + 3]))//if has more than 2 decimals.
                    {
                        string s1 = expression[i + 1].ToString() + expression[i + 2].ToString();
                        int j;
                        for (j = i + 1; j < expression.Length && char.IsDigit(expression[j]); j++) continue;
                        expression = expression.Remove(i + 1, j - i - 1).Insert(i + 1, s1); ;
                    }
                }
            }


        }

        public static bool IsValid(string expression)
        {
            //if its empty
            if (expression.Length == 0) return false;


            //if contains more than 2 consecutive letters and they're not "pi", return false
            for (int i = 0; i < expression.Length - 1; i++)
            {
                if (char.IsLetter(expression[i]) && char.IsLetter(expression[i + 1]) && ("" + expression[i] + expression[i + 1]) != "pi") return false;
            }
            return true;
        }

        //calculate derivative , use task.run

        public static Entity CalculateDerivative(string expression, DerivativeType power)
        {

            Entity a = expression;
            switch (power)
            {
                case DerivativeType.FirstDerivative:
                    return a.Differentiate("x", 1).InnerSimplified;

                case DerivativeType.SecondDerivative:
                    return a.Differentiate("x", 2).InnerSimplified;

                case DerivativeType.ThirdDerivative:
                    return a.Differentiate("x", 3).InnerSimplified;
                default:
                    return null;//unreachable anyway

            }

        }
        public static Entity CalculateIntegral(string expression) => ((Entity)expression).Integrate("x").InnerSimplified;
       

    }
}
