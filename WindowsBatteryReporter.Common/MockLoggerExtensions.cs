namespace WindowsBatteryReporter.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Microsoft.Extensions.Logging;

    using Moq;
    using Moq.Language.Flow;

    using Newtonsoft.Json;

    /// <summary>
    ///     Extension methods for convenient mock setups.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class MockLoggerExtensions
    {
        #region Setup LogDebug

        public static ISetup<ILogger<TLogger>> SetupDebugLogging<TLogger>(this Mock<ILogger<TLogger>> logger, string expectedMessage, params object[] expectedArguments)
        {
            return SetupDebugLogging(logger, expectedMessage, expectedArguments == null ? new List<object>() : expectedArguments.ToList(), null);
        }

        public static ISetup<ILogger<TLogger>> SetupDebugLogging<TLogger>(this Mock<ILogger<TLogger>> logger, string expectedMessage, IList<object> expectedArguments = null, bool verifiable = true, Dictionary<string, string> scopeState = null)
        {
            return SetupLogging(logger, LogLevel.Debug, expectedMessage, expectedArguments, verifiable, scopeState);
        }

        #endregion

        #region Setup LogInformation

        public static ISetup<ILogger<TLogger>> SetupInformationLogging<TLogger>(this Mock<ILogger<TLogger>> logger, string expectedMessage, params object[] expectedArguments)
        {
            return SetupInformationLogging(logger, expectedMessage, expectedArguments == null ? new List<object>() : expectedArguments.ToList(), null);
        }

        public static ISetup<ILogger<TLogger>> SetupInformationLogging<TLogger>(this Mock<ILogger<TLogger>> logger, string expectedMessage, IList<object> expectedArguments = null, bool verifiable = true, Dictionary<string, string> scopeState = null)
        {
            return SetupLogging(logger, LogLevel.Information, expectedMessage, expectedArguments, verifiable, scopeState);
        }

        #endregion

        #region Setup LogWarning

        public static ISetup<ILogger<TLogger>> SetupWarningLogging<TLogger>(this Mock<ILogger<TLogger>> logger, string expectedMessage, params object[] expectedArguments)
        {
            return SetupWarningLogging(logger, expectedMessage, expectedArguments == null ? new List<object>() : expectedArguments.ToList(), null);
        }

        public static ISetup<ILogger<TLogger>> SetupWarningLogging<TLogger>(this Mock<ILogger<TLogger>> logger, string expectedMessage, IList<object> expectedArguments = null, bool verifiable = true, Dictionary<string, string> scopeState = null)
        {
            return SetupLogging(logger, LogLevel.Warning, expectedMessage, expectedArguments, verifiable, scopeState);
        }

        public static ISetup<ILogger<TLogger>> SetupWarningLogging<TLogger, TException>(this Mock<ILogger<TLogger>> logger, string expectedMessage, params object[] expectedArguments) where TException : Exception
        {
            return SetupWarningLogging<TLogger, TException>(logger, expectedMessage, expectedArguments == null ? new List<object>() : expectedArguments.ToList(), null);
        }

        public static ISetup<ILogger<TLogger>> SetupWarningLogging<TLogger, TException>(this Mock<ILogger<TLogger>> logger, string expectedMessage, IList<object> expectedArguments = null, bool verifiable = true, Dictionary<string, string> scopeState = null) where TException : Exception
        {
            return SetupLogging<TLogger, TException>(logger, LogLevel.Warning, expectedMessage, expectedArguments, verifiable, scopeState);
        }

        public static ISetup<ILogger<TLogger>> SetupWarningLogging<TLogger, TException>(this Mock<ILogger<TLogger>> logger, TException expectedException, string expectedMessage, params object[] expectedArguments) where TException : Exception
        {
            return SetupWarningLogging(logger, expectedException, expectedMessage, expectedArguments == null ? new List<object>() : expectedArguments.ToList());
        }

        public static ISetup<ILogger<TLogger>> SetupWarningLogging<TLogger, TException>(this Mock<ILogger<TLogger>> logger, TException expectedException, string expectedMessage, IList<object> expectedArguments = null, bool verifiable = true, Dictionary<string, string> scopeState = null) where TException : Exception
        {
            return SetupLogging(logger, LogLevel.Warning, expectedException, expectedMessage, expectedArguments, verifiable, scopeState);
        }

        #endregion

        #region Setup LogError

        public static ISetup<ILogger<TLogger>> SetupErrorLogging<TLogger>(this Mock<ILogger<TLogger>> logger, string expectedMessage, params object[] expectedArguments)
        {
            return SetupErrorLogging(logger, expectedMessage, expectedArguments == null ? new List<object>() : expectedArguments.ToList(), null);
        }

        public static ISetup<ILogger<TLogger>> SetupErrorLogging<TLogger>(this Mock<ILogger<TLogger>> logger, string expectedMessage, IList<object> expectedArguments = null, bool verifiable = true, Dictionary<string, string> scopeState = null)
        {
            return SetupLogging(logger, LogLevel.Warning, expectedMessage, expectedArguments, verifiable, scopeState);
        }

        public static ISetup<ILogger<TLogger>> SetupErrorLogging<TLogger, TException>(this Mock<ILogger<TLogger>> logger, string expectedMessage, params object[] expectedArguments) where TException : Exception
        {
            return SetupErrorLogging<TLogger, TException>(logger, expectedMessage, expectedArguments == null ? new List<object>() : expectedArguments.ToList(), null);
        }

        public static ISetup<ILogger<TLogger>> SetupErrorLogging<TLogger, TException>(this Mock<ILogger<TLogger>> logger, string expectedMessage, IList<object> expectedArguments = null, bool verifiable = true, Dictionary<string, string> scopeState = null) where TException : Exception
        {
            return SetupLogging<TLogger, TException>(logger, LogLevel.Warning, expectedMessage, expectedArguments, verifiable, scopeState);
        }

        public static ISetup<ILogger<TLogger>> SetupErrorLogging<TLogger, TException>(this Mock<ILogger<TLogger>> logger, TException expectedException, string expectedMessage, params object[] expectedArguments) where TException : Exception
        {
            return SetupErrorLogging(logger, expectedException, expectedMessage, expectedArguments == null ? new List<object>() : expectedArguments.ToList());
        }

        public static ISetup<ILogger<TLogger>> SetupErrorLogging<TLogger, TException>(this Mock<ILogger<TLogger>> logger, TException expectedException, string expectedMessage, IList<object> expectedArguments = null, bool verifiable = true, Dictionary<string, string> scopeState = null) where TException : Exception
        {
            return SetupLogging(logger, LogLevel.Warning, expectedException, expectedMessage, expectedArguments, verifiable, scopeState);
        }

        #endregion

        public static ISetup<ILogger<TLogger>> SetupLogging<TLogger>(this Mock<ILogger<TLogger>> logger, LogLevel expectedLogLevel, string expectedMessage, params object[] expectedArguments)
        {
            return SetupLogging<TLogger, Exception>(logger, expectedLogLevel, expectedMessage, expectedArguments == null ? new List<object>() : expectedArguments.ToList());
        }

        public static ISetup<ILogger<TLogger>> SetupLogging<TLogger>(this Mock<ILogger<TLogger>> logger, LogLevel expectedLogLevel, string expectedMessage, IList<object> expectedArguments = null, bool verifiable = true, Dictionary<string, string> scopeState = null)
        {
            return SetupLogging<TLogger, Exception>(logger, expectedLogLevel, null, expectedMessage, expectedArguments, verifiable, scopeState);
        }

        public static ISetup<ILogger<TLogger>> SetupLogging<TLogger, TException>(this Mock<ILogger<TLogger>> logger, LogLevel expectedLogLevel, string expectedMessage, params object[] expectedArguments) where TException : Exception
        {
            return SetupLogging<TLogger, TException>(logger, expectedLogLevel, expectedMessage, expectedArguments == null ? new List<object>() : expectedArguments.ToList());
        }

        public static ISetup<ILogger<TLogger>> SetupLogging<TLogger, TException>(this Mock<ILogger<TLogger>> logger, LogLevel expectedLogLevel, string expectedMessage, IList<object> expectedArguments = null, bool verifiable = true, Dictionary<string, string> scopeState = null) where TException : Exception
        {
            return SetupLogging<TLogger, TException>(logger, expectedLogLevel, null, expectedMessage, expectedArguments, verifiable, scopeState);
        }

        public static ISetup<ILogger<TLogger>> SetupLogging<TLogger, TException>(this Mock<ILogger<TLogger>> logger, LogLevel expectedLogLevel, TException expectedException, string expectedMessage, params object[] expectedArguments) where TException : Exception
        {
            return SetupLogging(logger, expectedLogLevel, expectedException, expectedMessage, expectedArguments == null ? new List<object>() : expectedArguments.ToList());
        }

        public static ISetup<ILogger<TLogger>> SetupLogging<TLogger, TException>(this Mock<ILogger<TLogger>> logger, LogLevel expectedLogLevel, TException expectedException, string expectedMessage, IList<object> expectedArguments = null, bool verifiable = true, Dictionary<string, object> scopeState = null) where TException : Exception
        {
            ISetup<ILogger<TLogger>> loggerSetup;

            if (expectedException == null)
            {
                loggerSetup = logger.Setup(
                    x => x.Log(
                        It.Is<LogLevel>(y => y == expectedLogLevel),
                        It.IsAny<EventId>(),
                        It.Is<It.IsAnyType>((stateObject, _) => ValidateState(stateObject, expectedMessage, expectedArguments)),
                        It.IsAny<TException>(),
                        It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
            }
            else
            {
                loggerSetup = logger.Setup(
                    x => x.Log(
                        It.Is<LogLevel>(y => y == expectedLogLevel),
                        It.IsAny<EventId>(),
                        It.Is<It.IsAnyType>((stateObject, _) => ValidateState(stateObject, expectedMessage, expectedArguments)),
                        It.Is<TException>(y => y == expectedException),
                        It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
            }

            if (verifiable)
            {
                loggerSetup.Verifiable();
            }

            if (scopeState != null)
            {
                foreach (KeyValuePair<string, object> kvp in scopeState)
                {
                    ISetup<ILogger<TLogger>, IDisposable> scopeSetup = logger.Setup(x => x.BeginScope(It.Is<Dictionary<string, object>>(y => y[kvp.Key] == kvp.Value)));

                    if (verifiable)
                    {
                        scopeSetup.Verifiable();
                    }
                }
            }

            return loggerSetup;
        }

        private static bool ValidateState(object stateObject, string expectedMessage, IList<object> arguments)
        {
            IReadOnlyList<KeyValuePair<string, object>> values = (IReadOnlyList<KeyValuePair<string, object>>)stateObject;

            if (values.Count == 1)
            {
                return string.Equals(stateObject.ToString(), expectedMessage, StringComparison.Ordinal);
            }

            if (arguments == null || arguments.Count != values.Count - 1)
            {
                return false;
            }

            int index = 0;
            foreach (KeyValuePair<string, object> kvp in values)
            {
                if (string.Equals(kvp.Key, "{OriginalFormat}", StringComparison.Ordinal))
                {
                    if (!string.Equals(kvp.Value.ToString(), expectedMessage, StringComparison.Ordinal))
                    {
                        return false;
                    }

                    continue;
                }

                if (!ObjectsEqual(arguments[index], kvp.Value))
                {
                    return false;
                }

                index++;
            }

            return true;
        }

        private static bool ObjectsEqual(object expectedArgument, object loggerArgument)
        {
            if (ReferenceEquals(expectedArgument, loggerArgument))
            {
                return true;
            }

            if (Equals(expectedArgument, loggerArgument))
            {
                return true;
            }

            return string.Equals(JsonConvert.SerializeObject(expectedArgument), JsonConvert.SerializeObject(loggerArgument));
        }

        ///// <summary>
        /////     Sets up the mock logger.
        ///// </summary>
        ///// <typeparam name="T">The logger of type.</typeparam>
        ///// <param name="mockLogger">The mock logger.</param>
        ///// <param name="logLevel">The log level.</param>
        ///// <param name="eventId">The event identifier.</param>
        ///// <param name="message">The log message.</param>
        ///// <param name="exception">The exception thrown, if applicable.</param>
        //public static void SetupLog<T>(this Mock<ILogger<T>> mockLogger, LogLevel logLevel, EventId eventId, string message, Exception exception)
        //{
        //    if (mockLogger == null)
        //    {
        //        throw new ArgumentNullException(nameof(mockLogger));
        //    }

        //    mockLogger.Setup(x => x.Log(
        //        logLevel,
        //        eventId,
        //        It.Is<It.IsAnyType>((testObject, testType) => testObject.ToString() == message && testType.Name == "FormattedLogValues"),
        //        exception,
        //        It.IsAny<Func<It.IsAnyType, Exception, string>>()));
        //}
    }
}
