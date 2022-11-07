Feature: Rating
	To streamline the process of Opportunity rating.
	When user creates new Opportunity,
	the Opportunity will get Rating based on Number of Employees of related Account.

@Acceptance
Scenario: Related Account has unspecified Number of Employees
	Given I want to Create an Opportunity
	And I have specified an Account with Number of Employees equal to null
	When I try to save the record
	Then record is saved successfuly
	And Opportunity Rating is null

@Acceptance
Scenario: Related Account has 0 employees
	Given I want to Create an Opportunity
	And I have specified an Account with Number of Employees equal to 0
	When I try to save the record
	Then record is saved successfuly
	And Opportunity Rating is null

@Acceptance
Scenario: Related Account has 1 employee
	Given I want to Create an Opportunity
	And I have specified an Account with Number of Employees equal to 1
	When I try to save the record
	Then record is saved successfuly
	And Opportunity Rating is Cold

@Acceptance
Scenario: Related Account has 9 employees
	Given I want to Create an Opportunity
	And I have specified an Account with Number of Employees equal to 9
	When I try to save the record
	Then record is saved successfuly
	And Opportunity Rating is Cold

@Acceptance
Scenario: Related Account has 10 employees
	Given I want to Create an Opportunity
	And I have specified an Account with Number of Employees equal to 10
	When I try to save the record
	Then record is saved successfuly
	And Opportunity Rating is Warm

@Acceptance
Scenario: Related Account has 99 employees
	Given I want to Create an Opportunity
	And I have specified an Account with Number of Employees equal to 99
	When I try to save the record
	Then record is saved successfuly
	And Opportunity Rating is Warm

@Acceptance
Scenario: Related Account has 100 employees
	Given I want to Create an Opportunity
	And I have specified an Account with Number of Employees equal to 100
	When I try to save the record
	Then record is saved successfuly
	And Opportunity Rating is Hot

@Acceptance
Scenario: Related Account has 101 employees
	Given I want to Create an Opportunity
	And I have specified an Account with Number of Employees equal to 101
	When I try to save the record
	Then record is saved successfuly
	And Opportunity Rating is Hot

@Acceptance
Scenario: Opportunity has no related Account
	Given I want to Create an Opportunity
	And I have not specified an Account
	When I try to save the record
	Then record is not saved
	And an error with message "Attribute 'parentaccountid' on opportunity cannot be null." is shown