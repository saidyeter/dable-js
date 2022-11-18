# dable-js

A javascript library for listing data from API. 
I don't want to use datatable.js in asp.net projects because of jquery and bootstrap dependencies and usage complexity. 
Also when I encounter an error in datatable.js, the solution cannot be found easily.
For these reasons, I decided to write this library.
I also decided to create a different and generic filter mechanism. 
For that, I started another javascript library for the filter mechanism and I am going to create a repo.

## Installation

 - Download dable.js and dable.css
 
## Usage

- Link to css file 
```html
<link rel="stylesheet" href="~/lib/dable-js/dable.css" />
```
- Create an element to contain dable-js.
```html
<div id="root"></div>
```
- Link to js file 
```html
<script type="text/javascript" src='~/lib/dable-js/dable.js'></script>
```
- In DOMContentLoaded, use like this. Point container element id as first parameter. Second parameter is url to fecth data.
```html
<script type="text/javascript">
    document.addEventListener("DOMContentLoaded", function () {
        var dableJs = new DableJS("root", "/Home/DableApi")
        dableJs.init()
    });
</script>
```

![pure](images/screenshot-1.png)

While sending post request, dable sends 
```
    pageIndex
    recordPerPage
    orderColumnName
    orderAsc
```
as object and dable expects
```
    list
    totalCount
```
as object.

- Third parameter is options. 

- You can specify columns. If you don't specify, dable will create column titles according to fetched data.
```html
<script type="text/javascript">
    document.addEventListener("DOMContentLoaded", function () {
        var dableColumns = [
            { type: "data", title: "Customer Name", property: "firstName" },
            { type: "data", title: "Customer Surname", property: "familyName" },
            { type: "data", title: "Phone", property: "phoneNumber" },
            { type: "action", title: "E Mail", content: "<a href='mailto:${this.emailAddress}'>${this.emailAddress}</a>" },
            { type: "data", title: "Postal Address", property: "address" },
            { type: "action", title: "Buton", content: "<button id='btn-${this.id}' onclick='" + "alert(\"${this.firstName}\")" + "'>Click Me!</button>" },
        ];
        var options = {
            columns: dableColumns,
        }
        var dableJs = new DableJS("root", "/Home/DableApi", options)
        dableJs.init()
    });
</script>
``` 

![withColumns](images/screenshot-2.png)

-You can specify other options
```html
<script type="text/javascript">
      document.addEventListener("DOMContentLoaded", function () {     
        var options = {
            initCallback: callbackAfterLoaded,
            renderCallback: callbackAfterRendered,
            recordPerPage: 6
        }
        var dableJs = new DableJS("root", "/Home/DableApi", options)
        dableJs.init()
    });
    function callbackAfterLoaded() {
        console.log("dable loaded")
    }
    function callbackAfterRendered() {
        console.log("dable rendered")
    }
</script>
```

## To Do 

 - Wirte-Integrate a search mechanism
 - Learn JavaScript testing
 - Write test

## Author(s) - Contributor(s)

 - [Said Yeter](https://github.com/kordiseps)

## Licence

MIT License

Copyright (c) 2021 dable-js

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
