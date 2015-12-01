Feature: Users

Scenario: Users cannot be added without a permission
    Given the user has no permission to maintain user data
    When a user 'Hank Jennings' is added
    Then adding the user fails because of invalid permissions

Scenario: Adding users
    When a user 'Hank Jennings' is added
    And a user 'Norma Jennings' is added
    Then the following users exist:
    | Name           |
    | Hank Jennings  |
    | Norma Jennings |
    And all users are enabled

Scenario: Adding users with custom properties
    When a user 'Hank Jennings' is added with the following custom properties:
    | Name           | Value                                |
    | EmailAddress   | hank@jennings.net                    |
    | OrganizationId | DB42B633-DE5C-4414-A20F-D57AAED283C1 |
    | StreetAddress  |                                      |
    Then the following users exist:
    | Name          |
    | Hank Jennings |
    And the user 'Hank Jennings' has the following custom properties:
    | Name           | Value                                |
    | EmailAddress   | hank@jennings.net                    |
    | OrganizationId | DB42B633-DE5C-4414-A20F-D57AAED283C1 |
    | StreetAddress  |                                      |
    And all users are enabled

Scenario: Adding a user with no name is not possible
    When a user with no name is added
    Then adding the user fails

Scenario: Disabling a user cannot be done without a permission
    Given a user 'Hank Jennings' is added
    When the user has no permission to maintain user data
    And the user 'Hank Jennings' is disabled
    Then updating the user fails because of invalid permissions

Scenario: Disabling a user
    Given a user 'Hank Jennings' is added
    When the user 'Hank Jennings' is disabled
    Then the user 'Hank Jennings' is disabled

Scenario: Changing user's name cannot be done without a permission
    Given a user 'Hank Jennings' is added
    When the user has no permission to maintain user data
    And the name of the user 'Hank Jennings' is changed to 'Mike Modano'
    Then updating the user fails because of invalid permissions

Scenario: Changing user's name
    Given a user 'Hank Jennings' is added
    When the name of the user 'Hank Jennings' is changed to 'Mike Modano'
    Then the following users exist:
    | Name        |
    | Mike Modano |

Scenario: Clearing the name of a user is not possible
    Given a user 'Hank Jennings' is added
    When the name of the user 'Hank Jennings' is cleared
    Then updating the user fails

Scenario: Adding a user role cannot be done without a permission
    Given a role 'Tree modifiers' exists
    And a user 'Hank Jennings' is added
    When the user has no permission to maintain user data
    And the user 'Hank Jennings' is given the role 'Tree modifiers'
    Then adding the role fails because of invalid permissions

Scenario: Adding a user role
    Given a role 'Tree modifiers' exists
    And a user 'Hank Jennings' is added
    When the user 'Hank Jennings' is given the role 'Tree modifiers'
    Then the user 'Hank Jennings' has the following roles:
    | Role           |
    | Tree modifiers |

Scenario: Removing a user role cannot be done without a permission
    Given a role 'Tree modifiers' exists
    And a user 'Hank Jennings' is added
    And the user 'Hank Jennings' is given the role 'Tree modifiers'
    When the user has no permission to maintain user data
    And the user 'Hank Jennings' is removed from the role 'Tree modifiers'
    Then removing the role fails because of invalid permissions

Scenario: Removing a user role
    Given a role 'Tree modifiers' exists
    And a user 'Hank Jennings' is added
    And the user 'Hank Jennings' is given the role 'Tree modifiers'
    When the user 'Hank Jennings' is removed from the role 'Tree modifiers'
    Then the user 'Hank Jennings' has no roles

Scenario: Adding a user organization cannot be done without a permission
    Given an organization 'R&D' exists
    And a user 'Hank Jennings' is added
    When the user has no permission to maintain user data
    And the user 'Hank Jennings' is added to the organization 'R&D'
    Then adding the organization fails because of invalid permissions

Scenario: Adding a user organization
    Given an organization 'R&D' exists
    And an organization 'All' exists
    And a user 'Hank Jennings' is added
    
    When the user 'Hank Jennings' is added to the organization 'All'
    Then the user 'Hank Jennings' is in the following organizations:
    | Organization |
    | All          |

    When the user 'Hank Jennings' is added to the organization 'R&D'
    Then the user 'Hank Jennings' is in the following organizations:
    | Organization |
    | All          |
    | R&D          |

Scenario: Adding a disabled user organization fails
    Given an disabled organization 'R&D' exists
    And a user 'Hank Jennings' is added
    When the user 'Hank Jennings' is added to the organization 'R&D'
    Then adding the organization fails

Scenario: Removing a user organization cannot be done without a permission
    Given an organization 'R&D' exists
    And a user 'Hank Jennings' is added
    And the user 'Hank Jennings' is added to the organization 'R&D'
    When the user has no permission to maintain user data
    And the user 'Hank Jennings' is removed from the organization 'R&D'
    Then removing the organization fails because of invalid permissions

Scenario: Removing a user organization
    Given an organization 'R&D' exists
    And a user 'Hank Jennings' is added
    And the user 'Hank Jennings' is added to the organization 'R&D'
    When the user 'Hank Jennings' is removed from the organization 'R&D'
    Then the user 'Hank Jennings' is in no organization

