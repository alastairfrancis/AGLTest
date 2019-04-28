# AGL Code Test

Developer: Alastair Francis <BR>
Environment: C# .NET Core <BR>
Dev Environment: Visual Studio 2017<BR>

## Description: 
Consumes a JSON data feed with pet owner data.  The data can can be viewed by various applications, each written to demonstrate skills in different frameworks:
* API (with swagger interface)
* Console application
* Angular 7 Web application

## Architecture
Newtonsoft JSON framework is used to deserialise the data feed. A custom converter was implemented to filter the pet owners by criteria.  Most importantly to filter by pet type (Cat, Dog, and Fish).  For viewing pets by owner gender, AutoMapper is used to transform the pet owner data to a custom view type.  All types of viewer application (API, Console, and Angulatr Web) use the same shared implementation in the AGLTest.Common project.

## Build:
Open the solution file in Visual Studio to build, and run the application.

## Unit Tests:
Unit tests have been implemented in xUnit, and can be run in the Visual Studio test runner.

## Console Application:
AGLTest.Console is a portable .Net core project.  To run it from the command shell use the `dotnet` runtime:
	> dotnet AGLTest.DataViewer.dll

Custom views can be added by creating a class implementing the IDataView interface.  The ViewFactory will resolve all views that implement this interface.  I implemented this solution to demonstrate an example of using factories to isolate client implementation, from the plumbing associated adding a new feature. 

## Angular 7 Web Application:
Node.js is required to build, and run this project.  A controller was used to retrieve the data using the common service code.  In production I would prefer to host the API as a seperate service, and use the API to access data from the angular components. This would help to decouple the view from the source of the data, and would not require duplicate configuratio in appsettings.

