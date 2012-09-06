using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Jarvis.Client.Commands
{
    public class ExitCommand : ICommand
    {
        #region Implementation of ICommand

        /// <summary>
        /// Definisce il metodo da chiamare quando viene richiamato il comando.
        /// </summary>
        /// <param name="parameter">Dati utilizzati dal comando.Se il comando non richiede dati da passare, questo oggetto può essere impostato su null.</param>
        public void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Definisce il metodo che determina se il comando può essere eseguito nello stato corrente.
        /// </summary>
        /// <returns>
        /// true se questo comando può essere eseguito. In caso contrario false.
        /// </returns>
        /// <param name="parameter">Dati utilizzati dal comando.Se il comando non richiede dati da passare, questo oggetto può essere impostato su null.</param>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void OnCanExecuteChanged(EventArgs e)
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null) handler(this, e);
        }

        #endregion
    }
}
