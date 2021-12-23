Razor_API_Tutorial is a basic sight seeing tour of what Razor routing can offer to the API for those looking to start.

Write up here:
https://www.dreamincode.net/forums/topic/418813-razor-pages-core-31-intro-to-api/



=================
dreamincode.net tutorial backup ahead of decommissioning
 Post icon  Posted 03 April 2020 - 06:06 PM 
 
 
 [u][b]Introduction: API - what are they, and why are they useful?[/b][/u]

Websites provide a great way to format data, information, and present it in a pleasing way.  The trouble is sometimes that information being presented needs to be universally formatted, direct, and missing all the visual fluff.  

APIs fill that need to provide a consistent programmatic interface of "endpoints" external actors can request data from, or push data to.  

Uses would be a desktop application that pulls city statistic data on request, a mobile app that sends user data on request, or even another website accessing a collection of recent social media posts.

APIs help their callers with HTTP status code messages like 200 'ok', 404 forbidden, or 429 'too many requests'.  As an API developer you should be thinking on what messages need to be returned, what data to send with it, and how that works into your API endpoint.

https://en.wikipedia.org/wiki/List_of_HTTP_status_codes



[u][b]Software:[/b][/u]
-- Visual Studios 2019

[u][b]Concepts:[/b][/u]
-- C#
-- Core 3.1 / Razor pages
-- Routing
-- API

[u]Github link:[/u] https://github.com/modi1231/Razor_API_Tutorial


[u][b]Sections:[/b][/u]
[list]
[*]What is RESTful
[*]How DOTNET Core helped change them to be easier to make
[*]Project Setup 
[*]My Extra Setup steps.
[*]Basic API - return simple data
[*]Basic API - return class object
[*]Basic API - return list of objects
[*]Moderate API - return list of class object with parameter input
[*]Moderate - header reading/security
[*]Moderate API - POST data
[*]Moderate - Using multiple GETs in one controller
[/list]


[u][b]What is RESTful[/b][/u]
 
The vogue way of thinking of APIs is with Representational state transfer (REST).  REST is a stateless architecture pattern that sets some predefined ways to get and manipulate data.  Typically this is the usual HTTP methods like GET/POST.  REST benefits are typically fast, reliable, and not brittle.
https://en.wikipedia.org/wiki/Representational_state_transfer

An alternative architecture is SOAP, but that is not covered here.
https://en.wikipedia.org/wiki/SOAP


[u][b]How DOTNET Core helped change them to be easier to make[/b][/u]

DOTNET Core really shines in the API world with such deep and functional use of routing.  Routing of HTTP requests to various endpoints.  In a typical ASP.NET Core webpage that routing directs to pages to be displayed.  In the API world that can be redirected to endpoints and data calls!
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-3.1


[b][u]Project Setup [/u][/b]
Crack open Visual Studios -> New project -> ASP.NET Core WEb App -> Empty

In the 'startup' in 'configure' remove the 'endpoints.mapget' for hello world, and put in:
endpoints.MapControllers();        

In the 'configure services' add in the controllers.
services.AddControllers();

In the solution explorer, right click on your project and add two folders:
Controllers
Models

The controllers will hold your API code called by external folk, and the models will be the container for the data models returned.


[u][b]My Extra Setup steps.[/b][/u]

When I am making a razor page I like to keep my database access calls contained in their own folder and class.  It helps decouple the DB and business calls, and helps with rapid prototyping.  (I have other tutorials explaining the setup in detail, but just follow along).

Right click the project name -> add new folder -> 'Data'.

Right click on 'Data' -> add new class 'DataAccess'.


[b][u]Basic API - return simple data[/u][/b]

Data can be returned from the API endpoint in simple or complex ways.

The first endpoint to make will return a number.  This could be anything from the number of tickets in a help desk queue, or number of people checked into an event.  

Right click on Controllers folder -> add new scaffold item -> API controller - Empty.

Call it 'UsersController'.

Let it think for a minute.

The default code provides a class extending 'controllerbase' and two attributes.  One shows the routing used to call the end point, and the other flags the class to be used in API calls.

The [Route("api/[controller]")] will be important here shortly.

