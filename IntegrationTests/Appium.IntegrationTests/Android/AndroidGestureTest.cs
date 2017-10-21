﻿using Appium.IntegrationTests.Shared.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using System;
using System.Drawing;

namespace Appium.IntegrationTests.Android
{
    [TestFixture()]
    class AndroidGestureTest
    {
        private AndroidDriver<AndroidElement> driver;

        [SetUp]
        public void BeforeAll()
        {
            DesiredCapabilities capabilities = Env.isSauce() ?
                Caps.getAndroid501Caps(Apps.get("androidApiDemos")) :
                Caps.getAndroid19Caps(Apps.get("androidApiDemos"));
            if (Env.isSauce())
            {
                capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.SetCapability("name", "android - complex");
                capabilities.SetCapability("tags", new string[] { "sample" });
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIAndroid;
            driver = new AndroidDriver<AndroidElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            driver.Manage().Timeouts().ImplicitlyWait(Env.IMPLICIT_TIMEOUT_SEC);
            driver.CloseApp();
        }

        [SetUp]
        public void SetUp()
        {
            if (driver != null)
            {
                driver.LaunchApp();
            }
        }

        [TearDown]
        public void AfterAll()
        {
            if (driver != null)
            {
                driver.CloseApp();
                driver.Quit();
            }
            if (!Env.isSauce())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test()]
        [Category("Android")]
        public void SwipeTest()
        {
            driver.StartActivity("io.appium.android.apis",".graphics.FingerPaint");
            AndroidElement element = driver.FindElementById("android:id/content");
            Point point = element.Coordinates.LocationInDom;
            Size size = element.Size;
            driver.Swipe
            (
                point.X + 5,
                point.Y + 5,
                point.X + size.Width - 5,
                point.Y + size.Height - 5,
                200
            );

            driver.Swipe
            (
                point.X + size.Width - 5,
                point.Y + 5,
                point.X + 5,
                point.Y + size.Height - 5,
                2000
            );
        }

        [Test()]
        [Category("Android")]
        public void PincTest()
        {
            driver.StartActivity("io.appium.android.apis", ".graphics.TouchRotateActivity");
            AndroidElement element = driver.FindElementById("android:id/content");
            driver.Pinch(element);
        }

        [Test()]
        [Category("Android")]
        public void ZoomTest()
        {
            driver.StartActivity("io.appium.android.apis", ".graphics.TouchRotateActivity");
            AndroidElement element = driver.FindElementById("android:id/content");
            driver.Zoom(element);
        }
    }
}