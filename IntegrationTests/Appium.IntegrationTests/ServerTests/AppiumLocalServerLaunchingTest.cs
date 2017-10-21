﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Net;
using System.Threading;
using Appium.IntegrationTests.Shared.Helpers;

namespace Appium.IntegrationTests.ServerTests
{
    [TestFixture()]
    public class AppiumLocalServerLaunchingTest
    {
        private string PathToCustomizedAppiumJS;
        private string testIP;

        [SetUp]
        public void BeforeAll()
        {
            IPHostEntry host;
            string hostName = Dns.GetHostName();
            host = Dns.GetHostEntry(hostName);
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    testIP = ip.ToString();
                    break;
                }
            }
            Console.WriteLine(testIP);

            var nodePathHelper = new NodePathHelper();
            PathToCustomizedAppiumJS = nodePathHelper.GetNodePath();
        }

        [Test]
        [Category("Server")]
        public void CheckAbilityToBuildDefaultService()
        {
            AppiumLocalService service = AppiumLocalService.BuildDefaultService();
            try
            {
                service.Start();
                Assert.AreEqual(true, service.IsRunning);
            }
            finally
            {
                service.Dispose();
            }
        }

        [Test]
        [Category("Server")]
        public void CheckAbilityToBuildServiceUsingNodeDefinedInProperties()
        {
            AppiumLocalService service = null;
            try
            {
                string definedNode = PathToCustomizedAppiumJS;
                Environment.SetEnvironmentVariable(AppiumServiceConstants.AppiumBinaryPath, definedNode);
                service = AppiumLocalService.BuildDefaultService();
                service.Start();
                Assert.AreEqual(true, service.IsRunning);
            }
            finally
            {
                if (service != null)
                {
                    service.Dispose();
                }
                Environment.SetEnvironmentVariable(AppiumServiceConstants.AppiumBinaryPath, string.Empty);
            }
        }

        [Test]
        [Category("Server")]
        public void CheckAbilityToBuildServiceUsingNodeDefinedExplicitly()
        {
            AppiumLocalService service = null;
            try
            {
                service = new AppiumServiceBuilder().WithAppiumJS(new FileInfo(PathToCustomizedAppiumJS)).Build();
                service.Start();
                Assert.AreEqual(true, service.IsRunning);
            }
            finally
            {
                if (service != null)
                {
                    service.Dispose();
                }
            }
        }

        [Test]
        [Category("Server")]
        public void CheckAbilityToStartServiceOnAFreePort()
        {
            AppiumLocalService service = null;
            try
            {
                service = new AppiumServiceBuilder().UsingAnyFreePort().Build();
                service.Start();
                Assert.AreEqual(true, service.IsRunning);
            }
            finally
            {
                if (service != null)
                {
                    service.Dispose();
                }
            }
        }

        [Test]
        [Category("Server")]
        public void CheckStartingOfAServiceWithNonLocalhostIP()
        {
            AppiumLocalService service = new AppiumServiceBuilder().WithIPAddress(testIP).UsingPort(4000).
                Build();
            try
            {
                service.Start();
                Assert.IsTrue(service.IsRunning);
            }
            finally
            {
                service.Dispose();
            }
        }

        [Test]
        [Category("Server")]
        public void CheckAbilityToStartServiceUsingFlags()
        {
            AppiumLocalService service = null;
            OptionCollector args = new OptionCollector().AddArguments(GeneralOptionList.CallbackAddress(testIP))
                .AddArguments(GeneralOptionList.OverrideSession());
            try
            {
                service = new AppiumServiceBuilder().WithArguments(args).Build();
                service.Start();
                Assert.IsTrue(service.IsRunning);
            }
            finally
            {
                if (service != null)
                {
                    service.Dispose();
                }
            }
        }

        [Test]
        [Category("Server")]
        public void CheckAbilityToStartServiceUsingCapabilities()
        {
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability(MobileCapabilityType.PlatformName, "Android");
            capabilities.SetCapability(MobileCapabilityType.FullReset, true);
            capabilities.SetCapability(MobileCapabilityType.NewCommandTimeout, 60);
            capabilities.SetCapability(AndroidMobileCapabilityType.AppPackage, "io.appium.android.apis");
            capabilities.SetCapability(AndroidMobileCapabilityType.AppActivity, ".view.WebView1");

            OptionCollector args = new OptionCollector().AddCapabilities(capabilities);
            AppiumLocalService service = null;
            try
            {
                service = new AppiumServiceBuilder().WithArguments(args).Build();
                service.Start();
                Assert.IsTrue(service.IsRunning);
            }
            finally
            {
                if (service != null)
                {
                    service.Dispose();
                }
            }
        }

        [Test]
        [Category("Server")]
        public void CheckAbilityToStartServiceUsingCapabilitiesAndFlags()
        {
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability(MobileCapabilityType.PlatformName, "Android");
            capabilities.SetCapability(MobileCapabilityType.FullReset, true);
            capabilities.SetCapability(MobileCapabilityType.NewCommandTimeout, 60);
            capabilities.SetCapability(AndroidMobileCapabilityType.AppPackage, "io.appium.android.apis");
            capabilities.SetCapability(AndroidMobileCapabilityType.AppActivity, ".view.WebView1");

            OptionCollector args = new OptionCollector().AddCapabilities(capabilities).AddArguments(GeneralOptionList.CallbackAddress(testIP))
                .AddArguments(GeneralOptionList.OverrideSession());
            AppiumLocalService service = null;
            try
            {
                service = new AppiumServiceBuilder().WithArguments(args).
                    Build();
                service.Start();
                Assert.IsTrue(service.IsRunning);
            }
            finally
            {
                if (service != null)
                {
                    service.Dispose();
                }
            }
        }

        [Test]
        [Category("Server")]
        public void CheckAbilityToShutDownService()
        {
            AppiumLocalService service = AppiumLocalService.BuildDefaultService();
            service.Start();
            service.Dispose();
            Assert.IsTrue(!service.IsRunning);
        }

        [Test]
        [Category("Server")]
        public void CheckAbilityToStartAndShutDownFewServices()
        {
            AppiumLocalService service1 = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            AppiumLocalService service2 = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            AppiumLocalService service3 = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            AppiumLocalService service4 = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            service1.Start();
            service2.Start();
            service3.Start();
            service4.Start();
            service1.Dispose();
            Thread.Sleep(1000);
            service2.Dispose();
            Thread.Sleep(1000);
            service3.Dispose();
            Thread.Sleep(1000);
            service4.Dispose();
            Assert.IsTrue(!service1.IsRunning);
            Assert.IsTrue(!service2.IsRunning);
            Assert.IsTrue(!service3.IsRunning);
            Assert.IsTrue(!service4.IsRunning);
        }


        [Test]
        [Category("Server")]
        public void CheckTheAbilityToDefineTheDesiredLogFile()
        {
            FileInfo log = new FileInfo("Log.txt");
            AppiumLocalService service = new AppiumServiceBuilder().WithLogFile(log).Build();
            try
            {
                service.Start();
                Assert.IsTrue(log.Exists);
                Assert.IsTrue(log.Length > 0); //There should be Appium greeting messages
            }
            finally
            {
                service.Dispose();
                if (log.Exists)
                {
                    File.Delete(log.FullName);
                }
                service.Dispose();
            }
        }

    }
}