I like to keep the bulk of my web calls async so I gussie up[ my 'Get' with the async keyword, wrap the return in a task, and add 'async' to the prefix of the function name.

The function is simple - it returns 1 from a forced awaited taskrun.


[code]public async Task<ActionResult<int>> GetAsync()
{
	int lReturn = 0;

	await Task.Run(() =>
	{
		lReturn = 1;
	});

	return lReturn;
}[/code]

Save this and run it to make sure it works right.

Hit f5 to run the code.  Wait, nothing shows up!  Remember the route specified at the top of the class.

[img]https://i.imgur.com/NeUF3IW.jpg[/img]

We need to add the word 'api' and our controller's name 'users' to have it route to the Get.

https://localhost:44372/api/users

You should be seeing 1 appear!

[img]https://i.imgur.com/vJXmWfx.jpg[/img]

A little bit of clean up for my own sake.  Again this is to help decouple and separate the business logic from the database logic.

In my 'DataAccess' class I create a function

[code]        public async Task<ActionResult<int>> GetUserCountAsync()
        {
            int lReturn = 0;

            await Task.Run(() =>
            {
                lReturn = 1;
            });

            return lReturn;
        }[/code]


This easily could be swapped out for a database call or some other data input.

Back in UserController's GetAsync the code is now:

[code]        public async Task<ActionResult<int>> GetAsync()
        {
            DataAccess _data = new DataAccess();

            return await _data.GetUserCountAsync();
        }[/code]

The business view no longer is dependent on knowing where the data is coming from except that call.  


[b][u]Basic API - return class object[/u][/b]

A single basic data point is interesting, but what about classes?  Most certainly can!

In this scenario the API end point will return the location of a food truck for a given day, but any moderately complex class could be substituted.  The return could be consumed by a mobile app or secondary website.

Right click on the 'Models' folder -> add class 'TruckLocation'.

This will be the class returned to the API caller.  The basics will be the location, start, and end date time for the food truck.

[code]    public class TruckLocation
    {
        public string STREET { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string ZIP { get; set; }
        public DateTime START { get; set; }
        public DateTime STOP { get; set; }

    }[/code]

That's setup so let's add the controller/API endpoint.  Right click the 'controllers' folder -> add new scaffold item -> API Controller - Empty -> 'TruckLocationController'.

The scaffold setups as before, but this time we will start out using the 'DataAccess' class out of the box.  

The only major change is instead of returning an 'int' the function will return an object of 'TruckLocation'.

[code]        public async Task<ActionResult<TruckLocation>> GetAsync()
        {
            DataAccess _data = new DataAccess();

            return await _data.GetTruckLocationAsync();
        }[/code]

Inside the DataAccess the function fills out the data, and returns it.

  [code]      public async Task<TruckLocation> GetTruckLocationAsync()
        {
            TruckLocation temp = new TruckLocation();

            await Task.Run(() =>
            {
                temp.STREET = "1234 Apple Street";
                temp.CITY = "AnyTown";
                temp.STATE = "NE";
                temp.ZIP = "55512";
                temp.START = DateTime.Parse($"{DateTime.Now.ToShortDateString()} 11:00");
                temp.STOP = DateTime.Parse($"{DateTime.Now.ToShortDateString()} 15:00");
            });

            return temp;
        }[/code]
(Yes, there is a little razzle-dazzle in the start and stop to make it your current date, but it's just for fun).

Save it all, and run it.

Remember - the call needs to be routed to 'api' and then the controller name.
https://localhost:44372/api/TruckLocation

Return:
[code]{"street":"1234 Apple Street","city":"AnyTown","state":"NE","zip":"55512","start":"2020-04-03T11:00:00","stop":"2020-04-03T15:00:00"}
[/code]
The return is auto formatted into JSON, and has all the bits!  Just what we wanted!

[img]https://i.imgur.com/2oYHxi8.jpg[/img]

[b][u]Basic API - return list of objects[/u][/b]

This knowledge can be expanded to return a list of objects.  

In this scenario the API endpoint will return a list of attendees at some event.

Same as before in setup.

Model called 'Attendees'
[code]    public enum Type
    {
        Attendee = 0,
        Leader = 1
    }
    public class Attendees
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public Type TYPE { get; set; }
    }[/code]

A little razzle dazzle with using an enum type.

An 'Attendees' controller that returns a collection of Attendees

[code]    [Route("api/[controller]")]
    [ApiController]
    public class AttendeesController : ControllerBase
    {
        public async Task<ActionResult<List<Attendees>>> GetAsync()
        {
            DataAccess _data = new DataAccess();

            return await _data.GetAttendeesAsync();
        }
    }[/code]

The decoupled 'DataAccess' function.

[code]        public async Task<List<Attendees>> GetAttendeesAsync()
        {
            List<Attendees> temp = new List<Attendees>();

            await Task.Run(() =>
            {
                temp.Add(new Attendees()
                {
                    ID = 1,
                    NAME = "Terry Turtle",
                    TYPE = Models.Type.Leader
                });

                temp.Add(new Attendees()
                {
                    ID = 2,
                    NAME = "Sammy Squirrel",
                    TYPE = Models.Type.Attendee
                });

                temp.Add(new Attendees()
                {
                    ID = 3,
                    NAME = "Cammie Cat",
                    TYPE = Models.Type.Attendee
                });
            });

            return temp;
        }[/code]

Run it, fill in the right routing we get the JSON collection.

https://localhost:44372/api/Attendees

Return:
[code][{"id":1,"name":"Terry Turtle","type":1},{"id":2,"name":"Sammy Squirrel","type":0},{"id":3,"name":"Cammie Cat","type":0}][/code]

[img]https://i.imgur.com/cbYuIDO.jpg[/img]

[u][b]Moderate API - return list of class object with parameter input[/b][/u]

One can specify a parameter (or set of parameters) to pass to the API.

Piggy backing of the 'Attendees' from above let's create a new controller called 'Attendee'.  While a good practice would be to send the ID into the 'dataaccess' function and limit there for this example (and small dataset) I am using LINQ to do the filtering.

[code]    [Route("api/[controller]")]
    [ApiController]
    public class AttendeeController : ControllerBase
    {
        public async Task<ActionResult<Attendees>> GetAsync(int id)
        {
            DataAccess _data = new DataAccess();
            
            //get list of attendeeds
            List<Attendees> temp = await _data.GetAttendeesAsync();

            //use LINQ to get specific one
            Attendees a = (from x in temp
                          where x.ID == id
                          select x).FirstOrDefault();

            return a;
        }
    }[/code]


The API call is similarly the same except to specify parameters tack on a ?, the ID name and the value wanted.

[code]https://localhost:44372/api/Attendee?id=1
{"id":1,"name":"Terry Turtle","type":1}

https://localhost:44372/api/Attendee?id=2
{"id":2,"name":"Sammy Squirrel","type":0}

https://localhost:44372/api/Attendee?id=20
nothing[/code]


[b][u]Moderate - header reading/security[/u][/b]

The question of security becomes important when this goes into production.  Most anything on the web will be abused by someone along the way so it is best to think about security along your process.  

One of the important steps is add an API key and check that before the API dispenses data.  API keys are dolled out to users and added to the caller's header information.

(You should also look at forcing HTTPS and highest level of TLS version you are capable of utilizing.)

To show case this we need to create a test website to make the call.

In a new instance of Visual Studios create an empty Razor project (don't add this to the existing API project as the whole ports and routing gets weird).


ASP.NET Core Web App -> name: APITester -> core 3.1 & empty 

in the 'start up' add   services.AddRazorPages(); to the 'configure services' area, and in configure 'app.userendpoints' add   endpoints.MapRazorPages();


Add a folder to the project called 'Pages'

Add a razor page to the 'Pages' folder called 'index'.

Add to the 'Pages' folder -> new item -> view imports.
[code]@using APITester
@namespace APITester.Pages
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers[/code]

Run it and 'index' should show up in H1 styling.

In the index page code behind I am adding three properties.  A message for output, a string for the url, and a string for the controller name.

[code]        public string Message { get; set; }

        [BindProperty]
        public string sUrl { get; set; }

        [BindProperty]
        public string sController { get; set; }[/code]

I remove the 'OnGet' function.

I add a function of the async Post.

[code]  public async Task OnPostAsync()
{
}[/code]

Flipping to the HTML side I remove the 'index' and add in the following:

[code]<h1>Hello world</h1>
Result: @Model.Message
<br />
<form method="post">
    URL: <input type="text" asp-for="@Model.sUrl" /><br />
    Controller: <input type="text" asp-for="@Model.sController" /><br />
    <button type="submit">Submit</button>
</form>[/code]

The message output from our API call, and a form with a few textboxes so this can be used in different API testing.

With that all setup jump back to the code behind and let's get ready to call an API.

The bulk of this rests on the shoulders of the HTTPClient.

Read more here: https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=netframework-4.8

I use it to call the API with the 'HTTPRequestMessage' and get back a 'HTTPResponseMessage'.
[code]            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage req = new HttpRequestMessage())
                {
                    // the api end point
                    req.RequestUri = new Uri(sUrl + sController);
                    // we are getting data versus posting data
                    req.Method = HttpMethod.Get;
                    //header API key
                    req.Headers.Add("api_key", "1234");
                    //sending it all off
                    resp = await client.SendAsync(req);
                }
            }[/code]

I then deal with the response back from the API and display it to the page.
[code]            //display the response
            if (resp != null && resp.IsSuccessStatusCode)
            {
                var foo = resp.Content.ReadAsStringAsync();

                Message = $"Success! <br /><br />{foo.Result}";
            }
            else
            {
                Message = $"fail: {resp.StatusCode}";
            }[/code]

Put a pin in the header API key for a minute as we test this by getting the TruckLocation from above.

Start up your API solution.

After that loads, run your web page.

In the URL textbox put in 'https://localhost:44372/api/'.
In the controller textbox put in 'trucklocations'.
Click 'submit'

The page should snag the truck location data and display it with 'success!'.

[img]https://i.imgur.com/DZ2OITQ.jpg[/img]

You can test out the non parameter based API end points from above.

Now about the API checking.  Modifying the 'AttendeesController' in the API solution to add the following to the top of the 'GetAsync':
[code]
            StringValues headerValues;
            //Request holds all the header data and you can put a breakpoint here and look through them
            IHeaderDictionary h = Request.Headers;
            // I am concerned with the 'api_key' value
            h.TryGetValue("api_key", out headerValues);

            //if it is not the super secret '1234' then return from the API.
            if (headerValues[0] != "1234")
                return Unauthorized();[/code]

This checks that the API key is what I am looking for and continues or returns a 401 not authorized.

Full function:
 [code]       public async Task<ActionResult<List<Attendees>>> GetAsync()
        {
            StringValues headerValues;
            //Request holds all the header data and you can put a breakpoint here and look through them
            IHeaderDictionary h = Request.Headers;
            // I am concerned with the 'api_key' value
            h.TryGetValue("api_key", out headerValues);

            //if it is not the super secret '1234' then return from the API.
            if (headerValues[0] != "1234")
                return Unauthorized();

            DataAccess _data = new DataAccess();
            return await _data.GetAttendeesAsync();
        }[/code]

To check this start the API solution, start the web, and point the webpage to get the data for 'attendees'.

This works fine because the website code added the correct header key.

[img]https://i.imgur.com/1kx8aak.jpg[/img]

To test if the page did not provide the right key stop the web page solution.  Change the 'api_key' value to 12345
[code]                    req.Headers.Add("api_key", "1234");[/code]

Run the web solution again, and try to navigate to the 'attendees' API endpoint.

The response should be a failure! 

[img]https://i.imgur.com/k0lBHzf.jpg[/img]

Side note - while your API is running you can put breakpoints in the code and have them trigger when the website is connecting to the end point.

There are a whole host of returns you can have the API end point send besides unauthorized.  You would want to plan on returning standardized returns to handle many cases and let those who are using hte API get some knowledge on what's up.

Ex: 202 accepted, 400 badrequest, 409 conflict, 403 forbidden, 404 not found, etc.

Read more here:
https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase?view=aspnetcore-3.1

I want to stress this is a very simplified version of seeing how to read header keys and do minimal security checks.  It is not an end-all/be-all solution and there are more integrated ways of doing this.


[b][u]Moderate API - POST data[/u][/b]

To send data to the API to store in a database or collection requires a 'POST'.  The process should bundle up the data from the webpage, (and in this case) format it in JSON, throw the JSON over the fence to the API endpoint, and the endpoint breaks down the JSON for use.

This will be a good time to remember you can put a break point in your API endpoint code while running and see what the webpage post sends.

To test this out modifications need to be made to the projects.

First add the nuget package for 'newtonsoft.json'.

Tools -> nuget package manager -> manage packages for solution

In the APITester web page add a new form at the bottom of the existing code.  Similar to the one at the top, but this takes in a value.

[code]<br />
<hr />
<h3>API Post Test</h3>
<br />
<form method="post">   
    URL: <input type="text" style="width:250px;" asp-for="@Model.sUrl" /><br />
    Controller: <input type="text" style="width:250px;" asp-for="@Model.sController" /><br />
    Value: <input type="text" style="width:250px;" asp-for="@Model.lValue" /><br />
    <button type="submit" asp-page-handler="SendValue">Submit</button>
</form>[/code]

In the code behind add a property for the value at the top:
        [code][BindProperty]
        public int lValue { get; set; }[/code]

and the page handler specified.

The code looks awfully similar to what was used before, but utilizes Newtonsoft's JSON functions to quickly package data in a consistent JSON format.

[code]        public async Task OnPostSendValueAsync()
        {
            HttpResponseMessage resp;

            //clean up if I botch a copy/paste
            sUrl = sUrl.Trim();

            //clean up if I botch a copy/paste
            if (sUrl[sUrl.Length - 1] != '/')
                sUrl += "/";

            //Convert the value to some JSON formatting.
            var jsonDat = new
            {
                id = $"{lValue}"
            };

            //serialize the data to a JSON string.
            string json = JsonConvert.SerializeObject(jsonDat);

            using (HttpClient http = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, sUrl))
                {
                    //ship the JSON over to the API.
                    request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    resp = await http.SendAsync(request);
                };
            }


            if (resp.IsSuccessStatusCode)
            {
                Message = $"Success! {resp.StatusCode}";
            }
            else
            {
                Message = $"fail: {resp.StatusCode}";
            }
}[/code]

