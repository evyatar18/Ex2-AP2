﻿using Ex2.Model;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ex2.ViewModels.FlightDisplay
{
    public class ConnectButtonHandler : ICommand
    {
        public event EventHandler CanExecuteChanged;
        
        private IMainModel MainModel { get; set; }
        private ISettingsModel SettingsModel { get; set; }

        public ConnectButtonHandler(IMainModel mainModel,
            ISettingsModel settingsModel)
        {
            MainModel = mainModel;
            SettingsModel = settingsModel;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            /* close old connections */
            if (MainModel.ClientModel.IsOpen)
                MainModel.ClientModel.Close();
            if (MainModel.ServerModel.IsOpen)
                MainModel.ServerModel.Close();

            /* set connection parameters */
            MainModel.ClientModel.IP = SettingsModel.FlightServerIP;
            MainModel.ClientModel.Port = (uint)SettingsModel.FlightCommandPort;

            MainModel.ServerModel.Port = (uint)SettingsModel.FlightInfoPort;

            /* open connections */
            MainModel.ServerModel.Open();
            MainModel.ClientModel.Open();
        }
    }
}
