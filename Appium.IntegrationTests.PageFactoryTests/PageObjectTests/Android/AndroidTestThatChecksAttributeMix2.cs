﻿using Appium.Integration.Tests.Helpers;
using Appium.Integration.Tests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.PageObjects;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace Appium.IntegrationTests.PageFactoryTests.PageObjectTests.Android
{
    [TestFixture()]
    public class AndroidTestThatChecksAttributeMix2
    {
        private AndroidDriver<AppiumWebElement> driver;
        private AndroidPageObjectChecksAttributeMixOnNativeApp2 pageObject;

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
            driver = new AndroidDriver<AppiumWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            TimeOutDuration timeSpan = new TimeOutDuration(new TimeSpan(0, 0, 0, 5, 0));
            pageObject = new AndroidPageObjectChecksAttributeMixOnNativeApp2();
            PageFactory.InitElements(driver, pageObject, new AppiumPageObjectMemberDecorator(timeSpan));
        }

        [TearDown]
        public void AfterEach()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.isSauce())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test()]
        [Category("AndroidFailed")]
        public void CheckMobileElement()
        {
            Assert.NotNull(pageObject.GetMobileElementText());
        }

        [Test()]
        [Category("AndroidFailed")]
        public void CheckMobileElements()
        {
            Assert.GreaterOrEqual(pageObject.GetMobileElementSize(), 1);
        }

        [Test()]
        [Category("AndroidFailed")]
        public void CheckMobileElementProperty()
        {
            Assert.NotNull(pageObject.GetMobileElementPropertyText());
        }

        [Test()]
        [Category("AndroidFailed")]
        public void CheckMobileElementsProperty()
        {
            Assert.GreaterOrEqual(pageObject.GetMobileElementPropertySize(), 1);
        }

        [Test()]
        [Category("AndroidFailed")]
        public void CheckElementFoundUsingMultipleLocators()
        {
            Assert.NotNull(pageObject.GetMultipleFindByElementText());
        }

        [Test()]
        [Category("AndroidFailed")]
        public void CheckElementsFoundUsingMultipleLocators()
        {
            Assert.GreaterOrEqual(pageObject.GetMultipleFindByElementSize(), 10);
            Assert.LessOrEqual(pageObject.GetMultipleFindByElementSize(), 14);
        }

        [Test()]
        [Category("AndroidFailed")]
        public void CheckElementFoundUsingMultipleLocatorsProperty()
        {
            Assert.NotNull(pageObject.GetMultipleFindByElementPropertyText());
        }

        [Test()]
        [Category("AndroidFailed")]
        public void CheckElementsFoundUsingMultipleLocatorssProperty()
        {
            Assert.GreaterOrEqual(pageObject.GetMultipleFindByElementPropertySize(), 10);
            Assert.LessOrEqual(pageObject.GetMultipleFindByElementSize(), 14);
        }

        [Test()]
        [Category("AndroidFailed")]
        public void CheckElementFoundByChainedSearch()
        {
            Assert.NotNull(pageObject.GetFoundByChainedSearchElementText());
        }

        [Test()]
        [Category("AndroidFailed")]
        public void CheckElementsFoundByChainedSearch()
        {
            Assert.GreaterOrEqual(pageObject.GetFoundByChainedSearchElementSize(), 10);
            Assert.LessOrEqual(pageObject.GetMultipleFindByElementSize(), 14);
        }

        [Test()]
        [Category("AndroidFailed")]
        public void CheckFoundByChainedSearchElementProperty()
        {
            Assert.NotNull(pageObject.GetFoundByChainedSearchElementPropertyText());
        }

        [Test()]
        [Category("AndroidFailed")]
        public void CheckFoundByChainedSearchElementsProperty()
        {
            Assert.GreaterOrEqual(pageObject.GetFoundByChainedSearchElementPropertySize(), 10);
            Assert.LessOrEqual(pageObject.GetMultipleFindByElementSize(), 14);
        }

        [Test()]
        [Category("AndroidFailed")]
        public void CheckElementMatchedToAll()
        {
            Assert.NotNull(pageObject.GetMatchedToAllLocatorsElementText());
        }

        [Test()]
        [Category("AndroidFailed")]
        public void CheckElementsMatchedToAll()
        {
            Assert.GreaterOrEqual(pageObject.GetMatchedToAllLocatorsElementSize(), 1);
            Assert.LessOrEqual(pageObject.GetMatchedToAllLocatorsElementSize(), 13);
        }

        [Test()]
        [Category("AndroidFailed")]
        public void CheckElementMatchedToAllProperty()
        {
            Assert.NotNull(pageObject.GetMatchedToAllLocatorsElementPropertyText());
        }

        [Test()]
        [Category("AndroidFailed")]
        public void CheckElementMatchedToAllElementsProperty()
        {
            Assert.GreaterOrEqual(pageObject.GetMatchedToAllLocatorsElementPropertySize(), 1);
            Assert.LessOrEqual(pageObject.GetMatchedToAllLocatorsElementPropertySize(), 13);
        }
    }
}