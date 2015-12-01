Feature: Groups

Scenario: Group cannot be added without a permission
    Given the user has no permission to maintain user data
    When a group 'Administrators' is added
    Then adding the group fails because of invalid permissions

Scenario: Adding groups
    When a group 'Administrators' is added
    And a group 'Readers' is added with a description 'Can read the tree'
    Then the following groups exist:
    | Name           | Description       | External group | Disabled |
    | Administrators |                   |                | False    |
    | Readers        | Can read the tree |                | False    |
    And all groups are enabled

Scenario: Adding a group with no name is not possible
    When a group with no name is added
    Then adding the group fails

Scenario: Changing the name of a group cannot be done without a permission
    Given a group 'Administrators' is added
    And the user has no permission to maintain user data
    When the name of the group 'Administrators' is changed to 'Admins'
    Then updating the group fails because of invalid permissions

Scenario: Changing the name of a group
    Given a group 'Administrators' is added
    When the name of the group 'Administrators' is changed to 'Admins'
    Then the following groups exist:
    | Name   | Description | External group | Disabled |
    | Admins |             |                | False    |

Scenario: Clearing the name of a group is not possible
    Given a group 'Administrators' is added
    When the name of the group 'Administrators' is cleared
    Then updating the group fails

Scenario: Changing the description of a group cannot be done without a permission
    Given a group 'Administrators' is added
    And the user has no permission to maintain user data
    When the description of the group 'Administrators' is cleared
    Then updating the group fails because of invalid permissions

Scenario: Changing the description of a group
    Given a group 'Readers' is added with a description 'Can read the tree'
    When the description of the group 'Readers' is cleared
    Then the following groups exist:
    | Name    | Description | External group | Disabled |
    | Readers |             |                | False    |

Scenario: Changing the external group name of a group
    Given a group 'Readers' is added with a description 'Can read the tree'
    When the external group name of the group 'Readers' is changed to 'Externals'
    Then the following groups exist:
    | Name    | Description       | External group | Disabled |
    | Readers | Can read the tree | Externals      | False    |

Scenario: Clearing the external group name of a group
    Given a group 'Readers' is added with a description 'Can read the tree'
    And the external group name of the group 'Readers' is changed to 'Externals'
    When the external group name of the group 'Readers' is cleared
    Then the following groups exist:
    | Name    | Description       | External group | Disabled |
    | Readers | Can read the tree |                | False    |

Scenario: Clearing the external group name of a group is not possible without permissions
    Given a group 'Administrators' is added
    And the user has no permission to maintain user data
    When the external group name of the group 'Administrators' is cleared
    Then updating the group fails because of invalid permissions

Scenario: Disabling a group cannot be done without a permission
    Given a group 'Administrators' is added
    And the user has no permission to maintain user data
    When the group 'Administrators' is disabled
    Then updating the group fails because of invalid permissions

Scenario: Disabling a group
    Given a group 'Administrators' is added
    When the group 'Administrators' is disabled
    Then the group 'Administrators' is disabled

Scenario: Adding a user to a group cannot be done without a permission
    Given a user 'Hank Jennings'´exists
    And a group 'Administrators' is added
    When the user has no permission to maintain user data
    And the user 'Hank Jennings' is added to the group 'Administrators'
    Then adding the user to the group fails

Scenario: Adding a user to a group
    Given a user 'Hank Jennings'´exists
    And a group 'Administrators' is added
    When the user 'Hank Jennings' is added to the group 'Administrators'
    Then the group 'Administrators' has the following members:
    | User          |
    | Hank Jennings |

Scenario: Removing a user from a group cannot be done without a permission
    Given a user 'Hank Jennings'´exists
    And a group 'Administrators' is added
    And the user 'Hank Jennings' is added to the group 'Administrators'
    When the user has no permission to maintain user data
    And the user 'Hank Jennings'´is removed from the group 'Administrators'
    Then removing the user from the group fails

Scenario: Removing a user from a group
    Given a user 'Hank Jennings'´exists
    And a user 'Norma Jennings'´exists
    And a group 'Administrators' is added
    And the user 'Hank Jennings' is added to the group 'Administrators'
    And the user 'Norma Jennings' is added to the group 'Administrators'
    When the user 'Hank Jennings'´is removed from the group 'Administrators'
    Then the group 'Administrators' has the following members:
    | User           |
    | Norma Jennings |

Scenario: Creating group with duplicate name
    Given a group 'Administrators' is added
    When a group 'Administrators' is added
    Then operation fails because group with the same name already exists

Scenario: Updating group with duplicate name
    Given a group 'Administrators' is added
    And a group 'Admins' is added
    When the name of the group 'Admins' is changed to 'Administrators'
    Then operation fails because group with the same name already exists
