# Simple Survey

Simple Survey is a .NET Framework MVC 5 Web Application using n-tier architecture to create questions, create surveys and collect information. 
This is designed for an administrator to 
  - create and manage questions 
  - create and manage surveys 
  - assign surveys 
     - to users, 
     - by department 
     - by job title
 - view and take assigned surveys
 - view completed surveys
 
## Installation

To run this program, you will need:
  - Visual Studio Community
  - A web browser


## Usage

FLOW Notes: Register user -> Create Question -> Create Survey -> View Survey Details to Assign Survey to User -> My Surveys -> Take Survey -> View Completed  

1.  In the NAVBAR, register by entering First Name, Last Name, Department, Job Title, Email, Password, Confirm Password and click the Register button.

2.  In the NAVBAR, select "Question" to create a new question. Enter question text and select question type from a drop down menu. 
  - Current question TYPE options: Open Ended, Multiple Choice, Ordinal, Interval, Ratio. 
  - Future functionality for question CHOICE will be added for these question TYPES
    - Question Edit: Only for indicating if a question is active. For data integrity: not able to edit question text.)
    - Question Detail: Question Choices are currently limited to:
      - Strongly Agree
      - Agree
      - Neither Agree Nor Disagree
      - Disagree
      - Strongly Disagree

3. In the NAVBAR, select "Survey" to create a new survey. Enter Survey Title, Survey Description, StartDate, EndDate, and select questions from the list box. Multiple questions can be selected at once. For data integrity, once questions are added to a survey, they are unable to be changed.

4. From the Survey Index, surveys can be assigned to users by selecting Details. 


Note: at this time, all users have the ability to assign surveys to users and view completed. 
- In the future, admin roles and various user permissions will be established.

Note: A user can only complete one instance of a survey. Application will break if a user tries to take the same version of a survey. 

    

## Contributing
Pull requests are welcome. For major changes, please email emiliearains@gmail.com first to discuss what you would like to change.
Please make sure to update tests as appropriate.

## Resources Used
- [Things I've Learned About Writing Good READMEs](https://github.com/noffle/art-of-readme#readme)
- [Survey Web App Using ASP.NET MVC 4, Entity Framework 5  ](https://www.codeproject.com/Articles/584161/Survey-Web-App-Using-ASP-NET-MVC-4-Entity-Framewor)
- [Simple ASP.NET Survey Application ](https://www.c-sharpcorner.com/uploadfile/a185f3/asp-net-simple-survey-application/)

- [DataAnnotations - ForeignKey Attribute in EF 7 & EF](https://www.entityframeworktutorial.net/code-first/foreignkey-dataannotations-attribute-in-code-first.aspx)

- [Radio Buttons: Question Creation And Modification - SJSU - School of Information](https://ischool.sjsu.edu/phpquestionnaire-radio-question)
- [Working With Radio Buttons in ASP.NET Razor Pages ](https://www.learnrazorpages.com/razor-pages/forms/radios)
- [ASP.NET MVC Multiple Radiobutton Forms ](https://stackoverflow.com/questions/34290147/asp-net-mvc-multiple-radiobutton-forms)

## License
[MIT](https://choosealicense.com/licenses/mit/)
