using dablejs_aspnetcore_sample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace dablejs_aspnetcore_sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

      
        public JsonResult DableApi([FromBody] DableRequest req)
        {
            int pageIndex = req.pageIndex;
            int recordPerPage = req.recordPerPage;
            string orderColumnName = req.orderColumnName;
            bool orderAsc = req.orderAsc;
            try
            {
                var data = customers.AsQueryable();

                if (!string.IsNullOrEmpty(orderColumnName))
                {
                    var orderKey = orderColumnName + (orderAsc ? " asc" : " desc");
                    data = data.OrderBy(orderKey);
                }
                var totalCount = data.Count();
                var list = data.Skip(pageIndex * recordPerPage).Take(recordPerPage).ToList();
                var response = new
                {
                    totalCount = totalCount,
                    list = list
                };
                return Json(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Customer> customers = new List<Customer>
        {
            new Customer{Id=1, FirstName= "Ken",FamilyName= "Sánchez",PhoneNumber= "697-555-0142",EmailAddress= "ken0@adventure-works.com",Address= "1970 Napa Ct.",City= "Bothell", BirthDay = DateTime.ParseExact("1989-01-07", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=2, FirstName= "Terri",FamilyName= "Duffy",PhoneNumber= "819-555-0175",EmailAddress= "terri0@adventure-works.com",Address= "9833 Mt. Dias Blv.",City= "Bothell", BirthDay = DateTime.ParseExact("1988-01-24", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=3, FirstName= "Roberto",FamilyName= "Tamburello",PhoneNumber= "212-555-0187",EmailAddress= "roberto0@adventure-works.com",Address= "7484 Roundtree Drive",City= "Bothell", BirthDay = DateTime.ParseExact("1987-11-04", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=4, FirstName= "Rob",FamilyName= "Walters",PhoneNumber= "612-555-0100",EmailAddress= "rob0@adventure-works.com",Address= "9539 Glenside Dr",City= "Bothell", BirthDay = DateTime.ParseExact("1987-11-28", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=5, FirstName= "Gail",FamilyName= "Erickson",PhoneNumber= "849-555-0139",EmailAddress= "gail0@adventure-works.com",Address= "1226 Shoe St.",City= "Bothell", BirthDay = DateTime.ParseExact("1987-12-30", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=6, FirstName= "Jossef",FamilyName= "Goldberg",PhoneNumber= "122-555-0189",EmailAddress= "jossef0@adventure-works.com",Address= "1399 Firestone Drive",City= "Bothell", BirthDay = DateTime.ParseExact("1993-12-16", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=7, FirstName= "Dylan",FamilyName= "Miller",PhoneNumber= "181-555-0156",EmailAddress= "dylan0@adventure-works.com",Address= "5672 Hale Dr.",City= "Bothell", BirthDay = DateTime.ParseExact("1989-02-01", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=8, FirstName= "Diane",FamilyName= "Margheim",PhoneNumber= "815-555-0138",EmailAddress= "diane1@adventure-works.com",Address= "6387 Scenic Avenue",City= "Bothell", BirthDay = DateTime.ParseExact("1988-12-22", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=9, FirstName= "Gigi",FamilyName= "Matthew",PhoneNumber= "185-555-0186",EmailAddress= "gigi0@adventure-works.com",Address= "8713 Yosemite Ct.",City= "Bothell", BirthDay = DateTime.ParseExact("1989-01-09", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=10, FirstName= "Michael",FamilyName= "Raheem",PhoneNumber= "330-555-2568",EmailAddress= "michael6@adventure-works.com",Address= "250 Race Court",City= "Bothell", BirthDay = DateTime.ParseExact("1989-04-26", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=11, FirstName= "Ovidiu",FamilyName= "Cracium",PhoneNumber= "719-555-0181",EmailAddress= "ovidiu0@adventure-works.com",Address= "1318 Lasalle Street",City= "Bothell", BirthDay = DateTime.ParseExact("1990-11-28", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=12, FirstName= "Thierry",FamilyName= "D'Hers",PhoneNumber= "168-555-0183",EmailAddress= "thierry0@adventure-works.com",Address= "5415 San Gabriel Dr.",City= "Bothell", BirthDay = DateTime.ParseExact("1987-12-04", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=13, FirstName= "Janice",FamilyName= "Galvin",PhoneNumber= "473-555-0117",EmailAddress= "janice0@adventure-works.com",Address= "9265 La Paz",City= "Bothell", BirthDay = DateTime.ParseExact("1990-12-16", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=14, FirstName= "Michael",FamilyName= "Sullivan",PhoneNumber= "465-555-0156",EmailAddress= "michael8@adventure-works.com",Address= "8157 W. Book",City= "Bothell", BirthDay = DateTime.ParseExact("1990-12-23", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=15, FirstName= "Sharon",FamilyName= "Salavaria",PhoneNumber= "970-555-0138",EmailAddress= "sharon0@adventure-works.com",Address= "4912 La Vuelta",City= "Bothell", BirthDay = DateTime.ParseExact("1991-01-11", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=16, FirstName= "David",FamilyName= "Bradley",PhoneNumber= "913-555-0172",EmailAddress= "david0@adventure-works.com",Address= "40 Ellis St.",City= "Bothell", BirthDay = DateTime.ParseExact("1987-12-13", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=17, FirstName= "Kevin",FamilyName= "Brown",PhoneNumber= "150-555-0189",EmailAddress= "kevin0@adventure-works.com",Address= "6696 Anchor Drive",City= "Bothell", BirthDay = DateTime.ParseExact("1987-01-19", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=18, FirstName= "John",FamilyName= "Wood",PhoneNumber= "486-555-0150",EmailAddress= "john5@adventure-works.com",Address= "1873 Lion Circle",City= "Bothell", BirthDay = DateTime.ParseExact("1991-01-31", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=19, FirstName= "Mary",FamilyName= "Dempsey",PhoneNumber= "124-555-0114",EmailAddress= "mary2@adventure-works.com",Address= "3148 Rose Street",City= "Bothell", BirthDay = DateTime.ParseExact("1991-02-07", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=20, FirstName= "Wanida",FamilyName= "Benshoof",PhoneNumber= "708-555-0141",EmailAddress= "wanida0@adventure-works.com",Address= "6872 Thornwood Dr.",City= "Bothell", BirthDay = DateTime.ParseExact("1990-12-31", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=21, FirstName= "Terry",FamilyName= "Eminhizer",PhoneNumber= "138-555-0118",EmailAddress= "terry0@adventure-works.com",Address= "5747 Shirley Drive",City= "Bothell", BirthDay = DateTime.ParseExact("1989-02-23", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=22, FirstName= "Sariya",FamilyName= "Harnpadoungsataya",PhoneNumber= "399-555-0176",EmailAddress= "sariya0@adventure-works.com",Address= "636 Vine Hill Way",City= "Portland", BirthDay = DateTime.ParseExact("1988-12-05", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=23, FirstName= "Mary",FamilyName= "Gibson",PhoneNumber= "531-555-0183",EmailAddress= "mary0@adventure-works.com",Address= "6657 Sand Pointe Lane",City= "Seattle", BirthDay = DateTime.ParseExact("1989-01-05", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=24, FirstName= "Jill",FamilyName= "Williams",PhoneNumber= "510-555-0121",EmailAddress= "jill0@adventure-works.com",Address= "80 Sunview Terrace",City= "Duluth", BirthDay = DateTime.ParseExact("1989-01-11", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=25, FirstName= "James",FamilyName= "Hamilton",PhoneNumber= "870-555-0122",EmailAddress= "james1@adventure-works.com",Address= "9178 Jumping St.",City= "Dallas", BirthDay = DateTime.ParseExact("1989-01-27", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=26, FirstName= "Peter",FamilyName= "Krebs",PhoneNumber= "913-555-0196",EmailAddress= "peter0@adventure-works.com",Address= "5725 Glaze Drive",City= "San Francisco", BirthDay = DateTime.ParseExact("1988-11-24", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=27, FirstName= "Jo",FamilyName= "Brown",PhoneNumber= "632-555-0129",EmailAddress= "jo0@adventure-works.com",Address= "2487 Riverside Drive",City= "Nevada", BirthDay = DateTime.ParseExact("1988-02-20", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=28, FirstName= "Guy",FamilyName= "Gilbert",PhoneNumber= "320-555-0195",EmailAddress= "guy1@adventure-works.com",Address= "9228 Via Del Sol",City= "Phoenix", BirthDay = DateTime.ParseExact("1986-06-23", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=29, FirstName= "Mark",FamilyName= "McArthur",PhoneNumber= "417-555-0154",EmailAddress= "mark1@adventure-works.com",Address= "8291 Crossbow Way",City= "Memphis", BirthDay = DateTime.ParseExact("1989-01-16", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=30, FirstName= "Britta",FamilyName= "Simon",PhoneNumber= "955-555-0169",EmailAddress= "britta0@adventure-works.com",Address= "9707 Coldwater Drive",City= "Orlando", BirthDay = DateTime.ParseExact("1989-01-22", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=31, FirstName= "Margie",FamilyName= "Shoop",PhoneNumber= "818-555-0128",EmailAddress= "margie0@adventure-works.com",Address= "9100 Sheppard Avenue North",City= "Ottawa", BirthDay = DateTime.ParseExact("1988-12-28", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=32, FirstName= "Rebecca",FamilyName= "Laszlo",PhoneNumber= "314-555-0113",EmailAddress= "rebecca0@adventure-works.com",Address= "26910 Indela Road",City= "Montreal", BirthDay = DateTime.ParseExact("1988-12-22", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=33, FirstName= "Annik",FamilyName= "Stahl",PhoneNumber= "499-555-0125",EmailAddress= "annik0@adventure-works.com",Address= "10203 Acorn Avenue",City= "Calgary", BirthDay = DateTime.ParseExact("1988-12-10", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=34, FirstName= "Suchitra",FamilyName= "Mohan",PhoneNumber= "753-555-0129",EmailAddress= "suchitra0@adventure-works.com",Address= "94, rue Descartes",City= "Bordeaux", BirthDay = DateTime.ParseExact("1989-02-09", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=35, FirstName= "Brandon",FamilyName= "Heidepriem",PhoneNumber= "429-555-0137",EmailAddress= "brandon0@adventure-works.com",Address= "Pascalstr 951",City= "Berlin", BirthDay = DateTime.ParseExact("1989-02-01", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=36, FirstName= "Jose",FamilyName= "Lugo",PhoneNumber= "587-555-0115",EmailAddress= "jose0@adventure-works.com",Address= "34 Waterloo Road",City= "Melbourne", BirthDay = DateTime.ParseExact("1989-02-03", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=37, FirstName= "Chris",FamilyName= "Okelberry",PhoneNumber= "315-555-0144",EmailAddress= "chris2@adventure-works.com",Address= "Downshire Way",City= "Cambridge", BirthDay = DateTime.ParseExact("1989-02-28", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=38, FirstName= "Kim",FamilyName= "Abercrombie",PhoneNumber= "208-555-0114",EmailAddress= "kim1@adventure-works.com",Address= "8154 Via Mexico",City= "Detroit", BirthDay = DateTime.ParseExact("1990-01-09", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=39, FirstName= "Ed",FamilyName= "Dudenhoefer",PhoneNumber= "919-555-0140",EmailAddress= "ed0@adventure-works.com",Address= "3997 Via De Luna",City= "Cambridge", BirthDay = DateTime.ParseExact("1990-01-29", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=40, FirstName= "JoLynn",FamilyName= "Dobney",PhoneNumber= "903-555-0145",EmailAddress= "jolynn0@adventure-works.com",Address= "1902 Santa Cruz",City= "Bothell", BirthDay = DateTime.ParseExact("1987-12-19", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=41, FirstName= "Bryan",FamilyName= "Baker",PhoneNumber= "712-555-0113",EmailAddress= "bryan0@adventure-works.com",Address= "793 Crawford Street",City= "Kenmore", BirthDay = DateTime.ParseExact("1989-01-14", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=42, FirstName= "James",FamilyName= "Kramer",PhoneNumber= "119-555-0117",EmailAddress= "james0@adventure-works.com",Address= "463 H Stagecoach Rd.",City= "Kenmore", BirthDay = DateTime.ParseExact("1988-12-20", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=43, FirstName= "Nancy",FamilyName= "Anderson",PhoneNumber= "970-555-0118",EmailAddress= "nancy0@adventure-works.com",Address= "5203 Virginia Lane",City= "Kenmore", BirthDay = DateTime.ParseExact("1988-12-26", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=44, FirstName= "Simon",FamilyName= "Rapier",PhoneNumber= "963-555-0134",EmailAddress= "simon0@adventure-works.com",Address= "4095 Cooper Dr.",City= "Kenmore", BirthDay = DateTime.ParseExact("1988-12-01", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=45, FirstName= "Thomas",FamilyName= "Michaels",PhoneNumber= "278-555-0118",EmailAddress= "thomas0@adventure-works.com",Address= "6697 Ridge Park Drive",City= "Kenmore", BirthDay = DateTime.ParseExact("1989-02-19", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=46, FirstName= "Eugene",FamilyName= "Kogan",PhoneNumber= "173-555-0179",EmailAddress= "eugene1@adventure-works.com",Address= "5669 Ironwood Way",City= "Kenmore", BirthDay = DateTime.ParseExact("1989-02-01", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=47, FirstName= "Andrew",FamilyName= "Hill",PhoneNumber= "908-555-0159",EmailAddress= "andrew0@adventure-works.com",Address= "8192 Seagull Court",City= "Kenmore", BirthDay = DateTime.ParseExact("1989-02-15", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=48, FirstName= "Ruth",FamilyName= "Ellerbrock",PhoneNumber= "145-555-0130",EmailAddress= "ruth0@adventure-works.com",Address= "5553 Cash Avenue",City= "Kenmore", BirthDay = DateTime.ParseExact("1987-12-30", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=49, FirstName= "Barry",FamilyName= "Johnson",PhoneNumber= "206-555-0180",EmailAddress= "barry0@adventure-works.com",Address= "7048 Laurel",City= "Kenmore", BirthDay = DateTime.ParseExact("1993-11-29", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=50, FirstName= "Sidney",FamilyName= "Higa",PhoneNumber= "424-555-0189",EmailAddress= "sidney0@adventure-works.com",Address= "25 95th Ave NE",City= "Kenmore", BirthDay = DateTime.ParseExact("1988-01-26", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=51, FirstName= "Jeffrey",FamilyName= "Ford",PhoneNumber= "984-555-0185",EmailAddress= "jeffrey0@adventure-works.com",Address= "3280 Pheasant Circle",City= "Snohomish", BirthDay = DateTime.ParseExact("1988-02-13", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=52, FirstName= "Doris",FamilyName= "Hartwig",PhoneNumber= "328-555-0150",EmailAddress= "doris0@adventure-works.com",Address= "4231 Spar Court",City= "Snohomish", BirthDay = DateTime.ParseExact("1994-01-31", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=53, FirstName= "Diane",FamilyName= "Glimp",PhoneNumber= "202-555-0151",EmailAddress= "diane0@adventure-works.com",Address= "1285 Greenbrier Street",City= "Snohomish", BirthDay = DateTime.ParseExact("1988-03-21", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=54, FirstName= "Bonnie",FamilyName= "Kearney",PhoneNumber= "264-555-0150",EmailAddress= "bonnie0@adventure-works.com",Address= "5724 Victory Lane",City= "Snohomish", BirthDay = DateTime.ParseExact("1989-12-25", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=55, FirstName= "Taylor",FamilyName= "Maxwell",PhoneNumber= "508-555-0165",EmailAddress= "taylor0@adventure-works.com",Address= "591 Merriewood Drive",City= "Snohomish", BirthDay = DateTime.ParseExact("1993-12-31", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=56, FirstName= "Denise",FamilyName= "Smith",PhoneNumber= "869-555-0119",EmailAddress= "denise0@adventure-works.com",Address= "3114 Notre Dame Ave.",City= "Snohomish", BirthDay = DateTime.ParseExact("1989-01-29", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=57, FirstName= "Frank",FamilyName= "Miller",PhoneNumber= "167-555-0139",EmailAddress= "frank1@adventure-works.com",Address= "7230 Vine Maple Street",City= "Snohomish", BirthDay = DateTime.ParseExact("1989-02-16", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=58, FirstName= "Kendall",FamilyName= "Keil",PhoneNumber= "138-555-0128",EmailAddress= "kendall0@adventure-works.com",Address= "2601 Cambridge Drive",City= "Snohomish", BirthDay = DateTime.ParseExact("1988-11-28", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=59, FirstName= "Bob",FamilyName= "Hohman",PhoneNumber= "611-555-0116",EmailAddress= "bob0@adventure-works.com",Address= "2115 Passing",City= "Snohomish", BirthDay = DateTime.ParseExact("1988-12-17", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=60, FirstName= "Pete",FamilyName= "Male",PhoneNumber= "768-555-0123",EmailAddress= "pete0@adventure-works.com",Address= "4852 Chaparral Court",City= "Snohomish", BirthDay = DateTime.ParseExact("1989-01-04", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=61, FirstName= "Diane",FamilyName= "Tibbott",PhoneNumber= "361-555-0180",EmailAddress= "diane2@adventure-works.com",Address= "7726 Driftwood Drive",City= "Monroe", BirthDay = DateTime.ParseExact("1989-01-11", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=62, FirstName= "John",FamilyName= "Campbell",PhoneNumber= "435-555-0113",EmailAddress= "john0@adventure-works.com",Address= "3841 Silver Oaks Place",City= "Monroe", BirthDay = DateTime.ParseExact("1994-02-07", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=63, FirstName= "Maciej",FamilyName= "Dusza",PhoneNumber= "237-555-0128",EmailAddress= "maciej0@adventure-works.com",Address= "9652 Los Angeles",City= "Monroe", BirthDay = DateTime.ParseExact("1990-01-22", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=64, FirstName= "Michael",FamilyName= "Zwilling",PhoneNumber= "582-555-0148",EmailAddress= "michael7@adventure-works.com",Address= "4566 La Jolla",City= "Monroe", BirthDay = DateTime.ParseExact("1990-02-16", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=65, FirstName= "Randy",FamilyName= "Reeves",PhoneNumber= "961-555-0122",EmailAddress= "randy0@adventure-works.com",Address= "1356 Grove Way",City= "Monroe", BirthDay = DateTime.ParseExact("1990-02-16", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=66, FirstName= "Karan",FamilyName= "Khanna",PhoneNumber= "447-555-0186",EmailAddress= "karan0@adventure-works.com",Address= "4775 Kentucky Dr.",City= "Monroe", BirthDay = DateTime.ParseExact("1989-12-15", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=67, FirstName= "Jay",FamilyName= "Adams",PhoneNumber= "407-555-0165",EmailAddress= "jay0@adventure-works.com",Address= "4734 Sycamore Court",City= "Monroe", BirthDay = DateTime.ParseExact("1989-02-26", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=68, FirstName= "Charles",FamilyName= "Fitzgerald",PhoneNumber= "931-555-0118",EmailAddress= "charles0@adventure-works.com",Address= "896 Southdale",City= "Monroe", BirthDay = DateTime.ParseExact("1989-11-26", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=69, FirstName= "Steve",FamilyName= "Masters",PhoneNumber= "712-555-0170",EmailAddress= "steve0@adventure-works.com",Address= "2275 Valley Blvd.",City= "Monroe", BirthDay = DateTime.ParseExact("1989-02-08", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=70, FirstName= "David",FamilyName= "Ortiz",PhoneNumber= "712-555-0119",EmailAddress= "david2@adventure-works.com",Address= "1792 Belmont Rd.",City= "Monroe", BirthDay = DateTime.ParseExact("1988-12-08", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=71, FirstName= "Michael",FamilyName= "Ray",PhoneNumber= "156-555-0199",EmailAddress= "michael3@adventure-works.com",Address= "5734 Ashford Court",City= "Monroe", BirthDay = DateTime.ParseExact("1989-02-08", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=72, FirstName= "Steven",FamilyName= "Selikoff",PhoneNumber= "925-555-0114",EmailAddress= "steven0@adventure-works.com",Address= "5030 Blue Ridge Dr.",City= "Monroe", BirthDay = DateTime.ParseExact("1988-11-24", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=73, FirstName= "Carole",FamilyName= "Poland",PhoneNumber= "688-555-0192",EmailAddress= "carole0@adventure-works.com",Address= "158 Walnut Ave",City= "Monroe", BirthDay = DateTime.ParseExact("1988-12-12", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=74, FirstName= "Bjorn",FamilyName= "Rettig",PhoneNumber= "199-555-0117",EmailAddress= "bjorn0@adventure-works.com",Address= "8310 Ridge Circle",City= "Monroe", BirthDay = DateTime.ParseExact("1988-12-31", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=75, FirstName= "Michiko",FamilyName= "Osada",PhoneNumber= "984-555-0148",EmailAddress= "michiko0@adventure-works.com",Address= "3747 W. Landing Avenue",City= "Monroe", BirthDay = DateTime.ParseExact("1989-01-19", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=76, FirstName= "Carol",FamilyName= "Philips",PhoneNumber= "609-555-0153",EmailAddress= "carol0@adventure-works.com",Address= "2598 La Vista Circle",City= "Duvall", BirthDay = DateTime.ParseExact("1989-02-05", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=77, FirstName= "Merav",FamilyName= "Netz",PhoneNumber= "224-555-0187",EmailAddress= "merav0@adventure-works.com",Address= "9693 Mellowood Street",City= "Duvall", BirthDay = DateTime.ParseExact("1989-02-24", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=78, FirstName= "Reuben",FamilyName= "D'sa",PhoneNumber= "191-555-0112",EmailAddress= "reuben0@adventure-works.com",Address= "1825 Corte Del Prado",City= "Duvall", BirthDay = DateTime.ParseExact("1988-12-08", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=79, FirstName= "Eric",FamilyName= "Brown",PhoneNumber= "680-555-0118",EmailAddress= "eric1@adventure-works.com",Address= "5086 Nottingham Place",City= "Duvall", BirthDay = DateTime.ParseExact("1990-01-17", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=80, FirstName= "Sandeep",FamilyName= "Kaliyath",PhoneNumber= "166-555-0156",EmailAddress= "sandeep0@adventure-works.com",Address= "3977 Central Avenue",City= "Duvall", BirthDay = DateTime.ParseExact("1990-01-10", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=81, FirstName= "Mihail",FamilyName= "Frintu",PhoneNumber= "733-555-0128",EmailAddress= "mihail0@adventure-works.com",Address= "8209 Green View Court",City= "Duvall", BirthDay = DateTime.ParseExact("1989-12-22", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=82, FirstName= "Jack",FamilyName= "Creasey",PhoneNumber= "521-555-0113",EmailAddress= "jack1@adventure-works.com",Address= "8463 Vista Avenue",City= "Duvall", BirthDay = DateTime.ParseExact("1990-02-24", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=83, FirstName= "Patrick",FamilyName= "Cook",PhoneNumber= "425-555-0117",EmailAddress= "patrick1@adventure-works.com",Address= "5379 Treasure Island Way",City= "Duvall", BirthDay = DateTime.ParseExact("1990-02-05", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=84, FirstName= "Frank",FamilyName= "Martinez",PhoneNumber= "203-555-0196",EmailAddress= "frank3@adventure-works.com",Address= "3421 Bouncing Road",City= "Duvall", BirthDay = DateTime.ParseExact("1990-01-29", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=85, FirstName= "Brian",FamilyName= "Goldstein",PhoneNumber= "730-555-0117",EmailAddress= "brian2@adventure-works.com",Address= "991 Vista Verde",City= "Duvall", BirthDay = DateTime.ParseExact("1989-12-04", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=86, FirstName= "Ryan",FamilyName= "Cornelsen",PhoneNumber= "208-555-0114",EmailAddress= "ryan0@adventure-works.com",Address= "390 Ridgewood Ct.",City= "Carnation", BirthDay = DateTime.ParseExact("1988-12-29", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=87, FirstName= "Cristian",FamilyName= "Petculescu",PhoneNumber= "434-555-0133",EmailAddress= "cristian0@adventure-works.com",Address= "1411 Ranch Drive",City= "Carnation", BirthDay = DateTime.ParseExact("1988-12-15", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=88, FirstName= "Betsy",FamilyName= "Stadick",PhoneNumber= "405-555-0171",EmailAddress= "betsy0@adventure-works.com",Address= "9666 Northridge Ct.",City= "Carnation", BirthDay = DateTime.ParseExact("1989-12-11", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=89, FirstName= "Patrick",FamilyName= "Wedge",PhoneNumber= "413-555-0124",EmailAddress= "patrick0@adventure-works.com",Address= "3074 Arbor Drive",City= "Carnation", BirthDay = DateTime.ParseExact("1990-01-25", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=90, FirstName= "Danielle",FamilyName= "Tiedt",PhoneNumber= "500-555-0172",EmailAddress= "danielle0@adventure-works.com",Address= "9752 Jeanne Circle",City= "Carnation", BirthDay = DateTime.ParseExact("1990-02-13", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=91, FirstName= "Kimberly",FamilyName= "Zimmerman",PhoneNumber= "123-555-0167",EmailAddress= "kimberly0@adventure-works.com",Address= "7166 Brock Lane",City= "Seattle", BirthDay = DateTime.ParseExact("1990-01-05", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=92, FirstName= "Tom",FamilyName= "Vande Velde",PhoneNumber= "295-555-0161",EmailAddress= "tom0@adventure-works.com",Address= "7126 Ending Ct.",City= "Seattle", BirthDay = DateTime.ParseExact("1990-03-03", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=93, FirstName= "Kok-Ho",FamilyName= "Loh",PhoneNumber= "999-555-0155",EmailAddress= "kok-ho0@adventure-works.com",Address= "4598 Manila Avenue",City= "Seattle", BirthDay = DateTime.ParseExact("1988-12-20", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=94, FirstName= "Russell",FamilyName= "Hunter",PhoneNumber= "786-555-0144",EmailAddress= "russell0@adventure-works.com",Address= "5666 Hazelnut Lane",City= "Seattle", BirthDay = DateTime.ParseExact("1988-12-05", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=95, FirstName= "Jim",FamilyName= "Scardelis",PhoneNumber= "679-555-0113",EmailAddress= "jim0@adventure-works.com",Address= "1220 Bradford Way",City= "Seattle", BirthDay = DateTime.ParseExact("1988-12-12", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=96, FirstName= "Elizabeth",FamilyName= "Keyser",PhoneNumber= "318-555-0137",EmailAddress= "elizabeth0@adventure-works.com",Address= "5375 Clearland Circle",City= "Seattle", BirthDay = DateTime.ParseExact("1989-02-23", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=97, FirstName= "Mandar",FamilyName= "Samant",PhoneNumber= "140-555-0132",EmailAddress= "mandar0@adventure-works.com",Address= "2639 Anchor Court",City= "Seattle", BirthDay = DateTime.ParseExact("1989-02-03", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=98, FirstName= "Sameer",FamilyName= "Tejani",PhoneNumber= "990-555-0172",EmailAddress= "sameer0@adventure-works.com",Address= "502 Alexander Pl.",City= "Seattle", BirthDay = DateTime.ParseExact("1989-02-04", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=99, FirstName= "Nuan",FamilyName= "Yu",PhoneNumber= "913-555-0184",EmailAddress= "nuan0@adventure-works.com",Address= "5802 Ampersand Drive",City= "Seattle", BirthDay = DateTime.ParseExact("1988-12-30", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
            new Customer{Id=100, FirstName= "Lolan",FamilyName= "Song",PhoneNumber= "582-555-0178",EmailAddress= "lolan0@adventure-works.com",Address= "5125 Cotton Ct.",City= "Seattle", BirthDay = DateTime.ParseExact("1989-01-05", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)},
        };
    }
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDay { get; set; }
    }

    public class DableRequest
    {
        public int pageIndex { get; set; }
        public int recordPerPage { get; set; }
        public string orderColumnName { get; set; }
        public bool orderAsc { get; set; } = true;
    }
}
