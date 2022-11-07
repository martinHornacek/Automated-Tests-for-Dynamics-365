# Automated testing in Dynamics CRM
There are many different types of testing that you can use to make sure that changes to your code are working as expected. The rising interest in automation, the availability of tools and pressure on DevOps adoption causes these practices to become more widely used. This project aims to demonstrate how the main testing practices differ from each other within the context of Dynamics CRM application.

## Unit tests
Unit tests are useful when developing plug-ins, custom workflow activities, JavaScripts and other custom code for Dynamics CRM. They provide quick feedback during development - especially when TDD is applied. Unit tests are very low level and very close to the source code. They can be easily automated and can be quickly run as part of a continous integration build.

## Integration tests
Integration tests verify different modules and services used by an application and their mutual interactions. These types of tests are more expensive to run as they require multiple parts of the application to be up and running. They are sometimes referred to as end-to-end tests. These tests usually replicate user behavior with the application as a whole. They can verify various user workflows, for example, the Lead Qualification process in Dynamics CRM. Integration tests are very useful, but they're expensive to perform, brittle and can be hard to maintain.

## Acceptance tests
Acceptance tests are tests which verify if the system satisfies given business requirements. They require the entire application to be up and running and they more or less mimic user behavior with the application. These tests work against the components underneath the UI layer, which in case of Dynamics CRM means running the tests againt CRM SDK services and custom components like plug-ins, custom actions and custom worfklow activities.

## Schema tests
Schema test are suited to test the correctness of database models and definitions. In the context of Dynamics these tests validate Data Model definitions, for example, entity properties, attributes, relationships etc. When automated as part of continous integration build they provide a quick feedback on whether your Data Model guidelines are always met.

# OpportunityManagement solution
The **OpportunityManagement** solution contains a simple plug-in, called *RatingPlugin*, which implements a simple business requirement and which will be exercised by different types of tests. Testing the same component by different testing techniques provides us with an option to compare these testing techniques next to each other. The business requirement which the *RatingPlugin* aims to satisfy is the following.

* When an Opportunity is created, we want to pre-populate the *Rating* field based on the Number of Employees specified on the *Parent Account* record. The rules how to assign the Rating can be summarized by this table:

 | Number of Employees of the Parent Account | Opportunity Rating |
 |-------------------------------------------|--------------------|
 |        0 or null employees                |        null        |
 |        between 1 and 9 employees          |        Cold        |
 |      between 10 and 99 employees          |        Warm        |
 |         over 99 employees                 |        Hot         |

* The system should also raise an error given an attempt to create an Opportunity without Parent Account is made.

## Getting Started
  To see all the tests in action these steps need to be performed.

### Prerequisites
 * It's recommended to restore Nuget packages before you start working with the solution.

### Environment Setup
 * You need to have an empty Dynamics CRM organization ready at your disposal where you'll be working with the ***OpportunityManagement** solution.
 * You need to import the *OpportunityManagement_managed.zip* solution to your empty organization. The solution can be found in the root directory of the solution template.
 * The *Unit tests* and *Schema tests* are ready to be run out of the box as they do not require a connection to a Dynamics CRM organization.
 * The *Acceptance tests* and *Integration tests* require a connection to a Dynamics CRM organization. To run these tests against the organization you created, you'll have to configure the **app.config** files for coresponding projects.
 The setup consists of connection string configuration and testing user setup. You'll need to create a testing user in your target organization which will be used to run the tests. The user needs to be activated and should have some basic setup (security roles, business unit, ...).

 Once done, you should be able to run all the test from the Visual Studio Test Explorer window and analyze the results.

## Solution overview

### OpportunityManagement.AcceptanceTests project
* This project uses the popular **SpecFlow** framework together with **NUnit** unit testing framework to run the tests.

* Using **SpecFlow** allows you to define the requirements whereas **NUnit** is used to drive the automation of the tests. With **SpecFlow**, you document the requirements in **Gherkin** language using feature files that contain scenarios.

* Think of these specifications as your acceptance criteria for your user stories. The specification file serves also as a documentation for the feature itself.

### OpportunityManagement.Common project
* This shared project contains utility methods and metadata classes used by other projects in the solution.

### OpportunityManagement.IntegrationTests project
* This project contains tests that exercice the *RatingPlugin* from the perspective of CRM SDK services.

* The project uses the **NUnit** unit testing framework to automate the tests, but other than that the tests are written completely in C#.
  
* The tests are very similar to the tests found in the *OpportunityManagement.AcceptanceTests* project and they can be viewed as *Acceptance tests* without the **SpecFlow** layer.  

### OpportunityManagement.Plugins project
* This project contains the implementation of the *RatingPlugin*. Notice the design/structure of the plug-in iself and how the implementation separates the business logic from the plugin context. This separation of concerns makes the code open for extension, but sill closed for modification. Additionaly, this separation encourages writing readable and easily maintainable unit tests like those found in the *OpportunityManagement.UnitTests* project.

### Opportunity Management SchemaTests project
* This project tests the Data Model definition for the *OpportunityManagement* solution.

* The solution template contains extracted solution files in the *_SolutionFiles* folder which contains few custom fields on the Opportunity entity. The tests simply parse and analyze the XML files which would normally be tracked by TFS or Git.

* The project uses the **xUnit** unit testing framework and simply runs few simple predefined rules that should keep the Data Model intact.

### OpportunityManagement.UnitTests project
* This project uses the **NUnit** unit testing framework to exercise the logic of the *RatingPlugin* found in the *OpportunityManagement.Plugins* project.

* Key points to notice here is how quickly these tests run in comparison with, for example, the *Acceptance tests*.

* Also notice how close are the unit tests bound to the implementation of the *RatingPlugin*. If, for example, we were to rewrite the *RatingPlugin* from scratch to accomodate a new business requirement, we would not be able to rely on the unit test suite to provide a safety net for our changes.

* The *Integration and Acceptance tests* however are completely agnostic to the implementation of the *RatingPlugin*. We are completely free to change the underlying implementation of the *RatingPlugin* and both the *Integration and Acceptance tests* would still provide us with a feedback whether the original business requirement is still being met.

## Built With
* [SpecFlow](https://specflow.org/) - SpecFlow
* [NUnit](https://nunit.org/) - Nunit
* [xUnit](https://xunit.github.io/) - xUnit
* [moq](https://github.com/moq/moq) - moq
* [xrm-ci-framework](https://github.com/WaelHamze/xrm-ci-framework) - xrm-ci-framework

## Licence
* This project is licensed under the Apache License - see the LICENSE.md file for details.