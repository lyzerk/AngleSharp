﻿using AngleSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AngleSharp
{
    /// <summary>
    /// Represents global configuration for the AngleSharp library.
    /// This is independent of parsing / document creation options
    /// given in the DocumentOptions class.
    /// </summary>
    public sealed class Configuration
    {
        #region Singleton

        static Configuration instance;

        static Configuration()
        {
            Reset();
        }

        #endregion

        #region Members

        List<Type> requester;
        CultureInfo culture;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new default configuration.
        /// </summary>
        Configuration()
        {
            requester = new List<Type>();
            culture = CultureInfo.CurrentUICulture;
        }

        /// <summary>
        /// Copies the new configuration from the given one.
        /// </summary>
        /// <param name="config">The configuration to clone.</param>
        Configuration(Configuration config)
            : this()
        {
            requester.AddRange(config.requester);
            culture = config.culture;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if at least one HttpRequester has been registered.
        /// </summary>
        public static Boolean HasHttpRequester
        {
            get { return instance.requester.Count > 0; }
        }

        /// <summary>
        /// Gets the language (code, e.g. en-US, de-DE) to use.
        /// </summary>
        public static String Language
        {
            get { return instance.culture.Name; }
        }

        /// <summary>
        /// Gets or sets the culture to use.
        /// </summary>
        public static CultureInfo Culture
        {
            get { return instance.culture; }
            set { instance.culture = value ?? CultureInfo.CurrentUICulture; }
        }

        #endregion

        #region Register / Unregister

        /// <summary>
        /// Registers a new HttpRequester for making resource requests.
        /// </summary>
        /// <typeparam name="T">The type of the requester.</typeparam>
        public static void RegisterHttpRequester<T>()
            where T: IHttpRequester
        {
            var requester = typeof(T);

            if(!instance.requester.Contains(requester))
                instance.requester.Add(requester);
        }

        /// <summary>
        /// Removes a registered HttpRequester for making resource requests.
        /// </summary>
        /// <typeparam name="T">The type of the requester.</typeparam>
        public static void UnregisterHttpRequester<T>()
            where T : IHttpRequester
        {
            var requester = typeof(T);

            if (instance.requester.Contains(requester))
                instance.requester.Remove(requester);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resets the current configuration to the initial 
        /// configuration.
        /// </summary>
        public static void Reset()
        {
            instance = new Configuration();
        }

        /// <summary>
        /// Loads the specified configuration, overwriting the current one.
        /// </summary>
        /// <param name="config">The configuration to load.</param>
        public static void Load(Configuration config)
        {
            instance = config;
        }

        /// <summary>
        /// Saves the current configuration to be used later.
        /// </summary>
        /// <returns>The saved configuration state.</returns>
        public static Configuration Save()
        {
            var old = instance;
            instance = new Configuration(old);
            return old;
        }

        #endregion
    }
}