Flip to the API project, and add a new controller called 'PostCollectorController' per the scaffolding method that has been consistently used in the tutorial.

Also add the nuget package for 'newtonsoft.json'.

Change up the attributes to show HTTPPOST, and what it consumes (this could be changed to XML or other types as well).

This function could ship the data off to be saved, or added to some in memory collection.  Instead it simply prints out the value and returns ok or bad request.

[code]        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<int>> PostAsync(object val)
        {
            try
            {
                //convert in bound object to a Dictionary<string,string>
                Dictionary<string, string> temp = JsonConvert.DeserializeObject<Dictionary<string, string>>(val.ToString());

                //print it out to show it worked
                System.Diagnostics.Debug.WriteLine($"{temp["id"]}");
                //System.Diagnostics.Debug.WriteLine($"{temp["i2d"]}");
            }
            catch (Exception ex)
            {
                //if there is a problem return 'bad request'
                System.Diagnostics.Debug.WriteLine($"{ex.Message}");
                return BadRequest();
            }
            //return ok when it worked
            return Ok();
        }
    }
[/code]

Start the API code and then the website to test out.

The API output window shows the correct value!

[img]https://i.imgur.com/zeOz7g2.jpg[/img]

Now what about a bad request?  Stop the API code and comment out the Debug write line below that looks for the wrong key.  Start up the API and try again.

