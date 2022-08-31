namespace DevIO.Business.Models.Validations
{
    public class CpfValidator
    {
        public const int DocumentLength = 11;

        public static bool Validation(string document)
        {
            var numbersDocument = Utils.OnlyNumbers(document);

            if (!LengthValid(numbersDocument)) return false;
            return !HaveDigitRepeat(numbersDocument) && HaveDigitValids(numbersDocument);
        }

        private static bool LengthValid(string value)
        {
            return value.Length == DocumentLength;
        }

        private static bool HaveDigitRepeat(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HaveDigitValids(string value)
        {
            var number = value.Substring(0, DocumentLength - 2);
            var digitVerify = new DigitVerify(number)
                .WithMultiple(2, 11)
                .Substitule("0", 10, 11);
            var firstDigit = digitVerify.DigitCalculator();
            digitVerify.AddDigit(firstDigit);
            var secondDigit = digitVerify.DigitCalculator();

            return string.Concat(firstDigit, secondDigit) == value.Substring(DocumentLength - 2, 2);
        }
    }

    public class CnpjValidator
    {
        public const int Length = 14;

        public static bool Validation(string document)
        {
            var documentNumbers = Utils.OnlyNumbers(document);

            if (!HaveLengthValid(documentNumbers)) return false;
            return !HaveDigitisRepeat(documentNumbers) && HaveDigitValid(documentNumbers);
        }

        private static bool HaveLengthValid(string value)
        {
            return value.Length == Length;
        }

        private static bool HaveDigitisRepeat(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HaveDigitValid(string value)
        {
            var number = value.Substring(0, Length - 2);

            var digitVerify = new DigitVerify(number)
                .WithMultiple(2, 9)
                .Substitule("0", 10, 11);
            var firstDigit = digitVerify.DigitCalculator();
            digitVerify.AddDigit(firstDigit);
            var secondDigit = digitVerify.DigitCalculator();

            return string.Concat(firstDigit, secondDigit) == value.Substring(Length - 2, 2);
        }
    }

    public class DigitVerify
    {
        private string _number;
        private const int Module = 11;
        private readonly List<int> _multiples = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IDictionary<int, string> _replacements = new Dictionary<int, string>();
        private bool _moduleComplement = true;

        public DigitVerify(string numero)
        {
            _number = numero;
        }

        public DigitVerify WithMultiple(int firstMultiple, int lastMultiple)
        {
            _multiples.Clear();
            for (var i = firstMultiple; i <= lastMultiple; i++)
                _multiples.Add(i);

            return this;
        }

        public DigitVerify Substitule(string substitute, params int[] digits)
        {
            foreach (var i in digits)
            {
                _replacements[i] = substitute;
            }
            return this;
        }

        public void AddDigit(string digit)
        {
            _number = string.Concat(_number, digit);
        }

        public string DigitCalculator()
        {
            return !(_number.Length > 0) ? "" : GetDigitSum();
        }

        private string GetDigitSum()
        {
            var sum = 0;
            for (int i = _number.Length - 1, m = 0; i >= 0; i--)
            {
                var product = (int)char.GetNumericValue(_number[i]) * _multiples[m];
                sum += product;

                if (++m >= _multiples.Count) m = 0;
            }

            var mod = (sum % Module);
            var result = _moduleComplement ? Module - mod : mod;

            return _replacements.ContainsKey(result) ? _replacements[result] : result.ToString();
        }
    }

    public class Utils
    {
        public static string OnlyNumbers(string value)
        {
            var onlyNumber = "";
            foreach (var s in value)
            {
                if (char.IsDigit(s))
                {
                    onlyNumber += s;
                }
            }
            return onlyNumber.Trim();
        }
    }
}
