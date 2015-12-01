Feature: Organizations

Scenario: Organizations cannot be added without a permission
    Given the user has no permission to maintain user data
    When an organization 'Cleaners' is added
    Then adding the organization fails because of invalid permissions

Scenario: Adding organizations
    When an organization 'R&D' is added with a descrption 'Research and development'
    And an organization 'Cleaners' is added
    Then the following organizations exist:
    | Name     | Description              |
    | R&D      | Research and development |
    | Cleaners |                          |
    And all organizations are enabled

Scenario: Adding an organization with no name is not possible
    When an organization with no name is added
    Then adding the organization fails

Scenario: Changing organization name cannot be added without a permission
    Given an organization 'Cleaners' is added
    When the user has no permission to maintain user data
    And the name of the organization 'Cleaners' is changed to 'Sanitators'
    Then updating the organization fails because of invalid permissions

Scenario: Changing organization name
    Given an organization 'Cleaners' is added
    When the name of the organization 'Cleaners' is changed to 'Sanitators'
    Then the following organizations exist:
    | Name       | Description |
    | Sanitators |             |

Scenario: Clearing the name of an organization is not possible
    Given an organization 'Cleaners' is added
    When the name of the organization 'Cleaners' is cleared
    Then updating the organization fails

Scenario: Changing organization description cannot be added without a permission
    Given an organization 'R&D' is added with a descrption 'Research and development'
    When the user has no permission to maintain user data
    And the description of the organization 'R&D' is cleared
    Then updating the organization fails because of invalid permissions

Scenario: Changing organization description
    Given an organization 'R&D' is added with a descrption 'Research and development'
    When the description of the organization 'R&D' is cleared
    Then the following organizations exist:
    | Name | Description |
    | R&D  |             |

Scenario: Disabling an organization cannot be added without a permission
    Given an organization 'Cleaners' is added
    When the user has no permission to maintain user data
    And the organization 'Cleaners' is disabled
    Then updating the organization fails because of invalid permissions

Scenario: Disabling an organization
    Given an organization 'Cleaners' is added
    When the organization 'Cleaners' is disabled
    Then the organization 'Cleaners' is disabled

Scenario: Creating organization with duplicate name
    Given an organization 'Cleaners' is added
    When an organization 'Cleaners' is added
    Then operation fails because organization with the same name already exists

Scenario: Updating organization with duplicate name
    Given an organization 'Cleaners' is added
    And an organization 'Sanitators' is added
    When the name of the organization 'Cleaners' is changed to 'Sanitators'
    Then operation fails because organization with the same name already exists
