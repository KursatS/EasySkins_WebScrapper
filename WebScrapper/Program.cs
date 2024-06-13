using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Drawing;

//.txt file for keeping color history, path will be in root directory
StreamWriter sw = new StreamWriter("colorLog.txt");

//Opening browser
IWebDriver driver = new EdgeDriver();
driver.Navigate().GoToUrl("https://steamcommunity.com/openid/loginform/?goto=%2Fopenid%2Flogin%3Fopenid.mode%3Dcheckid_setup%26openid.ns%3Dhttp%253A%252F%252Fspecs.openid.net%252Fauth%252F2.0%26openid.identity%3Dhttp%253A%252F%252Fspecs.openid.net%252Fauth%252F2.0%252Fidentifier_select%26openid.claimed_id%3Dhttp%253A%252F%252Fspecs.openid.net%252Fauth%252F2.0%252Fidentifier_select%26openid.return_to%3Dhttps%253A%252F%252Fauth.csgoskins.link%252Freturn%26openid.realm%3Dhttps%253A%252F%252Fauth.csgoskins.link%252F%3Fopenid.mode%3Dcheckid_setup%26openid.ns%3Dhttp%253A%252F%252Fspecs.openid.net%252Fauth%252F2.0%26openid.identity%3Dhttp%253A%252F%252Fspecs.openid.net%252Fauth%252F2.0%252Fidentifier_select%26openid.claimed_id%3Dhttp%253A%252F%252Fspecs.openid.net%252Fauth%252F2.0%252Fidentifier_select%26openid.return_to%3Dhttps%253A%252F%252Fauth.csgoskins.link%252Freturn%26openid.realm%3Dhttps%253A%252F%252Fauth.csgoskins.link%252F");
Thread.Sleep(2000);

//Finding inputs for Steam account
IWebElement eMail = driver.FindElement(By.XPath("//*[@id=\"responsive_page_template_content\"]/div[1]/div[1]/div/div/div/div[2]/div/form/div[1]/input"));
IWebElement password = driver.FindElement(By.XPath("//*[@id=\"responsive_page_template_content\"]/div[1]/div[1]/div/div/div/div[2]/div/form/div[2]/input"));
IWebElement loginBtn = driver.FindElement(By.XPath("//*[@id=\"responsive_page_template_content\"]/div[1]/div[1]/div/div/div/div[2]/div/form/div[4]/button"));

//Filling inputs (Use your non-protection steam account. It doesn't support 2FA login)
eMail.SendKeys("vicrus374");
password.SendKeys("Muzlupasta162");
loginBtn.Click();
Thread.Sleep(2000);

//Second Auth
IWebElement loginBtn2 = driver.FindElement(By.XPath("//*[@id=\"imageLogin\"]"));
loginBtn2.Click();
Thread.Sleep(2000);

//Manual navigating to site
driver.Navigate().GoToUrl("https://easyskins.com/game/twist");
Thread.Sleep(2000);

//In that site every color has 'twist-history__item' classname, finding every color with that way
var colorsClasses = driver.FindElements(By.ClassName("twist-history__item"));
List<String> colors = new List<string>();
foreach (var color in colorsClasses)
{
    colors.Add(color.GetAttribute("class"));
}

//Reversed color list for right data flow (Last color always at the end)
colors.Reverse();
colors.ForEach(delegate (string color)
{
    Console.WriteLine(color);
    sw.WriteLine(color);
});
sw.Close();
//Every rounds last approximately 50 seconds, this section is for live data
while (true)
{
    Thread.Sleep(50000);
    StreamWriter sw2 = new StreamWriter("colorLog.txt", true);
    var nextColorsClasses = driver.FindElements(By.ClassName("twist-history__item"));
    colors.Add(nextColorsClasses.FirstOrDefault().GetAttribute("class"));
    sw2.WriteLine(nextColorsClasses.FirstOrDefault().GetAttribute("class"));
    Console.WriteLine(colors.Last());
    sw2.Close();
}