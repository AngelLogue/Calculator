using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calculator.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        #region Operations
        public enum Operation
        {
            CLEAR,
            BACK_SPACE,
            PLUS,
            MINUS,
            MULTIPLY,
            DIVIDE,
            POINT,
            PN,
            PERCENT,
            RESULT

        }
        #endregion

        public Operation CurrentOperation { get; set; }
        public double FirstNumber { get; set; }

        #region Full properties
        private string _result;

    public string Result
    {
        get { 
                return _result;
            }
        set { 
                if( _result != value )
                {
                    _result = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));
                }
            }
    } 
    #endregion

        #region Comands
    public ICommand TapNumberCommand
        {
            get
            {
                return new Command<string>(TapNumber);
            }
        }
    
    public ICommand TapOperationCommand
        {
            get
            {
                return new Command<string>(TapOperation);
            }
        }

#endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public DashboardViewModel()
        {
            Result = "0";
        } 
        #endregion

        #region Metodos
        private void TapNumber(string number)
        {
            if (Result == "0")
            {
                Result = number;
            }
            else
            {
                Result += number;
            }
        }

        private void TapOperation(string operation)
        {
            Enum.TryParse(operation, out Operation op);

            switch (op)
            {
                case Operation.CLEAR:
                    Result = "0";
                    break;
                case Operation.BACK_SPACE:

                    Result = Result.Length > 1 ? Result.Substring(0, Result.Length - 1) : "0";
                    
                    break;
                case Operation.PLUS:

                    FirstNumber = double.Parse(Result);
                    CurrentOperation = op;
                    Result = "0";

                    break;
                case Operation.MINUS:

                    FirstNumber = double.Parse(Result);
                    CurrentOperation = op;
                    Result = "0";

                    break;
                case Operation.MULTIPLY:

                    FirstNumber = double.Parse(Result);
                    CurrentOperation = op;
                    Result = "0";

                    break;
                case Operation.DIVIDE:

                    FirstNumber = double.Parse(Result);
                    CurrentOperation = op;
                    Result = "0";

                    break;
                case Operation.POINT:

                    Result = Result.Contains(".") ? Result : $"{Result}." ;

                    break;
                case Operation.PN:
                    
                    Result = Result  != "0" ? $"{double.Parse(Result) * -1}" : "0";

                    break;
                case Operation.PERCENT:
                    Result = $"{double.Parse(Result) / 100 * FirstNumber}";
                    break;
                case Operation.RESULT:
                    Result = ToOperation(FirstNumber, double.Parse(Result)).ToString();
                    break;
                default:
                    break;
            }

            if(op == Operation.CLEAR)
            {
                Result = "0";
            }

        }

        private double ToOperation(double firstNumber, double secondNumber)
        {
            switch (CurrentOperation)
            {
                case Operation.PLUS:
                    return firstNumber + secondNumber;
                case Operation.MINUS:
                    return firstNumber - secondNumber;
                case Operation.MULTIPLY:
                    return firstNumber * secondNumber;
                case Operation.DIVIDE:
                    return firstNumber / secondNumber;
                default:
                    return 0;
            }
        }
        #endregion
    }
}