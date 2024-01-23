# ActionFilterApi

This project will show a bare bones template for writing an IactionFilter to intercept REST Api messages.

## Table of Contents

- [Introduction]
- [Features]
- [Installation]
- [Usage]
- [License]

## Introduction

Briefly introduce the project and its purpose. Provide context for potential users and contributors.

## Features

List key features or functionalities of the project.

## Installation

Open the solution in Visual Studio.  This was built using VS 2022, targeting .Net 8

## Usage

1. Go to the OnActionExecuting method in the file filter/ApiLogActionFilter.cs, and set a break point somewhere in the method. (The executes BEFORE the code in your endpoint)
2. Go to the OnActionExecuted method in the file filter/ApiLogActionFilter.cs, and set a break point somewhere in the method.  (The executes AFTER the code in your endpoint)
3. Start the project in Debug mode (F5)
4. A browser window will open, with a Swagger UI that can be used to test.  Choose the GET or POST method, and execute the call.  The debugger will stop inside of the OnActionExecuting method where your breakpoint is, and you will be able to inspect the request.
	the variable requestBodyBuilder will contain the JSON that was sent into the endpoint.
5. The line filterContext.HttpContext.Request.Headers.Append("ApiRequestId", guid.ToString()); will add a uniqueIdentifier to a header called ApiRequestId.  This header will be used in the response, so you can correlate the request with the response (correlation Id)
6. After the line in OnActionExecuting method is hit, continue when done looking around, and the next stop will be in OnActionExecuted().
7. The line filterContext.HttpContext.Request.Headers.TryGetValue("ApiRequestId", out requestIdHeader); is where I pull the correlationId from the request.  You can use this id when storing your response to link it to the request that you stored.
8. The final step is the response.  For demo purposes, I simply return a basic response object with an id and a generic message.


## License

MIT License

Copyright (c) [2024] [Donnie Wishard]

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
