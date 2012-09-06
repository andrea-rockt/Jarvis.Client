﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Jarvis.Client.Converters
{
    [ValueConversion(typeof(double), typeof(double),ParameterType = typeof(double))]
    public class SubtractingConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        /// <summary>
        /// Converte un valore. 
        /// </summary>
        /// <returns>
        /// Un valore convertito.Se il metodo restituisce null, viene utilizzato il valore null valido.
        /// </returns>
        /// <param name="value">Valore prodotto dall'origine dell’associazione.</param><param name="targetType">Tipo della proprietà origine dell’associazione.</param><param name="parameter">Il parametro del convertitore da utilizzare.</param><param name="culture">Le impostazioni cultura da utilizzare nel convertitore.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value - Double.Parse((string)parameter);
        }

        /// <summary>
        /// Converte un valore. 
        /// </summary>
        /// <returns>
        /// Un valore convertito.Se il metodo restituisce null, viene utilizzato il valore null valido.
        /// </returns>
        /// <param name="value">Valore prodotto dalladestinazione dell’associazione.</param><param name="targetType">Tipo in cui eseguire la conversione.</param><param name="parameter">Il parametro del convertitore da utilizzare.</param><param name="culture">Le impostazioni cultura da utilizzare nel convertitore.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value + Double.Parse((string)parameter);
        }

        #endregion
    }
}
