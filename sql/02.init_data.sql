
-- 預設使用者及角色
INSERT INTO `role` (`roleID`, `Name`) VALUES
	(1, 'QA'),
	(2, 'RD'),
	(3, 'PM'),
	(4, 'Admin');

INSERT INTO `user` (`userID`, `Name`) VALUES
	(1, 'qa'),
	(2, 'rd'),
	(3, 'pm'),
	(4, 'admin');
	
INSERT INTO `userrole` (`userID`, `roleID`) VALUES
	(1, 1),
	(2, 2),
	(3, 3),
	(4, 4);
	
-- 預設issue類型
INSERT INTO `issuetype` (`ID`, `Name`) VALUES
	(1, 'bug'),
	(2, 'feature'),
	(3, 'testcase');
	
-- 使用者對issue的操作權限
SET @QA_RoleID := 1,
	@RD_RoleID := 2,
	@PM_RoleID := 3,
	@Admin_RoleID := 4;

SET @Bug_TypeID := 1,
	@Feature_TypeID := 2,
	@Testcase_TypeID := 3;

SET @Create_Action := 1,
	@Read_Action := 2,
	@Update_Action := 3,
	@Delete_Action := 4;
	
SET @New_IssueStatus := 1,
	@Resolved_IssueStatus := 2;

-- QA can create a bug, edit a bug and delete a bug.
INSERT INTO `roleissueaction` (`roleID`, `issueType`, `issueAction`) VALUES
	(@QA_RoleID, @Bug_TypeID, @Create_Action),
	(@QA_RoleID, @Bug_TypeID, @Read_Action),
	(@QA_RoleID, @Bug_TypeID, @Update_Action),
	(@QA_RoleID, @Bug_TypeID, @Delete_Action);

-- RD can resolve a bug.
INSERT INTO `roleissueaction` (`roleID`, `issueType`, `issueAction`) VALUES
	(@RD_RoleID, @Bug_TypeID, @Read_Action);
INSERT INTO `roleissuestatus` (`roleID`, `issueType`, `issueStatus`) VALUES
	(@RD_RoleID, @Bug_TypeID, @Resolved_IssueStatus);
	
-- PM can create new issue type feature
INSERT INTO `roleissueaction` (`roleID`, `issueType`, `issueAction`) VALUES
	(@PM_RoleID, @Feature_TypeID, @Create_Action),
	(@PM_RoleID, @Feature_TypeID, @Read_Action);
	
-- RD can resolve a feature.
INSERT INTO `roleissueaction` (`roleID`, `issueType`, `issueAction`) VALUES
	(@RD_RoleID, @Feature_TypeID, @Read_Action);
INSERT INTO `roleissuestatus` (`roleID`, `issueType`, `issueStatus`) VALUES
	(@RD_RoleID, @Feature_TypeID, @Resolved_IssueStatus);
	
-- testcase only QA can create and resolve. It’s read-only for other type of users.
INSERT INTO `roleissuestatus` (`roleID`, `issueType`, `issueStatus`) VALUES
	(@QA_RoleID, @Testcase_TypeID, @Resolved_IssueStatus);
	
INSERT INTO `roleissueaction` (`roleID`, `issueType`, `issueAction`) VALUES
	(@QA_RoleID, @Testcase_TypeID, @Read_Action),
	(@QA_RoleID, @Testcase_TypeID, @Create_Action),
	(@RD_RoleID, @Testcase_TypeID, @Read_Action),
	(@PM_RoleID, @Testcase_TypeID, @Read_Action);
	
-- admin full controll
INSERT INTO `roleissueaction` (`roleID`, `issueType`, `issueAction`) VALUES
	(@Admin_RoleID, @Bug_TypeID, @Create_Action),
	(@Admin_RoleID, @Bug_TypeID, @Read_Action),
	(@Admin_RoleID, @Bug_TypeID, @Update_Action),
	(@Admin_RoleID, @Bug_TypeID, @Delete_Action),
	
	(@Admin_RoleID, @Feature_TypeID, @Create_Action),
	(@Admin_RoleID, @Feature_TypeID, @Read_Action),
	(@Admin_RoleID, @Feature_TypeID, @Update_Action),
	(@Admin_RoleID, @Feature_TypeID, @Delete_Action),
	
	(@Admin_RoleID, @Testcase_TypeID, @Create_Action),
	(@Admin_RoleID, @Testcase_TypeID, @Read_Action),
	(@Admin_RoleID, @Testcase_TypeID, @Update_Action),
	(@Admin_RoleID, @Testcase_TypeID, @Delete_Action);
	
INSERT INTO `roleissuestatus` (`roleID`, `issueType`, `issueStatus`) VALUES
	(@Admin_RoleID, @Bug_TypeID, @Resolved_IssueStatus),
	(@Admin_RoleID, @Feature_TypeID, @Resolved_IssueStatus),
	(@Admin_RoleID, @Testcase_TypeID, @Resolved_IssueStatus);
	