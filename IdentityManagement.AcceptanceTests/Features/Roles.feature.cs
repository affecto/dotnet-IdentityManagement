﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code

using TechTalk.SpecFlow;

#pragma warning disable
namespace Affecto.IdentityManagement.AcceptanceTests.Features
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class RolesFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Roles.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Roles", "", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((TechTalk.SpecFlow.FeatureContext.Current != null) 
                        && (TechTalk.SpecFlow.FeatureContext.Current.FeatureInfo.Title != "Roles")))
            {
                RolesFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Add permission for role")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Roles")]
        public virtual void AddPermissionForRole()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add permission for role", ((string[])(null)));
#line 3
this.ScenarioSetup(scenarioInfo);
#line 4
    testRunner.Given("a role \'Tree modifiers\' exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 5
    testRunner.And("role \'Tree modifiers\' doesn\'t have \'Edit classification\' permission", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 6
    testRunner.When("permission \'Edit classification\' is added for role \'Tree modifiers\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Permission"});
            table1.AddRow(new string[] {
                        "Edit classification"});
#line 7
    testRunner.Then("role \'Tree modifiers\' has following permissions:", ((string)(null)), table1, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Remove permission from role")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Roles")]
        public virtual void RemovePermissionFromRole()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Remove permission from role", ((string[])(null)));
#line 11
this.ScenarioSetup(scenarioInfo);
#line 12
    testRunner.Given("a role \'Tree modifiers\' exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 13
    testRunner.And("role \'Tree modifiers\' has permission \'Edit classification\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
    testRunner.When("permission \'Edit classification\' is removed from role \'Tree modifiers\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Permission"});
#line 15
    testRunner.Then("role \'Tree modifiers\' has following permissions:", ((string)(null)), table2, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("User has must have role permission to add permissions for role")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Roles")]
        public virtual void UserHasMustHaveRolePermissionToAddPermissionsForRole()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("User has must have role permission to add permissions for role", ((string[])(null)));
#line 18
this.ScenarioSetup(scenarioInfo);
#line 19
    testRunner.Given("the user has no permission to maintain role permissions", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 20
    testRunner.And("a role \'Tree modifiers\' exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
    testRunner.And("role \'Tree modifiers\' doesn\'t have \'Edit classification\' permission", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
    testRunner.When("permission \'Edit classification\' is added for role \'Tree modifiers\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 23
    testRunner.Then("adding a new permission for role fails", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 24
    testRunner.And("role \'Tree modifiers\' has no permissions", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("User has must have role permission to remove permissions from role")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Roles")]
        public virtual void UserHasMustHaveRolePermissionToRemovePermissionsFromRole()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("User has must have role permission to remove permissions from role", ((string[])(null)));
#line 26
this.ScenarioSetup(scenarioInfo);
#line 27
    testRunner.Given("the user has no permission to maintain role permissions", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 28
    testRunner.And("a role \'Tree modifiers\' exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
    testRunner.And("role \'Tree modifiers\' has permission \'Edit classification\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
    testRunner.When("permission \'Edit classification\' is removed from role \'Tree modifiers\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 31
    testRunner.Then("removing a permission from role fails", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Permission"});
            table3.AddRow(new string[] {
                        "Edit classification"});
#line 32
    testRunner.And("role \'Tree modifiers\' has following permissions:", ((string)(null)), table3, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Changing the name of a role")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Roles")]
        public virtual void ChangingTheNameOfARole()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Changing the name of a role", ((string[])(null)));
#line 36
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Description",
                        "External group"});
            table4.AddRow(new string[] {
                        "Tree modifiers",
                        "Can edit the tree",
                        "Administrators"});
#line 37
    testRunner.Given("following roles exists:", ((string)(null)), table4, "Given ");
#line 40
    testRunner.When("the name of the role \'Tree modifiers\' is changed to \'Tree editors\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Description",
                        "External group"});
            table5.AddRow(new string[] {
                        "Tree editors",
                        "Can edit the tree",
                        "Administrators"});
#line 41
    testRunner.Then("there are following roles:", ((string)(null)), table5, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Clearing the name of a role is not allowed")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Roles")]
        public virtual void ClearingTheNameOfARoleIsNotAllowed()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Clearing the name of a role is not allowed", ((string[])(null)));
#line 45
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Description",
                        "External group"});
            table6.AddRow(new string[] {
                        "Tree modifiers",
                        "Can edit the tree",
                        "Administrators"});
#line 46
    testRunner.Given("following roles exists:", ((string)(null)), table6, "Given ");
#line 49
    testRunner.When("the name of the role \'Tree modifiers\' is cleared", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 50
    testRunner.Then("updating the role fails", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Description",
                        "External group"});
            table7.AddRow(new string[] {
                        "Tree modifiers",
                        "Can edit the tree",
                        "Administrators"});
#line 51
    testRunner.And("there are following roles:", ((string)(null)), table7, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Changing the name of a role is not possible without role editing permissions")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Roles")]
        public virtual void ChangingTheNameOfARoleIsNotPossibleWithoutRoleEditingPermissions()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Changing the name of a role is not possible without role editing permissions", ((string[])(null)));
#line 55
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Description",
                        "External group"});
            table8.AddRow(new string[] {
                        "Tree modifiers",
                        "Can edit the tree",
                        "Administrators"});
#line 56
    testRunner.Given("following roles exists:", ((string)(null)), table8, "Given ");
#line 59
    testRunner.And("the user has no permission to maintain role permissions", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
    testRunner.When("the name of the role \'Tree modifiers\' is changed to \'Tree editors\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 61
    testRunner.Then("updating the role fails because of invalid permissions", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Description",
                        "External group"});
            table9.AddRow(new string[] {
                        "Tree modifiers",
                        "Can edit the tree",
                        "Administrators"});
#line 62
    testRunner.Then("there are following roles:", ((string)(null)), table9, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Changing the description of a role")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Roles")]
        public virtual void ChangingTheDescriptionOfARole()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Changing the description of a role", ((string[])(null)));
#line 66
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Description",
                        "External group"});
            table10.AddRow(new string[] {
                        "Tree modifiers",
                        "Can edit the tree",
                        "Administrators"});
#line 67
    testRunner.Given("following roles exists:", ((string)(null)), table10, "Given ");
#line 70
    testRunner.When("the description of the role \'Tree modifiers\' is changed to \'Tree editors\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Description",
                        "External group"});
            table11.AddRow(new string[] {
                        "Tree modifiers",
                        "Tree editors",
                        "Administrators"});
