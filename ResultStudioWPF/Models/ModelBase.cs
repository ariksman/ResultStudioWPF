using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ResultStudioWPF.Models
{
    // followed this guide:  
    public class ModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        #region Property changed
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName, Action<bool> message)
        {
            if (this.PropertyChanged != null)
            {
                // property changed
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                // send app message (mvvm light toolkit)
                if (message != null)
                    message(this.IsValid);
            }
        }
        #endregion

        #region Notify data error
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        // get errors by property
        public IEnumerable GetErrors(string propertyName)
        {
            if (propertyName == null) return null;
            return this._errors.ContainsKey(propertyName) ? this._errors[propertyName] : null;
        }

        // has errors
        private bool _hasErrors;
        public bool HasErrors
        {
            get
            {
                if (_errors == null) _errors = new Dictionary<string, List<string>>();
                return (this._errors.Count > 0);
            }
            set { _hasErrors = value; }
        }

        // object is valid
        private bool _isValid;
        public bool IsValid
        {
            get { return !this.HasErrors; }
            set { _isValid = value; }
        }

        public void AddError(string propertyName, string error)
        {
            // Add error to list
            this._errors[propertyName] = new List<string>() { error };
            this.NotifyErrorsChanged(propertyName);
        }

        public void RemoveError(string propertyName)
        {
            // remove error
            if (this._errors.ContainsKey(propertyName))
                this._errors.Remove(propertyName);
            this.NotifyErrorsChanged(propertyName);
        }

        public void NotifyErrorsChanged(string propertyName)
        {
            // Notify
            if (this.ErrorsChanged != null)
                this.ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
        #endregion
    }
}
