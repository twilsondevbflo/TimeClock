Summary:
The TimeClock application is a .NET 5 Web API endpoint that hosts the functions required by the Simple Time Clock Application Test.

To test the functions described in the testing document the application will present a SwaggerUI page to allow users to submit data and see the result.


Below describes the Controller methods available.

Login - 
This method has one integer input parameter which should represent the userId.
For the sake of testing the functionality 2 Mocked up user accounts were created to test this function.  Userid 1 or 2 will get a successful login response, all other id's will return a login failure message.

GetHistory - 
This method takes an integer input parameter which represents the user's history which is being attempted to retrieved.  
A userId of 0 will return all history records in the order they were written to the history file. 

StartShift,StartBreak,StartLunch -
Each of these methods takes a JSON object in the format of the User class.  
Depending on the method and the value of the Status property an attempt to begin a new Shift,Break, or Lunch is generated.
Each attempt creates a History record detailing the result of the attempt.

EndShift,EndBreak,EndLunch -
Similar functionality to the "start" but attempts to end the shift based on the User object's current status


Statuses | Values
Offline  | 0
Active   | 1
Break    | 2
Lunch    | 3