#line 71
    testRunner.Then("there are following roles:", ((string)(null)), table11, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Clearing the description of a role")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Roles")]
        public virtual void ClearingTheDescriptionOfARole()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Clearing the description of a role", ((string[])(null)));
#line 75
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Description",
                        "External group"});
            table12.AddRow(new string[] {
                        "Tree modifiers",
                        "Can edit the tree",
                        "Administrators"});
#line 76
    testRunner.Given("following roles exists:", ((string)(null)), table12, "Given ");
#line 79
    testRunner.When("the description of the role \'Tree modifiers\' is cleared", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Description",
                        "External group"});
            table13.AddRow(new string[] {
                        "Tree modifiers",
                        "",
                        "Administrators"});
#line 80
    testRunner.Then("there are following roles:", ((string)(null)), table13, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Clearing the description of a role is not possible without role editing permissio" +
            "ns")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Roles")]
        public virtual void ClearingTheDescriptionOfARoleIsNotPossibleWithoutRoleEditingPermissions()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Clearing the description of a role is not possible without role editing permissio" +
                    "ns", ((string[])(null)));
#line 84
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Description",
                        "External group"});
            table14.AddRow(new string[] {
                        "Tree modifiers",
                        "Can edit the tree",
                        "Administrators"});
#line 85
    testRunner.Given("following roles exists:", ((string)(null)), table14, "Given ");
