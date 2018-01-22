using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using OxyPlot;
using ResultStudioWPF.Models;

namespace ResultStudioWPF.Messages
{
    public class AppMessages
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
            public static void Send(IEnumerable<MeasurementPoint> argument)
            {
                Messenger.Default.Send(argument);
            }

            public static void Register(object recipient, Action<IEnumerable<MeasurementPoint>> action)
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