Feature: Roles

Scenario: Add permission for role
    Given a role 'Tree modifiers' exists
    And role 'Tree modifiers' doesn't have 'Edit classification' permission
    When permission 'Edit classification' is added for role 'Tree modifiers'
    Then role 'Tree modifiers' has following permissions:
    | Permission          |
    | Edit classification |

Scenario: Remove permission from role
    Given a role 'Tree modifiers' exists
    And role 'Tree modifiers' has permission 'Edit classification'
    When permission 'Edit classification' is removed from role 'Tree modifiers'
    Then role 'Tree modifiers' has following permissions:
    | Permission |

Scenario: User has must have role permission to add permissions for role
    Given the user has no permission to maintain role permissions
    And a role 'Tree modifiers' exists
    And role 'Tree modifiers' doesn't have 'Edit classification' permission
    When permission 'Edit classification' is added for role 'Tree modifiers'
    Then adding a new permission for role fails
    And role 'Tree modifiers' has no permissions

Scenario: User has must have role permission to remove permissions from role
    Given the user has no permission to maintain role permissions
    And a role 'Tree modifiers' exists
    And role 'Tree modifiers' has permission 'Edit classification'
    When permission 'Edit classification' is removed from role 'Tree modifiers'
    Then removing a permission from role fails
    And role 'Tree modifiers' has following permissions:
    | Permission          |
    | Edit classification |

Scenario: Changing the name of a role
    Given following roles exists:
    | Name           | Description       | External group |
    | Tree modifiers | Can edit the tree | Administrators |
    When the name of the role 'Tree modifiers' is changed to 'Tree editors'
    Then there are following roles:
    | Name         | Description       | External group |
    | Tree editors | Can edit the tree | Administrators |

Scenario: Clearing the name of a role is not allowed
    Given following roles exists:
    | Name           | Description       | External group |
    | Tree modifiers | Can edit the tree | Administrators |
    When the name of the role 'Tree modifiers' is cleared
    Then updating the role fails
    And there are following roles:
    | Name           | Description       | External group |
    | Tree modifiers | Can edit the tree | Administrators |

Scenario: Changing the name of a role is not possible without role editing permissions
    Given following roles exists:
    | Name           | Description       | External group |
    | Tree modifiers | Can edit the tree | Administrators |
    And the user has no permission to maintain role permissions
    When the name of the role 'Tree modifiers' is changed to 'Tree editors'
    Then updating the role fails because of invalid permissions
    Then there are following roles:
    | Name           | Description       | External group |
    | Tree modifiers | Can edit the tree | Administrators |

Scenario: Changing the description of a role
    Given following roles exists:
    | Name           | Description       | External group |
    | Tree modifiers | Can edit the tree | Administrators |
    When the description of the role 'Tree modifiers' is changed to 'Tree editors'
    Then there are following roles:
    | Name           | Description  | External group |
    | Tree modifiers | Tree editors | Administrators |

Scenario: Clearing the description of a role
    Given following roles exists:
    | Name           | Description       | External group |
    | Tree modifiers | Can edit the tree | Administrators |
    When the description of the role 'Tree modifiers' is cleared
    Then there are following roles:
    | Name           | Description | External group |
    | Tree modifiers |             | Administrators |

Scenario: Clearing the description of a role is not possible without role editing permissions
    Given following roles exists:
    | Name           | Description       | External group |
    | Tree modifiers | Can edit the tree | Administrators |
    And the user has no permission to maintain role permissions
    When the description of the role 'Tree modifiers' is cleared
    Then updating the role fails because of invalid permissions
    Then there are following roles:
    | Name           | Description       | External group |
    | Tree modifiers | Can edit the tree | Administrators |

Scenario: Changing the external group name of a role
    Given following roles exists:
    | Name           | Description | External group |
    | Tree modifiers |             |                |
    When the external group name of the role 'Tree modifiers' is changed to 'Administrators'
    Then there are following roles:
    | Name           | Description | External group |
    | Tree modifiers |             | Administrators |

Scenario: Clearing the external group name of a role
    Given following roles exists:
    | Name           | Description       | External group |
    | Tree modifiers | Can edit the tree | Administrators |
    When the external group name of the role 'Tree modifiers' is cleared
    Then there are following roles:
    | Name           | Description       | External group |
    | Tree modifiers | Can edit the tree |                |

Scenario: Changing the external group name of a role is not possible without role editing permissions
    Given following roles exists:
    | Name           | Description       | External group |
    | Tree modifiers | Can edit the tree | Administrators |
    And the user has no permission to maintain role permissions
    When the external group name of the role 'Tree modifiers' is changed to 'Externals'
    Then updating the role fails because of invalid permissions
    Then there are following roles:
    | Name           | Description       | External group |
    | Tree modifiers | Can edit the tree | Administrators |

Scenario: Creating a role with duplicate name
    Given a role 'Tree modifiers' exists
    When a role 'Tree modifiers' is added
    Then operation fails because a role with the same name already exists

Scenario: Updating a role with duplicate name
    Given a role 'Tree modifiers' exists
    And a role 'Tree editors' exists
    When the name of the role 'Tree modifiers' is changed to 'Tree editors'
    Then operation fails because a role with the same name already exists