#line 88
    testRunner.And("the user has no permission to maintain role permissions", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 89
    testRunner.When("the description of the role \'Tree modifiers\' is cleared", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 90
    testRunner.Then("updating the role fails because of invalid permissions", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Description",
                        "External group"});
            table15.AddRow(new string[] {
                        "Tree modifiers",
                        "Can edit the tree",
                        "Administrators"});
#line 91
    testRunner.Then("there are following roles:", ((string)(null)), table15, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Changing the external group name of a role")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Roles")]
        public virtual void ChangingTheExternalGroupNameOfARole()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Changing the external group name of a role", ((string[])(null)));
#line 95
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table16 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Description",
                        "External group"});
            table16.AddRow(new string[] {
                        "Tree modifiers",
                        "",
                        ""});
#line 96
    testRunner.Given("following roles exists:", ((string)(null)), table16, "Given ");
#line 99
    testRunner.When("the external group name of the role \'Tree modifiers\' is changed to \'Administrator" +
                    "s\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table17 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Description",
                        "External group"});
            table17.AddRow(new string[] {
                        "Tree modifiers",
                        "",
                        "Administrators"});
#line 100
    testRunner.Then("there are following roles:", ((string)(null)), table17, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Clearing the external group name of a role")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Roles")]
        public virtual void ClearingTheExternalGroupNameOfARole()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Clearing the external group name of a role", ((string[])(null)));
#line 104
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table18 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Description",
                        "External group"});
            table18.AddRow(new string[] {
                        "Tree modifiers",
                        "Can edit the tree",
                        "Administrators"});
#line 105
    testRunner.Given("following roles exists:", ((string)(null)), table18, "Given ");
#line 108
    testRunner.When("the external group name of the role \'Tree modifiers\' is cleared", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table19 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Description",
                        "External group"});
            table19.AddRow(new string[] {
                        "Tree modifiers",
                        "Can edit the tree",
                        ""});
#line 109
    testRunner.Then("there are following roles:", ((string)(null)), table19, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Changing the external group name of a role is not possible without role editing p" +
            "ermissions")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Roles")]
        public virtual void ChangingTheExternalGroupNameOfARoleIsNotPossibleWithoutRoleEditingPermissions()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Changing the external group name of a role is not possible without role editing p" +
                    "ermissions", ((string[])(null)));
#line 113
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table20 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Description",
                        "External group"});
            table20.AddRow(new string[] {
                        "Tree modifiers",
                        "Can edit the tree",
                        "Administrators"});
#line 114
    testRunner.Given("following roles exists:", ((string)(null)), table20, "Given ");
#line 117
    testRunner.And("the user has no permission to maintain role permissions", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 118
    testRunner.When("the external group name of the role \'Tree modifiers\' is changed to \'Externals\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 119
    testRunner.Then("updating the role fails because of invalid permissions", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table21 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Description",
                        "External group"});
            table21.AddRow(new string[] {
                        "Tree modifiers",
                        "Can edit the tree",
                        "Administrators"});
#line 120
    testRunner.Then("there are following roles:", ((string)(null)), table21, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Creating a role with duplicate name")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Roles")]
        public virtual void CreatingARoleWithDuplicateName()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Creating a role with duplicate name", ((string[])(null)));
#line 124
this.ScenarioSetup(scenarioInfo);
#line 125
    testRunner.Given("a role \'Tree modifiers\' exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 126
    testRunner.When("a role \'Tree modifiers\' is added", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 127
    testRunner.Then("operation fails because a role with the same name already exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Updating a role with duplicate name")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Roles")]
        public virtual void UpdatingARoleWithDuplicateName()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Updating a role with duplicate name", ((string[])(null)));
#line 129
this.ScenarioSetup(scenarioInfo);
#line 130
    testRunner.Given("a role \'Tree modifiers\' exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 131
    testRunner.And("a role \'Tree editors\' exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 132
    testRunner.When("the name of the role \'Tree modifiers\' is changed to \'Tree editors\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 133
    testRunner.Then("operation fails because a role with the same name already exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
