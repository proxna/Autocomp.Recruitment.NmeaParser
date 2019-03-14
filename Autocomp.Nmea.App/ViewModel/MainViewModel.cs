using Autocomp.Nmea.Common;
using Autocomp.Nmea.Converter.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Autocomp.Nmea.App.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private readonly Regex nmeaCommunicationRegex;
        private readonly IMessageConverter messageConverter;
        public MainViewModel(IMessageConverter messageConverter)
        {
            this.messageConverter = messageConverter;
            nmeaCommunicationRegex = new Regex("^[$][A-Z]{5}[,][,.A-Z0-9]+[*]$");
            MessageValid = false;
            TraceLog = new ObservableCollection<string>();
        }

        private string nmeaMessageBody;

        public string NmeaMessageBody
        {
            get => nmeaMessageBody;
            set => Set(ref nmeaMessageBody, value);
        }

        private bool messageValid;

        public bool MessageValid
        {
            get => messageValid;
            set => Set(ref messageValid, value);
        }

        private string errorMessage;

        public string ErrorMessage
        {
            get => errorMessage;
            set => Set(ref errorMessage, value);
        }

        private ObservableCollection<string> traceLog;

        public ObservableCollection<string> TraceLog
        {
            get => traceLog;
            set => Set(ref traceLog, value);
        }

        private ICommand convertCommand;

        public ICommand ConvertCommand
        {
            get => convertCommand ?? (convertCommand = new RelayCommand(() =>
            {
                try
                {
                    var nmeaMsg = NmeaMessage.FromString(NmeaMessageBody);
                    var result = messageConverter.ConvertMessage(nmeaMsg);
                    TraceLog.Add(result.ToLogInformation());
                }
                catch(Exception ex)
                {
                    TraceLog.Add(ex.Message);
                }
            }));
        }

        private ICommand messageBodyChanged;

        public ICommand MessageBodyChanged
        {
            get => messageBodyChanged ?? (messageBodyChanged = new RelayCommand<string>(param =>
            {
                if (string.IsNullOrEmpty(param))
                {
                    MessageValid = false;
                }
                else if (!nmeaCommunicationRegex.Match(param).Success)
                {
                    MessageValid = false;
                    ErrorMessage = "Please input the message in right format";
                }
                else
                {
                    MessageValid = true;
                }
            }));
        }
    }
}