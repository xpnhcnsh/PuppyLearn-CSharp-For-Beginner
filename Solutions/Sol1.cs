namespace Solutions
{
    public class Sol1
    {
        public class Imaginary
        {
            public double Real { get; set; }
            public double Im { get; set; }

            public Imaginary(double real, double im)
            {
                Real = real;
                Im = im;
            }

            public static Imaginary Plus(Imaginary a, Imaginary b)
            {
                return new Imaginary(a.Real + b.Real, a.Im + b.Im);
            }

            public static Imaginary Minus(Imaginary a, Imaginary b)
            {
                return new Imaginary(a.Real - b.Real, a.Im - b.Im);
            }

            public static Imaginary Times(Imaginary a, Imaginary b)
            {
                return new Imaginary(a.Real * b.Real - a.Im * b.Im, a.Real * b.Im + a.Im * b.Real);
            }

            public Imaginary Conjugate()
            {
                return new Imaginary(Real, -Im);
            }

            private double TimesByConjugate()
            {
                return Math.Pow(Real, 2) + Math.Pow(Im, 2);
            }

            private Imaginary DivideByReal(double b)
            {
                return new Imaginary(Real / b, Im / b);
            }
            public static Imaginary Divide(Imaginary a, Imaginary b)
            {
                return Times(a, b.Conjugate()).DivideByReal(b.TimesByConjugate());
            }

            //public override string ToString()
            //{
            //    return $"{Real}+j{Im}";
            //}

            // 模式匹配
            public override string ToString() => (Real, Im) switch
            {
                { Im: 0 } => $"{Real}",
                // { Im: double im } when im == 0 => $"{Real}", //同上
                // (_ , 0) => $"{Real}", //同上
                { Real: 0, Im: > 0 } => $"j{Im}",
                // { Real: double real, Im: double im} when real == 0 && im > 0 => $"j{Im}", //大括号内无法使用==，只能使用>和<，所以==需要放在{}外面用when表示，此种方法同上。
                { Real: 0, Im: < 0 } => $"j{-Im}",
                { Im: < 0 } => $"{Real}-j{-Im}",
                _ => $"{Real}+j{Im}"
            };

            public static Imaginary operator +(Imaginary a, Imaginary b)
            {
                return Imaginary.Plus(a, b);
            }

            public static Imaginary operator -(Imaginary a, Imaginary b)
            {
                return Imaginary.Minus(a, b);
            }

            public static Imaginary operator *(Imaginary a, Imaginary b)
            {
                return Imaginary.Times(a, b);
            }

            public static Imaginary operator /(Imaginary a, Imaginary b)
            {
                return Imaginary.Divide(a, b);
            }
        }
    }
}