Scenario: Adding account to user
    Given a user 'Hank Jennings' is added
    When an active directory account with name 'hank@domain' is added for user 'Hank Jennings'
    Then user 'Hank Jennings' has an active directory account with name 'hank@domain'

Scenario: Adding a password account to user
    Given a user 'Hank Jennings' is added
    When an account with name 'hank@domain' and password 'VerySecret' is added for user 'Hank Jennings'
    Then user 'Hank Jennings' has an account with name 'hank@domain' and password 'VerySecret'

Scenario: Adding a password account without password to user
    Given a user 'Hank Jennings' is added
    When a password account with name 'hank@domain' without password is added for user 'Hank Jennings'
    Then adding new account fails because wrong account type is specified

Scenario: Checking for user's matching password
    Given a user 'Hank Jennings' is added
    And an account with name 'hank@domain' and password 'VerySecret' is added for user 'Hank Jennings'
    Then password 'VerySecret' matches to the password of user account 'hank@domain'

Scenario: Checking for user's non-matching password
    Given a user 'Hank Jennings' is added
    And an account with name 'hank@domain' and password 'VerySecret' is added for user 'Hank Jennings'
    Then password 'ThisIsWrong!' does not match to the password of user account 'hank@domain'

Scenario: Checking for user's non-matching case sensitive password
    Given a user 'Hank Jennings' is added
    And an account with name 'hank@domain' and password 'VerySecret' is added for user 'Hank Jennings'
    Then password 'verysecret' does not match to the password of user account 'hank@domain'

Scenario: Checking for matching password of non-existing user
    Given a user 'Hank Jennings' is added
    And an account with name 'hank@domain' and password 'VerySecret' is added for user 'Hank Jennings'
    Then password 'VerySecret!' does not match to the password of user account 'otherUser@domain'

Scenario: Updating user's account information
    Given a user 'Hank Jennings' is added
    And an active directory account with name 'hank@domain' is added for user 'Hank Jennings'
    When active directory account name is changed to 'jennings@domain' for user 'Hank Jennings'
    Then user 'Hank Jennings' has an active directory account with name 'jennings@domain'

Scenario: Adding empty account
    Given a user 'Hank Jennings' is added
    When an active directory account without name is added for user 'Hank Jennings'
    Then adding new account fails because of account's name is not specified

Scenario: Updating account with empty name
    Given a user 'Hank Jennings' is added
    And an active directory account with name 'hank@domain' is added for user 'Hank Jennings'
    When active directory account name is cleared for user 'Hank Jennings'
    Then updating account fails because of account's name is not specified

Scenario: Adding account that is already assigned to another user
    Given a user 'Hank Jennings' is added
    And a user 'Norma Jennings' is added
    And an active directory account with name 'jennings@domain' is added for user 'Hank Jennings'
    When an active directory account with name 'jennings@domain' is added for user 'Norma Jennings'
    Then adding new account fails because account is already assigned

Scenario: Updating account that is already assigned to another user
    Given a user 'Hank Jennings' is added
    And a user 'Norma Jennings' is added
    And an active directory account with name 'jennings@domain' is added for user 'Hank Jennings'
    And an active directory account with name 'norma@domain' is added for user 'Norma Jennings'
    When active directory account name is changed to 'jennings@domain' for user 'Norma Jennings'
    Then adding new account fails because account is already assigned

Scenario: Adding multiple accounts for user
    Given a user 'Hank Jennings' is added
    When an active directory account with name 'hank@domain' is added for user 'Hank Jennings'
    And a federated account with name 'hank' is added for user 'Hank Jennings'
    Then user 'Hank Jennings' has an active directory account with name 'hank@domain'
    And user 'Hank Jennings' has a federated account with name 'hank'

Scenario: Removing an account from user
    Given a user 'Hank Jennings' is added
    And an active directory account with name 'hank@domain' is added for user 'Hank Jennings'
    When removing active directory account from user 'Hank Jennings'
    Then user 'Hank Jennings' has no assigned active directory account

Scenario: Getting users by custom property value
    Given a user 'Hank Jennings' is added with the following custom properties:
    | Name           | Value                                |
    | EmailAddress   | hank@jennings.net                    |
    | OrganizationId | DB42B633-DE5C-4414-A20F-D57AAED283C1 |
    | StreetAddress  | Street 123                           |
	And a user 'Jean Jennings' is added with the following custom properties:
    | Name           | Value                                |
    | EmailAddress   | jean@jennings.net                    |
    | OrganizationId | C59A38A6-9414-433A-8611-181B1F96B7EC |
    | StreetAddress  |                                      |
	And a user 'Norma Jennings' is added with the following custom properties:
    | Name           | Value                                |
    | EmailAddress   | norma@jennings.net                   |
    | OrganizationId | C59A38A6-9414-433A-8611-181B1F96B7EC |
    | StreetAddress  | Street 123                           |
	When users are searched by custom property name 'StreetAddress' and value 'Street 123'
    Then the following users having custom properties are returned:
    | Name           | EmailAddress       | OrganizationId                       | StreetAddress |
    | Hank Jennings  | hank@jennings.net  | DB42B633-DE5C-4414-A20F-D57AAED283C1 | Street 123    |
    | Norma Jennings | norma@jennings.net | C59A38A6-9414-433A-8611-181B1F96B7EC | Street 123    |
