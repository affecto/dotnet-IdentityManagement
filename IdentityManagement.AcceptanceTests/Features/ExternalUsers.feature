Feature: ExternalUsers

Background:

    Given there are following external users:
    | User name | Display name  | Account name | Account type     |
    | Hank      | Hank Jennings | hank@domain  | Active directory |

    And external user 'Hank' is member of following groups:
    | Group  |
    | group1 |
    | group2 |
    | group3 |
    | group4 |

    And there are following groups in the identity management service:
    | Name | External group |
    | g1   | group1         |
    | g2   | group2         |
    | g3   |                |
    | g4   | group4         |

    And there are following roles in the identity management service:
    | Name | External group |
    | r1   |                |
    | r2   | group2         |
    | r3   | group3         |
    | r4   | group4         |

Scenario: External user is found by account name
    Given a new external user 'Hank' is added
    When account of external user 'Hank' in identity management service is found
    Then user information can be retrieved by account name of external user 'Hank'

Scenario: External user is not found by account name
    When account of external user 'Hank' in identity management service is not found
    And retrieving user information by account name of external user 'Hank'
    Then retrieving user information failed

Scenario: Add new external user with groups and roles
    When a new external user 'Hank' is added
    Then the following users exist:
         | Name          |
         | Hank Jennings |
    And user 'Hank Jennings' has an active directory account with name 'hank@domain'
    And the user 'Hank Jennings' has the following roles:
         | Role |
         | r2   |
         | r3   |
         | r4   |
    And the user 'Hank Jennings' has is a member of the following groups:
         | Group |
         | g1    |
         | g2    |
         | g4    |

 Scenario: Add new external user with custom properties
    When a new external user 'Hank' is added with custom property 'Email' set to 'hank@gmail.com'
    Then the following users exist:
         | Name          |
         | Hank Jennings |
    And user 'Hank Jennings' has custom property 'Email' with the value 'hank@gmail.com'

Scenario: Update groups and roles of existing external user
    Given a new external user 'Hank' is added
    And the external group name of the group 'g3' is changed to 'group4'
    And the external group name of the group 'g4' is changed to 'group5'
    And the external group name of the group 'g2' is cleared
    And the external group name of the role 'r1' is changed to 'group4'
    And the external group name of the role 'r2' is changed to 'group5'
    And the external group name of the role 'r3' is cleared
    When information of external user 'Hank' is updated
    Then the following users exist:
         | Name          |
         | Hank Jennings |
    And user 'Hank Jennings' has an active directory account with name 'hank@domain'
    And the user 'Hank Jennings' has the following roles:
         | Role |
         | r1   |
         | r3   |
         | r4   |
    And the user 'Hank Jennings' has is a member of the following groups:
         | Group |
         | g1    |
         | g2    |
         | g3    |
