using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAnalyzer.Types
{
    public class ValidatedInt
    {
        private int _Value;

        public int? Min { get; private set; }
        public int? Max { get; private set; }
        public IEnumerable<int> AllowedValues { get; private set; }

        public bool IsValid { get; set; }

        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (_Value == value)
                {
                    return;
                }

                _Value = value;

                if (Min != null)
                {
                    IsValid = IsValid && value >= Min;
                }
                if (Max != null)
                {
                    IsValid = IsValid && value <= Max;
                }
                if (AllowedValues != null && AllowedValues.Count() > 0)
                {
                    IsValid = IsValid && AllowedValues.Contains(value);
                }
            }
        }

        public ValidatedInt(int? min = null, int? max = null, IEnumerable<int> allowedValues = null)
        {
            IsValid = true;
            Min = min;
            Max = max;
            AllowedValues = allowedValues;
        }
    }
}
