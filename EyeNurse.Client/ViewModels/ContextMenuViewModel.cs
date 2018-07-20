﻿using Caliburn.Micro;
using EyeNurse.Client.Helpers;
using EyeNurse.Client.Services;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EyeNurse.Client.ViewModels
{
    public class ContextMenuViewModel : Screen
    {
        readonly AppServices _services;
        private Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public ContextMenuViewModel(AppServices servcies)
        {
            _services = servcies;
            IsAutoLaunch = AutoStartup.Check();
        }

        #region properties

        public AppServices Services => _services;

        #region IsPaused

        /// <summary>
        /// The <see cref="IsPaused" /> property's name.
        /// </summary>
        public const string IsPausedPropertyName = "IsPaused";

        private bool _IsPaused;

        /// <summary>
        /// IsPaused
        /// </summary>
        public bool IsPaused
        {
            get { return _IsPaused; }

            set
            {
                if (_IsPaused == value) return;

                _IsPaused = value;
                NotifyOfPropertyChange(IsPausedPropertyName);
            }
        }


        #endregion

        #region IsAutoLaunch

        /// <summary>
        /// The <see cref="IsAutoLaunch" /> property's name.
        /// </summary>
        public const string IsAutoLaunchPropertyName = "IsAutoLaunch";

        private bool _IsAutoLaunch;

        /// <summary>
        /// IsAutoLaunch
        /// </summary>
        public bool IsAutoLaunch
        {
            get { return _IsAutoLaunch; }

            set
            {
                if (_IsAutoLaunch == value) return;

                _IsAutoLaunch = value;
                NotifyOfPropertyChange(IsAutoLaunchPropertyName);
            }
        }

        #endregion

        #endregion

        #region methods
        public void StartWithOS()
        {
            IsAutoLaunch = !IsAutoLaunch;
            AutoStartup.Set(IsAutoLaunch);
            IsAutoLaunch = AutoStartup.Check();
        }

        public void Pause()
        {
            IsPaused = true;
            Services.Pause();
        }

        public void Resum()
        {
            IsPaused = false;
            Services.Resum();
        }

        public void ExitApp()
        {
            Application.Current.Shutdown();
        }

        public void RestImmediately()
        {
            Services.RestImmediately();
        }

        public void About()
        {
            try
            {
                Process.Start("https://mscoder.cn/categories/products/");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "About Ex");
            }
        }

        public void OpenConfigFolder()
        {
            try
            {
                var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                appData = Path.Combine(appData, "EyeNurse");
                Process.Start(appData);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "OpenConfigFolder Ex");
            }
        }

        #endregion
    }
}
