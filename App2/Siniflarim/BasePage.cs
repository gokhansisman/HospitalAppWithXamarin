using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace App2
{
    public class BaseModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation

        /// <summary>
        /// An event that fires when any of the property values change
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #region Raise Property Changed

        /// <summary>
        /// Raise Property Changed using an expression function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyExpr"></param>
        public void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpr)
        {
            var propertyName = GetMemberInfo(propertyExpr).Name;
            RaisePropertyChangedExplicit(propertyName);
        }

        /// <summary>
        /// Raise Property Changed using the CallerMemberName
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            RaisePropertyChangedExplicit(propertyName);
        }


        /// <summary>
        /// Raise Propery Changed explicitly passing the property name
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChangedExplicit(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion

        #region Set Property Value

        /// <summary>
        /// Set the property value using an expression function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageField"></param>
        /// <param name="newValue"></param>
        /// <param name="propertyExpr"></param>
        /// <returns></returns>
        protected bool SetPropertyValue<T>(ref T storageField, T newValue, Expression<Func<T>> propertyExpr)
        {
            if (Equals(storageField, newValue))
                return false;

            storageField = newValue;

            var propertyName = GetMemberInfo(propertyExpr).Name;
            RaisePropertyChangedExplicit(propertyName);

            return true;
        }

        /// <summary>
        /// Set the property value using the CallerMemberName
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storageField"></param>
        /// <param name="newValue"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetPropertyValue<T>(ref T storageField, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (Equals(storageField, newValue))
                return false;

            storageField = newValue;
            RaisePropertyChangedExplicit(propertyName);

            return true;
        }

        #endregion

        #region Helper Methods

        private MemberInfo GetMemberInfo(Expression expression)
        {
            MemberExpression operand;
            LambdaExpression lambdaExpression = (LambdaExpression)expression;
            if (lambdaExpression.Body as UnaryExpression != null)
            {
                UnaryExpression body = (UnaryExpression)lambdaExpression.Body;
                operand = (MemberExpression)body.Operand;
            }
            else
            {
                operand = (MemberExpression)lambdaExpression.Body;
            }
            return operand.Member;
        }

        #endregion

        #endregion
    }
}