You should succeed at failing here.

[img]https://i.imgur.com/EKzXx8I.jpg[/img]

This can be extrapolated to sending in complex data, lists of objects, and so on.  


[b][u]Moderate - Using multiple GETs in one controller[/u][/b]

The final area is a few steps off of normal, but conceivably someone's business requirements stipulate to do this.

For the examples shown it has been one controller to one API endpoint.  It honestly is best to keep the code separate and the routing shallow.

To achieve this task the routing needs to be changed up a wee bit.

In the API solution create a controller (from scaffold) called 'ManyGetsController'.

Changing up the attributes changes up the routing Razor will do.

[code]    [Route("api/manygets")]
    [ApiController]
    public class ManyGetsController : ControllerBase
    {
        public string Get()
        {
            return "default get";
        }
    }[/code]


Testing that first shows what is expected when following the routing guide.

[code]https://localhost:44372/api/manygets[/code]

Returns: "default get"

More gets can be added by specifying the route in each function's attributes.

[code]        [Route("getone")]
        public string GetOne()
        {
            return "1";
        }

        [Route("gettwo")]
        public string GetTwo()
        {
            return "2";
        }

        [Route("getthree")]
        public string GetThree()
        {
            return "3";
        }[/code]

Trying those out show they work as expected.

https://localhost:44372/api/manygets/getone
Returns: 1

https://localhost:44372/api/manygets/gettwo
Returns: 2

https://localhost:44372/api/manygets/getsix
Returns: nothing


[b][u]Wrap up:[/u][/b]

That concludes the sight seeing tour of using Web APIs with Razor.  Routing plays a crazy good role to make up the API endpoints as needed, and just the tip of the iceberg on validation, keys, and route tweaking were seen.

While the tutorial used JSON other formats of data could be used.  XML, HTML, plain text, and so on.

This should give anyone a good starting point to creating APIs to be consumed by other sites, processes, and mobile apps to a useful degree.  Sending, and receiving, data has many benefits when streamlined.  



[u][b]Reading list:[/b][/u]
https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio
https://nordicapis.com/what-is-the-difference-between-web-services-and-apis/
https://dotnet.microsoft.com/apps/aspnet/apis
https://en.wikipedia.org/wiki/Representational_state_transfer
https://docs.microsoft.com/en-us/aspnet/web-pages/overview/api-reference/asp-net-web-pages-api-reference
