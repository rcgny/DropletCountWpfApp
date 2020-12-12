// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Windows.Controls;

namespace DropletCountWpf.UI.Helpers
{
    public class ThresholdRangeRule : ValidationRule
    {
        public  int Min { get; set; }
        public  int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var threshold = 0;

            try
            {
                if (((string) value).Length > 0)
                    threshold = int.Parse((string) value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }

            if ((threshold < Min) || (threshold > Max))
            {
                return new ValidationResult(false,
                    "Please enter a Droplet Threshold in the range: " + Min + " - " + Max + ".");
            }
            return new ValidationResult(true, null);
        }
    }
}