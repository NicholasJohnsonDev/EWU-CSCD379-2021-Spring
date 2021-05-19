using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlaywrightSharp;
using System.Linq;
using SecretSanta.Web.Tests;

namespace SecretSanta.Web.Test
{
    [TestClass]
    public class EndToEndTests
    {
        private static WebHostServerFixture<Web.Startup, SecretSanta.Api.Startup> Server;

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            Server = new();
        }

        [TestMethod]
        public async Task LaunchHomePage()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);
            //the variable under here is searching the html form for these divs and then looking for the contents. 
            var headerContent = await page.GetTextContentAsync("body > header > div > a");

            await page.ScreenshotAsync("SecretSanta/test/SecretSanta.E2E.Tests/TestScreenshots/LaunchHomePage.png");
            Assert.AreEqual("Secret Santa", headerContent);
        }


        [TestMethod]
        public async Task VerifyUsers()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await page.ClickAsync("text=Users");

            await page.ScreenshotAsync("SecretSanta/test/SecretSanta.E2E.Tests/TestScreenshots/VerifyUsers.png");
            Assert.IsTrue(response.Ok);
        }

        [TestMethod]
        public async Task VerifyGifts()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await page.ClickAsync("text=Gifts");

            await page.ScreenshotAsync("SecretSanta/test/SecretSanta.E2E.Tests/TestScreenshots/VerifyGifts.png");
            Assert.IsTrue(response.Ok);
        }

        [TestMethod]
        public async Task VerifyGroups()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await page.ClickAsync("text=Groups");

            await page.ScreenshotAsync("SecretSanta/test/SecretSanta.E2E.Tests/TestScreenshots/VerifyGroups.png");
            Assert.IsTrue(response.Ok);
        }

        [TestMethod]
        public async Task CreateGift()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true,
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await page.ClickAsync("text=Gifts");

            await page.WaitForSelectorAsync("body > section > section > section");
            var Gifts = await page.QuerySelectorAllAsync("body > section > section > section");
            int initialNumberGifts = Gifts.Count();
            Assert.AreEqual(initialNumberGifts, Gifts.Count());

            await page.ClickAsync("text=Create");

            await page.TypeAsync("input#Title", "TestGiftTitle");
            await page.TypeAsync("input#Description", "TestGiftDescription");
            await page.TypeAsync("input#Url", "TestGiftURL.com");
            await page.TypeAsync("input#Priority", "1");
            await page.SelectOptionAsync("select#UserId", "1");

            await page.ClickAsync("text=Create");
            Gifts = await page.QuerySelectorAllAsync("body > section > section > section");

            await page.ScreenshotAsync("SecretSanta/test/SecretSanta.E2E.Tests/TestScreenshots/CreateGift.png");
            Assert.AreEqual(initialNumberGifts + 1, Gifts.Count());
        }

        [TestMethod]
        public async Task ModifyLastGift()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true,
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await page.ClickAsync("text=Gifts");

            await page.ClickAsync("body > section > section > section:last-child > a > section");
            Assert.IsTrue(response.Ok);


            await page.TypeAsync("input#Title", "TestUpdateGiftTitle");
            await page.TypeAsync("input#Description", "TestUpdateGiftDescription");
            await page.TypeAsync("input#Url", "TestUpdateGiftURL.com");
            await page.TypeAsync("input#Priority", "1");
            await page.SelectOptionAsync("select#UserId", "1");

            await page.ClickAsync("text=Update");
            var sectionText = await page.GetTextContentAsync("body > section > section > section:last-child > a > section > div");

            await page.ScreenshotAsync("SecretSanta/test/SecretSanta.E2E.Tests/TestScreenshots/ModifyLastGift.png");
            Assert.IsTrue(sectionText.Contains("TestUpdate"));
        }

        [TestMethod]
        public async Task DeleteGift()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true,
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await page.ClickAsync("text=Gifts");

            var Gifts = await page.QuerySelectorAllAsync("body > section > section > section");
            int count = Gifts.Count();
            Assert.AreEqual(count, Gifts.Count());

            page.Dialog += (_, args) => args.Dialog.AcceptAsync();

            await page.ClickAsync("body > section > section > section:last-child > a > section > form > button");
            Gifts = await page.QuerySelectorAllAsync("body > section > section > section");

            await page.ScreenshotAsync("SecretSanta/test/SecretSanta.E2E.Tests/TestScreenshots/DeleteGift.png");
            Assert.AreEqual(count - 1, Gifts.Count());
        }
    }
}
