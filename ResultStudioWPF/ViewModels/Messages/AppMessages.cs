using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using ResultStudioWPF.Models;

namespace ResultStudioWPF.ViewModels.Messages
{
    public static class AppMessages
    {
        public static class RefreshDataGrid
        {
            public static void Send(bool argument)
            {
                Messenger.Default.Send(argument);
            }

            public static void Register(object recipient, Action<bool> action)
            {
                Messenger.Default.Register(recipient, action);
            }
        }

        public static class PlotDataSet
        {
            public static void Send(IEnumerable<MeasurementPointViewModel> argument)
            {
                Messenger.Default.Send(argument);
            }

            public static void Register(object recipient, Action<IEnumerable<MeasurementPointViewModel>> action)
            {
                Messenger.Default.Register(recipient, action);
            }
        }

        public static class PlotRefresh
        {
            public static void Send(bool argument)
            {
                Messenger.Default.Send(argument);
            }

            public static void Register(object recipient, Action<bool> action)
            {
                Messenger.Default.Register(recipient, action);
            }
        }


        public static class EntityIsValid
        {
            public static void Send(bool argument)
            {
                Messenger.Default.Send(argument);
            }

            public static void Register(object recipient, Action<bool> action)
            {
                Messenger.Default.Register(recipient, action);
            }
        }
    }
}